using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using hospital_project.Models;
using hospital_project.Models.ViewModels;

namespace hospital_project.Controllers
{
    public class DepartmentController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();


        static DepartmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }
        // GET: Department/List
        public ActionResult List()
        {
            //Objective: communicate with our department data API to retrieve a list of departments
            //curl https://localhost:44324/api/departmentdata/listdepartments

            string url = "DepartmentData/ListDepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<Models.DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            Debug.WriteLine("Number of departments received: ");
            Debug.WriteLine(departments.Count());


            return View(departments);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            DetailsDepartment ViewModel = new DetailsDepartment();

            //Objective: communicate with our department data API to retrieve a specific department
            //curl https://localhost:44383/api/departmentdata/finddepartment/{id}

            string url = "DepartmentData/FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;

            ViewModel.SelectedDepartment = SelectedDepartment;

            //show associated physicians with this department between here

            url = "PhysicianData/ListPhysiciansForDepartment/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<PhysicianDto> TaggedPhysicians = response.Content.ReadAsAsync<IEnumerable<PhysicianDto>>().Result;

            ViewModel.TaggedPhysicians = TaggedPhysicians;

            url = "PhysicianData/ListPhysiciansNotForDepartment/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<PhysicianDto> AvailablePhysicians = response.Content.ReadAsAsync<IEnumerable<PhysicianDto>>().Result;

            ViewModel.AvailablePhysicians = AvailablePhysicians;

            return View(ViewModel);
        }
        //POST: Department/Associate/{department_id}
        [HttpPost]

        public ActionResult Associate(int id, int physician_id)
        {
            Debug.WriteLine("Attempting to associate department: " + id + " with physician: " + physician_id);
            //call API to associate department with physician
            string url = "DepartmentData/AssociateDepartmentWithPhysician/" + id + "/" + physician_id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        //Get: Department/UnAssociate/{id}?physician_id={physician_id}
        [HttpGet]

        public ActionResult UnAssociate(int id, int physician_id)
        {
            Debug.WriteLine("Attempting to unassociate department: " + id + " with physician: " + physician_id);
            //call API to unassociate department with physician
            string url = "DepartmentData/UnassociateDepartmentWithPhysician/" + id + "/" + physician_id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        public ActionResult Error()
        {
            return View();
        }

        //HEREEEE CONTINUEEE HEREEEEEEEE

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(Department department)
        {
            // Parses dates from input string

            Debug.WriteLine("The inputted department name is: ");
            Debug.WriteLine(department.department_name);
            //Objective: add a new department into our system using the API
            //curl -H "Content-Type:application/json" -d department.json https://localhost:44324/api/departmentdata/adddepartment
            string url = "DepartmentData/AddDepartment";

            string jsonpayload = jss.Serialize(department);

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

        // GET: Department/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            UpdateDepartment ViewModel = new UpdateDepartment();

            //the existing department information

            string url = "DepartmentData/FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;

            Debug.WriteLine("DepartmentController.cs: SelectedDepartment's department name: " + SelectedDepartment.department_name);
            Debug.WriteLine("DepartmentController.cs: Department id for edit is: " + id);

            ViewModel.SelectedDepartment = SelectedDepartment;
            return View(ViewModel);
        }

        // POST: Department/Update/5
        [HttpPost]
        public ActionResult Update(int id, Department department)
        {
            string url = "DepartmentData/UpdateDepartment/" + id;
            string jsonpayload = jss.Serialize(department);
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

        // GET: Department/Delete/5

        public ActionResult DeleteConfirm(int id)
        {
            string url = "DepartmentData/FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PhysicianDto SelectedPhysician = response.Content.ReadAsAsync<PhysicianDto>().Result;
            return View(SelectedPhysician);
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "DepartmentData/DeleteDepartment/" + id;
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