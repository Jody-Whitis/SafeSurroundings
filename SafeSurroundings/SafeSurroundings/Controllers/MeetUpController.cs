using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafeSurroundings.Data.Models;
using SafeSurroundings.Data.Services;
using SafeSurroundings.Models;
using SafeSurroundings.Services;

namespace SafeSurroundings.Controllers
{
    public class MeetUpController : Controller
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
        public ActionResult Index()
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
                        PlaceName = p.Name,
                        MeetTime = m.MeetTime
                    };
                return View(meetUpViewModels);
            }
            catch
            {
                return View("Index", "Home");
            }
             
          
        }

        [HttpGet]
        [UserAuthentication]
        public ActionResult AddMeetUp()
        {
            MeetUpViewModel meetUpViewModel = new MeetUpViewModel();
            meetUpViewModel.PlacestoMeet = placeTable.GetAll();
            return View(meetUpViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthentication]
        public ActionResult AddMeetUp(MeetUp newMeetUp)
        {
            try
            {
                meetUpTable.Add(newMeetUp);
                return RedirectToAction("Index","MeetUp");
            }
            catch{
                return View("Error");
            }
        }
    
    }
}