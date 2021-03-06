﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WardRobe.Models
{
    public class Trip
    {
        public int ID { get; set; }

        [Display(Name = "Trip Name")]
        public string TripName { get; set; }

        [Display(Name = "Start Date")]
        public DateTime Date { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
    }
}
