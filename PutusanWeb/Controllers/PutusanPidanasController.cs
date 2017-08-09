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
using System.Reflection;
using System.Text;
using Common;
using System.Web.Mvc.Html;


namespace PutusanWeb.Controllers
{
    public class PutusanPidanasController : Controller
    {
        private EntityContext db = new EntityContext();

        // GET: PutusanPidanas
        public ActionResult Index()
        {
            var putusanPidanas = db.PutusanPidanas.Include(p => p.HakimKetua).Include(p => p.Penuntut);
            return View(putusanPidanas.ToList());
        }

        // GET: PutusanPidanas/Details/5

        public ActionResult Details(int? id = 3637)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //int nomor = Convert.ToInt32(id);
            PutusanPidana putusanPidana = db.PutusanPidanas.Where(x => x.ID == id).Include(o => o.HakimKetua).Include(x => x.Peradilan).Include(x => x.DataDetail).FirstOrDefault();
            if (putusanPidana == null)
            {
                return HttpNotFound();
            }

            if (putusanPidana.CatatanAmar != null)
            {
                putusanPidana.CatatanAmar = putusanPidana.CatatanAmar.Replace(";", ";" + System.Environment.NewLine);
                putusanPidana.CatatanAmar = putusanPidana.CatatanAmar.Replace("M E N G A D I L I", "M E N G A D I L I " + System.Environment.NewLine);
                putusanPidana.CatatanAmar = putusanPidana.CatatanAmar.Replace("--", "");

                
            } 
            if(putusanPidana.Tersangka != null)
            {
                putusanPidana.Tersangka = putusanPidana.Tersangka.Replace("-", " ");
            }
            if (putusanPidana.Anggota != null)
            {
                putusanPidana.Anggota = putusanPidana.Anggota.Replace("-", " ");
            }
            //putusanPidana.CatatanAmar = putusanPidana.CatatanAmar.Replace(";", "<br />");
           
            //putusanPidana.Amar = link+" " + putusanPidana.Amar;

           

           // var a = "Email is locked, click " +
           //ControllerHtml.Html(this).Hyperlink("www.google.com", "unlock").ToString();

            //putusanPidana.CatatanAmar = InsertLinkforUU(putusanPidana.CatatanAmar);

            //if (putusanPidana.DataDetail != null)
            //{
            //    putusanPidana.DataDetail.TextPutusan = InsertLinkforUU(putusanPidana.DataDetail.TextPutusan);
            //}


            return View(putusanPidana);
        }

     
        public ActionResult DependentDetails(string query)
        {
            //int id = 51;

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            byte[] data = Convert.FromBase64String(query);
            string decodedString = Encoding.UTF8.GetString(data).Trim();
            decodedString = Helper.fixNameCompact(Helper.fixPutusanNumber(decodedString));

            //int nomor = Convert.ToInt32(id);
            //PutusanPidana putusanPidana = db.PutusanPidanas.Where(x => x.ID == id).Include(o => o.HakimKetua).FirstOrDefault();


            PutusanPidana putusanPidana = db.PutusanPidanas.Where(x => x.NomorPutusanCompact == decodedString).Include(o => o.HakimKetua).Include(x => x.Peradilan).Include(x => x.DataDetail).FirstOrDefault();
            if (putusanPidana == null)
            {
                return View("Details", null);
            }

            if (putusanPidana.CatatanAmar != null)
            {
                putusanPidana.CatatanAmar = putusanPidana.CatatanAmar.Replace(";", ";" + System.Environment.NewLine);
                putusanPidana.CatatanAmar = putusanPidana.CatatanAmar.Replace("M E N G A D I L I", "M E N G A D I L I " + System.Environment.NewLine);
                putusanPidana.CatatanAmar = putusanPidana.CatatanAmar.Replace("--", "");
            }
            //putusanPidana.CatatanAmar = putusanPidana.CatatanAmar.Replace(";", "<br />");

            return View("Details", putusanPidana);
        }


        public ActionResult GetUndangUndang(string query= "penipuan")
        {

            List<UndangUndang> lUndangs = db.UndangUndangs.Where(x => x.TextPasal.Contains(query)).ToList();
            

            return PartialView("_UndangUndang", lUndangs);
        }


        // GET: PutusanPidanas/Create 
        public ActionResult Create()
        {
            ViewBag.HakimKetuaID = new SelectList(db.PenegakHukums, "ID", "Catatan");
            ViewBag.PenuntutID = new SelectList(db.PenegakHukums, "ID", "Catatan");
            return View();
        }

        // POST: PutusanPidanas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomorPutusan,Proses,TanggalRegister,TanggalMusyawarah,TanggalDibacakan,Klasifikasi,SubKlasifikasi,Panitera,JenisLembagaPeradilan,LembagaPeradilan,Tersangka,Amar,CatatanAmar,HakimKetuaID,Anggota,PenuntutID,Yurisprudensi,StatusTahanan,BerkekuatanHukumTetap,Kaidah,Ringkasan,Raw")] PutusanPidana putusanPidana)
        {
            if (ModelState.IsValid)
            {
                db.PutusanPidanas.Add(putusanPidana);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HakimKetuaID = new SelectList(db.PenegakHukums, "ID", "Catatan", putusanPidana.HakimKetuaID);
            ViewBag.PenuntutID = new SelectList(db.PenegakHukums, "ID", "Catatan", putusanPidana.PenuntutID);
            return View(putusanPidana);
        }

        // GET: PutusanPidanas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PutusanPidana putusanPidana = db.PutusanPidanas.Find(id);
            if (putusanPidana == null)
            {
                return HttpNotFound();
            }
            ViewBag.HakimKetuaID = new SelectList(db.PenegakHukums, "ID", "Catatan", putusanPidana.HakimKetuaID);
            ViewBag.PenuntutID = new SelectList(db.PenegakHukums, "ID", "Catatan", putusanPidana.PenuntutID);
            return View(putusanPidana);
        }

        // POST: PutusanPidanas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomorPutusan,Proses,TanggalRegister,TanggalMusyawarah,TanggalDibacakan,Klasifikasi,SubKlasifikasi,Panitera,JenisLembagaPeradilan,LembagaPeradilan,Tersangka,Amar,CatatanAmar,HakimKetuaID,Anggota,PenuntutID,Yurisprudensi,StatusTahanan,BerkekuatanHukumTetap,Kaidah,Ringkasan,Raw")] PutusanPidana putusanPidana)
        {
            if (ModelState.IsValid)
            {
                db.Entry(putusanPidana).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HakimKetuaID = new SelectList(db.PenegakHukums, "ID", "Catatan", putusanPidana.HakimKetuaID);
            ViewBag.PenuntutID = new SelectList(db.PenegakHukums, "ID", "Catatan", putusanPidana.PenuntutID);
            return View(putusanPidana);
        }

        // GET: PutusanPidanas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PutusanPidana putusanPidana = db.PutusanPidanas.Find(id);
            if (putusanPidana == null)
            {
                return HttpNotFound();
            }
            return View(putusanPidana);
        }

        // POST: PutusanPidanas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PutusanPidana putusanPidana = db.PutusanPidanas.Find(id);
            db.PutusanPidanas.Remove(putusanPidana);
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

        //public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        //{
        //    var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
        //    var model = html.Encode(metadata.Model).Replace("\r\n", "<br />\r\n");

        //    if (String.IsNullOrEmpty(model))
        //        return MvcHtmlString.Empty;

        //    return MvcHtmlString.Create(model);
        //}

        public string fixLongSentence(string data)
        {

            string res = "";
            string restbefore = "";
            data = data.ToLower();
            int index = data.IndexOf(".");

            int n;
            

            while (index >= 0)
            {

                res = data.Substring(index, 1);
                restbefore = data.Substring(index-2, 1);
                //bool isNumeric = int.TryParse(restbefore, out n);
                if (res != " " && restbefore == ".")
                {
                    data = data.Insert(index + 1, System.Environment.NewLine);
                }



                index = data.IndexOf(".", index + 1);


            }




            return data;
        }


       public string InsertLinkforUU( string data)
        {
            
            var a = ControllerHtml.Html(this).ActionLink("Test Adding link", "Details", "PenegakHukums", new { id = 1 }, null).ToString(); 

            return a+ " " +data;
        }

    }


    
}
