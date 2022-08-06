using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeSurroundings.Data.Tools;

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
    }
}
