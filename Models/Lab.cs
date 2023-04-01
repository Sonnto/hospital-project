using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hospital_project.Models
{
    public class Lab
    {
        [Key]
        public int LabId { get; set; }
        public string LabName { get; set; }
    }

    public class LabDto
    {
        public int LabId { get; set; }
        public string LabName { get; set; }
    }
}