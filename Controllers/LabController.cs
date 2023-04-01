using hospital_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using hospital_project.Models.ViewModels;
using System.Web.UI.WebControls;

namespace hospital_project.Controllers
{
    public class LabController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static LabController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }
        // GET: Lab/List
        public ActionResult List()
        {

            string url = "labdata/ListLabs";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<LabDto> Labs = response.Content.ReadAsAsync<IEnumerable<LabDto>>().Result;
            return View(Labs);
        }

        // GET: Lab/Details/5
        public ActionResult Details(int id)
        {
            DetailsLab ViewModel = new DetailsLab();

            string url = "labdata/findlab/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            LabDto SelectedLab = response.Content.ReadAsAsync<LabDto>().Result;
            ViewModel.SelectedLab = SelectedLab;


            return View(ViewModel);
        }

        // GET: Lab/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lab/Create
        [HttpPost]
        public ActionResult Create(Lab lab)
        {

            string url = "labdata/addlab";
            string jsonpayload = jss.Serialize(lab);
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

        // GET: Lab/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateLab ViewModel = new UpdateLab();
            string url = "labdata/findlab/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            LabDto SelectedLab = response.Content.ReadAsAsync<LabDto>().Result;

            ViewModel.SelectedLab = SelectedLab;


            return View(ViewModel);
        }

        // POST: Lab/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Lab lab)
        {
            string url = "labdata/updatelab/" + id;
            string jsonpayload = jss.Serialize(lab);
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

        // GET: Lab/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "labdata/findlab/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            LabDto selectedlab = response.Content.ReadAsAsync<LabDto>().Result;
            return View(selectedlab);
        }

        // POST: Lab/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "labdata/deletelab/" + id;
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
