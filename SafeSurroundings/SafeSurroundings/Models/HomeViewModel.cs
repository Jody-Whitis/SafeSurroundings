using SafeSurroundings.Data.Models;
using SafeSurroundings.Services;
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
        public IEnumerable<MeetUp> ListofMeetups{get;set;}

        public string FormatDate(DateTime meetDate)
        {
            try
            {
                return meetDate.ToString("MM/dd/yyyy");
            }
            catch
            {
                return "00/00/0000";
            }
        }

        public string FormatTime(DateTime meetTime)
        {
            try
            {
                return meetTime.ToShortTimeString();
            }
            catch
            {
                return "00:00";
            }
        }
       
    }
}