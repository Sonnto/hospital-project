using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hospital_project.Models
{
    public class Physician
    {
        [Key]
        public int physician_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        // a physician can belong to one or more departments
        public ICollection<Department> Departments { get; set; }

    }

    public class PhysicianDto
    {
        public int physician_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        // a physician can belong to one or more departments
        public ICollection<Department> Departments { get; set; }
    }
}