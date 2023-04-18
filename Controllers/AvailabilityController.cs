using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using hospital_project.Models;
using System.Web.Script.Serialization;
using hospital_project.Models.ViewModels;
using System.Globalization;
using System.Xml.Linq;

namespace hospital_project.Controllers
{
    public class AvailabilityController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();


        static AvailabilityController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }
        // GET: Availability/List
        public ActionResult List()
        {
            //Objective: communicate with our availability data API to retrieve a list of physicians and their availabilities
            //curl https://localhost:44324/api/availabilityData/listavailabilities

            string url = "AvailabilityData/ListAvailabilities";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<AvailabilityDto> availabilities = response.Content.ReadAsAsync<IEnumerable<AvailabilityDto>>().Result;
            Debug.WriteLine("Number of availabilities received: ");
            Debug.WriteLine(availabilities.Count());


            return View(availabilities);
        }

        // GET: Availability/Details/5
   
        public ActionResult Details(int id)
        {
            DetailsAvailability ViewModel = new DetailsAvailability();

            //Objective: communicate with our availabilities data API to retrieve a specific physician's availability
            //curl https://localhost:44383/api/availabilitydata/findavailability/{id}

            string url = "AvailabilityData/FindAvailability/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            AvailabilityDto SelectedAvailability = response.Content.ReadAsAsync<AvailabilityDto>().Result;

            ViewModel.SelectedAvailability = SelectedAvailability;

            /*
            //show associated physicians with this availability between here

            url = "AvailabilityData/ListPhysiciansForAvailability/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<AvailabilityDto> TaggedPhysicians = response.Content.ReadAsAsync<IEnumerable<AvailabilityDto>>().Result;

            ViewModel.TaggedPhysicians = (IEnumerable<PhysicianDto>)TaggedPhysicians;

            url = "PhysicianData/ListPhysiciansNotForDepartment/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<AvailabilityDto> AvailablePhysicians = response.Content.ReadAsAsync<IEnumerable<AvailabilityDto>>().Result;

            ViewModel.AvailablePhysicians = (IEnumerable<PhysicianDto>)AvailablePhysicians;

            */

            return View(ViewModel);
        }
        // vvvv ======== NOT SURE IF Associate AND UnAssociate ACTIONS ARE NEEDED ======== vvvv

        // May not need because you don't associate an availability; it will be manually entered and updated by the user; it will be a string, not an integar usually one capital letter, followed by a lower case letter to denote one of the seven days in any combination: i.e. Mo, Tu, We, Th, Fr, Sa, Su) - therefore, may delete actions that may not be needed

        //POST: Availability/Associate/{physician_id}
        /*
        [HttpPost]

        public ActionResult Associate(int id, int physician_id)
        {
            Debug.WriteLine("Attempting to associate availability: " + id + " with physician: " + physician_id);
            //call API to associate physician with department
            string url = "AvailabilityDaa/AssociatePhysicianWithAvailability/" + id + "/" + department_id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        //Get: Physician/UnAssociate/{id}?department_id={department_id}
        [HttpGet]

        public ActionResult UnAssociate(int id, int department_id)
        {
            Debug.WriteLine("Attempting to unassociate physician: " + id + " with department: " + department_id);
            //call API to unassociate physician with department
            string url = "PhysicianData/UnassociatePhysicianWithDepartment/" + id + "/" + department_id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        } */

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult New()
        {
            //Information about Physicians

            string url = "PhysicianData/ListPhysicians";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<PhysicianDto> PhysicianOptions = response.Content.ReadAsAsync<IEnumerable<PhysicianDto>>().Result;

            return View(PhysicianOptions);
        }

        // ^^^^ ======== NOT SURE IF Associate AND UnAssociate ACTIONS ARE NEEDED ======== ^^^^


        // vvvv ======== NOT SURE IF Create ACTION NEEDED ======== vvvv

        //May not be needed because availability_date is not a pre-set but a string the user can be said and there is no "list of availabilities"

        // POST: Availability/Create
        [HttpPost]
        public ActionResult Create(Availability availability)
        {
            // Parses dates from input string

            Debug.WriteLine("The inputted availability date is: ");
            Debug.WriteLine(availability.availability_dates);
            //Objective: add a new availability date into our system for the physician using the API
            //curl -H "Content-Type:application/json" -d availability.json https://localhost:44324/api/availabilitydata/addavailability
            string url = "AvailabilityData/AddAvailability";

            string jsonpayload = jss.Serialize(availability);

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

        // ^^^^ ======== NOT SURE IF Create ACTION NEEDED ======== ^^^^
   

        // GET: Availability/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            UpdateAvailability ViewModel = new UpdateAvailability();

            //the existing availability information of a physician

            string url = "AvailabilityData/FindAvailability/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AvailabilityDto SelectedAvailability = response.Content.ReadAsAsync<AvailabilityDto>().Result;

            Debug.WriteLine("AvailabilityController.cs: SelectedAvailability's Physician name: " + SelectedAvailability.physician_first_name +" " + SelectedAvailability.physician_last_name);
            Debug.WriteLine("AvailabilityController.cs: Availability id for edit is: " + id);

            ViewModel.SelectedAvailability = SelectedAvailability;
            return View(ViewModel);
        }

        // POST: Availability/Update/5
        [HttpPost]
        public ActionResult Update(int id, Availability availability)
        {
            string url = "AvailabilityData/UpdateAvailability/" + id;
            string jsonpayload = jss.Serialize(availability);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(content);
                return RedirectToAction("List");
            }
            else
            {
                Debug.WriteLine(content);
                return RedirectToAction("Error");
            }
        }

        // GET: Availability/Delete/5

        public ActionResult DeleteConfirm(int id)
        {
            string url = "AvailabilityData/FindAvailability/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AvailabilityDto SelectedAvailability = response.Content.ReadAsAsync<AvailabilityDto>().Result;
            return View(SelectedAvailability);
        }

        // POST: Availability/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "AvailabilityData/DeleteAvailability/" + id;
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
