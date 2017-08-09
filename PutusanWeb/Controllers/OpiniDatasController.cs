using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataModel;
using PutusanWeb.Models;

namespace PutusanWeb.Controllers
{
    public class OpiniDatasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OpiniDatas
        public ActionResult Index()
        {
            return View(db.OpiniDatas.ToList());
        }

        // GET: OpiniDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpiniData opiniData = db.OpiniDatas.Find(id);
            if (opiniData == null)
            {
                return HttpNotFound();
            }
            return View(opiniData);
        }

        // GET: OpiniDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OpiniDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TextOpini,PenulisOpini")] OpiniData opiniData)
        {
            if (ModelState.IsValid)
            {
                db.OpiniDatas.Add(opiniData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(opiniData);
        }

        // GET: OpiniDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpiniData opiniData = db.OpiniDatas.Find(id);
            if (opiniData == null)
            {
                return HttpNotFound();
            }
            return View(opiniData);
        }

        // POST: OpiniDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TextOpini,PenulisOpini")] OpiniData opiniData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opiniData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(opiniData);
        }

        // GET: OpiniDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpiniData opiniData = db.OpiniDatas.Find(id);
            if (opiniData == null)
            {
                return HttpNotFound();
            }
            return View(opiniData);
        }

        // POST: OpiniDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OpiniData opiniData = db.OpiniDatas.Find(id);
            db.OpiniDatas.Remove(opiniData);
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
