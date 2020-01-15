using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rabobank.Training.ClassLibrary;

namespace Rabobank.Training.WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private IFundOfMandatesProcessor _fundOfMandates = null;
        private IConfiguration config;
        private readonly ILoggerFactory loggerFactory;

        public PortfolioController(IFundOfMandatesProcessor fundOfMandates, IConfiguration config, ILoggerFactory loggerfactory)
        {
            _fundOfMandates = fundOfMandates;
            this.config = config;
            this.loggerFactory = loggerfactory;
        }

        // GET: api/Portfolio
        [HttpGet]
        public IEnumerable<PositionVM> Get()
        {
            PortfolioVM portfolio = null;
            List<PositionVM> lstPositions = null;
            List<FundOfMandates> lstFoms = null;

            try
            {
                _fundOfMandates = new ProcessHolder();

                var fomXmlPath = config["FundsOfMandatesFile"];
                lstFoms = new List<FundOfMandates>();
                lstPositions = new List<PositionVM>();

                portfolio = _fundOfMandates.GetPortfolio();
                lstFoms = _fundOfMandates.ReadFundOfMandatesFile(fomXmlPath);

                foreach (PositionVM pVM in portfolio.Positions)
                {
                    PositionVM posVM = pVM;
                    foreach (FundOfMandates fom in lstFoms)
                    {
                        posVM = _fundOfMandates.GetCalculatedMandates(pVM, fom);
                    }

                    lstPositions.Add(posVM);
                }                
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger("Porfolio Controller Logger");
                logger.LogError(e, "Error occured while retrieving Positions", null);
                throw e;
            }

            return lstPositions.ToArray();
        }
    }
}
