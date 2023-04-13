using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospital_project.Models.ViewModels
{
    public class UpdateDepartment
    {
        //This viewmodel is a class which stores info that we need to present to /Department/Update/{id}

        //the existing department information

        public DepartmentDto SelectedDepartment { get; set; }
    }
}