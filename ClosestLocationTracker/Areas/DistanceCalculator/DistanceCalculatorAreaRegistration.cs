using System.Web.Mvc;

namespace ClosestLocationTracker.Areas.DistanceCalculator
{
    public class DistanceCalculatorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DistanceCalculator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DistanceCalculator_default",
                "DistanceCalculator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}