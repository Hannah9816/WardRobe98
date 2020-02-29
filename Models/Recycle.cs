using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WardRobe.Models
{
    public class Recycle
    {
        public int ID { get; set; }
        public string Name { get; set;}
        public string Type { get; set; }
        public string PhoneNo { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }

        [Display(Name = "Logo")]
        public string ImageUrl { get; set; }
        public string FileName { get; set; }
    }
}
