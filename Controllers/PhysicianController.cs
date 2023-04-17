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
    public class PhysicianController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();


        static PhysicianController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }
        // GET: Physician/List
        public ActionResult List()
        {
            //Objective: communicate with our physician data API to retrieve a list of physicians
            //curl https://localhost:44324/api/physicianedata/listphysicians

            string url = "PhysicianData/ListPhysicians";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<PhysicianDto> physicians = response.Content.ReadAsAsync<IEnumerable<PhysicianDto>>().Result;
            Debug.WriteLine("Number of physicians received: ");
            Debug.WriteLine(physicians.Count());


            return View(physicians);
        }

        // GET: Physician/Details/5
        public ActionResult Details(int id)
        {
            DetailsPhysician ViewModel = new DetailsPhysician();

            //Objective: communicate with our physician data API to retrieve a specific physician
            //curl https://localhost:44324/api/physiciandata/findphysician/{id}

            string url = "PhysicianData/FindPhysician/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            PhysicianDto SelectedPhysician = response.Content.ReadAsAsync<PhysicianDto>().Result;

            ViewModel.SelectedPhysician = SelectedPhysician;

            //show associated departments with this physician between here

            url = "DepartmentData/ListDepartmentsForPhysician/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> TaggedDepartments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;

            ViewModel.TaggedDepartments = TaggedDepartments;

            url = "DepartmentData/ListDepartmentsNotForPhysician/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> AvailableDepartments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;

            ViewModel.AvailableDepartments = AvailableDepartments;

            return View(ViewModel);
        }

        //POST: Physician/Associate/{physician_id}
        [HttpPost]

        public ActionResult Associate(int id, int department_id)
        {
            Debug.WriteLine("Attempting to associate physician: " + id + " with department: " + department_id);
            //call API to associate physician with department
            string url = "PhysicianData/AssociatePhysicianWithDepartment/" + id + "/" + department_id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        //Get: Physician/Unassociate/{id}?department_id={department_id}
        [HttpGet]

        public ActionResult Unassociate(int id, int department_id)
        {
            Debug.WriteLine("Attempting to unassociate physician: " + id + " with department: " + department_id);
            //call API to unassociate physician with department
            string url = "PhysicianData/UnassociatePhysicianWithDepartment/" + id + "/" + department_id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        // POST: Physician/Create
        [HttpPost]
        public ActionResult Create(Physician physician)
        {
            // Parses dates from input string

            Debug.WriteLine("The inputted physician name is: ");
            Debug.WriteLine(physician.first_name + physician.last_name);
            //Objective: add a new physician into our system using the API
            //curl -H "Content-Type:application/json" -d physician.json https://localhost:44324/api/physiciandata/addphysician
            string url = "PhysicianData/AddPhysician";

            string jsonpayload = jss.Serialize(physician);

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

        // GET: Physician/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            UpdatePhysician ViewModel = new UpdatePhysician();

            //the existing physician information

            string url = "PhysicianData/FindPhysician/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PhysicianDto SelectedPhysician = response.Content.ReadAsAsync<PhysicianDto>().Result;

            Debug.WriteLine("PhysicianController.cs: SelectedPhysician's full name: " + SelectedPhysician.first_name +" " + SelectedPhysician.last_name);
            Debug.WriteLine("PhysicianController.cs: Physician id for edit is: " + id);

            ViewModel.SelectedPhysician = SelectedPhysician;
            return View(ViewModel);
        }

        // POST: Physician/Update/5
        [HttpPost]
        public ActionResult Update(int id, Physician physician)
        {
            string url = "PhysicianData/UpdatePhysician/" + id;
            string jsonpayload = jss.Serialize(physician);
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

        // GET: Physician/Delete/5

        public ActionResult DeleteConfirm(int id)
        {
            string url = "PhysicianData/FindPhysician/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PhysicianDto SelectedPhysician = response.Content.ReadAsAsync<PhysicianDto>().Result;
            return View(SelectedPhysician);
        }

        // POST: Physician/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "PhysicianData/DeletePhysician/" + id;
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
