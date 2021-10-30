using SafeSurroundings.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace SafeSurroundings.Models
{
    public class ProfileViewModel
    {
        public Profile UserProfile { get; set; }
        public int ProfileID { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public Boolean IsTwoFactor { get; set; }
        public Boolean IsSubscribed { get; set; }
        public DateTime LastLogin { get; set; }
        public string LastLoginDevice { get; set; }
        public Image profileImage { get; set; }
        public Byte[] profileImageBytes { get; set; }
        public string ConvertBooleantoDisplay(Boolean Flag)
        {
            string displayFlag = string.Empty;
            displayFlag = Flag == true ? "Yes" : "No";
            return displayFlag;
        } 
    }

}