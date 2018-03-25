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
    public class ContragentAccountsController : ApiController
    {
        private readonly IContragentAccountsRepository _contragentAccountsRepository;

        public ContragentAccountsController()
        {

        }

        public ContragentAccountsController(IContragentAccountsRepository contragentAccountsRepository)
        {
            _contragentAccountsRepository = contragentAccountsRepository;
        }

        // GET: api/ContragentAccounts
        public IEnumerable<Account> Get(int id)
        {
            return _contragentAccountsRepository.GetAllContragentAccounts(id);
        }
    }
}
