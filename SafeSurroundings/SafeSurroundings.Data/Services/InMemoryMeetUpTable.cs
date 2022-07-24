using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeSurroundings.Data.Models;

namespace SafeSurroundings.Data.Services
{
    public class InMemoryMeetUpTable
    {
        List<MeetUp> meetUps;

        public InMemoryMeetUpTable()
        {
            meetUps = new List<MeetUp> { new MeetUp {ID=1,PlaceID=1, MeetTime=DateTime.Today, PlaceName="Starbucks", PersonID=1, Details="Meet Someone to Hangout"} };
        }

        public void Add(MeetUp newMeetup)
        {
            newMeetup.ID = meetUps != null && meetUps.Any() ? meetUps.Max(m => m.ID) + 1 : 0;        
            meetUps.Add(newMeetup);
        }

        public void Delete(int id)
        {
            MeetUp meetup = meetUps.Where(m => m.ID == id).FirstOrDefault();
            meetUps.Remove(meetup);
        }
      
        public MeetUp Update(MeetUp meetupToUpdate)
        {
            try
            {
                MeetUp meetUpFromUpdate = meetUps.Where(m => m.ID == meetupToUpdate.ID).FirstOrDefault();
                meetUpFromUpdate.MeetTime = meetupToUpdate.MeetTime;
                meetUpFromUpdate.PlaceName = meetupToUpdate.PlaceName;
                meetUpFromUpdate.PlaceID = meetupToUpdate.PlaceID;
                meetUpFromUpdate.Details = meetupToUpdate.Details;
                return meetUpFromUpdate;
            }
            catch
            {
                return null;
            }
         
        }

        public IEnumerable<MeetUp> GetAll()
        {
            return meetUps.OrderBy(m => m.ID);
        }

        public IEnumerable<MeetUp> GetByPersonID(int PersonID)
        {
            return meetUps.Where(x => x.PersonID == PersonID);
        }

        public IEnumerable<MeetUp> GetByPersonIDToday(int PersonID)
        {
            return meetUps.Where(x => x.PersonID == PersonID && x.MeetTime.ToString("MM/dd/yyyy").Equals(DateTime.Now.ToString("MM/dd/yyyy")));
        }

    }
}
