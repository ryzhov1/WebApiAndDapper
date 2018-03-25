using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Главная";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Upload()
        {
            ViewBag.Title = "Загрузить файл";

            return View();
        }

        public ActionResult Preview(string id)
        {
            if (id == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Предварительный просмотр";
            ViewData["fileName"] = id;

            return View();
        }
    }
}
