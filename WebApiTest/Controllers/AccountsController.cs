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
    public class AccountsController : ApiController
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController()
        {

        }

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        // GET: api/Accounts
        public IEnumerable<Account> Get()
        {
            return _accountRepository.GetAllAccounts();
        }

        // GET: api/Accounts/5
        public Account Get(int id)
        {
            return _accountRepository.Get(id);
        }

        // POST: api/Accounts
        public IHttpActionResult Post([FromBody]Account account)
        {
            _accountRepository.Create(account);
            return Created(Request.RequestUri + account.Id.ToString(), account);
        }

        // PUT: api/Accounts/5
        public IHttpActionResult Put(int id, [FromBody]Account account)
        {
            account.Id = id;
            _accountRepository.Update(account);
            return Ok();
        }

        // DELETE: api/Accounts/5
        public IHttpActionResult Delete(int id)
        {
            _accountRepository.Delete(id);
            return Ok();
        }
    }
}
