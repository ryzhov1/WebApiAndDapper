using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.FileImport;
using DataAccess.Repositories;

namespace WebApiTest.Controllers
{
    [RoutePrefix("api/Preview")]
    public class PreviewController : ApiController
    {
        private readonly ISavingRepository _savingRepository;

        public PreviewController()
        {

        }

        public PreviewController(ISavingRepository savingRepository)
        {
            _savingRepository = savingRepository;
        }

        // GET: api/Preview/5
        public IEnumerable<Contragent> Get(string id)
        {
            string root = System.Web.HttpContext.Current.Server.MapPath("~/Files/");
            string fileFullName = root + id;
            
            if (!System.IO.File.Exists(fileFullName))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Файл не найден на сервере. ID = {0}", id)),
                    ReasonPhrase = "Файл не найден на сервере"
                };
                throw new HttpResponseException(resp);
            }

            var FileImporter = new FileImporter(fileFullName);

            if (!FileImporter.TestImport())
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("При импорте данных из файла произошла ошибка. Сообщение = {0}.", FileImporter.LastError)),
                    ReasonPhrase = "При импорте данных из файла произошла ошибка"
                };
                throw new HttpResponseException(resp);
            }

            return FileImporter.GetAllContragents();
        }

        // Put: api/Preview
        [HttpPut]
        [Route("PutAllContragents/{id}")]
        public IHttpActionResult PutAllContragents(string id)
        {
            string root = System.Web.HttpContext.Current.Server.MapPath("~/Files/");
            string fileFullName = root + id;

            if (!System.IO.File.Exists(fileFullName))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Файл не найден на сервере. ID = {0}", id)),
                    ReasonPhrase = "Файл не найден на сервере"
                };
                throw new HttpResponseException(resp);
            }

            var FileImporter = new FileImporter(fileFullName);

            if (!FileImporter.TestImport())
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("При импорте данных из файла произошла ошибка. Сообщение = {0}.", FileImporter.LastError)),
                    ReasonPhrase = "При импорте данных из файла произошла ошибка"
                };
                throw new HttpResponseException(resp);
            }

            try
            {
                _savingRepository.SaveAllContragents(FileImporter.GetAllContragents());
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("При сохранении файла в БД произошла ошибка. Текст = {0}", e.Message)),
                    ReasonPhrase = "При сохранении файла в БД произошла ошибка"
                };
                throw new HttpResponseException(resp);
            }

            return Ok("Успешный импорт в БД");
        }

        [HttpPut]
        [Route("PutAllContragentsFast/{id}")]
        public IHttpActionResult PutAllContragentsFast(string id)
        {
            string root = System.Web.HttpContext.Current.Server.MapPath("~/Files/");
            string fileFullName = root + id;

            if (!System.IO.File.Exists(fileFullName))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Файл не найден на сервере. ID = {0}", id)),
                    ReasonPhrase = "Файл не найден на сервере"
                };
                throw new HttpResponseException(resp);
            }

            var FileImporter = new FileImporter(fileFullName);

            if (!FileImporter.TestImport())
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("При импорте данных из файла произошла ошибка. Сообщение = {0}.", FileImporter.LastError)),
                    ReasonPhrase = "При импорте данных из файла произошла ошибка"
                };
                throw new HttpResponseException(resp);
            }

            try
            {
                _savingRepository.SaveAllContragentsFast(FileImporter.GetAllContragents());
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("При сохранении файла в БД произошла ошибка. Текст = {0}", e.Message)),
                    ReasonPhrase = "При сохранении файла в БД произошла ошибка"
                };
                throw new HttpResponseException(resp);
            }

            return Ok("Успешный импорт в БД");
        }
    }
}
