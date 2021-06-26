using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Data.Models
{
    public class Profile
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime LastLogin { get; set; }
        public Boolean IsLocked { get; set; }
        public string LastLoginDevice { get; set; }
        public Boolean IsSubscribed { get; set; }
        public Boolean IsTwoFactor { get; set; }
        public IEnumerable<int> ListofMeetUpID { get; set; }
    }
}
