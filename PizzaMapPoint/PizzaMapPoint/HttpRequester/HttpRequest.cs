using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PizzaMapPoint.Models;
using PizzaMapPoint.ViewModels;
using PizzaMapPoint.Views;

namespace PizzaMapPoint.HttpRequester
{
    public class HttpRequest
    {
        public static async Task<IEnumerable<PizzaRestaurantViewModel>> LoadRemoteDataAsync(double longitude, double latitude)
        {
            string oauthToken = "XNAW3AJSCISM5YIRV2TXFMF4FI1ZZWX1C4PLITQATS5BJK1X&v=20130926";
            var client = new HttpClient();
            client.MaxResponseContentBufferSize = 1024 * 1024; // Read up to 1 MB of data
            HttpResponseMessage response = await client.GetAsync(new Uri(
                "https://api.foursquare.com/v2/venues/search?ll=" + latitude + "," + longitude + "&oauth_token=" +
                oauthToken));
            string resultRaw = await response.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<RootObject>(resultRaw);

            ICollection<PizzaRestaurantViewModel> pizzas= CreatePizzaRestaurantsAndPizzaGroups(responseData.Response.Venues);

            return pizzas;
        }

        private static ICollection<PizzaRestaurantViewModel> CreatePizzaRestaurantsAndPizzaGroups(List<Venue> data)
        {
            List<PizzaRestaurantViewModel> restaurants = new List<PizzaRestaurantViewModel>();

            foreach (Venue venue in data)
            {
                foreach (object cat in venue.Categories)
                {
                    if (cat.ToString().Contains("Pizza") || cat.ToString().Contains("Restaurant") 
                        || cat.ToString().Contains("Bakery") || cat.ToString().Contains("Diner"))
                    {
                        var pizzaRestaurant = new PizzaRestaurantViewModel
                            (
                                venue.Name,
                                venue.Contact.FormattedPhone,
                                venue.Location.Address,
                                venue.Location.Distance,
                                venue.Location.Country,
                                venue.Location.Lng,
                                venue.Location.Lat,
                                venue.Url,
                                venue.Location.City 
                            );
                        restaurants.Add(pizzaRestaurant);
                    }
                }
            }

            return restaurants;
        }

        public static async void LoadCoordinatesDataAsync(string query)
        {
            try
            {
                var client = new HttpClient();
                client.MaxResponseContentBufferSize = 1024 * 1024; // Read up to 1 MB of data
                HttpResponseMessage response = client.GetAsync(new Uri(
                    "http://maps.googleapis.com/maps/api/geocode/json?address=" + query + "&sensor=false")).Result;

                string resultRaw = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<Models.SeachresultModels.RootObject>(resultRaw);

                foreach (var item in responseData.results)
                {
                    GlobalConfig.SearchLatitude = item.geometry.location.lat;
                    GlobalConfig.SearchLonngitude = item.geometry.location.lng;
                }
            }
            catch (Exception)
            {
                Helper.ErrorMessage.MessageBox("Please check your internet connection and try again later.");
            }

        }
    }
}
