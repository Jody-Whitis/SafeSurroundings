using SafeSurroundings.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Data.Services
{
    public class InMemoryProfileTable
    {
        List<Profile> accounts;
        
        public InMemoryProfileTable()
        {
            accounts = new List<Profile> { new Profile { ID = 1, UserName = "jodywhitis0407@gmail.com", DisplayName="Jody",
                Password = "test", IsActive = true, LastLogin = DateTime.Now, IsSubscribed = false,
                IsTwoFactor=false, ListofMeetUpID = new List<int>{ 1}} };
        }

        public IEnumerable<Profile> GetAll()
        {
            return accounts.OrderBy(a => a.ID);
        } 

        public Profile GetLoginAccount(string user,string password)
        {
            return accounts.Where(a => a.UserName == user && a.Password == password).FirstOrDefault();
        }

        public Profile SelectAccount(int id)
        {
            return accounts.Where(a => a.ID == id).FirstOrDefault();
        }
        
        public void AddAccount(Profile newAccount)
        {
            newAccount.ID = accounts.Max(a => a.ID) + 1;
            accounts.Add(newAccount);
        }
        
        public void Update(int id)
        {
            
        }

        public void Delete(int id)
        {
            Profile account = accounts.Where(a => a.ID == id).FirstOrDefault();
            accounts.Remove(account);
        }

        public void AddMeetup(int id, int meetUpID)
        {
            Profile account = accounts.Where(a => a.ID == id).FirstOrDefault();
            List<int> currentMeetups = account.ListofMeetUpID.ToList();
            currentMeetups.Add(meetUpID);
            account.ListofMeetUpID = currentMeetups;
        }

        public void RemoveMeetup(int id, int meetupID)
        {
            Profile account = accounts.Where(a => a.ID == id).FirstOrDefault();
            List<int> currentMeetups = account.ListofMeetUpID.ToList();
            currentMeetups.Remove(meetupID);
            account.ListofMeetUpID = currentMeetups;
        }
    }
}
