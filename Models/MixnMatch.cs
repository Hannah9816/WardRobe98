using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WardRobe.Models
{
    public class MixnMatch
    {
        public int ID { get; set; }
        public string Top { get; set; }
        public string Bottom { get; set; }
        public string UserId { get; set; }

        public string TopUrl { get; set; }
        public string TopFile { get; set; }
        public string BottomUrl { get; set; }
        public string BottomFile { get; set; }
    }
}
