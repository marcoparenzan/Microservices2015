using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Call4Pizza.Totem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pizzas()
        {
            return View();
        }

        public ActionResult Beverages()
        {
            return View();
        }
    }
}