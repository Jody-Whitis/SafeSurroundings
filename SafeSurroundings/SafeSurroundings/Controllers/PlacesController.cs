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
    public class PlacesController : Controller
    {
        InMemoryPlaceTable placeTable;

        public PlacesController(InMemoryPlaceTable placeTable)
        {
            this.placeTable = placeTable;
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

         

    }
}