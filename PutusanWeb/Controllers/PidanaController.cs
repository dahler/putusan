using Common;
using DataAccess;
using DataModel;
using PagedList;
using PutusanWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PutusanWeb.Controllers
{
    public class PidanaController : Controller
    {
        private EntityContext db = new EntityContext();

        // GET: PidanaViewModels
        public ActionResult Index(string query, int page = 1, int pageSize = 10)
        {

            


            //ExtractJson extr = new ExtractJson();
            //extr.ReadJson();


            var model = new List<PidanaViewModel>();
            int count = 0;

            if (query != string.Empty)
            {
                try
                {

                    using (var context = new EntityContext())
                    {

                       
                        List<Common.Filter> filter = new List<Common.Filter>();
                        Common.Filter cF = null;

                        List<PutusanPidana> lrest = new List<PutusanPidana>();

                        if (query != null)
                        {
                            string[] param = query.Trim().Split(' ');
                            ViewBag.CurrentFilter = query;


                            /********************************************
                            * buat di local
                            ********************************************/

                            //foreach (string temp in param)
                            //{
                            //    cF = new Common.Filter();
                            //    cF.Value = temp.ToLower();
                            //    cF.PropertyName = "Raw";
                            //    cF.Operation = Op.Contains;
                            //    filter.Add(cF);
                            //}

                            //var deleg = Helper.ExpressionBuilder.GetExpression<PutusanPidana>(filter).Compile();
                            //lrest = context.PutusanPidanas.Where(deleg)
                            //    .OrderByDescending(x => x.TanggalDibacakan)
                            //    .Skip((page - 1) * pageSize)
                            //    .Take(pageSize).ToList();

                            //count = context.PutusanPidanas.Where(deleg).Count();



                            /************************************************
                             *  end buat dii local
                             * ***************************************/




                            /********************************************
                             * buat di server
                             ********************************************/
                            var values = new StringBuilder();
                            values.AppendFormat("{0}", param[0]);
                            for (int i = 1; i < param.Length; i++)
                                values.AppendFormat(" and {0}", param[i]);

                            var sql = string.Format(
                            "SELECT * FROM PutusanPidanas WHERE  CONTAINS ([Raw],   '{0}')", values);


                            //lrest = context.Set<PutusanPidana>().SqlQuery(sql).Take(100).ToList();

                            lrest = context.Set<PutusanPidana>().SqlQuery(sql)
                                .OrderByDescending(x => x.TanggalDibacakan)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize).ToList();

                            count = context.Set<PutusanPidana>().SqlQuery(sql).Count();

                            /************************************************
                             *  end buar server
                             * ***************************************/




                        }
                        else
                        {
                            lrest = context.PutusanPidanas.OrderByDescending(x => x.TanggalDibacakan)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize).ToList();

                            count = context.PutusanPidanas.Count();
                        }

                        //var value = context.PutusanPidanas.Where(x => x.CatatanAmar.Contains()).ToList();

                        foreach (var employee in lrest)
                        {
                            var empModel = new PidanaViewModel();
                            empModel.ID = employee.ID;
                            empModel.NomorPutusan = employee.NomorPutusan;
                            empModel.Penuntut = employee.Penuntut;
                            empModel.Amar = employee.Amar;
                            empModel.TanggalRegister = employee.TanggalRegister;
                            empModel.TanggalDibacakan = employee.TanggalDibacakan;
                            empModel.CatatanAmar = employee.CatatanAmar;
                            empModel.Klasifikasi = employee.Klasifikasi;
                            //if (employee.HakimKetua != null)
                            //{
                            //    empModel.HakimKetua = employee.HakimKetua;
                            //}
                            //empModel.LembagaPeradilan = employee.Peradilan.Nama;
                            //employee.HakimAnggota.ToList()[0].Nama
                            //empModel.CatatanAmar = employee.IsEmployeeRetired ? "Yes" : "No";
                            model.Add(empModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            var resultAsPagedList = new StaticPagedList<PidanaViewModel>(model, page, pageSize, count);

            return View(resultAsPagedList);
          
        }


        public string addLink(string data)
        {
            //string link = HtmlHelper.GenerateLink(this.ControllerContext.RequestContext, System.Web.Routing.RouteTable.Routes, "My link", "Default", "About", "Home", null, null);

            string link = "test <br/>";

            //string link = "<a href='" + Url.Action("PutusanPidanas", "Details", new { id = 1 }, null) + "><span>Edit Group</span></a>";
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine(link);
            //sb.AppendLine(data);
           
           
           data = link + data;
            return data;
        }
       

        //[HttpGet]
        //public ActionResult Search()
        //{
        //    return PartialView("_SearchFormPartial");
        //}

        

       
    }
}
