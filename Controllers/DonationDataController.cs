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
    public class DonationDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(DonationDto))]
        public IHttpActionResult ListDonations()
        {
            List<Donation> Donations = db.Donations.ToList();
            List<DonationDto> DonationDtos = new List<DonationDto>();

            Donations.ForEach(r => DonationDtos.Add(new DonationDto()
            {
                DonationID = r.DonationID,
                DonationLevel = r.DonationLevel,
                MinAmount = r.MinAmount
            }));

            return Ok(DonationDtos);
        }

        // GET: api/DonationData/5
        [ResponseType(typeof(DonationDto))]
        [HttpGet]
        public IHttpActionResult FindDonation(int id)
        {
            Donation Donation = db.Donations.Find(id);
            DonationDto DonationDto = new DonationDto()
            {
                DonationID = Donation.DonationID,
                DonationLevel = Donation.DonationLevel,
                MinAmount = Donation.MinAmount
            };
            if (Donation == null)
            {
                return NotFound();
            }

            return Ok(DonationDto);
        }

        // PUT: api/DonationData/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDonation(int id, Donation Donation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Donation.DonationID)
            {
                return BadRequest();
            }

            db.Entry(Donation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationExists(id))
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

        // POST: api/DonationData
        [ResponseType(typeof(Donation))]
        [HttpPost]
        public IHttpActionResult AddDonation(Donation Donation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Donations.Add(Donation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Donation.DonationID }, Donation);
        }

        // DELETE: api/DonationData/5
        [ResponseType(typeof(Donation))]
        public IHttpActionResult DeleteDonation(int id)
        {
            Donation Donation = db.Donations.Find(id);
            if (Donation == null)
            {
                return NotFound();
            }

            db.Donations.Remove(Donation);
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

        private bool DonationExists(int id)
        {
            return db.Donations.Count(e => e.DonationID == id) > 0;
        }
    }
}