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
using System.Web.Security;
using hospital_project.Models;

namespace hospital_project.Controllers
{
    public class ProjectDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectData/ListProjects
        public IHttpActionResult ListProjects()
        {
            List<Project> Projects = db.Projects.ToList();
            List<ProjectDto> ProjectDtos = new List<ProjectDto>();

            Projects.ForEach(b => ProjectDtos.Add(new ProjectDto()
            {
                ProjectId = b.ProjectId,
                ProjectName = b.ProjectName,
                LabId = b.Labs.LabId,
                LabName = b.Labs.LabName
      
            }));

            return Ok(ProjectDtos);
        }
        // GET: api/ProjectData/ListProjectsForLab/1
        [HttpGet]
        [ResponseType(typeof(ProjectDto))]
        public IHttpActionResult ListProjectsForLab(int id)
        {
            List<Project> Projects = db.Projects.Where(
                 m => m.Lab.Any(
                     a => a.LabId == id)
                 ).ToList();
            List<ProjectDto> ProjectDtos = new List<ProjectDto>();

            Projects.ForEach(m => ProjectDtos.Add(new ProjectDto()
            {
                ProjectId = m.ProjectId,
                ProjectName = m.ProjectName
             
            }));

            return Ok(ProjectDtos);
        }

        // GET: api/ProjectData/FindProject/5
        [ResponseType(typeof(ProjectDto))]
        public IHttpActionResult FindProject(int id)
        {
            Project project = db.Projects.Find(id);
            ProjectDto ProjectDto = new ProjectDto()
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                LabId = project.Labs.LabId,
                LabName = project.Labs.LabName

            };
            if (project == null)
            {
                return NotFound();
            }

            return Ok(ProjectDto);
        }

        // PUT: api/ProjectData/UpdateProject/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateProject(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            db.Entry(project).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/ProjectData/AddProject
        [ResponseType(typeof(Project))]
        [HttpPost]
        public IHttpActionResult AddProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Projects.Add(project);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = project.ProjectId }, project);
        }

        // DELETE: api/ProjectData/5
        [ResponseType(typeof(Project))]
        [HttpPost]
        public IHttpActionResult DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            db.Projects.Remove(project);
            db.SaveChanges();

            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.ProjectId == id) > 0;
        }
    }
}