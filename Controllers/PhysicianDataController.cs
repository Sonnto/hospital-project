using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;
using hospital_project.Models;

namespace hospital_project.Controllers
{
    public class PhysicianDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PhysicianData/ListPhysicians
        [HttpGet]
        public IEnumerable<PhysicianDto> ListPhysicians()
        {
            List<Physician> Physicians = db.Physicians.ToList();
            List<PhysicianDto> PhysicianDtos = new List<PhysicianDto>();

            Physicians.ForEach(p => PhysicianDtos.Add(new PhysicianDto()
            {
                physician_id = p.physician_id,
                first_name = p.first_name,
                last_name = p.last_name,
                email = p.email,
            }));

            return PhysicianDtos;
        }

        // GET: api/PhysicianData/ListPhysiciansForDepartment
        [HttpGet]
        [ResponseType(typeof(PhysicianDto))]
        public IHttpActionResult ListPhysiciansForDepartment(int id)
        {
            //ask physician that have departments that match ID
            List<Physician> Physicians = db.Physicians.Where(p=>p.Departments.Any(d=>d.department_id==id)).ToList();
            List<PhysicianDto> PhysicianDtos = new List<PhysicianDto>();
            Physicians.ForEach(p=>PhysicianDtos.Add(new PhysicianDto(){
                physician_id = p.physician_id ,
                first_name =p.first_name,
                last_name=p.last_name,
                email=p.email,
            }));

            return Ok(PhysicianDtos);
        }

        // POST: api/PhysicianData/AssociatePhysicianWithDepartment/{physician_id}/{department_d}
        [HttpPost]
        [Route("api/PhysicianData/AssociatePhysicianWithDepartment/{physician_id}/{department_id}")]
        public IHttpActionResult AssociatePhysicianWithDepartment(int physician_id, int department_id)
        {
            Physician SelectedPhysician = db.Physicians.Include
                (p => p.Departments).Where
                (p => p.physician_id == physician_id).FirstOrDefault();
            Department SelectedDepartment = db.Departments.Find(department_id);

            if (SelectedPhysician == null || SelectedDepartment == null)
            {
                return NotFound();
            }
            Debug.WriteLine("Input physician id is: " + physician_id);
            Debug.WriteLine("Selected physician name is: " + SelectedPhysician.first_name + SelectedPhysician.last_name);
            Debug.WriteLine("Input department id to be added: " + department_id);
            Debug.WriteLine("Selected department name to be added: " + SelectedDepartment.department_name);

            SelectedPhysician.Departments.Add(SelectedDepartment);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/PhysicianData/UnassociatePhysicianWithDepartment/{physician_id}/{department_id}
        [HttpPost]
        [Route("api/PhysicianData/UnassociatePhysicianWithDepartment/{physician_id}/{department_id}")]
        public IHttpActionResult UnAssociateAnimeWithGenre(int physician_id, int department_id)
        {
            Physician SelectedPhysician = db.Physicians.Include(p => p.Departments).Where(p => p.physician_id == physician_id).FirstOrDefault();
            Department SelectedDepartment = db.Departments.Find(department_id);

            if (SelectedPhysician == null || SelectedDepartment == null)
            {
                return NotFound();
            }
            Debug.WriteLine("Input physician id is: " + physician_id);
            Debug.WriteLine("Selected Physician's name is: " + SelectedPhysician.first_name + SelectedPhysician.last_name);
            Debug.WriteLine("Input department id to be removed: " + department_id);
            Debug.WriteLine("Selected department name to be removed: " + SelectedDepartment.department_name);

            SelectedPhysician.Departments.Remove(SelectedDepartment);
            db.SaveChanges();

            return Ok();
        }

        // GET: api/PhysicianData/FindPhysician/5
        [ResponseType(typeof(Physician))]
        [HttpGet]
        public IHttpActionResult FindPhysician(int id)
        {
            Physician Physician = db.Physicians.Find(id);
            PhysicianDto PhysicianDto = new PhysicianDto()
            {
                physician_id = Physician.physician_id,
                first_name = Physician.first_name,
                last_name = Physician.last_name,
                email = Physician.email,
            };
            if (Physician == null)
            {
                return NotFound();
            }

            return Ok(PhysicianDto);
        }

        // POST: api/PhysicianData/UpdatePhysician/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePhysician(int id, Physician physician)
        {
            Debug.WriteLine("Reached the update physician method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != physician.physician_id)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + " " + id);
                Debug.WriteLine("POST parameter" + " " + physician.physician_id);
                Debug.WriteLine("Value of physician.physician_id: " + physician.physician_id);

                return BadRequest();
                //Current issue: mismatched ID. Unsure why.
            }

            db.Entry(physician).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicianExists(id))
                {
                    Debug.WriteLine("Physician not found");
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

        // POST: api/PhysicianData/AddPhysician/5
        [ResponseType(typeof(Physician))]
        [HttpPost]
        public IHttpActionResult AddPhysician(Physician physician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Physicians.Add(physician);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = physician.physician_id }, physician);
        }

        // POST: api/PhysicianData/DeletePhysician/5
        [ResponseType(typeof(Physician))]
        [HttpPost]
        public IHttpActionResult DeletePhysician(int id)
        {
            Physician physician = db.Physicians.Find(id);
            if (physician == null)
            {
                return NotFound();
            }

            db.Physicians.Remove(physician);
            db.SaveChanges();

            return Ok(physician);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhysicianExists(int id)
        {
            return db.Physicians.Count(e => e.physician_id == id) > 0;
        }
    }
}