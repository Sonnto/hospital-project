using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hospital_project.Models
{
    public class Department
    {
        [Key]
        public int department_id { get; set; }
        public string department_name { get; set; }

        //A department can have multiple physicians associated with them
        public ICollection<Physician> Physicians{ get; set; }
    }

    public class DepartmentDto
    {
        public int department_id { get; set; }
        public string department_name { get; set; }

        //A department can have multiple physicians associated with them
        public ICollection<Physician> Physicians { get; set; }
    }
}