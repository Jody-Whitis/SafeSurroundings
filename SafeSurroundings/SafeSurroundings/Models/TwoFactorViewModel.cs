using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SafeSurroundings.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace SafeSurroundings.Models
{
    public class TwoFactorViewModel
    {
        [IntegerValidator(MinValue = 1000, MaxValue = 9999)]
        public int TwoFactorCode { get; set; }
    }
}