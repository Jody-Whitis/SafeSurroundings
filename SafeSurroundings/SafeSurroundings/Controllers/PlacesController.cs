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

        public PlacesController(InMemoryPlaceTable placeTable, InMemoryMeetUpTable meetUpTable):base()
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
                Response.StatusCode = SetStatus(ex.Message);
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
                Response.StatusCode = SetStatus(ex.Message);
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
                Response.StatusCode = SetStatus(ex.Message);
                meetupsByPlace = new List<MeetUp>();
            }
            return Json(JsonConvert.SerializeObject(meetupsByPlace), JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [UserAuthentication]
        public override ActionResult Edit(int? EditID)
        {
            if ((ModelState.IsValid) && (EditID != null))
            {
                PlacesViewModel placesViewModel = new PlacesViewModel();
                placesViewModel.PlaceModel = placeTable.GetAll().Where(p => p.ID == EditID).FirstOrDefault();
                return View(placesViewModel);
            }
            else
            {
                SetStatus("Place Not Found to Edit");
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthentication]
        public ActionResult Edit(PlacesViewModel placeToEdit)
        {
            try
            {
                placeToEdit.PlaceModel = placeTable.Update(placeToEdit.PlaceModel);
                placeToEdit.PlaceModel.Safety.Add(placeToEdit.Safety);
                Response.StatusCode = placeToEdit == null || placeToEdit.PlaceModel == null ? SetStatus("Created") : SetStatus(null);
                return RedirectToAction("Index", "Places");
            }
            catch(Exception ex)
            {
                Response.StatusCode = SetStatus(ex.Message);
                return View("Index", "Place");
            }
        }


    }
}