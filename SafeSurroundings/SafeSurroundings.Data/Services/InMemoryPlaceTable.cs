using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeSurroundings.Data.Models;
using SafeSurroundings.Data.Models.Ratings;
namespace SafeSurroundings.Data.Services
{
    public class InMemoryPlaceTable 
    {
        List<Place> places;

        public InMemoryPlaceTable()
        {
            places = new List<Place> { new Place { ID = 1, Name = "Starbucks", OpenHour = DateTime.Parse("8:00:00"), CloseHour = DateTime.Parse("17:00:00"),
                X_Coordinates = 100, Y_Coordinates = 100, Address="123 St Ave", Safety = new List<short>{(short)Ratings.Rating.Great}, Ratings = new List<short>{(short)Ratings.Rating.Great}}};
        }

        public void Add(Place newPlace)
        {
            newPlace.ID = places.Max(p => p.ID) + 1;
            places.Add(newPlace);
        }

        public void Delete(int id)
        {
            Place place = places.Where(p => p.ID == id).FirstOrDefault();
            places.Remove(place);
        }

        public Place Update(Place placetoUpdate)
        {
            try
            {
                Place placeFromUpdate = places.Where(p => p.ID == placetoUpdate.ID).FirstOrDefault();
                placeFromUpdate.OpenHour = placetoUpdate.OpenHour;
                placeFromUpdate.CloseHour = placetoUpdate.CloseHour;
                placeFromUpdate.X_Coordinates = placetoUpdate.X_Coordinates;
                placeFromUpdate.Y_Coordinates = placetoUpdate.Y_Coordinates;
                placeFromUpdate.Name = placetoUpdate.Name;
                return placeFromUpdate;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Place> GetAll()
        {
            return places.OrderBy(p => p.ID);
        }

        public Place GetResult(int id)
        {
            return places.Where(p => p.ID == id).FirstOrDefault();
        }

        public IEnumerable<Place> GetPlacesByIDs(IEnumerable<int> placeIDs)
        {
            try
            {
                return places.Where(p => placeIDs.Contains(p.ID));
            }
            catch
            {
                return null;
            }
        }

        public Place UpdateRating(int id, short rating)
        {
            try
            {
                Place placeToUpdate = places.Where(p => p.ID == id).FirstOrDefault();
                placeToUpdate.Ratings.Add(rating);
                return placeToUpdate;
            }
            catch
            {
                return null;
            }
        }

        public Place UpdateSafety(int id, short rating)
        {
            try
            {
                Place placeToUpdate = places.Where(p => p.ID == id).FirstOrDefault();
                placeToUpdate.Safety.Add(rating);
                return placeToUpdate;
            }
            catch
            {
                return null;
            }
        }
    }

}
