using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
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
        const string  baseAddress = "http://localhost:9000/";
        [Test]
        public async void Get1000LotsByJson()
        {
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                await Get1000Lots(client);
            }
        }

        [Test]
        public async void Get1000Lots100TimesByJson()
        {

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                for (int i = 0; i < 100; i++)
                {
                    await Get1000Lots(client);
                }
            }
        }

        private static async Task Get1000Lots(HttpClient client, bool useProtoBuf = false)
        {
            var start = DateTime.Now;
            var response = client.GetAsync(baseAddress + "portfolio/1/lots").Result;

            var formatters = new List<System.Net.Http.Formatting.MediaTypeFormatter>();
            if (useProtoBuf)
                formatters.Add(new ProtoBufFormatter());
            else
                formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            IEnumerable<Lot> lots = await response.Content.ReadAsAsync<IEnumerable<Lot>>(formatters);
            Console.WriteLine(String.Format("time to get lots: {0}", (DateTime.Now - start).TotalMilliseconds));
            Assert.That(lots.ToList().Count, Is.EqualTo(1000));
        }


        private static async Task Get1Lot(HttpClient client, bool useProtoBuf = false)
        {
            var start = DateTime.Now;
            var response = client.GetAsync(baseAddress + "lot/1").Result;
            
            var formatters = new List<System.Net.Http.Formatting.MediaTypeFormatter>();
            if (useProtoBuf)
                formatters.Add(new ProtoBufFormatter());
            else
                formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            Lot lot = await response.Content.ReadAsAsync<Lot>(formatters);
            Console.WriteLine(String.Format("time to get lot: {0}", (DateTime.Now - start).TotalMilliseconds));
            Assert.That(lot.LotId, Is.EqualTo(1));
        }

        [Test]
        public async void Get1000LotsByProtobuf()
        {

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));

                await Get1000Lots(client, true);
            }
        }

        [Test]
        public async void Get1000Lots100TimesByProtobuf()
        {

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));
                for (var i = 0; i < 100; i++)
                {
                    await Get1000Lots(client, true);
                }

            }
        }

        [Test]
        public async void Get1Lot100TimesByJson()
        {
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-json"));
                for (var i = 0; i < 100; i++)
                {
                    await Get1Lot(client, true);
                }

            }
        }
        [Test]
        public async void Get1Lot100TimesByProtoBuf()
        {
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));
                for (var i = 0; i < 100; i++)
                {
                    await Get1Lot(client, true);
                }

            }
        }
    }
}
