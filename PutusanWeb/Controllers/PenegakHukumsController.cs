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
    public class PenegakHukumsController : Controller
    {
        private EntityContext db = new EntityContext();

        // GET: PenegakHukums
        public ActionResult Index()
        {
            return View(db.PenegakHukums.ToList());
        }

        // GET: PenegakHukums/Details/5
        //public ActionResult Details(int? id)
        public ActionResult Details(int id=66)
        {
            if (id == null)
            {
                 id = 66;
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenegakHukum penegakHukum = db.PenegakHukums.Include(k => k.kasusHakimKetua).Where(x=> x.ID == id ).FirstOrDefault();
            if (penegakHukum == null)
            {
                return HttpNotFound();
            }
            Session["penegak"] = penegakHukum;
            return View(penegakHukum);
        }

        // GET: PenegakHukums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PenegakHukums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HukumPidanaID,JenisPenegak,Catatan,Nama,Alamat,TanggalLahir,Kelamin")] PenegakHukum penegakHukum)
        {
            if (ModelState.IsValid)
            {
                db.PenegakHukums.Add(penegakHukum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(penegakHukum);
        }

        // GET: PenegakHukums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenegakHukum penegakHukum = db.PenegakHukums.Find(id);
            if (penegakHukum == null)
            {
                return HttpNotFound();
            }
            return View(penegakHukum);
        }

        // POST: PenegakHukums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HukumPidanaID,JenisPenegak,Catatan,Nama,Alamat,TanggalLahir,Kelamin")] PenegakHukum penegakHukum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(penegakHukum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(penegakHukum);
        }

        // GET: PenegakHukums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenegakHukum penegakHukum = db.PenegakHukums.Find(id);
            if (penegakHukum == null)
            {
                return HttpNotFound();
            }
            return View(penegakHukum);
        }

        // POST: PenegakHukums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PenegakHukum penegakHukum = db.PenegakHukums.Find(id);
            db.PenegakHukums.Remove(penegakHukum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult AjaxMethod()
        {
            PenegakHukum penegakHukum = new PenegakHukum();
             if (Session["penegak"]!= null)
             {
                 penegakHukum = (PenegakHukum)Session["penegak"];
             }
            List<ChartDetail> lC = new List<ChartDetail>();
            ChartDetail cd = null;
            Random random =  new Random();
            for (int i = 2012 ; i <= 2017; i++)
            {
                cd = new ChartDetail();

                cd.JumlahPutusan = penegakHukum.kasusHakimKetua.Where(x => Convert.ToDateTime(x.TanggalDibacakan).Year == i).ToList().Count;
                cd.Tahun = i.ToString();
                lC.Add(cd);

            }
            return Json(lC);
            //return null;
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

    public class ChartDetail
    {
        public string Tahun { get; set; }
        public int JumlahPutusan { get; set; }
    }

}
