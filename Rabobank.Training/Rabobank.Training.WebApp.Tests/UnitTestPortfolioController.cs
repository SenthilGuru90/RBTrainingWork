using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rabobank.Training.ClassLibrary;
using Rabobank.Training.WebApp.Controllers;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;


namespace Rabobank.Training.WebApp.Tests
{   
    [TestClass]
    public class UnitTestPortfolioController
    {
        private IFundOfMandatesProcessor fundOfMandatesPrc = null;
        private IConfiguration config;
        private ILoggerFactory loggerFactory;

        [TestMethod]
        public void PortfolioControllerReturnArrayOfPositionsTrue()
        {
            fundOfMandatesPrc = Substitute.For<IFundOfMandatesProcessor>();
            config = Substitute.For<IConfiguration>();
            loggerFactory = Substitute.For<ILoggerFactory>();            
            config["FundsOfMandatesFile"] = "..\\..\\..\\TestData\\FundsOfMandatesData.xml";
            loggerFactory.ClearReceivedCalls();            

            PortfolioController portfolioController = new PortfolioController(fundOfMandatesPrc, config, loggerFactory);
            var result = portfolioController.Get();
            result.Should().HaveCount(c => c > 0, "Because Portfolio Controller API Get method returns Array of Positions based on the input");
        }

        [TestMethod]
        public void PortfolioControllerReturnsFivePositionsTrue()
        {
            fundOfMandatesPrc = Substitute.For<IFundOfMandatesProcessor>();
            config = Substitute.For<IConfiguration>();
            loggerFactory = Substitute.For<ILoggerFactory>();
            config["FundsOfMandatesFile"] = "..\\..\\..\\TestData\\FundsOfMandatesData.xml";
            loggerFactory.ClearReceivedCalls();

            PortfolioController portfolioController = new PortfolioController(fundOfMandatesPrc, config, loggerFactory);
            var result = portfolioController.Get();
            result.Should().HaveCount(c => c == 5, "Because Get Portfolio method is having fixed 5 positions. So it should return 5 postions");

        }

    }
}
