using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataModel
{
    class DisplayModel
    {
    }

    public class DDModel
    {
        public int ID { get; set; }
        public SelectList YearList;
        public List<int> Year { get; set; }
    }
}
