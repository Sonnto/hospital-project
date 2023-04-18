using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace hospital_project.Models.ViewModels
{
    public class DetailsDonation
    {

        public DonationDto SelectedDonation { get; set; }

        public IEnumerable<PhilantropistDto> RelatedPhilantropists { get; set; }


    }
}