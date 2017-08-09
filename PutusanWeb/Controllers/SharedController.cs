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
using PagedList;

namespace PutusanWeb.Controllers
{
    public class SharedController : Controller
    {
        private EntityContext db = new EntityContext();


        //[ChildActionOnly]
        public ActionResult Index(string pnid)
        {
            DDModel mod = new DDModel();
            List<int> yea = new List<int>();
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem item = null;
            for (int i =0; i < 10; i ++)
            {
                item = new SelectListItem();
                int ye = DateTime.Now.Year - i;
                yea.Add(ye);
                item.Value = ye.ToString();
                item.Text = ye.ToString();
                list.Add(item);
            }
            mod.Year = yea;
            mod.YearList = new SelectList(list, "Value", "Text"); 
            
            //List<ChartDetail> lCD = new List<ChartDetail>();
            TempData["pnid"] = pnid;
            return PartialView("_ListPutusanStat", mod);
        }

        //public ActionResult Index(int? page)
        //{
        //    var listPaged = page.ToString();//GetPagedNames(page); // GetPagedNames is found in BaseController
        //    if (listPaged == null)
        //        return HttpNotFound();

        //    ViewBag.Names = listPaged;
        //    return Request.IsAjaxRequest()
        //        ? (ActionResult)PartialView("_ListPutusanStat")
        //        : View();
        //}


        public ActionResult DaftarPutusan(string pengadilanID = "", int page = 1, int pageSize = 15, string hakimID = "" )
        {

            var model = new List<PutusanPidana>();
            int  count = 0;
            int id;

            if (pengadilanID != string.Empty)
            {
                id = Convert.ToInt32(pengadilanID);

                count = db.PutusanPidanas.Where(x => x.PeradilanID == id).Count();
                model = db.PutusanPidanas.Where(x => x.PeradilanID == id)
                             .OrderByDescending(x => x.TanggalDibacakan)
                             .Skip((page - 1) * pageSize)
                             .Take(pageSize).ToList();

                ViewBag.Pengadilan = pengadilanID;
            }
            else if (hakimID != string.Empty)
            {
                id = Convert.ToInt32(hakimID);
                
                count = db.PutusanPidanas.Where(x => x.HakimKetuaID == id).Count();
                model = db.PutusanPidanas.Where(x => x.HakimKetuaID == id)
                             .OrderByDescending(x => x.TanggalDibacakan)
                             .Skip((page - 1) * pageSize)
                             .Take(pageSize).ToList();

                ViewBag.Hakim = hakimID;
            }

            var resultAsPagedList = new StaticPagedList<PutusanPidana>(model, page, pageSize, count);

            return PartialView("ListPutusan", resultAsPagedList);
            //return PartialView("ListPutusan", model.ToPagedList(page, pageSize));

            //return View("ListPutusan.cshtml", model.ToPagedList(page, pageSize));
        }

        [HttpPost]
        public JsonResult RetrieveStatistic()
        {
            return getStatistics("");
        }


        [HttpPost]
        public JsonResult FilterStatistic(string year)
        {
           
            return  getStatistics(year);
        }

        public JsonResult  getStatistics(string year)
        {
            int id = Convert.ToInt16(TempData["pnid"]);
           
               DateTime end = new DateTime(DateTime.Now.Year, 12, 31);
               DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
            
            if (year != string.Empty )
            {
                 end = new DateTime(Convert.ToInt32(year), 12, 31);
                 start = new DateTime(Convert.ToInt32(year), 1, 1);
            }


            List<PutusanPidana> lPutusan = db.PutusanPidanas.Where(x => x.TanggalDibacakan <= end && x.TanggalDibacakan >= start && x.PeradilanID == id).ToList();
            List<PutusanPidana> lPutusanTemp = lPutusan.GroupBy(x => x.SubKlasifikasi).Select(x => x.First()).ToList();
            List<ChartDetail> lC = new List<ChartDetail>();
            ChartDetail cd = null;
            Random random = new Random();
            for (int i = 0; i < lPutusanTemp.Count; i++)
            {
                cd = new ChartDetail();

                cd.JumlahPutusan = lPutusan.Where(x => x.SubKlasifikasi == lPutusanTemp[i].SubKlasifikasi).ToList().Count;
                cd.SubKlasifikasi = lPutusanTemp[i].SubKlasifikasi;
                cd.Tahun = i.ToString();
                lC.Add(cd);

            }
            TempData["pnid"] = id;
            return Json(lC.OrderByDescending(x => x.JumlahPutusan));
        }
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
