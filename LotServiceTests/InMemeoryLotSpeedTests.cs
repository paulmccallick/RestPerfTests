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
    public class InMemeoryLotSpeedTests
    {
        [TestFixtureSetUp]
        public void BeforeAll()
        {
            LotRepository.UseCouch = false;
        }



        [Test]
        public async void Get1000Lots100TimesByJson()
        {
            
            await TestHelpers.GetPortfolioLots(100, false);
        }



        [Test]
        public async void Get1000Lots100TimesByProtobuf()
        {
            await TestHelpers.GetPortfolioLots(100, true);
        }

        [Test]
        public async void Get1Lot100TimesByJson()
        {
            await TestHelpers.Get1Lot(100, false);
        }

        [Test]
        public async void Get1Lot100TimesByProtoBuf()
        {
            await TestHelpers.Get1Lot(100, true);
        }
    }
}
