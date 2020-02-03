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
    [Authorize]
    public class ArvutiTellimusController:Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Lisa()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Lisa([Bind(Include = "Kirjeldus")] ArvutiTellimus LisabUue)
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
            var komplekteerimata = db.Arvutitellimused
              .Where(u => u.Korpus >= 0 && u.Kuvar >= 0 && u.Komplekt == -1)
              .ToList();
            return View(komplekteerimata);
        }

        public ActionResult Komplekteeritud()
        {
            var model = db.Arvutitellimused
                       .Where(u => u.Korpus == 1 && u.Kuvar == 1 && u.Komplekt == -1)
                       .ToList();
            return View(model);
        }

        public ActionResult KomplekteeritudMuut(int id, int Komplekt)
        {
            ArvutiTellimus tellimus = db.Arvutitellimused.Find(id);
            if (tellimus == null)
            {
                return HttpNotFound();
            }
            tellimus.Komplekt = Komplekt;
            db.Entry(tellimus).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Komplekteeritud");
        }

        public ActionResult KorpusKuvar()
        {
            var model = db.Arvutitellimused
                       .Where(u => u.Korpus == 0 || u.Kuvar == 0)
                       .ToList();
            return View(model);
        }

        public ActionResult KorpusKuvarMuut(int id, int kuvar, int korpus)
        {
            ArvutiTellimus tellimus = db.Arvutitellimused.Find(id);
            if (tellimus == null)
            {
                return HttpNotFound();
            }
            tellimus.Kuvar = kuvar;
            tellimus.Korpus = korpus;
            db.Entry(tellimus).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("KorpusKuvar");
        }

        public ActionResult PakimisLeht()
        {
            string[] olemas = { "", "", "Olemas", "Ei Ole Olemas" };
            var model = db.Arvutitellimused
              .OrderBy(u => u.Kirjeldus)
              .Where(u => u.Korpus == 1 && u.Kuvar == 1 && u.Pakitud == 0)
              .Select(u => new PakimisLeht
              {
                  Id = u.Id,
                  Kirjeldus = u.Kirjeldus,
                  Komplekt = u.Komplekt, 
                  Kuvar = u.Kuvar==0?"":u.Kuvar==1?"Olemas": "Ei Ole Olemas",
                  Korpus = u.Korpus == 0?"":u.Korpus==1?"Olemas" : "Ei Ole Olemas",
                  Pakitud = u.Pakitud == 0?"": u.Pakitud==1?"Pakitud" : "Ei Ole Pakitud",
              }).ToList();
            return View(model);
        }

        public ActionResult PakimisLehtMuut(int id, int pakk)
        {
            ArvutiTellimus tellimus = db.Arvutitellimused.Find(id);
            if (tellimus == null)
            {
                return HttpNotFound();
            }
            tellimus.Pakitud = pakk;
            db.Entry(tellimus).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("PakimisLeht");
        }

        [AllowAnonymous]
        public ActionResult Statistikaleht()
        {
            ViewBag.koikkokku = db.Arvutitellimused.Count();
            ViewBag.valmis = db.Arvutitellimused
                .Where(u => u.Pakitud == 1)
                .Count();
            return View();
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
        public ActionResult Create([Bind(Include = "Id,Kirjeldus,Komplekt,Korpus,Kuvar,Pakitud")] ArvutiTellimus arvutiTellimus)
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
        public ActionResult Edit([Bind(Include = "Id,Kirjeldus,Komplekt,Korpus,Kuvar,Pakitud")] ArvutiTellimus arvutiTellimus)
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
