using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using hospital_project.Models;
using System.Web.Script.Serialization;
using hospital_project.Models.ViewModels;


namespace hospital_project.Controllers
{
    public class PhilantropistController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();


        static PhilantropistController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }   
        
        
        // GET: Philantropist/List
        public ActionResult List()
        {
            //curl https://localhost:44324/api/philantropistdata/listphilantropists

            
            string url = "philantropistdata/listphilantropists";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<PhilantropistDto> philantropists = response.Content.ReadAsAsync<IEnumerable<PhilantropistDto>>().Result;
            //Debug.WriteLine("Number of philantropists recieved : ");
            //Debug.WriteLine(philantropists.Count());

            return View(philantropists);
        }

        // GET: Philantropist/Details/5
        public ActionResult Details(int id)
        {
            DetailsPhilantropist ViewModel = new DetailsPhilantropist();

            //curl https://localhost:44324/api/philantropistdata/findphilantropists/{id}

            string url = "philantropistdata/findphilantropist/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            PhilantropistDto SelectedPhilantropist = response.Content.ReadAsAsync<PhilantropistDto>().Result;
            //Debug.WriteLine("Philantropist recieved : ");
            //Debug.WriteLine(selectedphilantropist.PhilantropistFirstName);

            ViewModel.SelectedPhilantropist = SelectedPhilantropist;

            //show associated departments with this philantropist
            url = "departmentdata/listdepartmentsforphilantropist/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> DonatedDepartments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;

            ViewModel.DonatedDepartments = DonatedDepartments; 

            url = "departmentdata/listdepartmentsnotdonatedtobyphilantropist/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> AvailableDepartments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;

            ViewModel.AvailableDepartments = AvailableDepartments;


            return View(ViewModel);

        }


        //POST: Philantropist/Associate/{philantropistid}
        [HttpPost]
        public ActionResult Associate(int id, int DepartmentID)
        {
            Debug.WriteLine("Attempting to associate Philantropist :" + id + " with Department " + DepartmentID);

            //call our api to associate Philantropist with department
            string url = "philantropistdata/associatephilantropistwithdepartment/" + id + "/" + DepartmentID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }


        //Get: Philantropist/UnAssociate/{id}?DepartmentID={DepartmentID}
        [HttpGet]
        public ActionResult UnAssociate(int id, int DepartmentID)
        {
            Debug.WriteLine("Attempting to unassociate Philantropist :" + id + " with Department: " + DepartmentID);

            //call our api to unassociate Philantropist with Department
            string url = "philantropistdata/unassociatephilantropistwithdepartment/" + id + "/" + DepartmentID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }


        public ActionResult Error()
        {
            return View();
        }



        // GET: Philantropist/New
        public ActionResult New()
        {
            //information about all donations in the system.
            //GET api/donationdata/listdonations

            string url = "donationdata/listdonations";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DonationDto> DonationOptions = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;


            return View(DonationOptions);
        }

        // POST: Philantropist/Create
        [HttpPost]
        public ActionResult Create(Philantropist philantropist)
        {

            string url = "philantropistdata/addphilantropist";

            string jsonpayload = jss.Serialize(philantropist);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
           
        }

        // GET: Philantropist/Edit/5
        public ActionResult Edit(int id)
        {
            UpdatePhilantropist ViewModel = new UpdatePhilantropist();

            //the existing Philantropist information
            string url = "philantropistdata/findphilantropist/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PhilantropistDto SelectedPhilantropist = response.Content.ReadAsAsync<PhilantropistDto>().Result;

            ViewModel.SelectedPhilantropist = SelectedPhilantropist;

            // all donation levels to choose from when updating this Philantropist
            //the existing donation information
            url = "donationdata/listdonations/";
            response = client.GetAsync(url).Result;
            IEnumerable<DonationDto> DonationOptions = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;

            ViewModel.DonationOptions = DonationOptions;

            return View(ViewModel);
        }

        // POST: Philantropist/Update/5
        [HttpPost]
        public ActionResult Update(int id, Philantropist philantropist)
        {
            string url = "philantropistdata/updatephilantropist/" + id;
            string jsonpayload = jss.Serialize(philantropist);
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

        // GET: Philantropist/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "philantropistdata/findphilantropist/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PhilantropistDto SelectedPhilantropist = response.Content.ReadAsAsync<PhilantropistDto>().Result;
            return View(SelectedPhilantropist);
        }

        // POST: Philantropist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Philantropist philantropist)
        {
            string url = "philantropistdata/deletephilantropist/" + id;
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
