using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using hospital_project.Models;

namespace hospital_project.Controllers
{
    public class ResearchersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Researchers
        public ActionResult Index()
        {
            var researchers = db.Researchers.Include(r => r.Projects);
            return View(researchers.ToList());
        }

        // GET: Researchers/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Researcher researcher = db.Researchers.Find(id);
            if (researcher == null)
            {
                return HttpNotFound();
            }
            return View(researcher);
        }

        // GET: Researchers/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");
            return View();
        }

        // POST: Researchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResearcherId,ResearcherName,ResearchLeader,ProjectId")] Researcher researcher)
        {
            if (ModelState.IsValid)
            {
                db.Researchers.Add(researcher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", researcher.ProjectId);
            return View(researcher);
        }

        // GET: Researchers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Researcher researcher = db.Researchers.Find(id);
            if (researcher == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", researcher.ProjectId);
            return View(researcher);
        }

        // POST: Researchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResearcherId,ResearcherName,ResearchLeader,ProjectId")] Researcher researcher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(researcher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", researcher.ProjectId);
            return View(researcher);
        }

        // GET: Researchers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Researcher researcher = db.Researchers.Find(id);
            if (researcher == null)
            {
                return HttpNotFound();
            }
            return View(researcher);
        }

        // POST: Researchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Researcher researcher = db.Researchers.Find(id);
            db.Researchers.Remove(researcher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
