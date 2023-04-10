using hospital_project.Migrations;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hospital_project.Models
{
    public class VolunteerShift
    {
        [Key]
        public int shift_id { get; set; }

        [ForeignKey("Volunteers")]
        public int volunteer_id { get; set; }
        public virtual Volunteer Volunteers { get; set; }

        [ForeignKey("Departments")]
        public int department_id { get; set; }
        public virtual Department Departments { get; set; }

        public string date { get; set; }

    }
}