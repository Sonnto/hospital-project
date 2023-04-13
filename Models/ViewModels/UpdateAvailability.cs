using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospital_project.Models.ViewModels
{
    public class UpdateAvailability
    {
        //This viewmodel is a class which stores info that we need to present to /Availability/Update/{id}

        //the existing physician availability information

        public PhysicianDto SelectedPhysician { get; set; }
    }
}