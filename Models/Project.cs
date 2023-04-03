using hospital_project.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hospital_project.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public ICollection<Lab> Lab { get; set; }

        [ForeignKey("Labs")]

        public int LabId { get; set; }

        public virtual Lab Labs { get; set; }
        
    }
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        

        public int LabId { get; set; }
        public string LabName { get; set; }

    }
}