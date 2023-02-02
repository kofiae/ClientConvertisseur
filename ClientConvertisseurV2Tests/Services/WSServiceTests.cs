using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.ViewModels;
using System.Collections.ObjectModel;

namespace ClientConvertisseurV2.Services.Tests
{
    [TestClass()]
    public class WSServiceTests
    {
        [TestMethod()]
        public void WSServiceTest()
        {
            WSService wSService = new WSService("https://localhost:44353/api/");
            Assert.IsNotNull(wSService);
        }

        [TestMethod()]
        public void GetDevisesAsyncTest()
        {
            //Arrange
            WSService wSService = new WSService("https://localhost:44353/api/");


            //Act
            var result = wSService.GetDevisesAsync("devises").Result;
            Thread.Sleep(1000);

            //Assert
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(Task<List<Devise>>)
        }
    }
}