using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace hospital_project.Models
{
    public class Patient
    {
        [Key]
        public int patient_id { get; set; }

        public int healthcard_id { get; set; }
        public string patient_fname { get; set; }
        public string patient_surname { get; set; }

        public DateTime patient_birthday { get; set; }

        public string patient_phoneNum { get; set; }

        public string patient_condition { get; set; }

        [ForeignKey("Physician")]
        
        public int primary_physician_id { get; set; }
    }
        public class PatientDto { 
        
        public int patient_id { get; set; }

        public int healthcard_id { get; set; }

        public string patient_fname { get; set; }

        public string patient_surname { get; set; }

        public DateTime patient_birthday { get; set; }

        public string patient_phoneNum { get; set; }

        public string patient_condition { get; set; }

        public int primary_physician_id { get; set; }
    }
}
