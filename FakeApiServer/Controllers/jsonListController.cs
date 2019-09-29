using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FakeApiServer.Controllers
{
    public class jsonListController : Controller
    {

        [Route("/jsonList")]
        public ActionResult Index()
        {
            ViewBag.Title = "jsonList";

            return View();
        }
    }
}
