using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using hospital_project.Models;
using hospital_project.Models.ViewModels;
using System.Web.Script.Serialization;


namespace hospital_project.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient/List
       
        public ActionResult List()
        {
            //objective: communicate with patient data api to retrieve a list of patients
            //curl https:// api/patientdata/listpatients

            string url = "patientdata/listpatients";
            HttpResponseMessage response = Client.GetAsync(url).Result;

            IEnumerable<PatientDto> patients = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;
            return View(Patients);
        }

        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
            DetailsPatient ViewModel = new DetailsPatient();
            //objective: communicate with patient data api to retrieve a list of patients
            //curl https:// api/patientdata/findpatient/{id}

            string url = "patientdata/findpatient/" + id;
            HttpResponseMessage response = Client.GetAsync(Url).Result;

            PatientDto SelectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;

            ViewModel.SelectedPatient = SelectedPatient;

            return View(ViewModel);
        }

        //POST: Patient/Associate/{patientid}
        [HttpPost]
        [Authorize]
        public ActionResult(int id, int AppointmentID)
        {
            return RedirectToAction("Details/" + id);
        }

        // GET: Patient/New
        [Authorize]

        public ActionResult New()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
