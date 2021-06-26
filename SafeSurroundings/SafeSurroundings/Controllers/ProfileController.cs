using SafeSurroundings.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafeSurroundings.Data.Services;
using SafeSurroundings.Data.Models;

namespace SafeSurroundings.Controllers
{
    public class ProfileController : BaseController
    {

        InMemoryProfileTable inMemoryAccountTable;
        public ProfileController(InMemoryProfileTable inMemoryAccountTable)
        {
            this.inMemoryAccountTable = inMemoryAccountTable;
        }

        [HttpGet]
        [UserAuthentication]
        public override ActionResult Index()
        {
            try
            {
                Profile currentProfile = new Profile();
                currentProfile = (Profile)Session["profile"];
                return View();
            }
            catch (Exception ex)
            {
                SetStatus(ex.Message);
                return View();
            }
        }
    }
}