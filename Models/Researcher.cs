using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hospital_project.Models
{
    public class Researcher
    {
        [Key]
        public int ResearcherId { get; set; }

        public string ResearcherName { get; set; }

        public int ProjectId { get; set; }

        public bool ResearchLeader { get; set; }


    }
}