using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospital_project.Models.ViewModels
{
    public class DetailsPhilantropist
    {

        public PhilantropistDto SelectedPhilantropist { get; set; }

        public DonationDto DonationForPhilantropist { get; set; }

        public IEnumerable<DepartmentDto> DonatedDepartments { get; set; }

        public IEnumerable<DepartmentDto> AvailableDepartments { get; set; }
    }
}