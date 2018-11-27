using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PetApp.Models;
using Windows.Web.Http;
using PetApp.ViewModels;

namespace PetApp.Data
{
    public class WebHelper
    {
        static HttpClient httpClient = new HttpClient();

        private static Uri CreateBaseUri(string action = "find", string output = "basic")
        {
            Uri uri = new Uri($"https://api.petfinder.com/pet.{action}?key={PetfinderSecrets.Key}&output={output}&format=json");

            return uri;
        }

        private static async Task<JObject> GetResponseJObjectAsync(Uri requestUri)
        {
            //Add a user-agent header to the GET request. 
            var headers = httpClient.DefaultRequestHeaders;

            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            JObject jObject = null;

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

                jObject = JObject.Parse(httpResponseBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Web request error");
                throw ex;
            }

            return jObject;
        }

        public static async Task<IEnumerable<Pet>> GetPetsAsync(int count = 10)
        {
            Uri requestUri = new Uri($"{CreateBaseUri()}&count={count}&location=98103");

            List<Pet> pets = new List<Pet>();

            try
            {
                var jObject = await GetResponseJObjectAsync(requestUri);

                var returnedPets = jObject.SelectToken("petfinder.pets.pet");

                foreach (var item in returnedPets)
                {
                    Pet pet = new Pet()
                    {
                        Name = item.SelectToken("name.$t").ToString(),
                        AnimalType = item.SelectToken("animal.$t").ToString(),
                        Age = item.SelectToken("age.$t").ToString(),
                        Id = item.SelectToken("id.$t").ToString()
                    };

                    pets.Add(pet);
                }
                return pets;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message);
            }

            return null;
        }


        public static async Task<string> GetDetailsAsync(string id)
        {
            Uri baseUri = CreateBaseUri("get");

            Uri requestUri = new Uri($"{baseUri}&id={id}");

            string description = string.Empty;

            try
            {
                var jObject = await GetResponseJObjectAsync(requestUri);
                var petResponse = jObject.SelectToken("petfinder.pet");

                description = petResponse.SelectToken("description.$t").ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message);
            }

            return description;
        }
    }
}
