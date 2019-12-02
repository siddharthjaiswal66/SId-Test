using ClosestLocationTracker.Areas.DistanceCalculator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace ClosestLocationTracker.Areas.DistanceCalculator.Infrastructure
{
    public class ReadLocations
    {

        //Read the CSV File to Get All the Location Pre Defined in the File
        public List<LocationDetails> ReadLocationFromCSV() {

            List<LocationDetails> Locations = new List<LocationDetails>();
            try
            {
                //Reading the File Line By Line
                using (StreamReader streamreader = new StreamReader(ConfigurationManager.AppSettings["FilePath"]))
                {
                    string address = null;
                    //Read until the end of the file is reached.                
                    while ((address = streamreader.ReadLine()) != null)
                    {
                        //Add Data to the List and cast it to the Location Details Option 
                        Locations.Add(new LocationDetails() {Destination = address });
                    }
                }
                return Locations;

            }
            catch (Exception)
            {
                throw new Exception("Error Reading File");
            }
        }

        public List<LocationDetails> FindDistances(string origin, out string statuscode)
        {
            List<LocationDetails> locationDetails = null;
            if (string.IsNullOrEmpty(origin))
            {
                statuscode = "INVALID_ORIGIN_ADDRESS";
                return locationDetails;
            }
            else
            {
                //Get the Distance Between the two location from the Google API with the Personal Key
                //string uri = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&key=AIzaSyD8kbnC-LIVjdbx5H9UbTGYSu9hVDHuoKk";
                try
                {
                    statuscode = "SOME_ERROR_OCCURED_IN_FILE_READING";
                    locationDetails = new ReadLocations().ReadLocationFromCSV();
                    List<LocationDetails> FinalLocationDetails = new List<LocationDetails>();
                    using (WebClient webClient = new WebClient())
                    {
                        foreach (var item in locationDetails)
                        {

                            string uri = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + item.Destination + "&key=AIzaSyD8kbnC-LIVjdbx5H9UbTGYSu9hVDHuoKk";
                            Double distance =0.0;
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.LoadXml(webClient.DownloadString(uri));
                            statuscode = xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText;
                            if (statuscode == "OK")
                            {
                                statuscode = xmldoc.GetElementsByTagName("status")[1].ChildNodes[0].InnerText;
                                if( statuscode == "OK")
                                {
                                    //Handle IF location is valid or Not
                                    XmlNodeList distancenode = xmldoc.GetElementsByTagName("distance");
                                    distance = Convert.ToDouble(distancenode[0].ChildNodes[1].InnerText.Replace(" km", string.Empty));
                                    FinalLocationDetails.Add(new LocationDetails() { Origin = origin, Destination = item.Destination, Distance = distance });
                                }
                                else
                                {
                                    throw new Exception(statuscode);
                                }

                            }
                            else
                            {
                                throw new Exception(statuscode);
                            }
                            
                        }
                       
                    }
                    return FinalLocationDetails;
                }
                catch (Exception ex)
                {
                    if (String.IsNullOrEmpty(ex.Message))
                    {
                        throw new Exception("Error Fetching the Data");
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

        }

        public List<LocationDetails> GetLocationsItems(string origin)
        {
           string uri = "http://localhost:50726/api/Location?origin="+origin;
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    return JsonConvert.DeserializeObject<List<LocationDetails>>(webClient.DownloadString(uri));
                }

            }
            catch (Exception ex)
            {
                if (String.IsNullOrEmpty(ex.Message))
                {
                    throw new Exception("Error Calling Location Service");
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
        }

      }

    }