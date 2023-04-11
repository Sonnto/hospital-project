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
        
        public datetime appointment_date { get; set; }
        
        [ForeignKey("Patient")]
        
        public int patient_id { get; set; }
        
        [ForeignKey("Physician")]
        
        public int physician_id { get; set; }
    }
    
    public class AppointmentDto
    {

        public int appointment_id { get; set; }
        
        public string appointment_name { get; set; }
        
        public datetime appointment_date { get; set; }
        
        public int patient_id { get; set; }
        
        public int physician_id { get; set; }
    }
    
    
}
