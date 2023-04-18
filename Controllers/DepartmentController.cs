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
using hospital_project.Migrations;

namespace hospital_project.Controllers
{
    public class DepartmentController : Controller
    {


        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();


        static DepartmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }




        // GET: Department/List
        public ActionResult List()
        {
            //objective: communicate with our Department data api to retrieve a list of Departments


            string url = "departmentdata/listdepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<DepartmentDto> Departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            //Debug.WriteLine("Number of Departments received : ");
            //Debug.WriteLine(Departments.Count());


            return View(Departments);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            DetailsDepartment ViewModel = new DetailsDepartment();

            //objective: communicate with our Department data api to retrieve one Department

            string url = "departmentdata/findDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            Debug.WriteLine("Keeper received : ");
            Debug.WriteLine(SelectedDepartment.department_name);

            ViewModel.SelectedDepartment = SelectedDepartment;

            //show all philantropists that have donated to the department
            url = "philantropistdata/listphilantropistsfordepartment/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<PhilantropistDto> PhilantropistsWhoDonated = response.Content.ReadAsAsync<IEnumerable<PhilantropistDto>>().Result;

            ViewModel.PhilantropistsWhoDonated = PhilantropistsWhoDonated;


            return View(ViewModel);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Department/New
        public ActionResult New()
        {
            return View();
        }


        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(Department Department)
        {
            Debug.WriteLine("the json payload is :");
            string url = "departmentdata/adddepartment";


            string jsonpayload = jss.Serialize(Department);
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
        public ActionResult Edit(int id)
        {
            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            return View(SelectedDepartment);
        }

        // POST: Department/Update/5
        [HttpPost]
        public ActionResult Update(int id, Department Department)
        {
            string url = "Departmentdata/updatedepartment/" + id;
            string jsonpayload = jss.Serialize(Department);
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

        // GET: Department/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            return View(SelectedDepartment);
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "departmentdata/deletedepartment/" + id;
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
