using hospital_project.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hospital_project.Models
{
    public class Availability
    {
        [Key]
        public int availability_id { get; set; }

        [ForeignKey("Physicians")]
        public int physician_id { get; set; }
        public virtual Physician Physicians { get; set; }
        [ForeignKey("Departments")]
        public int department_id { get; set; }
        public virtual Department Departments { get; set; }
        public string availability_dates { get; set; }

    }
}