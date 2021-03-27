using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SafeSurroundings.Data.Models;
using SafeSurroundings.Data.Services;
using SafeSurroundings.Models;
using SafeSurroundings.Services;
using System.Net;

namespace SafeSurroundings.Controllers
{
    public class MeetUpController : BaseController
    {
        InMemoryMeetUpTable meetUpTable;
        InMemoryPlaceTable placeTable;
        public MeetUpController(InMemoryMeetUpTable meetUpTable, InMemoryPlaceTable placeTable)
        {
            this.meetUpTable = meetUpTable;
            this.placeTable = placeTable;
        }

        [HttpGet]
        [UserAuthentication]
        public override ActionResult Index()
        {
            List<Place> places = new List<Place>();
            places = placeTable.GetAll().ToList();
            List<MeetUp> meetUps = new List<MeetUp>();
            meetUps = meetUpTable.GetAll().ToList();
            MeetUpViewModel meetupViewModel = new MeetUpViewModel();

            try
            {
                IEnumerable<MeetUpViewModel> meetUpViewModels =
                    from m in meetUps
                    join p in places on m.PlaceID equals p.ID
                    select new MeetUpViewModel
                    {
                        ID=m.ID,
                        PlaceName = p.Name,
                        MeetTime = m.MeetTime
                    };
                return View(meetUpViewModels);
            }
            catch(Exception ex)
            {
                base.SetStatus(ex.Message);
                return View("Index", "Home");
            }
             
          
        }

        [HttpGet]
        [UserAuthentication]
        public override ActionResult Add()
        {
            MeetUpViewModel meetUpViewModel = new MeetUpViewModel();
            meetUpViewModel.PlacestoMeet = placeTable.GetAll();
            return View(meetUpViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthentication]
        public ActionResult Add(MeetUpViewModel newMeetUpViewModel)
        {
            try
            {
                MeetUp newMeetup = new MeetUp();
                newMeetup.MeetTime = newMeetUpViewModel.MeetTime;
                newMeetup.PlaceID = newMeetUpViewModel.PlaceID;
                newMeetup.PlaceName = placeTable.GetAll().Where(p => p.ID == newMeetup.PlaceID).Select(p => p.Name).FirstOrDefault();
                meetUpTable.Add(newMeetup);
                Response.StatusCode = SetStatus(null);
                return RedirectToAction("Index","MeetUp");
            }
            catch (Exception ex){
                Response.StatusCode = SetStatus(ex.Message);
                return View("Error");
            }
        }

        [HttpGet]
        [UserAuthentication]
        public ActionResult Edit(int? meetUpToEditID)
        {
            MeetUp meetupSelected = new MeetUp();
            if ((ModelState.IsValid) && (meetUpToEditID != null))
            {
                meetupSelected = meetUpTable.GetAll().Where(m => m.ID == meetUpToEditID).FirstOrDefault();
                return View(meetupSelected);
            }
            else
            {
                SetStatus("Meetup Not Found to Edit");
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthentication]
        public ActionResult Edit(MeetUp meetupToEdit)
        {
            try
            {
                //edit(meetuptoedit)
                Response.StatusCode = SetStatus(null);
                return View("Index", "Meetup");
            }
            catch(Exception ex)
            {
                Response.StatusCode = SetStatus(ex.Message);
                return View("Edit", "Meetup");
              
            }
        }

        [HttpGet]
        public ActionResult GetMeetUpByID(int MeetUpID = 0)
        {
            List<MeetUp> meetupsByPlace = new List<MeetUp>();
            try
            {
                meetupsByPlace = meetUpTable.GetAll().Where(m => m.ID == MeetUpID).ToList();
            }
            catch (Exception ex)
            {
                Response.StatusCode = SetStatus(ex.Message);
                meetupsByPlace = new List<MeetUp>();
            }
            return Json(JsonConvert.SerializeObject(meetupsByPlace), JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Indicates a post creates the object to add from the request.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected override int SetStatus(string ex)
        {
           if ((ex != null) && (! string.IsNullOrEmpty(ex.ToString()))){
                return Convert.ToInt16(HttpStatusCode.Created);
            }
            else
            {
                return Convert.ToInt16(HttpStatusCode.NotAcceptable);
            }
        }

    }
}