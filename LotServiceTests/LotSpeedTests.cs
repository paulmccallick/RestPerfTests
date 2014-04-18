using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LotContracts;
using LotService;
using Microsoft.Owin.Hosting;
using NUnit.Framework;
using WebApiContrib.Formatting;

namespace LotServiceTests
{
    [TestFixture]
    public class LotSpeedTests
    {
        [Test]
        public async void Get1000LotsByJson()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(baseAddress + "api/lot").Result;
                var start = DateTime.Now;
                IEnumerable<Lot> lots = await response.Content.ReadAsAsync<IEnumerable<Lot>>();
                Console.WriteLine(String.Format("time to get lots: {0}", (DateTime.Now - start).TotalMilliseconds));
                Assert.That(lots.ToList().Count,Is.EqualTo(1000));
            }
        }
        [Test]
        public async void Get1000Lots100TimesByJson()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                for (int i = 0; i < 100; i++)
                {
                    var response = client.GetAsync(baseAddress + "api/lot").Result;
                    var start = DateTime.Now;
                    IEnumerable<Lot> lots = await response.Content.ReadAsAsync<IEnumerable<Lot>>();
                    Console.WriteLine(String.Format("time to get lots: {0}", (DateTime.Now - start).TotalMilliseconds));
                    Assert.That(lots.ToList().Count, Is.EqualTo(1000));
                }
            }
        }
        [Test]
        public async void Get1000LotsByProtobuf()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));

                var response = client.GetAsync(baseAddress + "api/lot").Result;
                var start = DateTime.Now;
                IEnumerable<Lot> lots =
                    await response.Content.ReadAsAsync<IEnumerable<Lot>>(new[] {new ProtoBufFormatter()});
                Console.WriteLine(String.Format("time to get lots: {0}", (DateTime.Now - start).TotalMilliseconds));
                Assert.That(lots.ToList().Count, Is.EqualTo(1000));


            }
        }

        [Test]
        public async void Get1000Lots100TimesByProtobuf()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));
                for (var i = 0; i < 100; i++)
                {
                    var response = client.GetAsync(baseAddress + "api/lot").Result;
                    var start = DateTime.Now;
                    IEnumerable<Lot> lots =
                        await response.Content.ReadAsAsync<IEnumerable<Lot>>(new[] {new ProtoBufFormatter()});
                    Console.WriteLine(String.Format("time to get lots: {0}", (DateTime.Now - start).TotalMilliseconds));
                    Assert.That(lots.ToList().Count, Is.EqualTo(1000));
                }

            }
        }
    }
}
