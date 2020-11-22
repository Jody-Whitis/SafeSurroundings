using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafeSurroundings.Models
{
    public class HomeViewModel
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
        public string DisplayName { get; set; }

    }
}