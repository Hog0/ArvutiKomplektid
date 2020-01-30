using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArvutiKomplektid.Models;

namespace ArvutiKomplektid.Controllers
{
    public class ArvutiTellimusController:Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Lisa()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Lisa([Bind(Include = "Id,Kirjeldus,Korpus,Kuvar,Pakitud")] ArvutiTellimus LisabUue)
        {
            if(ModelState.IsValid)
                {
                    db.Arvutitellimused.Add(LisabUue);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
         return View(LisabUue);
        }

        public ActionResult Komplekteerimata()
        {
            var komplekteeritud = db.Arvutitellimused
              .Where(u => u.Korpus == 0 || u.Kuvar == 0 || u.Pakitud == 0)
              .ToList();
            return View(komplekteeritud);
        }



        // GET: ArvutiTellimus
        public ActionResult Index()
        {
            return View(db.Arvutitellimused.ToList());
        }

        // GET: ArvutiTellimus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArvutiTellimus arvutiTellimus = db.Arvutitellimused.Find(id);
            if (arvutiTellimus == null)
            {
                return HttpNotFound();
            }
            return View(arvutiTellimus);
        }

        // GET: ArvutiTellimus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArvutiTellimus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Kirjeldus,Korpus,Kuvar,Pakitud")] ArvutiTellimus arvutiTellimus)
        {
            if (ModelState.IsValid)
            {
                db.Arvutitellimused.Add(arvutiTellimus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arvutiTellimus);
        }

        // GET: ArvutiTellimus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArvutiTellimus arvutiTellimus = db.Arvutitellimused.Find(id);
            if (arvutiTellimus == null)
            {
                return HttpNotFound();
            }
            return View(arvutiTellimus);
        }

        // POST: ArvutiTellimus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Kirjeldus,Korpus,Kuvar,Pakitud")] ArvutiTellimus arvutiTellimus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arvutiTellimus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arvutiTellimus);
        }

        // GET: ArvutiTellimus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArvutiTellimus arvutiTellimus = db.Arvutitellimused.Find(id);
            if (arvutiTellimus == null)
            {
                return HttpNotFound();
            }
            return View(arvutiTellimus);
        }

        // POST: ArvutiTellimus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArvutiTellimus arvutiTellimus = db.Arvutitellimused.Find(id);
            db.Arvutitellimused.Remove(arvutiTellimus);
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
