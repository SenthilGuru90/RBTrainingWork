using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Rabobank.Training.ClassLibrary.Tests
{
    [TestClass]
    public class UnitTestFundsOfMandate
    {
        ProcessHolder prc = new ProcessHolder();
        string xmlPath = "..\\..\\..\\TestData\\FundsOfMandatesData.xml";
        

        [TestMethod]
        public void ReadFundOfMandatesFile_ValidNameShouldWork()
        {
            var actual = prc.ReadFundOfMandatesFile("..\\..\\..\\TestData\\FundsOfMandatesData.xml");

            Assert.IsTrue(actual.Count > 0);
        }
        
        [TestMethod]
        public void ListOfFundOfMandates_HavingElements_ReturnsTrue()
        {
            List<FundOfMandates> Result = prc.ReadFundOfMandatesFile(xmlPath);
            //Making sure that Collection is having at least one element
            Result.Should().NotBeNull().And.HaveCount(c => c > 0).And.OnlyHaveUniqueItems();            
        }

        [TestMethod]
        public void FundOfMandates_HavingInstrumentNameWithStringType_ReturnsTrue()
        {
            List<FundOfMandates> Result = prc.ReadFundOfMandatesFile(xmlPath);

            object objInstrumentName = null;
            foreach (var item in Result.Select(x=>x.InstrumentName))
            {
                objInstrumentName = item;
                objInstrumentName.Should().NotBeNull();
                objInstrumentName.Should().BeOfType<string>("because a {0} is set", typeof(string));
            }          
        }

        [TestMethod]
        public void FundOfMandates_HavingInstrumentCodeWithStringType_ReturnsTrue()
        {
            List<FundOfMandates> Result = prc.ReadFundOfMandatesFile(xmlPath);

            object objInstrumentCode = null;
            foreach (var item in Result.Select(x => x.InstrumentCode))
            {
                objInstrumentCode = item;
                objInstrumentCode.Should().NotBeNull();
                objInstrumentCode.Should().BeOfType<string>("because a {0} is set", typeof(string));
            }
        }

        [TestMethod]
        public void FundOfMandates_HavingLiquidityAllocationWithDecimalType_ReturnsTrue()
        {
            List<FundOfMandates> Result = prc.ReadFundOfMandatesFile(xmlPath);
            object objLiquidityAllocation = null;
            foreach (var item in Result.Select(x => x.LiquidityAllocation))
            {
                objLiquidityAllocation = item;
                objLiquidityAllocation.Should().NotBeNull();
                objLiquidityAllocation.Should().BeOfType<decimal>("because a {0} is set", typeof(decimal));
            }
        }

        [TestMethod]
        public void FundOfMandates_HavingMandates_ReturnsTrue()
        {
            List<FundOfMandates> Result = prc.ReadFundOfMandatesFile(xmlPath);
            IEnumerable<object> obMandates = null;
            obMandates = Result.Select(x => x.Mandates);

            obMandates.Should().NotBeNull().And.HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
        }

        [TestMethod]
        public void FundOfMandates_HavingMandates_EachMandateHavingMandateIDOfTypeString_ReturnsTrue()
        {
            List<FundOfMandates> Result = prc.ReadFundOfMandatesFile(xmlPath);
            IEnumerable<object> objMandates = Result.Select(x => x.Mandates);

            object objMandateID = null;

            foreach (object item in objMandates)
            {
                foreach (object mtitem in item as Mandate[])
                {
                    objMandateID = ((Mandate)mtitem).MandateId;
                    objMandateID.Should().NotBeNull();
                    objMandateID.Should().BeOfType<string>("because a {0} is set", typeof(string));
                }
            }
        }

        [TestMethod]
        public void FundOfMandates_HavingMandates_EachMandateHavingMandateNameOfTypeString_ReturnsTrue()
        {
            List<FundOfMandates> Result = prc.ReadFundOfMandatesFile(xmlPath);
            IEnumerable<object> objMandates = Result.Select(x => x.Mandates);

            object objMandateName = null;

            foreach (object item in objMandates)
            {
                foreach (object mtitem in item as Mandate[])
                {
                    objMandateName = ((Mandate)mtitem).MandateName;
                    objMandateName.Should().NotBeNull();
                    objMandateName.Should().BeOfType<string>("because a {0} is set", typeof(string));
                }
            }
        }

        [TestMethod]
        public void FundOfMandates_HavingMandates_EachMandateHavingAllocationOfTypeDecimal_ReturnsTrue()
        {
            List<FundOfMandates> Result = prc.ReadFundOfMandatesFile(xmlPath);
            IEnumerable<object> objMandates = Result.Select(x => x.Mandates);

            object objAllocation = null;

            foreach (object item in objMandates)
            {
                foreach (object mtitem in item as Mandate[])
                {
                    objAllocation = ((Mandate)mtitem).Allocation;
                    objAllocation.Should().NotBeNull();
                    objAllocation.Should().BeOfType<decimal>("because a {0} is set", typeof(decimal)); 
                }
            }
        }

        [TestMethod]
        public void GetPortfolioMethodReturnsPortfolioVMWhichHasListOfPositions()
        {
            PortfolioVM portfolioVM = prc.GetPortfolio();

            portfolioVM.Should().NotBeNull();
        }

        [TestMethod]
        public void FundOfMandatesReturnsPositionVM()
        {
            List<FundOfMandates> Result = prc.ReadFundOfMandatesFile(xmlPath);
            PortfolioVM portfolio = prc.GetPortfolio();
            PositionVM pVM = null;

            pVM = prc.GetCalculatedMandates(portfolio.Positions.ElementAt(1), Result.ElementAt(1));

            pVM.Should().NotBeNull().And.BeOfType<PositionVM>();
        }

    }
}
