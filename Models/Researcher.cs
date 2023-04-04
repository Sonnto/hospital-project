using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace hospital_project.Models
{
    public class Researcher
    {
        [Key]
        public int ResearcherId { get; set; }

        public string ResearcherName { get; set; }

     
        public bool ResearchLeader { get; set; }

        //Hi Christine, I know the ForeignKey should be Single as Project and public virtual should also be single
        //But after I update and correct them, it keeps have bugs

        [ForeignKey("Projects")]

        public int ProjectId { get; set; }

        public virtual Project Projects { get; set; }

    }
    public class ResearcherDto
    {
        public int ResearcherId { get; set; }

        public string ResearcherName { get; set; }

        public int ProjectId { get; set; }
        public int ProjectName { get; set; }
        public bool ResearchLeader { get; set; }
    }
}