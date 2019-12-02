using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClosestLocationTracker.Areas.DistanceCalculator.Models
{
    public class LocationDetails 
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Double Distance { get; set; }

    }
}