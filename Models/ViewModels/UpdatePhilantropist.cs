using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace hospital_project.Models.ViewModels
{
    public class UpdatePhilantropist
    {

        // the existing Philantropist information
        public PhilantropistDto SelectedPhilantropist { get; set; }

        //All Donation levels to choose from when updating this philantropist
        public IEnumerable<DonationDto> DonationOptions { get; set; }
    }
}