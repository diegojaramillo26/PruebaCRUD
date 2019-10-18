using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PruebaCRUD.Models;

namespace PruebaCRUD.Controllers
{
    public class AvesController : Controller
    {
        private dbEntities db = new dbEntities();

        // GET: Aves
        public ActionResult Index(string searchNombre, string searchZona)
        {

            var Zonas = db.TONT_ZONAS.ToList();
            ViewBag.Zonas = new SelectList(Zonas, "CDZONA", "DSNOMBRE");
            //ViewBag.selectedValue = db.Country.Where(a => a.CountryId == country.CountryId).Select(a => a.CountryName).SingleOrDefault();

            if (!String.IsNullOrEmpty(searchNombre) && !String.IsNullOrEmpty(searchZona)) 
            {
                var aves = from b in db.TONT_AVES
                            where (b.DSNOMBRE_CIENTIFICO.Contains(searchNombre) || b.DSNOMBRE_COMUN.Contains(searchNombre)) && b.TONT_PAISES.Any( z => z.TONT_ZONAS.CDZONA == searchZona)
                            select b;
                return View(aves.ToList());
            }
            else
            {
                return View(db.TONT_AVES.ToList());
            }
            
        }

        // GET: Aves/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TONT_AVES tONT_AVES = db.TONT_AVES.Find(id);
            if (tONT_AVES == null)
            {
                return HttpNotFound();
            }
            return View(tONT_AVES);
        }

        // GET: Aves/Create
        public ActionResult Create()
        {
            List<TONT_PAISES> ListaPaises = db.TONT_PAISES.ToList();
            ViewBag.ListaPaises = ListaPaises;
            return View();
        }

        // POST: Aves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CDAVE,DSNOMBRE_COMUN,DSNOMBRE_CIENTIFICO")] TONT_AVES tONT_AVES)
        {
            if (ModelState.IsValid)
            {
                db.TONT_AVES.Add(tONT_AVES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tONT_AVES);
        }

        // GET: Aves/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TONT_AVES tONT_AVES = db.TONT_AVES.Find(id);
            if (tONT_AVES == null)
            {
                return HttpNotFound();
            }
            return View(tONT_AVES);
        }

        // POST: Aves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CDAVE,DSNOMBRE_COMUN,DSNOMBRE_CIENTIFICO")] TONT_AVES tONT_AVES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tONT_AVES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tONT_AVES);
        }

        // GET: Aves/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TONT_AVES tONT_AVES = db.TONT_AVES.Find(id);
            if (tONT_AVES == null)
            {
                return HttpNotFound();
            }
            return View(tONT_AVES);
        }

        // POST: Aves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TONT_AVES tONT_AVES = db.TONT_AVES.Find(id);
            db.TONT_AVES.Remove(tONT_AVES);
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
