﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospital_project.Models.ViewModels
{
    public class UpdatePhysician
    {
        //This viewmodel is a class which stores info that we need to present to /Physician/Update/{id}

        //the existing physician information

        public PhysicianDto SelectedPhysician { get; set; }
    }
}