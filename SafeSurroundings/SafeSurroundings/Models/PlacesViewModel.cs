using SafeSurroundings.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SafeSurroundings.Data.Models.Ratings.Ratings;

namespace SafeSurroundings.Models
{
    public class PlacesViewModel
    {
        public Place PlaceModel { get; set; }
        public List<Place> ListofAllPlaces{get;set;}
        //for signed in accounts, frequent places
        public string GetSafetyName(Place placeToRate) {
                return placeToRate != null && placeToRate.Safety != null ? Enum.GetName(typeof(Safety), 
                    Convert.ToInt16(placeToRate.Safety.Average(r => r))) : "Place Not Yet Rated!";
        }

        public short Safety { get; set; }
        public short[] AvailableSafety
        {
            get
            {
               List<short> safetyValues = new List<short>();
               Array enumValues =  Enum.GetValues(typeof(Safety));
          
               foreach(var enumValue in enumValues)
                {
                    safetyValues.Add((short)enumValue);
                } 
               return safetyValues.ToArray();
            }
        }
    }
}