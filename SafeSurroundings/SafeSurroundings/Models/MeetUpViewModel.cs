using SafeSurroundings.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafeSurroundings.Models
{
    public class MeetUpViewModel
    {
        public string PlaceName { get; set; }
        public DateTime MeetTime { get; set; }
        public IEnumerable<Place> PlacestoMeet { get; set; }
        public MeetUp NewMeetUp { get; set; }
     }
}