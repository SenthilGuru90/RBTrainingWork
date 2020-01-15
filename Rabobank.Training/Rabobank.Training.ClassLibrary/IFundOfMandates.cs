using System;
using System.Collections.Generic;
using System.Text;

namespace Rabobank.Training.ClassLibrary
{
    public interface IFundOfMandatesProcessor
    {
        public List<FundOfMandates> ReadFundOfMandatesFile(string FileName);

        public PortfolioVM GetPortfolio();   

        public PositionVM GetCalculatedMandates(PositionVM position, FundOfMandates fom);


    }
}
