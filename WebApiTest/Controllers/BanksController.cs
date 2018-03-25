using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiTest.Controllers
{
    public class BanksController : ApiController
    {
        private readonly IBankRepository _bankRepository;

        public BanksController()
        {

        }

        public BanksController(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        // GET: api/Banks
        public IEnumerable<Bank> Get()
        {
            return _bankRepository.GetAllBanks();
        }

        // GET: api/Banks/5
        public Bank Get(int id)
        {
            return _bankRepository.Get(id);
        }

        // POST: api/Banks
        public IHttpActionResult Post([FromBody]Bank bank)
        {
            _bankRepository.Create(bank);
            return Created(Request.RequestUri + bank.Id.ToString(), bank);
        }

        // PUT: api/Banks/5
        public IHttpActionResult Put(int id, [FromBody]Bank bank)
        {
            bank.Id = id;
            _bankRepository.Update(bank);
            return Ok();
        }

        // DELETE: api/Banks/5
        public IHttpActionResult Delete(int id)
        {
            _bankRepository.Delete(id);
            return Ok();
        }
    }
}
