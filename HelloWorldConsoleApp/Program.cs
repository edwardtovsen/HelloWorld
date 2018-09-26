using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MessageApi.Models;
using Newtonsoft.Json;

namespace HelloWorldConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CallMessageApi().Wait();

            Console.Write("\nPress any key to continue... ");
            Console.ReadLine();
        }

        static async Task CallMessageApi()
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