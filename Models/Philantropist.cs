using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_project.Models
{
    public class Philantropist
    {
        [Key]
        public int PhilantropistID { get; set; }
        public string PhilantropistFirstName { get; set; }
        public string PhilantropistLastName { get; set; }
        public string Company { get; set; }


        //A philantropist belongs to one donation tier
        //A donation tier can have many philantropists
        [ForeignKey("Departments")]
        public int DonationID { get; set; }
        public virtual Donation Donations { get; set; }
        
        //A philantropist can donate to many departments 
        public ICollection<Department> Departments { get; set; }

    }

    public class PhilantropistDto
    {
        public int PhilantropistID { get; set; }
        public string PhilantropistFirstName { get; set; }
        public string PhilantropistLastName { get; set; }
        public string Company { get; set; }
        public int DonationID { get; set; }

    }
}