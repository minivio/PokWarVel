using System.Web.Mvc;

namespace PokWarVel.Areas.zoneMembre
{
    public class zoneMembreAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "zoneMembre";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "zoneMembre_default",
                "zoneMembre/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}