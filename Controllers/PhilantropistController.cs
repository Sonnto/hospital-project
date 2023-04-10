using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using hospital_project.Models;
using System.Web.Script.Serialization;

namespace hospital_project.Controllers
{
    public class PhilantropistController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();


        static PhilantropistController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/philantropistdata/");
        }   
        
        
        // GET: Philantropist/List
        public ActionResult List()
        {
            //curl https://localhost:44324/api/philantropistdata/listphilantropists

            
            string url = "listphilantropists";
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

            //curl https://localhost:44324/api/philantropistdata/findphilantropists/{id}

            string url = "findphilantropist/" +id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            PhilantropistDto selectedphilantropist = response.Content.ReadAsAsync<PhilantropistDto>().Result;
            //Debug.WriteLine("Philantropist recieved : ");
            //Debug.WriteLine(selectedphilantropist.PhilantropistFirstName);
            return View(selectedphilantropist);
        }



        public ActionResult Error()
        {
            return View();
        }



        // GET: Philantropist/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Philantropist/Create
        [HttpPost]
        public ActionResult Create(Philantropist philantropist)
        {

            string url = "addphilantropist";

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
            return View();
        }

        // POST: Philantropist/Edit/5
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

        // GET: Philantropist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Philantropist/Delete/5
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
