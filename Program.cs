using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Weather;

class Program
{
    

    static async Task Main()
    {
        string apiKey = "API-KEY";// Your api key from api.openweathermap.org
        Console.WriteLine("City|Country: ");
        string city = Console.ReadLine();
        
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
               
                var result = JsonConvert.DeserializeObject<WeatherInfo.root>(responseBody);

                WeatherInfo.root outPut = result;
                var TempInFar = (outPut.main.temp * 9 / 5) + 32;
                Console.WriteLine($"Tempature in {city}: {outPut.main.temp}°C, {TempInFar}°F");
                Console.ReadLine();
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error to request API: {e.Message}");
            }
        }
    }
}
