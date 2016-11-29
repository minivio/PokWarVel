using MarvelApi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MarvelApi
{
    public class MarvelRequester: Requester
    {

        /// <summary>
        /// A timespan to compose eventually a hash
        /// </summary>
        private static TimeSpan t = new TimeSpan();
        /// <summary>
        /// The public key if necessary
        /// </summary>
        private static string Key = "0bfd26549bdc6c0fd88b9c18b683958c";
        /// <summary>
        /// The private key if necessary
        /// </summary>
        private static string pKey = "f4e18eba62280c33faf3093108a09af7c908daf00bfd26549bdc6c0fd88b9c18b683958c";
        /// <summary>
        /// The hash to compute 
        /// </summary>
        private static string hash;
        /// <summary>
        /// The URL parameters if necessary
        /// </summary>
        private string urlParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarvelRequester"/> class.
        /// </summary>
        public MarvelRequester():base("https://gateway.marvel.com/")
        {
            
        }

        public List<Characters> GetCharacters(int limit = 1, int offset = 0, string Endpoint = "v1/public/characters")
        {
            //Marvel accept a maximum of 100 character by requests
            if (limit > 100)
            {
                throw new InvalidOperationException("Limit 0 to 100 only");
            }
            //1- Get the time
            t = DateTime.Now.TimeOfDay;
            //2- Calculate hash in accordance with marvel documentation
            hash = Tools.CalculateMD5LikeMarvel(t,pKey).ToLower();
            //3- Prepare the parameters
            urlParameters = "?ts=" + t.ToString().Replace(":", "").Replace(".", "") + "&limit=" + limit + "&offset=" + offset + "&apikey=" + Key + "&hash=" + hash;

            //4- Get the json response from marvel API
            string json = base.Execute(Endpoint, urlParameters);
            if (json != "")
            {
                List<Characters> lc = null;
                CharacterDataWrapper cd = JsonConvert.DeserializeObject<CharacterDataWrapper>(json);

                if(cd.data!=null)
                {
                    CharacterDataContainer CDC = cd.data;
                    if(CDC.results.Count>0)
                    {
                        lc = CDC.results;
                    }
                }

         
                return lc;
            }
            else
            {
                return null;
            }
        }

        public List<Characters> GetCharactersContainingString(string toLookFor = "")
        {
            bool moreResults = true;
            List<Characters> resultats = new List<Characters>();
            int newOffset = 0;

            do
            {
                List<Characters> newListe = this.GetCharacters(limit: 100, offset: newOffset);

                if (newListe != null)
                {
                    foreach (Characters perso in newListe)
                    {
                        if (perso.name.ToLower().Contains(toLookFor.ToLower()))
                        {
                            resultats.Add(perso);
                        }
                    }
                    newOffset += 100;
                }
                else
                {
                    moreResults = false;
                }

            } while (moreResults);

            return resultats;
        }

        public List<Characters> SearchCharacters(int limit = 1, int offset = 200, string Endpoint = "v1/public/characters", string SearchString = "")
        {
            //Marvel accept a maximum of 100 character by requests
            if (limit > 100)
            {
                throw new InvalidOperationException("Limit 0 to 100 only");
            }
            //1- Get the time
            t = DateTime.Now.TimeOfDay;
            //2- Calculate hash in accordance with marvel documentation
            hash = Tools.CalculateMD5LikeMarvel(t, pKey).ToLower();
            //3- Prepare the parameters
            urlParameters = "?nameStartsWith="+HttpUtility.UrlEncode(SearchString) +"&ts=" + t.ToString().Replace(":", "").Replace(".", "") + "&limit=" + limit + "&offset=" + offset + "&apikey=" + Key + "&hash=" + hash;

            //4- Get the json response from marvel API
            string json = base.Execute(Endpoint, urlParameters);
            if (json != "")
            {
                List<Characters> lc = null;
                CharacterDataWrapper cd = JsonConvert.DeserializeObject<CharacterDataWrapper>(json);

                if (cd.data != null)
                {
                    CharacterDataContainer CDC = cd.data;
                    if (CDC.results.Count > 0)
                    {
                        lc = CDC.results;
                    }
                }


                return lc;
            }
            else
            {
                return null;
            }
        }
        public Characters GetCharacterById(long id, int limit = 1, int offset = 0, string Endpoint = "v1/public/characters")
        {
            //Marvel accept a maximum of 100 character by requests
            if (limit > 100)
            {
                throw new InvalidOperationException("Limit 0 to 100 only");
            }
            //1- Get the time
            t = DateTime.Now.TimeOfDay;
            //2- Calculate hash in accordance with marvel documentation
            hash = Tools.CalculateMD5LikeMarvel(t, pKey).ToLower();
            //3- Prepare the parameters
            urlParameters = "/"+ id +"?ts=" + t.ToString().Replace(":", "").Replace(".", "") + "&limit=" + limit + "&offset=" + offset + "&apikey=" + Key + "&hash=" + hash;

            //4- Get the json response from marvel API
            string json = base.Execute(Endpoint, urlParameters);
            if (json != "")
            {
                Characters c = null;
                CharacterDataWrapper cd = JsonConvert.DeserializeObject<CharacterDataWrapper>(json);

                if (cd.data != null)
                {
                    CharacterDataContainer CDC = cd.data;
                    if (CDC.results.Count > 0)
                    {
                        c = CDC.results.First();
                    }
                }


                return c;
            }
            else
            {
                return null;
            }
        }


    }
}
