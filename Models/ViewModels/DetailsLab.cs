using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospital_project.Models.ViewModels
{
    public class DetailsLab
    {
        public LabDto SelectedLab { get; set; }
        public IEnumerable<ProjectDto> RelatedProjects { get; set; }

    }
}