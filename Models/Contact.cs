using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WardRobe.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        public string PhoneNo { get; set; }
        public string Subject { get; set; }

        [Display(Name = "Messages & Content")]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
