using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafeSurroundings.Data.Models;
using SafeSurroundings.Models;
using SafeSurroundings.Data.Services;
using SafeSurroundings.Services;
using SafeSurroundings.Data.Tools;

namespace SafeSurroundings.Controllers
{
    public class HomeController : Controller
    {
        InMemoryPersonTable personTable;
        InMemoryProfileTable profileTable;
        InMemoryMeetUpTable meetupTable;
        InMemoryPlaceTable placeTable;
        public HomeController(InMemoryPersonTable personTable, InMemoryProfileTable accountTable, InMemoryMeetUpTable meetupTable,InMemoryPlaceTable placeTable)
        {
            this.personTable = personTable;
            this.profileTable = accountTable;
            this.meetupTable = meetupTable;
            this.placeTable = placeTable;
         }

        [HttpGet]
        public ActionResult Index()
        {
            if((Session.Keys.Count > 0) && (Session["sessionGUID"] != null) && (!string.IsNullOrEmpty(Session["sessionGUID"].ToString())))
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
                Profile loginAccount = new Profile();
                loginAccount = profileTable.GetLoginAccount(userLogin.User, userLogin.Password);

                if((loginAccount != null))
                {
                    Session.Add("user", loginAccount.UserName);
                    Session.Add("name", loginAccount.DisplayName);
                    Session.Add("id", loginAccount.ID);
                    Session.Add("profile", loginAccount);
                    Session.Add("avatarSrc", ImageTools.GetImageScrFromBytes(loginAccount.ProfileImage));
                    loginAccount.LastLogin = DateTime.Now;
                    loginAccount.LastLoginDevice = Environment.MachineName.ToString(); 
                    loginAccount = profileTable.Update(loginAccount);

                    if (loginAccount.IsTwoFactor)
                    {
                       return RedirectToAction("TwoFactorAuthenication", "Home");
                    }
                    else
                    {
                    Session.Add("sessionGUID", Guid.NewGuid());
                    return RedirectToAction("Index","Home");
                    }
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
        public ActionResult TwoFactorAuthenication()
        {
            Session.Add("TwoFactorAuth", false);
            TwoFactorViewModel twoFactorProfile = new TwoFactorViewModel();
            Session.Add("TwoFactorCode",Data.Models.TwoFactorAuthenication.GetTwoFactorCode());

            string emailTemplate = System.IO.File.ReadAllText(Server.MapPath(@"~/EmailTemplates/EmailTemplateBase.html"));
            string emailBody = System.IO.File.ReadAllText(Server.MapPath(@"~/EmailTemplates/TwoFactorAuthentication.html"));

            Data.Models.TwoFactorAuthenication.SendTwoFactor(Convert.ToString(Session["user"]), Convert.ToInt16(Session["TwoFactorCode"]), emailTemplate.Replace(EmailTools.InsertTextMarker, emailBody));
            
            return View(twoFactorProfile);  
        }

        [HttpPost]
        public ActionResult TwoFactorAuthenication(TwoFactorViewModel twoFactorProfile)
        {
            if ((ModelState.IsValid) && (twoFactorProfile.TwoFactorCode == Convert.ToInt16(Session["TwoFactorCode"])))
            {
                Session["TwoFactorAuth"] = true;
                Session.Remove("TwoFactorCode");
                Session.Add("sessionGUID", Guid.NewGuid());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Login Unsuccessful", "Incorrect Code");
                return View();
            }
        }

        [HttpGet]
        [UserAuthentication]
        public ActionResult HomePage(HomeViewModel homeViewModel)
        {
            try
            {
                IEnumerable<Place> listOfPlaces = placeTable.GetAll();
                int id = Convert.ToInt32(Session["id"]);
                homeViewModel.DisplayName = Convert.ToString(Session["name"]);
                homeViewModel.ListofMeetups = meetupTable.GetByPersonIDToday(id);              
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