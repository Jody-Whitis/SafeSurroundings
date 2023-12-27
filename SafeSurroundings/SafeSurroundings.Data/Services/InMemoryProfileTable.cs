using SafeSurroundings.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SafeSurroundings.Data.Tools;

namespace SafeSurroundings.Data.Services
{
    public class InMemoryProfileTable
    {
        List<Profile> profileList;
        
        public InMemoryProfileTable()
        {
            profileList = new List<Profile> { new Profile { ID = 1, UserName = "projtestcred@gmail.com", DisplayName="Test",
                Password = "test", IsActive = true, LastLogin = DateTime.Now,LastLoginDevice="Test Computer", IsSubscribed = false,
                IsTwoFactor=false, TwoFactorCode = 0, ListofMeetUpID = new List<int>{1}, ProfileImage = ImageTools.GetImageBytes()}};
        }

        public IEnumerable<Profile> GetAll()
        {
            return profileList.OrderBy(a => a.ID);
        } 

        public Profile GetLoginAccount(string user,string password)
        {
            return profileList.Where(a => a.UserName == user && a.Password == password).FirstOrDefault();
        }

        public Profile SelectAccount(int id)
        {
            return profileList.Where(a => a.ID == id).FirstOrDefault();
        }
        
        public void AddAccount(Profile newAccount)
        {
            newAccount.ID = profileList.Max(a => a.ID) + 1;
            profileList.Add(newAccount);
        }
        
        public Profile Update(Profile profileToUpdate)
        {
            try
            {
                Profile profileFromUpdate = profileList.Where(p => p.ID == profileToUpdate.ID).FirstOrDefault();
                if ((profileToUpdate.DisplayName != null) && (profileFromUpdate.DisplayName != profileToUpdate.DisplayName)) { profileFromUpdate.DisplayName = profileToUpdate.DisplayName; }
                if (profileFromUpdate.IsTwoFactor != profileToUpdate.IsTwoFactor) { profileFromUpdate.IsTwoFactor = profileToUpdate.IsTwoFactor; }
                if (profileFromUpdate.IsSubscribed != profileToUpdate.IsSubscribed) { profileFromUpdate.IsSubscribed = profileToUpdate.IsSubscribed; }
                if ((profileToUpdate.ProfileImage != null) && (profileToUpdate.ProfileImage.Length > 0)) { profileFromUpdate.ProfileImage = profileToUpdate.ProfileImage; };
                return profileFromUpdate;
            }
            catch
            {
                return null;
            }
        }

        public void Delete(int id)
        {
            Profile account = profileList.Where(a => a.ID == id).FirstOrDefault();
            profileList.Remove(account);
        }

        public void AddMeetup(int id, int meetUpID)
        {
            Profile account = profileList.Where(a => a.ID == id).FirstOrDefault();
            List<int> currentMeetups = account.ListofMeetUpID.ToList();
            currentMeetups.Add(meetUpID);
            account.ListofMeetUpID = currentMeetups;
        }

        public void RemoveMeetup(int id, int meetupID)
        {
            Profile account = profileList.Where(a => a.ID == id).FirstOrDefault();
            List<int> currentMeetups = account.ListofMeetUpID.ToList();
            currentMeetups.Remove(meetupID);
            account.ListofMeetUpID = currentMeetups;
        }

        public void UpdateProfileImage(int id, byte[] imageBytes)
        {
            Profile account = profileList.Where(a => a.ID == id).FirstOrDefault();
            account.ProfileImage = imageBytes;
        }
    }
}
