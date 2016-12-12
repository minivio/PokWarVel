using MarvelApi;
using MarvelApi.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PokWarVel.Models
{
    public class PopularModel
    {
        private long _idHero;
        private string _nomHero;
        private float _ratingHero;

        public long IdHero
        {
            get { return _idHero; }
            set { _idHero = value; }
        }
        public string NomHero
        {
            get { return _nomHero; }
            set { _nomHero = value; }
        }
        public float RatingHero
        {
            get { return _ratingHero; }
            set { _ratingHero = value; }
        }

      


        /// <summary>
        /// Permet d'instancier une liste de PopularModel à partir de l'id du héro
        /// </summary>
        /// <returns>une liste de popularmodel</returns>
        public static List<PopularModel> getPopularity()
        {
            MarvelRequester r = new MarvelRequester();
            List<PopularModel> mostPopular = new List<PopularModel>();
            SqlConnection oConn = new SqlConnection(@"Data Source=26R2-14\WADSQL;Initial Catalog=PokWarVelDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            SqlCommand oCmd;

            try
            {
                oConn.Open();
                oCmd = new SqlCommand(@"SELECT TOP 5 idHero, AVG(convert(float,rating)) as moyenne FROM Eval GROUP BY idHero ORDER BY moyenne DESC, COUNT(idHero) DESC", oConn);
                SqlDataReader oDr = oCmd.ExecuteReader();
                while (oDr.Read())
                {

                    Characters newHero = r.GetCharacterById((long)oDr["idHero"]);
                    PopularModel pm = new PopularModel();
                    pm.NomHero = newHero.name;
                    pm.IdHero = newHero.id;
                    pm.RatingHero = float.Parse(oDr["moyenne"].ToString());

                    mostPopular.Add(pm);
                }
                oConn.Close();

            }
            
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }

            return mostPopular;
        }


    }
}