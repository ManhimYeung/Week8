using APIClientApp;
using Newtonsoft.Json.Linq;
using System;
using TechTalk.SpecFlow;

namespace ApiTestApp.SpecFlow
{
    [Binding]
    [Scope(Feature = "SinglePostcodeRequest")]
    public class SinglePostcodeRequestStepDefinitions
    {
        private static string _testDataLocation = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestData\");
        private static SinglePostcodeService _spcs;

        [BeforeFeature]
        [Given(@"I had initialised the Single PostcodeService")]
        public static void GivenIHadInitialisedTheSinglePostcodeService()
        {
            _spcs = new SinglePostcodeService();
        }

        [When(@"I make a request for the postcode ""([^""]*)""")]
        public async Task WhenIMakeARequestForThePostcode(string postcode)
        {
            await _spcs.MakeRequestAsync(postcode);
        }

        [Then(@"the status in the JSON response body should be (.*)")]
        public void ThenTheStatusInTheJSONResponseBodyShouldBe(string status)
        {
            Assert.That(_spcs.JsonResponse["status"].ToString(), Is.EqualTo(status));
        }

        [Then(@"the status header should be (.*)")]
        public void ThenTheStatusHeaderShouldBe(int status)
        {
            Assert.That(_spcs.GetStatusCode(), Is.EqualTo(status));
        }

        [Then(@"the JSON response body should match the JSON schema in ""([^""]*)""")]
        public void ThenTheJSONResponseBodyShouldMatchTheJSONSchemaIn(string location)
        {
            var expectedJsonString = File.ReadAllText(_testDataLocation + location);
            var expectedJsonObject = JObject.Parse(expectedJsonString);
            Assert.That(_spcs.JsonResponse, Is.EqualTo(expectedJsonObject));
        }
    }
}
