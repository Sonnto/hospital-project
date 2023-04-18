using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using hospital_project.Models;
using System.Diagnostics;

namespace hospital_project.Controllers
{
    public class AvailabilityDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AvailabilityData/ListAvailabilities
        [HttpGet]
        public IEnumerable<AvailabilityDto> ListAvailabilities()
        {
            List<Availability> Availabilities = db.Availabilities.ToList();
            List<AvailabilityDto> AvailabilityDtos = new List<AvailabilityDto>();

            Availabilities.ForEach(a => AvailabilityDtos.Add(new AvailabilityDto()
            {
                availability_id=a.availability_id,
                physician_first_name=a.Physicians.first_name,
                physician_last_name=a.Physicians.last_name,
                physician_email=a.Physicians.email,
                availability_dates=a.availability_dates,
            }));

            return AvailabilityDtos;
        }

        // GET: api/AvailabilityData/FindAvailability/5
        [ResponseType(typeof(Availability))]
        [HttpGet]
        public IHttpActionResult FindAvailability(int id)
        {
            Availability Availability = db.Availabilities.Find(id);
            AvailabilityDto AvailabilityDto = new AvailabilityDto()
            {
                availability_id = Availability.availability_id,
                physician_first_name = Availability.Physicians.first_name,
                physician_last_name = Availability.Physicians.last_name,
                physician_email = Availability.Physicians.email,
                availability_dates = Availability.availability_dates,
            };
            if (Availability == null)
            {
                return NotFound();
            }

            return Ok(AvailabilityDto);
        }

        // Post: api/AvailabilityData/UpdateAvailability/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult PutAvailability(int id, Availability availability)
        {
            Debug.WriteLine("I have reached the update availability method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != availability.availability_id)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + availability.availability_id);
                return BadRequest();
            }

            db.Entry(availability).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvailabilityExists(id))
                {
                    Debug.WriteLine("Availability not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AvailabilityData/AddAvailability/5
        [ResponseType(typeof(Availability))]
        public IHttpActionResult PostAvailability(Availability availability)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Availabilities.Add(availability);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = availability.availability_id }, availability);
        }

        // POST: api/AvailabilityData/DeleteAvailability/5
        [ResponseType(typeof(Availability))]
        public IHttpActionResult DeleteAvailability(int id)
        {
            Availability availability = db.Availabilities.Find(id);
            if (availability == null)
            {
                return NotFound();
            }

            db.Availabilities.Remove(availability);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AvailabilityExists(int id)
        {
            return db.Availabilities.Count(e => e.availability_id == id) > 0;
        }
    }
}