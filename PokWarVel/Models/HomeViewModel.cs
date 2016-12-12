using MarvelApi;
using MarvelApi.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PokWarVel.Models
{
    public class HomeViewModel
    {
        MarvelRequester r = new MarvelRequester();


        private List<PopularModel> _listPopularity;
        private List<Characters> _listCharacters;

        public List<PopularModel> ListPopularity
        {
            get { return PopularModel.getPopularity(); }
        }
        public List<Characters> ListCharacters
        {
            get { return r.GetCharacters(limit: 100); }
        }


   
    }
}