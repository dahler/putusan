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
    public class PengadilansController : Controller
    {
        private EntityContext db = new EntityContext();

        // GET: Pengadilans
        public ActionResult Index()
        {
            return View(db.Pengadilans.ToList());
        }

        // GET: Pengadilans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pengadilan pengadilan = db.Pengadilans.Include(x => x.Kasus).Where( x=> x.ID == id).FirstOrDefault();//.Find(id);
            if (pengadilan == null)
            {
                return HttpNotFound();
            }
            return View(pengadilan);
        }

        // GET: Pengadilans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pengadilans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nama,Alamat,NamaCompact,JenisLembagaPeradilan")] Pengadilan pengadilan)
        {
            if (ModelState.IsValid)
            {
                db.Pengadilans.Add(pengadilan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pengadilan);
        }

        // GET: Pengadilans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pengadilan pengadilan = db.Pengadilans.Find(id);
            if (pengadilan == null)
            {
                return HttpNotFound();
            }
            return View(pengadilan);
        }

        // POST: Pengadilans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nama,Alamat,NamaCompact,JenisLembagaPeradilan")] Pengadilan pengadilan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pengadilan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pengadilan);
        }

        // GET: Pengadilans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pengadilan pengadilan = db.Pengadilans.Find(id);
            if (pengadilan == null)
            {
                return HttpNotFound();
            }
            return View(pengadilan);
        }

        // POST: Pengadilans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pengadilan pengadilan = db.Pengadilans.Find(id);
            db.Pengadilans.Remove(pengadilan);
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

        [HttpPost]
        public JsonResult RetrieveStatistic()
        {
            PenegakHukum penegakHukum = new PenegakHukum();
            if (Session["penegak"] != null)
            {
                penegakHukum = (PenegakHukum)Session["penegak"];
            }
            List<ChartDetail> lC = new List<ChartDetail>();
            ChartDetail cd = null;
            Random random = new Random();
            for (int i = 2012; i <= 2017; i++)
            {
                cd = new ChartDetail();

                cd.JumlahPutusan = penegakHukum.kasusHakimKetua.Where(x => Convert.ToDateTime(x.TanggalDibacakan).Year == i).ToList().Count;
                cd.Tahun = i.ToString();
                lC.Add(cd);

            }
            return Json(lC);
            //return null;
        }

        public class ChartDetail
        {
            public string Tahun { get; set; }
            public string Klasifikasi { get; set; }
            public string SubKlasifikasi { get; set; }
            public int JumlahPutusan { get; set; }
        }

    }
}
