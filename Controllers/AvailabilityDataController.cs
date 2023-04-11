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
                department_name=a.Departments.department_name,
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
                availability_id=Availability.availability_id,
                physician_first_name=Availability.Physicians.first_name,
                physician_last_name=Availability.Physicians.last_name,
                department_name=Availability.Departments.department_name,
                physician_email=Availability.Physicians.email,
                availability_dates = Availability.availability_dates,
            };
            if (Availability == null)
            {
                return NotFound();
            }

            return Ok(AvailabilityDto);
        } //FIX THE REST UNDER HERE AROUND 11:00

        // GET: api/Availabilities
        public IQueryable<Availability> GetAvailabilities()
        {
            return db.Availabilities;
        }

        // GET: api/Availabilities/5
        [ResponseType(typeof(Availability))]
        public IHttpActionResult GetAvailability(int id)
        {
            Availability availability = db.Availabilities.Find(id);
            if (availability == null)
            {
                return NotFound();
            }

            return Ok(availability);
        }

        // PUT: api/Availabilities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAvailability(int id, Availability availability)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != availability.availability_id)
            {
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Availabilities
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

        // DELETE: api/Availabilities/5
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

            return Ok(availability);
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