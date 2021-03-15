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

namespace SafeSurroundings.Controllers
{
    public class PlacesController : Controller
    {
        InMemoryPlaceTable placeTable;
        InMemoryMeetUpTable meetupTable;

        public PlacesController(InMemoryPlaceTable placeTable, InMemoryMeetUpTable meetUpTable)
        {
            this.placeTable = placeTable;
            this.meetupTable = meetUpTable;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Place> places = new List<Place>();
            places = placeTable.GetAll().ToList();
            PlacesViewModel placesViewModel = new PlacesViewModel();
            placesViewModel.ListofAllPlaces = places;
            return View(placesViewModel);
        }

        [HttpGet]
        [UserAuthentication]
        public ActionResult AddPlace()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthentication]
        public ActionResult AddPlace(Place newPlace)
        {
            try
            {
                placeTable.Add(newPlace);
                return RedirectToAction("Index", "Places");
            }
            catch
            {
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
            catch
            {
                  meetupsByPlace = new List<MeetUp>();
            }          
            return Json(JsonConvert.SerializeObject(meetupsByPlace), JsonRequestBehavior.AllowGet);

        }

         

    }
}