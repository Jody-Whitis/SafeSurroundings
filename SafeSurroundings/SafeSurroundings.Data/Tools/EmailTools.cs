using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using SafeSurroundings.Data.Models;

namespace SafeSurroundings.Data.Tools
{
    public class EmailTools
    {
        public static readonly string InsertTextMarker = "***insert***";
        protected static readonly string EmailTemplate = @"EmailTemplates\EmailTemplate.html";
 
        /// <summary>
        /// Pass a subject, list of recipients, and email body string.
        /// </summary>
        /// <param name="emailSubject"></param>
        /// <param name="recipientList"></param>
        /// <param name="emailBody"></param>
        /// <returns></returns>
        public static Boolean SendEmail(string emailSubject, List<string> recipientList, string emailBody)
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage mailMessage = new MailMessage();           
            Boolean isSent = false;

            try
            {                
                smtp.UseDefaultCredentials = Properties.Settings.Default.emailDefaultCreditials;
                smtp.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.emailCreditials,Properties.Settings.Default.passEmailCreditials);
                smtp.Port = Properties.Settings.Default.emailPort;
                smtp.EnableSsl = true;
                smtp.Host = Properties.Settings.Default.emailServer;

                mailMessage.Sender = new MailAddress(Properties.Settings.Default.senderEmail);
                mailMessage.From = new MailAddress(Properties.Settings.Default.emailSender);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = emailSubject;

                foreach(string sender in recipientList)
                {
                    mailMessage.To.Add(sender);
                }
                 
                mailMessage.Body = emailBody;
                
                smtp.Send(mailMessage);
                isSent = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                isSent = false;
            }
            return isSent;
        }

        #region EmailBodyBuilding
        public static string BuildRemindersText(IEnumerable<MeetUp> meetupsToSend)
        {
            StringBuilder meetupRows = new StringBuilder(); 
            
            meetupRows.Append("<tr style='border-bottom:1px solid navy'>");
            meetupRows.Append("<td style='border-bottom:1px solid navy;'>");
            meetupRows.Append("</td>");
            meetupRows.Append("<td style='text-align:center;border-bottom:1px solid navy;padding:0px'>");
            meetupRows.Append("<b>MeetUp Remainders</b>");
            meetupRows.Append("</td>");
            meetupRows.Append("<td style='border-bottom:1px solid navy;'>");
            meetupRows.Append("</td>");
            meetupRows.Append("</tr>");

            foreach (MeetUp meetupRecord in meetupsToSend)
            {
                meetupRows.Append("<tr style='border-bottom:1px solid navy'>");
                meetupRows.Append("<td style='text-align:center;border-right:1px solid navy;padding:0px;border-bottom:1px solid navy;padding:0px'>");
                meetupRows.Append(meetupRecord.PlaceName);
                meetupRows.Append("</td>");
                meetupRows.Append("<td style='text-align:center;border-right:1px solid navy;padding:0px;border-bottom:1px solid navy;padding:0px'>");
                meetupRows.Append(meetupRecord.MeetTime);
                meetupRows.Append("</td>");
                meetupRows.Append("<td style='text-align:center;border-bottom:1px solid navy;padding:0px'>");
                meetupRows.Append(meetupRecord.Details);
                meetupRows.Append("</td>");
                meetupRows.Append("</tr>");
            }
            return meetupRows.ToString();
        }
        #endregion
    }
}
