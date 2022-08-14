using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Data.Models
{
    public class TwoFactorAuthentication
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

        public static bool SendTwoFactor(string emailRecipient, int code, string emailBody)
        {
            emailBody = emailBody.Replace(Tools.EmailTools.InsertTextMarker, code.ToString());
            return Tools.EmailTools.SendEmail("Two Factor Code", new List<string>() {emailRecipient},emailBody);
        }
    }
}
