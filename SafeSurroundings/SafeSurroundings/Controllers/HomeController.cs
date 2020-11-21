using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafeSurroundings.Data.Models;
using SafeSurroundings.Models;

namespace SafeSurroundings.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Test", "Home");
        }

        public ActionResult Test()
        {
            TestViewModel testviewModel = new TestViewModel();
            TestModel test = new TestModel();
            test.ID = -1;
            test.Name = "Test from Model Project";
            testviewModel.Name = "Test Initial";
            testviewModel.testModel = test;
            return View(testviewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}