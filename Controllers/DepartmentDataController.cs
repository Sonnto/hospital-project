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
using hospital_project.Models;

namespace hospital_project.Controllers
{
    public class DepartmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DepartmentData/ListDepartments
        [HttpGet]
        public IEnumerable<DepartmentDto> ListDepartments()
        {
            List<Department> Departments = db.Departments.ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

            Departments.ForEach(d => DepartmentDtos.Add(new DepartmentDto()
            {
                department_id = d.department_id,
                department_name = d.department_name,
            }));

            return DepartmentDtos;
        }

        // GET: api/DepartmentList/ListDepartmentsForPhysician/{physician_id}
        [HttpGet]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult ListDepartmentsForPhysician(int id)
        {
            List<Department> Departments = db.Departments.Where(
                d => d.Physicians.Any(
                    p => p.physician_id == id)
                ).ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

            Departments.ForEach(d => DepartmentDtos.Add(new DepartmentDto()
            {
                department_id = d.department_id,
                department_name = d.department_name,
            }));

            return Ok(DepartmentDtos);
        }

        // GET: api/DepartmentList/ListDepartmentsNotForPhysician/{physician_id}
        [HttpGet]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult ListDepartmentsNotForPhysician(int id)
        {
            List<Department> Departments = db.Departments.Where(
                d => !d.Physicians.Any(
                    p => p.physician_id == id)
                ).ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

            Departments.ForEach(d => DepartmentDtos.Add(new DepartmentDto()
            {
                department_id = d.department_id,
                department_name = d.department_name,
            }));

            return Ok(DepartmentDtos);
        }

        // POST: api/DepartmentData/AssociateDepartmentWithPhysician/{department_id}/{physician_id}
        [HttpPost]
        [Route("api/DepartmentData/AssociateDepartmentWithPhysician/{department_id}/{physician_id}")]
        public IHttpActionResult AssociateDepartmentWithPhysician(int department_id, int physician_id)
        {
            Department SelectedDepartment = db.Departments.Include
                (d => d.Physicians).Where
                (d => d.department_id == department_id).FirstOrDefault();
            Physician SelectedPhysician = db.Physicians.Find(physician_id);

            if (SelectedDepartment == null || SelectedPhysician == null)
            {
                return NotFound();
            }
            Debug.WriteLine("Input department id is: " + department_id);
            Debug.WriteLine("Selected department name is: " + SelectedDepartment.department_name);
            Debug.WriteLine("Input physician id to be added: " + physician_id);
            Debug.WriteLine("Selected physician name to be added: " + SelectedPhysician.first_name + " " + SelectedPhysician.last_name);

            SelectedDepartment.Physicians.Add(SelectedPhysician);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/DepartmentData/UnassociateDepartmentWithPhysician/{department_id}/{physician_id}
        [HttpPost]
        [Route("api/DepartmentData/UnassociateDepartmentWithPhysician/{department_id}/{physician_id}")]
        public IHttpActionResult UnassociateDepartmentWithPhysician(int department_id, int physician_id)
        {
            Department SelectedDepartment = db.Departments.Include(d => d.Physicians).Where(d => d.department_id == department_id).FirstOrDefault();
            Physician SelectedPhysician = db.Physicians.Find(physician_id);

            if (SelectedDepartment == null || SelectedPhysician == null)
            {
                return NotFound();
            }
            Debug.WriteLine("Input department id is: " + department_id);
            Debug.WriteLine("Selected department name to be removed: " + SelectedDepartment.department_name);
            Debug.WriteLine("Input physician id to be removed: " + physician_id);
            Debug.WriteLine("Selected Physician's name is: " + SelectedPhysician.first_name + SelectedPhysician.last_name);

            SelectedPhysician.Departments.Remove(SelectedDepartment);
            db.SaveChanges();

            return Ok();
        }

        /**** EDIT ABOVE *****/

        // GET: api/DepartmentData/FindDepartment/5
        [ResponseType(typeof(Department))]
        [HttpGet]
        public IHttpActionResult FindDepartment(int id)
        {
            Department Department = db.Departments.Find(id);
            DepartmentDto DepartmentDto = new DepartmentDto()
            {
                department_id = Department.department_id,
                department_name = Department.department_name,
            };
            if (Department == null)
            {
                return NotFound();
            }

            return Ok(DepartmentDto);
        }

        // POST: api/DepartmentData/UpdateDepartment/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDepartment(int id, Department department)
        {
            Debug.WriteLine("I have reached the update department method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != department.department_id)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + department.department_id);
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

        // POST: api/DepartmentData/AddDepartment/5
        [ResponseType(typeof(Department))]
        [HttpPost]
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

        // POST: api/DepartmentData/DeleteDepartment/5
        [ResponseType(typeof(Department))]
        [HttpPost]
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