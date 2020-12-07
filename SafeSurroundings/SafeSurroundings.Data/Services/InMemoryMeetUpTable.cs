using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeSurroundings.Data.Models;

namespace SafeSurroundings.Data.Services
{
    public class InMemoryMeetUpTable: ILocalTables
    {
        List<MeetUp> meetUps;

        public InMemoryMeetUpTable()
        {
            meetUps = new List<MeetUp> { new MeetUp {ID=1,PlaceID=1, MeetTime=DateTime.Today } };
        }

        public void Add(MeetUp newMeetup)
        {
            newMeetup.ID = meetUps.Max(m => m.ID )+ 1;
            meetUps.Add(newMeetup);
        }

        public void Delete(int id)
        {
            MeetUp meetup = meetUps.Where(m => m.ID == id).FirstOrDefault();
            meetUps.Remove(meetup);
        }
      
        public void Update(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MeetUp> GetAll()
        {
            return meetUps.OrderBy(m => m.ID);
        }

        public IEnumerable<MeetUp> GetByIDs(IEnumerable<int> ids)
        {
            List<MeetUp> listOfMeetups = new List<MeetUp>();

            listOfMeetups= meetUps.TakeWhile(x => ids.Contains(x.ID)).ToList();
            return listOfMeetups;
        }

    }
}
