using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
//using DataModel;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccess
{
    public class EntityContext : DbContext
    {
        public EntityContext()
            : base("name=DefaultConnection")
        {

        }

        public static EntityContext Create()
        {
            return new EntityContext();
        }

        //public System.Data.Entity.DbSet<PutusanPidana> PutusanPidanas { get; set; }
        //public System.Data.Entity.DbSet<PenegakHukum> PenegakHukums { get; set; }

        
    }

}
