using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospital_project.Models.ViewModels
{
    public class DetailsPhysician
    {
        public PhysicianDto SelectedPhysician { get; set; }
        public IEnumerable<DepartmentDto> TaggedDepartments { get; set; }
        public IEnumerable<DepartmentDto> AvailableDepartments { get; set; }
    }
}