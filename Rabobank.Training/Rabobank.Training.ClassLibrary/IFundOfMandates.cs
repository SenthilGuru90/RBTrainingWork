using System;
using System.Collections.Generic;
using System.Text;

namespace Rabobank.Training.ClassLibrary
{
    public interface IFundOfMandates
    {
        public List<FundOfMandates> ReadFundOfMandatesFile(string FileName);

        public PortfolioVM GetPortfolio();   

        public PositionVM GetCalculatedMandates(PortfolioVM portfolio, FundOfMandates fom);
    }
}
