using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Data.Models
{
    public class Place
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public double X_Coordinates { get; set; }
        public double Y_Coordinates { get; set; }
        public DateTime OpenHour { get; set; }
        public DateTime CloseHour { get; set; }
        public Byte[] PlacePicture { get; set; }
    }
}
