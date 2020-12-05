using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafeSurroundings.Data.Models;
using SafeSurroundings.Models;
using SafeSurroundings.Data.Services;
using SafeSurroundings.Services;

namespace SafeSurroundings.Controllers
{
    public class HomeController : Controller
    {
        InMemoryPersonTable personTable;
        InMemoryAccountTable accountTable;
        InMemoryMeetUpTable meetupTable;
        public HomeController(InMemoryPersonTable personTable, InMemoryAccountTable accountTable, InMemoryMeetUpTable meetupTable)
        {
            this.personTable = personTable;
            this.accountTable = accountTable;
            this.meetupTable = meetupTable;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if((Session.Keys.Count>0) && (!string.IsNullOrEmpty(Session["sessionGUID"].ToString())))
            {
                return RedirectToAction("HomePage");
            }
            else
            {
            return View();
            }
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                Account loginAccount = new Account();
                loginAccount = accountTable.GetLoginAccount(userLogin.User, userLogin.Password);

                if((loginAccount != null))
                {
                    Session.Add("user", loginAccount.UserName);
                    Session.Add("name", loginAccount.DisplayName);
                    Session.Add("id", loginAccount.ID);
                    Session.Add("sessionGUID", Guid.NewGuid());
                return RedirectToAction("Index","Home");

                }
                else
                {
                    ModelState.AddModelError("Login Unsuccessful", "Incorrect Login");
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Test", "Home");
            }
        }

        [HttpGet]
        [UserAuthentication]
        public ActionResult HomePage(HomeViewModel homeViewModel)
        {
            try
            {
                int id = Convert.ToInt32(Session["id"]);
                homeViewModel.DisplayName = Convert.ToString(Session["name"]);
                IEnumerable<int> meetupIDs = accountTable.SelectAccount(Convert.ToInt32(Session["id"])).ListofMeetUpID;
                homeViewModel.ListofMeetups = meetupTable.GetByIDs(meetupIDs);
                return View(homeViewModel);
            }
            catch
            {
                return RedirectToAction("Test", "Home");
            }
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

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
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