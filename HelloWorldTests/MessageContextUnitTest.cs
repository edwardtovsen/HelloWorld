using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using MessageApi.Models;

namespace HelloWorldTests
{
    [TestClass]
    public class MessageContextUnitTest
    {
         
        [TestMethod]
        public void MessageContextCount()
        {
            CallMessageApi().Wait();
            //Assert.IsTrue
        }

        public async Task CallMessageApi()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //GET Method  
                HttpResponseMessage response = await client.GetAsync("api/message");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();

                    var messages = JsonConvert.DeserializeObject<List<Message>>(jsonString);

                    foreach (var msg in messages)
                    {
                        Console.WriteLine("{0}", msg.Text);
                    }
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }
    }
}
