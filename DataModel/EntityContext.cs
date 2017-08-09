using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using DataModel;

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

        public System.Data.Entity.DbSet<PutusanPidana> PutusanPidanas { get; set; }
        public System.Data.Entity.DbSet<PenegakHukum> PenegakHukums { get; set; }
        public System.Data.Entity.DbSet<DetailData> DetailDatas { get; set; }
        public System.Data.Entity.DbSet<UndangUndang> UndangUndangs { get; set; }
        public System.Data.Entity.DbSet<Propinsi> Propinsis { get; set; }
        public System.Data.Entity.DbSet<Pengadilan> Pengadilans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PutusanPidana>()
                        .HasOptional(m => m.Penuntut)
                        .WithMany(a => a.kasusPenuntut)
                        .HasForeignKey(m => m.PenuntutID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<PutusanPidana>()
                        .HasOptional(m => m.HakimKetua)
                        .WithMany(a => a.kasusHakimKetua)
                        .HasForeignKey(m => m.HakimKetuaID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<PenegakHukum>()
                       .HasMany(m => m.kasusHakimAnggota)
                       .WithMany(a => a.HakimAnggota)
                       .Map(cs =>
                       {
                           cs.MapLeftKey("HakimAnggotaRefID");
                           cs.MapLeftKey("KasusHakimAnggotaRefID");
                           cs.ToTable("KasusAnggota");
                       }
                       );

            modelBuilder.Entity<PutusanPidana>()
                       .HasOptional(m => m.Peradilan)
                       .WithMany(a => a.Kasus)
                       .HasForeignKey(m => m.PeradilanID)
                       .WillCascadeOnDelete(false);

            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Branch>().HasOptional(b => b.PrimaryContact).WithMany(a => a.PrimaryContactFor).HasForeignKey(b=>b.PrimaryContactID);
            //modelBuilder.Entity<Branch>().HasOptional(b => b.SecondaryContact).WithMany (a => a.ScondaryContactFor).HasForeignKey(b=>b.ScondaryContactID);
            //}

            //protected override void OnModelCreating(DbModelBuilder modelBuilder)
            //{
            //modelBuilder.Entity<Match>()
            //            .HasRequired(m => m.HomeTeam)
            //            .WithMany(t => t.HomeMatches)
            //            .HasForeignKey(m => m.HomeTeamId)
            //            .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Match>()
            //            .HasRequired(m => m.GuestTeam)
            //            .WithMany(t => t.AwayMatches)
            //            .HasForeignKey(m => m.GuestTeamId)
            //            .WillCascadeOnDelete(false);
        }
    }

}
