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
using hospital_project.Migrations;

namespace hospital_project.Controllers
{
    public class DepartmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [HttpGet]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult ListDepartments()
        {
            List<Department> Departments = db.Departments.ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

            Departments.ForEach(c => DepartmentDtos.Add(new DepartmentDto()
            {
                department_id = c.department_id,
                department_name = c.department_name
            }));

            return Ok(DepartmentDtos);
        }



        [HttpGet]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult ListDepartmentsForPhilantropist(int id)
        {
            List<Department> Departments = db.Departments.Where(
                c => c.Philantropists.Any(
                    d => d.PhilantropistID == id)
                ).ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

            Departments.ForEach(c => DepartmentDtos.Add(new DepartmentDto()
            {
                department_id = c.department_id,
                department_name = c.department_name
            }));

            return Ok(DepartmentDtos);
        }


       
        [HttpGet]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult ListDepartmentsNotDonatedToByPhilantropist(int id)
        {
            List<Department> Companions = db.Departments.Where(
                c => !c.Philantropists.Any(
                    d => d.PhilantropistID == id)
                ).ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

            Companions.ForEach(c => DepartmentDtos.Add(new DepartmentDto()
            {
                department_id = c.department_id,
                department_name = c.department_name
            }));

            return Ok(DepartmentDtos);
        }

        [ResponseType(typeof(DepartmentDto))]
        [HttpGet]
        public IHttpActionResult FindDepartment(int id)
        {
            Department Department = db.Departments.Find(id);
            DepartmentDto DepartmentDto = new DepartmentDto()
            {
                department_id = Department.department_id,
                department_name = Department.department_name
            };
            if (Department == null)
            {
                return NotFound();
            }

            return Ok(DepartmentDto);
        }













        // PUT: api/DepartmentData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateDepartment(int id, Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.department_id)
            {
                return BadRequest();
            }

            db.Entry(department).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/DepartmentData
        [ResponseType(typeof(Department))]
        public IHttpActionResult AddDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departments.Add(department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = department.department_id }, department);
        }

        // DELETE: api/DepartmentData/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult DeleteDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
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

        private bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.department_id == id) > 0;
        }
    }
}