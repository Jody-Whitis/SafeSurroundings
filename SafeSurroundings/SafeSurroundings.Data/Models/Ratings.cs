using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Data.Models.Ratings
{
    public static class Ratings
    {
        public enum Rating: short
        {
            Awesome = 5,
            Great = 4,
            Ok = 3,
            Not_Good = 2,
            Terrible = 1
        }

        public enum Safety : short
        {   
            Super_Safe = 5,
            Pretty_Safe = 4,
            Ok = 3,
            Risky = 2,
            UnSafe = 1
        }
    }
}
