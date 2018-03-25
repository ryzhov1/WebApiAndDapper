using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiTest.Controllers
{
    public class DeleteDataController : ApiController
    {

        private readonly IDeletingRepository _deletingRepository;

        public DeleteDataController()
        {

        }

        public DeleteDataController(IDeletingRepository deletingRepository)
        {
            _deletingRepository = deletingRepository;
        }

        // DELETE: api/DeleteData/5
        public IHttpActionResult Delete()
        {
            _deletingRepository.DeleteAllData();
            return Ok("БД успешно очищена");
        }
    }
}
