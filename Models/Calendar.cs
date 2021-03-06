﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WardRobe.Models
{
    public class Calendar
    {
        public int Id { get; set; }

        [Display(Name = "Outfit of the Day")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public string UserId { get; set; }
        public int Wardrobe { get; set; }
        public string ImageUrl { get; set; }
        public string FileName { get; set; }
    }
}
