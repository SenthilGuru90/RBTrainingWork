using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rabobank.Training.ClassLibrary;

namespace Rabobank.Training.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private IFundOfMandates _fundOfMandates;
        private List<FundOfMandates> Result;
        private List<PositionVM> lstPositions;

        public PortfolioController(IFundOfMandates fundOfMandates)
        {
            _fundOfMandates = fundOfMandates;
            Result = _fundOfMandates.ReadFundOfMandatesFile("TestData\\FundsOfMandatesData.xml");
        }

        // GET: api/Portfolio
        [HttpGet]
        public IEnumerable<PositionVM> Get()
        {
            PortfolioVM portfolio = new PortfolioVM();
            PositionVM position = new PositionVM();

            portfolio = _fundOfMandates.GetPortfolio();

            lstPositions = new List<PositionVM>();

            foreach (FundOfMandates fom in Result)
            {
                position = new PositionVM();
                position = _fundOfMandates.GetCalculatedMandates(portfolio, fom);
                lstPositions.Add(position);
            }
                       
            return lstPositions.ToArray();
        }

        // GET: api/Portfolio/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Portfolio
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Portfolio/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
