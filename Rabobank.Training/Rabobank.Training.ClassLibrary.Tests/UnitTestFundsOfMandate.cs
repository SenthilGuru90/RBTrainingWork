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
        public List<FundOfMandates> Result => prc.ReadFundOfMandatesFile("..\\..\\..\\TestData\\FundsOfMandatesData.xml");

        [TestMethod]
        public void ListOfFundOfMandates_HavingElements_ReturnsTrue()
        {
            //Making sure that Collection is having at least one element
            Result.Should().NotBeNull().And.HaveCount(c => c > 0).And.OnlyHaveUniqueItems();            
        }

        [TestMethod]
        public void FundOfMandates_HavingInstrumentNameWithStringType_ReturnsTrue()
        {
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
            IEnumerable<object> obMandates = null;
            obMandates = Result.Select(x => x.Mandates);

            obMandates.Should().NotBeNull().And.HaveCount(c => c > 0).And.OnlyHaveUniqueItems();
        }

        [TestMethod]
        public void FundOfMandates_HavingMandates_EachMandateHavingMandateIDOfTypeString_ReturnsTrue()
        {            
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
        public void TestPath1()
        {
            FundOfMandates fom = Result.ElementAt(1);
            var Test1 = prc.GetCalculatedMandates(prc.GetPortfolio(), fom);
        }

    }
}
