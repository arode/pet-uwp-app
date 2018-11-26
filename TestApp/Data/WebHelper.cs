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

        public static async Task<IEnumerable<Pet>> GetPetsAsync(int count = 10)
        {
            //Add a user-agent header to the GET request. 
            var headers = httpClient.DefaultRequestHeaders;

            Uri requestUri = new Uri($"https://api.petfinder.com/pet.getRandom?key={PetfinderSecrets.Key}&output=basic&format=json");//&count={count}&location=98103");

            List<Pet> pets = new List<Pet>();

            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

                var jObject = JObject.Parse(httpResponseBody);
                var petResult = jObject.SelectToken("petfinder.pet");

                //var returnedPets = jObject.SelectTokens("petfinder.pets.pet");

                //foreach (var item in returnedPets)
                //{
                //var x = item.SelectToken("name");
                //var y = item.SelectToken("options");

                Pet pet = new Pet()
                {
                    Name = petResult.SelectToken("name.$t").ToString(),
                    AnimalType = petResult.SelectToken("animal.$t").ToString(),
                    Age = petResult.SelectToken("age.$t").ToString()
                };

                pets.Add(pet);
                //}
                return pets;
            }
            catch(Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return null;
        }

    }
}
