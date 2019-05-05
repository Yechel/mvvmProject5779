using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static BL.functions.ApiAdressToCoordinate;

namespace BL.functions
{
    public class ApiAdressToCoordinate
    {

        public class GeoCoordinate
        {
            public IList<Result> results { get; set; }
            public string status { get; set; }
        }

        public class AddressComponent
        {
            public string long_name { get; set; }
            public string short_name { get; set; }
            public IList<string> types { get; set; }
        }

        public class Location
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Northeast
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Southwest
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Viewport
        {
            public Northeast northeast { get; set; }
            public Southwest southwest { get; set; }
        }

        public class Geometry
        {
            public Location location { get; set; }
            public string location_type { get; set; }
            public Viewport viewport { get; set; }
        }

        public class Result
        {
            public IList<AddressComponent> address_components { get; set; }
            public string formatted_address { get; set; }
            public Geometry geometry { get; set; }
            public string place_id { get; set; }
            public IList<string> types { get; set; }


        }



        /********     PRIVATE API  FOR Google Adress to Coourdinate service    **********/
        public const string API_KEY = "AIzaSyDiSEvb5lEtA1qouymyM29eUJ3Fz-o3HZk";
        // public const string BASE_URL = "https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=YOUR_API_KEY";
        public const string BASE_URL = "https://maps.googleapis.com/maps/api/geocode/json?address={1}&key={0}";

        /************* מימוש ********/
        //private async void GetGeoCoordinate(string address) 
        //{
        //    var Coordinate = await ViewModels.GeoCoordinateApi.GetGeoCoordinateAsync(address);
        //    double let = Coordinate.results[0].geometry.location.lat;
        //    double lng = Coordinate.results[0].geometry.location.lng;
        //}
       

        public static async Task<GeoCoordinate> GetGeoCoordinateAsync(string address)
        {
            GeoCoordinate result = new GeoCoordinate();

            string url = string.Format(BASE_URL, API_KEY, address);

            using (HttpClient client = new HttpClient())
            {

                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GeoCoordinate>(json);
            }

            return result;

        }





      //getting adres from lat log
        //public static async Task<string> GetStreetAddressForCoordinates(double lat, double lng)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri("http://nominatim.openstreetmap.org");
        //    HttpResponseMessage httpResult = await httpClient.GetAsync(
        //    //    String.Format("reverse?format=json&lat={0}&lon={1}", latitude, longitude));

        //    JsonObject jsonObject = JsonObject.Parse(await httpResult.Content.ReadAsStringAsync());

        //    return string.Format("{0} {1}", jsonObject.GetNamedObject("address").GetNamedString("house"),
        //                                    jsonObject.GetNamedObject("address").GetNamedString("road"));
       // }

        //      static string baseUri =
        //"http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
        //      string location = string.Empty;



        //fOR TESTINGGGGGGGGGGGGGGGGGGGGGG
        static string baseUri =
            "https://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452&key=API_KEY";
        string location = string.Empty;
      ///// FOR TESTINGGGGGGGGGGGGGGGGGGGGGGGG
      


        public static async Task<string> RetrieveFormatedAddress(string lat, string lng)
        {
            string result ="no result yet";
            string latlng = lat+lng;
            string url = string.Format(baseUri, API_KEY, latlng);

            using (HttpClient client = new HttpClient())
            {

                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<string>(json);
            }

            return result;
            //            string requestUri = string.Format(baseUri, lat, lng);
            //            string adr = "";
            //            using (WebClient wc = new WebClient())
            //            {
            //                var result = wc.DownloadString(requestUri);
            //                adr = result;
            //                var xmlElm = XElement.Parse(result);
            //                var status = (from elm in xmlElm.Descendants()
            //                              where
            //elm.Name == "status"
            //                              select elm).FirstOrDefault();
            //                if (status.Value.ToLower() == "ok")
            //                {
            //                    var res = (from elm in xmlElm.Descendants()
            //                               where
            //elm.Name == "formatted_address"
            //                               select elm).FirstOrDefault();
            //                    requestUri = res.Value;
            //                }
            //            }
            //            return adr;

        }
    }
}
