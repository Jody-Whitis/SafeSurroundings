using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafeSurroundings.Data.Models;
using SafeSurroundings.Models;
using SafeSurroundings.Data.Services;
using SafeSurroundings.Services;
using Newtonsoft.Json;
using System.Net;

namespace SafeSurroundings.Controllers
{
    public class PlacesController : BaseController
    {
        InMemoryPlaceTable placeTable;
        InMemoryMeetUpTable meetupTable;

        public PlacesController(InMemoryPlaceTable placeTable, InMemoryMeetUpTable meetUpTable)
        {
            this.placeTable = placeTable;
            this.meetupTable = meetUpTable;
        }

        [HttpGet]
        public override ActionResult Index()
        {
            List<Place> places = new List<Place>();
            try
            {
                places = placeTable.GetAll().ToList();
                PlacesViewModel placesViewModel = new PlacesViewModel();
                placesViewModel.ListofAllPlaces = places;
                return View(placesViewModel);
            }
            catch (Exception ex)
            {
                Response.StatusCode = SetStatus(ex);
                return View("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthentication]
        public ActionResult Add(Place newPlace)
        {
            try
            {
                placeTable.Add(newPlace);
                return RedirectToAction("Index", "Places");
            }
            catch (Exception ex)
            {
                Response.StatusCode = SetStatus(ex);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult GetMeetUpByPlaceID(int PlaceID = 0)
        {
            List<MeetUp> meetupsByPlace = new List<MeetUp>();
            try
            {
                meetupsByPlace = meetupTable.GetAll().Where(m => m.PlaceID == PlaceID).ToList();
            }
            catch(Exception ex)
            {
                Response.StatusCode = SetStatus(ex);
                meetupsByPlace = new List<MeetUp>();
            }
            return Json(JsonConvert.SerializeObject(meetupsByPlace), JsonRequestBehavior.AllowGet);

        }



    }
}