using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RezervareFilmeNet8.API
{
    public class RestServices
    {
        public HttpClient client;
        public JsonSerializerOptions options;

        public List<Movies> movies { get; set; }
        public RestServices()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://www.omdbapi.com/");
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<Movies> GetMovie(string title)
        {
            Movies movies = new ();
            RestServices restServices = new ();
            Uri uri = new (restServices.client.BaseAddress + "?apikey=1ed706f8&t=" + title.Trim());
            try
            {
                HttpResponseMessage response = await restServices.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    movies = JsonSerializer.Deserialize<Movies>(content);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return movies;
        }
    }


}
