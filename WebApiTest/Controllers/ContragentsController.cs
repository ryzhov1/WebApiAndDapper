using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace WebApiTest.Controllers
{
    [RoutePrefix("api/Contragents")]
    public class ContragentsController : ApiController
    {
        private readonly IContragentRepository _contragentRepository;

        public ContragentsController()
        {

        }

        public ContragentsController(IContragentRepository contragentRepository)
        {
            _contragentRepository = contragentRepository;
        }

        // GET: api/Contragents
        public IEnumerable<Contragent> Get()
        {
            return _contragentRepository.GetAllContragents();
        }

        // GET: api/Contragents/5
        public Contragent Get(int id)
        {
            return _contragentRepository.Get(id);
        }

        [HttpGet]
        [Route("DuplicatesInn")]
        public IEnumerable<string> GetDuplicatesInn()
        {
            return _contragentRepository.GetDuplicatesInn();
        }

        // POST: api/Contragents
        public IHttpActionResult Post([FromBody]Contragent contragent)
        {
            _contragentRepository.Create(contragent);
            return Created(Request.RequestUri + contragent.Id.ToString(), contragent);
        }

        // PUT: api/Contragents/5
        public IHttpActionResult Put(int id, [FromBody]Contragent contragent)
        {
            contragent.Id = id;
            _contragentRepository.Update(contragent);
            return Ok();
        }

        // DELETE: api/Contragents/5
        public IHttpActionResult Delete(int id)
        {
            _contragentRepository.Delete(id);
            return Ok();
        }
    }
}
