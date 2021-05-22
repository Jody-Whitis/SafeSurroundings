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
                        ID = m.ID,
                        PlaceName = p.Name,
                        MeetTime = m.MeetTime
                    };
                return View(meetUpViewModels);
            }
            catch (Exception ex)
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
                Response.StatusCode = SetStatus("Created");
                return RedirectToAction("Index", "MeetUp");
            }
            catch (Exception ex)
            {
                Response.StatusCode = SetStatus(ex.Message);
                return View("Error");
            }
        }

        [HttpGet]
        [UserAuthentication]
        public override ActionResult Edit(int? meetUpToEditID)
        {
            if ((ModelState.IsValid) && (meetUpToEditID != null))
            {
                MeetUpViewModel meetUpViewModel = new MeetUpViewModel();
                meetUpViewModel.PlacestoMeet = placeTable.GetAll();
                meetUpViewModel.NewMeetUp = meetUpTable.GetAll().Where(m => m.ID == meetUpToEditID).FirstOrDefault();
                return View(meetUpViewModel);

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
        public ActionResult Edit(MeetUpViewModel meetupToEdit)
        {
            try
            {
                meetupToEdit.NewMeetUp.PlaceName = placeTable.GetAll().Where(p => p.ID == meetupToEdit.NewMeetUp.PlaceID).FirstOrDefault().Name;
                meetupToEdit.NewMeetUp = meetUpTable.Update(meetupToEdit.NewMeetUp);

                Response.StatusCode = meetupToEdit == null ? SetStatus("Created") : SetStatus(null);
                return RedirectToAction("Index", "Meetup");
            }
            catch (Exception ex)
            {
                Response.StatusCode = SetStatus(ex.Message);
                return View("Index", "Meetup");

            }
        }
        [HttpDelete]
        public ActionResult Delete(int ID)
        {
            MeetUp deletedMeetup = new MeetUp();
            deletedMeetup = meetUpTable.GetAll().Where(m => m.ID == ID).FirstOrDefault();
           if (deletedMeetup != null)
            {
            meetUpTable.Delete(ID);
            Response.StatusCode = SetStatus("Deleted");
            return View("Index", "MeetUp");
            }
            else
            {
                Response.StatusCode = SetStatus(null);
                return View("Error");
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
            if ((ex != null) && (!string.IsNullOrEmpty(ex.ToString())))
            {
                return Convert.ToInt16(HttpStatusCode.Created);
            }
            else
            {
                return Convert.ToInt16(HttpStatusCode.NotAcceptable);
            }
        }

    }
}