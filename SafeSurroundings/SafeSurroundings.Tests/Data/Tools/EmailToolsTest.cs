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
        [TestMethod]
        public void TestEmailSend()
        {
            bool isSent = SendEmail("Test", new List<string>() { "jodywhitis0407@gmail.com" });
            Assert.IsTrue(isSent);
        }
    }
}
