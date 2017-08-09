using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataModel;

namespace PutusanWeb.Controllers
{
    public class UndangUndangsController : Controller
    {
        private EntityContext db = new EntityContext();

        // GET: UndangUndangs
        public ActionResult Index()
        {
            return View(db.UndangUndangs.ToList());
        }

        // GET: UndangUndangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UndangUndang undangUndang = db.UndangUndangs.Find(id);
            if (undangUndang == null)
            {
                return HttpNotFound();
            }
            return View(undangUndang);
        }

        public ActionResult QueryDetails(string query, string type)
        {


            JenisUndangUndang jenisuu = (JenisUndangUndang)Enum.Parse(typeof(JenisUndangUndang), type);
            UndangUndang undangUndang = db.UndangUndangs.Where(x => x.PasalKompak == query && x.JenisUU == jenisuu).FirstOrDefault();

            //if (undangUndang != null)
            //{
            //    undangUndang.TextPasal = undangUndang.TextPasal.Replace(".",  System.Environment.NewLine);
                
            //}
           

            //UndangUndang undangUndang = null;

            return View("Details", undangUndang);
        }


        // GET: UndangUndangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UndangUndangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Pasal,TextPasal,JenisUU,HukumanPenjara")] UndangUndang undangUndang)
        {
            if (ModelState.IsValid)
            {
                db.UndangUndangs.Add(undangUndang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(undangUndang);
        }

        // GET: UndangUndangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UndangUndang undangUndang = db.UndangUndangs.Find(id);
            if (undangUndang == null)
            {
                return HttpNotFound();
            }
            return View(undangUndang);
        }

        // POST: UndangUndangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Pasal,TextPasal,JenisUU,HukumanPenjara")] UndangUndang undangUndang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(undangUndang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(undangUndang);
        }

        // GET: UndangUndangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UndangUndang undangUndang = db.UndangUndangs.Find(id);
            if (undangUndang == null)
            {
                return HttpNotFound();
            }
            return View(undangUndang);
        }

        // POST: UndangUndangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UndangUndang undangUndang = db.UndangUndangs.Find(id);
            db.UndangUndangs.Remove(undangUndang);
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
