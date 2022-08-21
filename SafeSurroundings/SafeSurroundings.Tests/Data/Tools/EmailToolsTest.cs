using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeSurroundings.Data.Tools;
using SafeSurroundings.Data.Models;

namespace SafeSurroundings.Tests.Data.Tools
{
    [TestClass]
    public class EmailToolsTest : EmailTools
    {         
        /// <summary>
        /// Set email recipient for testing.
        /// </summary>
        /// <param name="emailBody"></param>
        /// <param name="emailRecipient"></param>
        [DataTestMethod()]
        [DataRow("projtestcred@gmail.com","<table style='align-content:center; color:navy; font-size:20px; width:50%; border: 5px solid navy'><tr style='text-align:center'><td>1 row </td></tr></table>")]
        [DataRow("projtestcred@gmail.com", "<table style='align-content:center; color:navy; font-size:20px; width:50%; border: 5px solid navy'><tr style='text-align:center'><td>2 row </td></tr><tr style='text-align:center'><td>2 row </td></tr></table>")]
        [TestMethod]
        public void TestEmailSend(string emailRecipient,string emailBody)
        {
            bool isSent = SendEmail("Test", new List<string>() { emailRecipient }, emailBody);
            Assert.IsTrue(isSent);
        }


        [DataTestMethod()]
        [DataRow("projtestcred@gmail.com", "<table style='align-content:center; border-collapse:collapse; color:navy; font-size:20px; width:50%; border: 5px solid navy;margin:0px'>$</table>","$")]
        [TestMethod]
        public void TestSendRemainder(string emailRecipient, string emailTemplate,string placeHolder)
        {
            List<MeetUp> listOfMeetups = new List<MeetUp>();
            listOfMeetups.Add(new MeetUp { PlaceName = "Starbucks", MeetTime = DateTime.Now, Details = "Unit Testing" } );
            listOfMeetups.Add(new MeetUp { PlaceName = "Secret Spot", MeetTime = DateTime.Now.AddDays(1), Details = "Secret Meeting" });

            string emailBody = BuildRemindersText(listOfMeetups);
            emailTemplate = emailTemplate.Replace(placeHolder, emailBody);

            bool isSent = SendEmail("Test", new List<string>() { emailRecipient }, emailTemplate);

            Assert.IsTrue(isSent);          
        }
    }
}
