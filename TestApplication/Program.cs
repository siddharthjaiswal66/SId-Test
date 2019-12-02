using ClosestLocationTracker.Areas.DistanceCalculator.Infrastructure;
using ClosestLocationTracker.Areas.DistanceCalculator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            List<LocationDetails> locationDetails = ReadFile();
            //var values = FindDistances(locationDetails[0].Destination, locationDetails[1].Destination, out string statuscode);
            foreach (var address in locationDetails)
            {
                Console.WriteLine(address.Destination);
            }
            Console.ReadLine();
            
        }
        public static List<LocationDetails> ReadFile()
        {
            List<LocationDetails> Locations = new List<LocationDetails>();
            try
            {
                //Reading the File Line By Line
                using (StreamReader streamreader = new StreamReader(@"C:\Users\sjaiswal\source\repos\ClosestLocationTracker\TestApplication\Assest\addresslist.txt"))
                {
                    string address = null;
                    //Read until the end of the file is reached.                
                    while ((address = streamreader.ReadLine()) != null)
                    {
                        //Add Data to the List and cast it to the Location Details Option 
                        Locations.Add(new LocationDetails() { Destination = address });
                    }
                }
                return Locations;
            }
            catch (Exception)
            {
                throw new Exception("Error Reading File");
            }
        }
        //public static List<LocationDetails> FindDistances(string origin, string destination, out string statuscode)
        //{
        //    List<LocationDetails> locationDetails = null;
        //    if (string.IsNullOrEmpty(origin))
        //    {
        //        statuscode = "ENTER_VALID_ORIGIN_ADDRESS";
        //        return locationDetails;
        //    }
        //    if (string.IsNullOrEmpty(destination))
        //    {
        //        statuscode = "ENTER_VALID_DESTINATION_ADDRESS";
        //        return locationDetails;
        //    }
        //    else
        //    {
        //        //Get the Distance Between the two location from the Google API with the Personal Key
        //        string uri = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&key=AIzaSyD8kbnC-LIVjdbx5H9UbTGYSu9hVDHuoKk";
        //        try
        //        {
        //            //locationDetails = new ReadLocations().ReadLocationFromCSV();
        //            using (WebClient webClient = new WebClient())
        //            {
        //                Double distance = 0.0;
        //                XmlDocument xmldoc = new XmlDocument();
        //                statuscode = xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText;
        //                xmldoc.LoadXml(webClient.DownloadString(uri));
        //                if (statuscode == "OK")
        //                {
        //                    XmlNodeList distancenode = xmldoc.GetElementsByTagName("distance");
        //                    distance = distancenode[0].ChildNodes[1].InnerText;
        //                }
        //                locationDetails.Add(new LocationDetails() { Origin = origin, Destination = destination, Distance = distance });
        //            }
        //            return locationDetails;
        //        }
        //        catch (Exception)
        //        {

        //            throw new Exception("Error Fetching the Data");
        //        }
        //    }

        //}
    }
}
