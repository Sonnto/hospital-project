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
    public class LabDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/LabData/ListLabs
        [HttpGet]
        public IHttpActionResult ListLabs()
        {
            List<Lab> Labs = db.Labs.ToList();
            List<LabDto> LabDtos = new List<LabDto>();

            Labs.ForEach(a => LabDtos.Add(new LabDto()
            {
                LabId=a.LabId,
                LabName=a.LabName
            }));

            return Ok(LabDtos);
        }

        // GET: api/LabData/FindLab/2
        [ResponseType(typeof(Lab))]
        [HttpGet]
        public IHttpActionResult FindLab(int id)
        {
            Lab lab = db.Labs.Find(id);
            LabDto LabDto = new LabDto()
            {
                LabId=lab.LabId,
                LabName=lab.LabName 
            };
            if (lab == null)
            {
                return NotFound();
            }

            return Ok(LabDto);
        }

        // POST: api/LabData/UpdateLab/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateLab(int id, Lab lab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lab.LabId)
            {
                return BadRequest();
            }

            db.Entry(lab).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabExists(id))
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

        // POST: api/LabData/AddLab
        [HttpPost]
        [ResponseType(typeof(Lab))]
        public IHttpActionResult AddLab(Lab lab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Labs.Add(lab);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lab.LabId }, lab);
        }

        // POST: api/LabData/DeleteLab/3
        [ResponseType(typeof(Lab))]
        [HttpPost]
        public IHttpActionResult DeleteLab(int id)
        {
            Lab lab = db.Labs.Find(id);
            if (lab == null)
            {
                return NotFound();
            }

            db.Labs.Remove(lab);
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

        private bool LabExists(int id)
        {
            return db.Labs.Count(e => e.LabId == id) > 0;
        }
    }
}