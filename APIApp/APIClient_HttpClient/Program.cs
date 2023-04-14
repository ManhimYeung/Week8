using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;

namespace APIClient_HttpClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                var singlePostcodeRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get
                };

                singlePostcodeRequest.Headers.Add("Accept", "application/json");
                var postcode = "EC2Y 5AS";
                singlePostcodeRequest.RequestUri = new Uri($"https://api.postcodes.io/postcodes/{postcode}");

                try
                {
                    HttpResponseMessage singlePostcodeResponse = await client.SendAsync(singlePostcodeRequest);
                    Console.WriteLine(singlePostcodeResponse.Content.ReadAsStringAsync().Result);

                    // Take this single response and get the status code and headers e.g. Date
                    // Serialise to a JObject (need to install Newtonsoft)
                    // Query JObject examples
                    // Repeat with Bulkpostcode look up

                    // status code
                    Console.WriteLine(singlePostcodeResponse.StatusCode);
                    // header.date
                    Console.WriteLine(singlePostcodeResponse.Headers.Date);
                    // serialise
                    JObject jObject = JObject.Parse(singlePostcodeResponse.Content.ReadAsStringAsync().Result);
                    // Query examples
                    Console.WriteLine(jObject["result"]["postcode"]);
                    Console.WriteLine(jObject["result"]["nuts"]); 
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }


            }
            using (var client = new HttpClient())
            {
                var bulkPostcodeRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Post
                };

                bulkPostcodeRequest.Headers.Add("Accept", "application/json");
                var postcodes = new string[] { "EC2Y 5AS", "CF14 1RP" };

                bulkPostcodeRequest.Content = JsonContent.Create(postcodes); // big thanks to lucas, glen and kevin here
                bulkPostcodeRequest.RequestUri = new Uri($"https://api.postcodes.io/postcodes/{postcodes}");

                try
                {
                    HttpResponseMessage bulkPostcodeResponse = await client.SendAsync(bulkPostcodeRequest);
                    Console.WriteLine(bulkPostcodeResponse.Content.ReadAsStringAsync().Result);

                    // header.date
                    Console.WriteLine(bulkPostcodeResponse.Headers.Date);
                    // serialise
                    JObject jObject = JObject.Parse(bulkPostcodeResponse.Content.ReadAsStringAsync().Result);
                    // Query examples
                    Console.WriteLine(jObject["result"][0]["result"]["nuts"]);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }


            }
        }
    }
}