using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafeSurroundings.Data.Models;
using SafeSurroundings.Data.Services;

namespace SafeSurroundings.Controllers
{
    public class MeetUpController : Controller
    {
        InMemoryMeetUpTable meetUpTable;

        public MeetUpController(InMemoryMeetUpTable meetUpTable)
        {
            this.meetUpTable = meetUpTable;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}