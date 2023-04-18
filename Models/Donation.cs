using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace hospital_project.Models
{
    public class Donation
    {
        [Key]
        public int DonationID { get; set; }
        public string DonationLevel { get; set; }
        public int MinAmount { get; set; }
    }

    public class DonationDto
    {
        public int DonationID { get; set; }

        public string DonationLevel { get; set; }

        public int MinAmount { get; set; }
    }
}