using ClosestLocationTracker.Areas.DistanceCalculator.Infrastructure;
using ClosestLocationTracker.Areas.DistanceCalculator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClosestLocationTracker.Areas.DistanceCalculator.Controllers
{
    public class LocationController : ApiController
    {
        // GET: api/Location
        public IEnumerable<LocationDetails> Get(string origin)
        {
            return new ReadLocations().FindDistances(origin,out string statuscode).OrderBy(x=>x.Distance).Take(5);

        }
    }
}
