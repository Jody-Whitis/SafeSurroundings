using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Data.Models
{
    public class MeetUp
    {
        public int ID { get; set; }
        public DateTime MeetTime { get; set; }
        public int PlaceID { get; set; }

        [StringLength(maximumLength: 255)]
        public string PlaceName { get; set; }
        public int PersonID { get; set; }
        protected string detailsValue = string.Empty;

        [StringLength(maximumLength: 255)]
        public string Details
        {
            get
            {
                return detailsValue;
            }
            set
            {
                detailsValue = value;
            }
        }
    }
}
