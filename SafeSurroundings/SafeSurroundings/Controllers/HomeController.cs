using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafeSurroundings.Data.Models;
using SafeSurroundings.Models;
using SafeSurroundings.Data.Services;

namespace SafeSurroundings.Controllers
{
    public class HomeController : Controller
    {
        InMemoryPersonTable personTable;

        public HomeController(InMemoryPersonTable personTable)
        {
            this.personTable = personTable;
        }

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

        public ActionResult TestData()
        {
            Person personTest = personTable.GetAll().FirstOrDefault();
            TestViewModel testViewModel = new TestViewModel();
            testViewModel.Name = personTest.DisplayName;
            return View(testViewModel);
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