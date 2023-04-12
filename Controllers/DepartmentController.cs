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

            //Objective: communicate with our department data API to retrieve a specific anime
            //curl https://localhost:44383/api/departmentdata/finddepartment/{id}

            string url = "DepartmentData/FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;

            ViewModel.SelectedDepartment = SelectedDepartment;

            //show associated genres with this anime between here

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

        // POST: Physician/Create
        [HttpPost]
        public ActionResult Create(Physician physician)
        {
            // Parses dates from input string

            Debug.WriteLine("The inputted physician name is: ");
            Debug.WriteLine(physician.first_name + physician.last_name);
            //Objective: add a new physician into our system using the API
            //curl -H "Content-Type:application/json" -d anime.json https://localhost:44324/api/physiciandata/addphysician
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

        // GET: Anime/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            UpdatePhysician ViewModel = new UpdatePhysician();

            //the existing physician information

            string url = "PhysicianData/FindPhysician/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PhysicianDto SelectedPhysician = response.Content.ReadAsAsync<PhysicianDto>().Result;

            Debug.WriteLine("PhysicianController.cs: SelectedPhysician's anime_type_id: " + SelectedPhysician.first_name + " " + SelectedPhysician.last_name);
            Debug.WriteLine("PhysicianController.cs: Physician id for edit is: " + id);

            ViewModel.SelectedPhysician = SelectedPhysician;


            // vvvvv THIS PART DOES NOT SEEM TO BE NEEDED vvvvv

            //also include all animeTypes to choose from when updating this anime
            url = "animetypedata/listanimetypes/";
            response = client.GetAsync(url).Result;
            IEnumerable<AnimeTypeDto> AnimeTypesOptions = response.Content.ReadAsAsync<IEnumerable<AnimeTypeDto>>().Result;

            Debug.WriteLine("AnimeController.cs: AnimeTypesOptions: " + AnimeTypesOptions);

            ViewModel.AnimeTypesOptions = AnimeTypesOptions;

            // ^^^^ THIS PART DOES NOT SEEM TO BE NEEDED ^^^^

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