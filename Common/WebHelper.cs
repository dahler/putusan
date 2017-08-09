using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using System.Web.Routing;
using System.IO;
using System.Text.RegularExpressions;
using DataModel;


namespace Common
{
    public static class WebHelper
    {

        public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.Model).Replace("\n", "<br />\r\n");
            //var model = html.Encode(metadata.Model).Replace("\n", System.Environment.NewLine);

            if (String.IsNullOrEmpty(model))
                return MvcHtmlString.Empty;

            return MvcHtmlString.Create(model);
        }

        public static MvcHtmlString Hyperlink(this HtmlHelper helper, string url, string linkText)
        {
            return MvcHtmlString.Create(String.Format("<a href='{0}'>{1}</a>", url, linkText));
        }


        public static MvcHtmlString UpdateStringwithLink(this HtmlHelper htmlHelper, string src)
        {
            string rest = src;
            //int pasal = 0;
            //string part;
            //int index = 0;
            //int KUHP = 0;
            //KUHP = rest.ToLower().IndexOf("kuhp");

            //while (KUHP > 0)
            //{
            //    pasal = rest.ToLower().IndexOf("pasal", KUHP - 50);

            //    if (pasal != -1 && pasal < KUHP)
            //    {
            //        part = rest.Substring(pasal + 5, KUHP - (pasal + 5)).Trim();
            //        string[] hasil = part.Split(' ');

            //        for (int i = 0; i < hasil.Length; i++)
            //        {
            //            index = rest.ToLower().IndexOf(hasil[i], pasal);
            //            if (Regex.IsMatch(hasil[i], @"^\d+"))
            //            {
            //                var a = htmlHelper.ActionLink(hasil[i], "Details", "PenegakHukums", new { query = hasil[i] }, null).ToString();
            //                rest = rest.Remove(index, hasil[i].Length);
            //                rest = rest.Insert(index, a);
            //                KUHP = KUHP + a.Length;
            //            }
            //        }

            //    }


            //    KUHP = rest.ToLower().IndexOf("kuhp", KUHP + 1);
            //    //.pasal.KUHP = KUHP + 1;

            //}

            rest = UpdateString(htmlHelper, rest, "kuhp");
            rest = UpdateString(htmlHelper, rest, "kuh pidana");

            //var a = htmlHelper.ActionLink("Test Adding link", "Details", "PenegakHukums", new { id = 1 }, null).ToString(); 
            return MvcHtmlString.Create(rest);
        }

         public static string UpdateString(this HtmlHelper htmlHelper, string src, string UU)
         {
             string rest = src;
             int pasal = 0;
             string part;
             int index = 0;
             int KUHP = 0;
             KUHP = rest.ToLower().IndexOf(UU);

             while (KUHP > 0)
             {
                 pasal = rest.ToLower().IndexOf("pasal", KUHP - 50);

                 if (pasal != -1 && pasal < KUHP)
                 {
                     part = rest.Substring(pasal + 5, KUHP - (pasal + 5)).Trim();
                     string[] hasil = part.Split(' ');
                     string hasilSetelah = "";

                     for (int i = 0; i < hasil.Length; i++)
                     {
                         index = rest.ToLower().IndexOf(hasil[i], pasal);
                         hasilSetelah = "";
                         if (Regex.IsMatch(hasil[i], @"^\d+"))
                         {
                             if (i+1 < hasil.Length)
                             {
                                 
                                 hasilSetelah = hasil[i + 1];
                                 if (hasilSetelah.Length != 1 && hasilSetelah != "ter" && hasilSetelah != "bis")
                                 {
                                     hasilSetelah = "";
                                 }

                             }
                             var a = htmlHelper.ActionLink(hasil[i], "QueryDetails", "UndangUndangs", new { query = hasil[i] + hasilSetelah, type = JenisUndangUndang.KUHP }, new { target = "_blank", @class = "btn btn-circle-sm btn-info" }).ToString();
                             rest = rest.Remove(index, hasil[i].Length);
                             rest = rest.Insert(index, a);
                             KUHP = KUHP + a.Length;
                         }
                     }

                 }

                 KUHP = rest.ToLower().IndexOf("kuhp", KUHP + 1);
                 
             }
             return rest;
         }

    }


    public static class ControllerHtml
    {
        // this class from internal TemplateHelpers class in System.Web.Mvc namespace
        private class ViewDataContainer : IViewDataContainer
        {
            public ViewDataContainer(ViewDataDictionary viewData)
            {
                ViewData = viewData;
            }

            public ViewDataDictionary ViewData { get; set; }
        }

        private static HtmlHelper htmlHelper;

        public static HtmlHelper Html(Controller controller)
        {
            if (htmlHelper == null)
            {
                var vdd = new ViewDataDictionary();
                var tdd = new TempDataDictionary();
                var controllerContext = controller.ControllerContext;
                var view = new RazorView(controllerContext, "/", "/", false, null);
                htmlHelper = new HtmlHelper(new ViewContext(controllerContext, view, vdd, tdd, new StringWriter()),
                     new ViewDataContainer(vdd), RouteTable.Routes);
            }
            return htmlHelper;
        }

        public static HtmlHelper Html(Controller controller, object model)
        {
            if (htmlHelper == null || htmlHelper.ViewData.Model == null || !htmlHelper.ViewData.Model.Equals(model))
            {
                var vdd = new ViewDataDictionary();
                vdd.Model = model;
                var tdd = new TempDataDictionary();
                var controllerContext = controller.ControllerContext;
                var view = new RazorView(controllerContext, "/", "/", false, null);
                htmlHelper = new HtmlHelper(new ViewContext(controllerContext, view, vdd, tdd, new StringWriter()),
                     new ViewDataContainer(vdd), RouteTable.Routes);
            }
            return htmlHelper;
        }
    }
    
}
