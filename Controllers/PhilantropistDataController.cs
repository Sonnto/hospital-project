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
using hospital_project.Migrations;
using System.Diagnostics;

namespace hospital_project.Controllers
{
    public class PhilantropistDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PhilantropistData/ListPhilantropists
        [HttpGet]
        [ResponseType(typeof(PhilantropistDto))]

        public IHttpActionResult ListPhilantropists()
        {
            List<Philantropist> Philantropists = db.Philantropists.ToList();
            List<PhilantropistDto> PhilantropistDtos = new List<PhilantropistDto>();

            Philantropists.ForEach(p => PhilantropistDtos.Add(new PhilantropistDto(){
                PhilantropistID = p.PhilantropistID,
                PhilantropistFirstName = p.PhilantropistFirstName,
                PhilantropistLastName = p.PhilantropistLastName,
                Company = p.Company,
                DonationID = p.Donations.DonationID
            }));
            return Ok(PhilantropistDtos);
        }








        //nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn









        [HttpGet]
        [ResponseType(typeof(PhilantropistDto))]
        public IHttpActionResult ListPhilantropistsForDonation(int id)
        {
            List<Philantropist> Philantropists = db.Philantropists.Where(a => a.DonationID == id).ToList();
            List<PhilantropistDto> PhilantropistDtos = new List<PhilantropistDto>();

            Philantropists.ForEach(a => PhilantropistDtos.Add(new PhilantropistDto()
            {
                PhilantropistID = a.PhilantropistID,
                PhilantropistFirstName = a.PhilantropistFirstName,
                PhilantropistLastName = a.PhilantropistLastName,
                Company = a.Company,
                DonationID = a.Donations.DonationID
            }));

            return Ok(PhilantropistDtos);
        }



        [HttpGet]
        [ResponseType(typeof(PhilantropistDto))]
        public IHttpActionResult ListPhilantropistsForDepartment(int id)
        {
            //all Philantropist that have departments which match with our ID
            List<Philantropist> Philantropists = db.Philantropists.Where(
                a => a.Departments.Any(
                    c => c.department_id == id
                )).ToList();
            List<PhilantropistDto> PhilantropistDtos = new List<PhilantropistDto>();

            Philantropists.ForEach(a => PhilantropistDtos.Add(new PhilantropistDto()
            {
                PhilantropistID = a.PhilantropistID,
                PhilantropistFirstName = a.PhilantropistFirstName,
                PhilantropistLastName = a.PhilantropistLastName,
                Company = a.Company,
                DonationID = a.Donations.DonationID
            }));

            return Ok(PhilantropistDtos);
        }


        [HttpPost]
        [Route("api/PhilantropistData/AssociatePhilantropistWithDepartment/{philantropistid}/{departmentid}")]
        public IHttpActionResult AssociatePhilantropistsWithDepartment(int philantropistid, int departmentid)
        {

            Philantropist SelectedPhilantropist = db.Philantropists.Include(a => a.Departments).Where(a => a.PhilantropistID == departmentid).FirstOrDefault();
            Department SelectedDepartment = db.Departments.Find(departmentid);

            if (SelectedPhilantropist == null || SelectedDepartment == null)
            {
                return NotFound();
            }


            SelectedPhilantropist.Departments.Add(SelectedDepartment);
            db.SaveChanges();

            return Ok();
        }



        [HttpPost]
        [Route("api/PhilantropistData/UnAssociatePhilantropistWithDepartment/{philantropistid}/{departmentid}")]
        public IHttpActionResult UnAssociatePhilantropistWithDepartment(int philantropistid, int departmentid)
        {

            Philantropist SelectedPhilantropist = db.Philantropists.Include(a => a.Departments).Where(a => a.PhilantropistID == philantropistid).FirstOrDefault();
            Department SelectedDepartment = db.Departments.Find(departmentid);

            if (SelectedPhilantropist == null || SelectedDepartment == null)
            {
                return NotFound();
            }

            SelectedPhilantropist.Departments.Remove(SelectedDepartment);
            db.SaveChanges();

            return Ok();
        }









        //nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn








        // GET: api/PhilantropistData/FindPhilantropist/2
        [ResponseType(typeof(Philantropist))]
        [HttpGet]
        public IHttpActionResult FindPhilantropist(int id)
        {
            Philantropist Philantropist = db.Philantropists.Find(id);
            PhilantropistDto PhilantropistDto = new PhilantropistDto()
            {
                PhilantropistID = Philantropist.PhilantropistID,
                PhilantropistFirstName = Philantropist.PhilantropistFirstName,
                PhilantropistLastName = Philantropist.PhilantropistLastName,
                Company = Philantropist.Company,
                DonationID = Philantropist.Donations.DonationID
            };
            if (Philantropist == null)
            {
                return NotFound();
            }

            return Ok(PhilantropistDto);
        }

        // POST: api/PhilantropistData/UpdatePhilantropist/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePhilantropist(int id, Philantropist philantropist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != philantropist.PhilantropistID)
            {
                return BadRequest();
            }

            db.Entry(philantropist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhilantropistExists(id))
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

        // POST: api/PhilantropistData/AddPhilantropist
        [ResponseType(typeof(Philantropist))]
        [HttpPost]

        public IHttpActionResult AddPhilantropist(Philantropist philantropist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Philantropists.Add(philantropist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = philantropist.PhilantropistID }, philantropist);
        }

        // POST: api/PhilantropistData/DeletePhilantropist/5
        [ResponseType(typeof(Philantropist))]
        [HttpPost]
        public IHttpActionResult DeletePhilantropist(int id)
        {
            Philantropist philantropist = db.Philantropists.Find(id);
            if (philantropist == null)
            {
                return NotFound();
            }

            db.Philantropists.Remove(philantropist);
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

        private bool PhilantropistExists(int id)
        {
            return db.Philantropists.Count(e => e.PhilantropistID == id) > 0;
        }
    }
}