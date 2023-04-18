using hospital_project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using hospital_project.Models.ViewModels;


namespace hospital_project.Controllers
{
    public class DonationController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DonationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }
        // GET: Donation/List
        public ActionResult List()
        {
            //objective: communicate with our Donation data api to retrieve a list of Donations
            


            string url = "donationdata/listdonations";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<DonationDto> donations = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;
            //Debug.WriteLine("Number of Donations received : ");
            //Debug.WriteLine(Donations.Count());


            return View(donations);
        }

        // GET: Donation/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our Donation data api to retrieve one Donation

            DetailsDonation ViewModel = new DetailsDonation();

            string url = "donationdata/finddonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
            Debug.WriteLine("Donation received : ");
            Debug.WriteLine(SelectedDonation.DonationID);

            ViewModel.SelectedDonation = SelectedDonation;

            //showcase information about philantropists related to this Donation
            //send a request to gather information about philantropists related to a particular Donation ID
            url = "philantropistdata/listphilantropistsfordonation/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<PhilantropistDto> RelatedPhilantropists = response.Content.ReadAsAsync<IEnumerable<PhilantropistDto>>().Result;

            ViewModel.RelatedPhilantropists = RelatedPhilantropists;


            return View(ViewModel);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Donation/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Donation/Create
        [HttpPost]
        public ActionResult Create(Donation Donation)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(Donation.DonationLevel);
            //objective: add a new Donation into our system using the API
            string url = "donationdata/adddonation";


            string jsonpayload = jss.Serialize(Donation);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }


        // GET: Donation/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "donationdata/finddonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
            return View(SelectedDonation);
        }

        // POST: Donation/Update/5
        [HttpPost]
        public ActionResult Update(int id, Donation Donation)
        {

            string url = "donationdata/updatedonation/" + id;
            string jsonpayload = jss.Serialize(Donation);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Donation/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "donationdata/finddonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
            return View(SelectedDonation);
        }

        // POST: Donation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "donationdata/deletedonation/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}