using ClosestLocationTracker.Areas.DistanceCalculator.Infrastructure;
using ClosestLocationTracker.Areas.DistanceCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClosestLocationTracker.Areas.DistanceCalculator.Controllers
{
    public class SearchController : Controller
    {
        // GET: DistanceCalculator/Search
        public ActionResult Index(string origin)
        {
            if (!String.IsNullOrEmpty(origin)) {
                try
                {
                    return View(new ReadLocations().GetLocationsItems(origin));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message; 
                    return View(new List<LocationDetails>());
                }
            }
            
            return View(new List<LocationDetails>());
        }
    }
}