using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospital_project.Models.ViewModels
{
    public class DetailsAvailability
    {
        public DepartmentDto SelectedAvailability { get; set; }
        public IEnumerable<PhysicianDto> TaggedPhysicians { get; set; }
        public IEnumerable<PhysicianDto> AvailablePhysicians { get; set; }
    }
}