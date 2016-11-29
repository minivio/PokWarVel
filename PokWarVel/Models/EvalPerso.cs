using MarvelApi.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PokWarVel.Models
{
    public class EvalPerso
    {
        #region Fields
        private int _idEval;
        private long _idHero;
        private string _typeHero;
        private int _rating;
        private Characters _hero;
        private string _commentaire;
        private List<string> _listeCommentaires;

        #endregion


        #region Properties
        /// <summary>
        /// Gets or sets the identifier of the evaluation
        /// </summary>
        /// <value>ID specification when DB use</value>
        public int IdEval
        {
            get { return _idEval; }
            set { _idEval = value; }
        }
        /// <summary>
        /// Gets or sets the identifier of the hero
        /// </summary>
        /// <value>ID hero</value>
        public long IdHero
        {
            get { return _idHero; }
            set { _idHero = value; }
        }
        /// <summary>
        /// Gets or sets the rating
        /// </summary>
        /// <value></value>
        public int Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }
        /// <summary>
        /// Gets or sets the date and time of evaluation
        /// </summary>
        /// <value></value>
        public DateTime DateEval
        {
            get { return DateTime.Now; }

        }

        public string TypeHero
        {
            get { return _typeHero; }
            set
            {
                if (value != null)
                {
                    _typeHero = value;
                }
                else _typeHero = "Marvel";
            }
        }

        public Characters Hero
        {
            get { return _hero; }
            set { _hero = value; }
        }
        public string Commentaire
        {
            get { return _commentaire; }
            set { _commentaire = value; }
        }

        public List<string> ListeCommentaires
        {
            get { return this.getAllComments(); }
        }

        #endregion

        public EvalPerso()
        {
            this.TypeHero = null;
        }
        

        public bool Save()
        {
            SqlConnection oConn = new SqlConnection(@"Data Source=26R2-14\WADSQL;Initial Catalog=PokWarVelDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            
            SqlCommand oCmd;

            try
            {
                oConn.Open();

                oCmd = new SqlCommand(@"INSERT INTO Eval (idHero, typeHero,rating, dateEval, commentaire) VALUES (@idHero, @typeHero, @rating, @dateEval, @commentaire)", oConn);
                SqlParameter paramIdHero = new SqlParameter("@idHero", this.IdHero);
                SqlParameter paramTypeHero = new SqlParameter("@typeHero", this.TypeHero);
                SqlParameter paramRating = new SqlParameter("@rating", this.Rating);
                SqlParameter paramDateEval = new SqlParameter("@dateEval", this.DateEval);
                SqlParameter paramCommentaire = new SqlParameter("@commentaire", this.Commentaire);

                oCmd.Parameters.Add(paramIdHero);
                oCmd.Parameters.Add(paramTypeHero);
                oCmd.Parameters.Add(paramRating);
                oCmd.Parameters.Add(paramDateEval);
                oCmd.Parameters.Add(paramCommentaire);

                oCmd.ExecuteNonQuery();

                oConn.Close();

            }
            catch (Exception ex)
            {
                if (oConn.State == System.Data.ConnectionState.Open)
                {
                    oConn.Close();
                }
               
                return false;
            }
            return true;
        }

        public List<string> getAllComments()
        {
            List<string> listeCommentaires = new List<string>();
            SqlConnection oConn = new SqlConnection(@"Data Source=26R2-14\WADSQL;Initial Catalog=PokWarVelDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            SqlCommand oCmd;

            try
            {
                oConn.Open();
                oCmd = new SqlCommand(@"SELECT commentaire, idUser FROM Eval WHERE idHero=@idHero AND commentaire IS NOT NULL", oConn);
                SqlParameter paramIdHero = new SqlParameter("@idHero", this.IdHero);
                oCmd.Parameters.Add(paramIdHero);
                SqlDataReader oDr = oCmd.ExecuteReader();

                while (oDr.Read())
                {
                    string newComment = oDr["commentaire"] == DBNull.Value ? "" : oDr["commentaire"].ToString();
                    listeCommentaires.Add(newComment);
                }
            }
            catch (Exception ex)
            {
                listeCommentaires.Add(ex.ToString());
            }
            return listeCommentaires;
        }

    }
}