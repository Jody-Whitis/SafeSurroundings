using SafeSurroundings.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafeSurroundings.Models
{
    public class PlacesViewModel
    {
        public Place PlaceModel { get; set; }
        public List<Place> ListofAllPlaces{get;set;}
        //for signed in accounts, frequent places
    }
}