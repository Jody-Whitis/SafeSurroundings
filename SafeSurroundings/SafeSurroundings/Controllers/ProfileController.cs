using SafeSurroundings.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafeSurroundings.Data.Services;
using SafeSurroundings.Data.Models;
using SafeSurroundings.Models;

namespace SafeSurroundings.Controllers
{
    public class ProfileController : BaseController
    {

        InMemoryProfileTable inMemoryAccountTable;
        InMemoryProfileTable inMemoryProfileTable;
        public ProfileController(InMemoryProfileTable inMemoryAccountTable, InMemoryProfileTable inMemoryProfileTable)
        {
            this.inMemoryAccountTable = inMemoryAccountTable;
            this.inMemoryProfileTable = inMemoryProfileTable;
        }

        [HttpGet]
        [UserAuthentication]
        public override ActionResult Index()
        {
            try
            {
                ProfileViewModel profileViewModel = new ProfileViewModel();
                Profile currentProfile = new Profile();
                currentProfile = (Profile)Session["profile"];
                //Add object reflection dll
                profileViewModel.UserName = currentProfile.UserName;
                profileViewModel.ProfileID = currentProfile.ID;
                profileViewModel.DisplayName = currentProfile.DisplayName;
                profileViewModel.IsSubscribed = currentProfile.IsSubscribed;
                profileViewModel.IsTwoFactor = currentProfile.IsTwoFactor;
                profileViewModel.LastLogin = currentProfile.LastLogin;
                profileViewModel.LastLoginDevice = currentProfile.LastLoginDevice;


                return View(profileViewModel);
            }
            catch (Exception ex)
            {
                SetStatus(ex.Message);
                return View();
            }
        }

        [HttpGet]
        [UserAuthentication]
        public override ActionResult Edit(int? EditID)
        {
            Profile profiletoEdit = new Profile();
            ProfileViewModel profileViewModel = new ProfileViewModel();
            try
            {
                profiletoEdit = inMemoryProfileTable.GetAll().Where(p => p.ID == EditID).FirstOrDefault();
                profileViewModel.DisplayName = profiletoEdit.DisplayName;
                profileViewModel.IsSubscribed = profiletoEdit.IsSubscribed;
                profileViewModel.IsTwoFactor = profiletoEdit.IsTwoFactor;
                Response.StatusCode = SetStatus(string.Empty);
                return View(profileViewModel);
            }
            catch (Exception ex){
                Response.StatusCode = SetStatus(ex.Message);
                return View("Error");
            }
        }

        [HttpPost]
        [UserAuthentication]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileViewModel profileViewModelToUpdate)
        {
            try
            {



                Response.StatusCode = SetStatus(string.Empty);
                return RedirectToAction("Index", "Profile");
            }
            catch(Exception ex)
            {
                Response.StatusCode = SetStatus(ex.Message);
                return View();
            }
        }
    }
}