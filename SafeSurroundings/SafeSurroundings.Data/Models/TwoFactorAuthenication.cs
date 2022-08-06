using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Data.Models
{
    public class TwoFactorAuthenication
    {

        public static int GetTwoFactorCode()
        {
            int twoFactorCode = 0;
            try
            {
                Random generatingCode = new Random();
                twoFactorCode = generatingCode.Next(1000,9999);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return twoFactorCode;
        }
    }
}
