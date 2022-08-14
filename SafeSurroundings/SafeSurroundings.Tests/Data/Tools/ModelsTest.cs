using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Tests.Data.Tools
{
    [TestClass]
    public class TwoFactorAuthenticationTests : SafeSurroundings.Data.Models.TwoFactorAuthenication
    {
        [TestMethod]
        public void GetTwoFactor()
        {
            int twoFactorCode = GetTwoFactorCode();

            Console.WriteLine($"Two Factor Code: {twoFactorCode}");

            Assert.IsTrue(twoFactorCode > 0);
        }
    }
}
