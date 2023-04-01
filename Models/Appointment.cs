using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hospital_project.Models
{
    public class Appointment
    {
        [Key]
        public int appointment_id { get; set; }
        public string appointment_name { get; set; }
        public string appointment_date { get; set; }
    }
}