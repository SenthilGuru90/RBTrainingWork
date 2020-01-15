using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Linq;

namespace Rabobank.Training.ClassLibrary
{
    public class ProcessHolder : IFundOfMandatesProcessor
    {
        public PositionVM GetCalculatedMandates(PositionVM position, FundOfMandates fom)
        {
            decimal CalculatedSum = 0;
            List<MandateVM> lstMtVm = new List<MandateVM>();

            if (position.Code == fom.InstrumentCode)
            {
                foreach (Mandate mandt in fom.Mandates)
                {
                    MandateVM mtVM = new MandateVM { Allocation = mandt.Allocation / 100, Name = mandt.MandateName, Value = Math.Round(position.Value * mandt.Allocation / 100) };
                    lstMtVm.Add(mtVM);

                    CalculatedSum = CalculatedSum + Math.Round(position.Value * mandt.Allocation / 100);
                }

                if (fom.LiquidityAllocation > 0)
                {
                    MandateVM mtVM = new MandateVM { Allocation = fom.LiquidityAllocation / 100, Name = "Liquidity", Value = Math.Round(position.Value - CalculatedSum) };
                    lstMtVm.Add(mtVM);
                }

                position.Mandates = lstMtVm;
            }

            return position;
        }

        public PortfolioVM GetPortfolio()
        {
            List<PositionVM> lstPositions = new List<PositionVM>();

            PositionVM position1 = new PositionVM();
            position1.Code = "NL0000009165";
            position1.Name = "Heineken";
            position1.Value = 12345;
            lstPositions.Add(position1);

            PositionVM position2 = new PositionVM();
            position2.Code = "NL0000287100";
            position2.Name = "Optimix Mix Fund";
            position2.Value = 23456;
            lstPositions.Add(position2);

            PositionVM position3 = new PositionVM();
            position3.Code = "LU0035601805";
            position3.Name = "DP Global Strategy L High";
            position3.Value = 34567;
            lstPositions.Add(position3);

            PositionVM position4 = new PositionVM();
            position4.Code = "NL0000292332";
            position4.Name = "Rabobank Core Aandelen Fonds T2";
            position4.Value = 45678;
            lstPositions.Add(position4);

            PositionVM position5 = new PositionVM();
            position5.Code = "LU0042381250";
            position5.Name = "Morgan Stanley Invest US Gr Fnd";
            position5.Value = 56789;
            lstPositions.Add(position5);

            PortfolioVM portfolio = new PortfolioVM();
            portfolio.Positions = lstPositions;

            return portfolio;
        }

        public List<FundOfMandates> ReadFundOfMandatesFile(string FileName)
        {
            List<FundOfMandates> fom = new List<FundOfMandates>();

            XDocument doc = XDocument.Load(FileName);

            XNamespace ns = "http://amt.rnss.rabobank.nl/";

            IEnumerable<XElement> descendants = doc.Descendants(ns + "FundOfMandates");

            string InstrumentName = string.Empty;
            string InstrumentCode = string.Empty;
            decimal LiquidityAllocation = 0;
            string MandateId = string.Empty;
            string MandateName = string.Empty;
            decimal Allocation = 0;
            List<Mandate> mandatesField = new List<Mandate>();

            foreach (XElement item in descendants)
            {
                if (item.HasElements)
                {
                    foreach (XNode item1 in item.Nodes())
                    {
                        XElement element = ((System.Xml.Linq.XElement)item1);

                        if (element.Name.LocalName == "InstrumentName")
                        {
                            InstrumentName = element.Value;
                        }
                        else if (element.Name.LocalName == "InstrumentCode")
                        {
                            InstrumentCode = element.Value;
                        }
                        else if (element.Name.LocalName == "LiquidityAllocation")
                        {
                            LiquidityAllocation = Convert.ToDecimal(element.Value);
                        }
                        else if (element.Name.LocalName == "Mandates")
                        {
                            mandatesField = new List<Mandate>();
                            IEnumerable<XElement> arrMts = element.Descendants(ns + "Mandate");

                            foreach (XElement mt in arrMts)
                            {
                                if (mt.HasElements)
                                {
                                    foreach (XNode mtItem in mt.Nodes())
                                    {
                                        XElement mtElement = ((System.Xml.Linq.XElement)mtItem);

                                        if (mtElement.Name.LocalName == "MandateId")
                                        {
                                            MandateId = mtElement.Value;
                                        }
                                        else if (mtElement.Name.LocalName == "MandateName")
                                        {
                                            MandateName = mtElement.Value;
                                        }
                                        else if (mtElement.Name.LocalName == "Allocation")
                                        {
                                            Allocation = Convert.ToDecimal(mtElement.Value);
                                        }
                                    }

                                    mandatesField.Add(new Mandate { Allocation = Allocation, MandateId = MandateId, MandateName = MandateName });
                                }
                            }
                        }

                    }
                }

                fom.Add(new FundOfMandates
                {
                    InstrumentName = InstrumentName,
                    InstrumentCode = InstrumentCode,
                    LiquidityAllocation = LiquidityAllocation,
                    Mandates = mandatesField.ToArray()
                });
            }

            return fom;
        }
    }
}
