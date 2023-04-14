using System.Net.Http;

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
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}