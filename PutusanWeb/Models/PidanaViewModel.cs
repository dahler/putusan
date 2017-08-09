using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel;
using System.ComponentModel.DataAnnotations;
namespace PutusanWeb.Models
{
    public class PidanaViewModel
    {
        public int ID { get; set; }
        public string NomorPutusan { get; set; }
        public TingkatProses Proses { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]  
        public DateTime? TanggalRegister { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TanggalDibacakan { get; set; }
        public string Klasifikasi { get; set; }
        public int SubKlasifikasi { get; set; }
        public string Panitera { get; set; }
        public JenisLembaga JenisLembagaPeradilan { get; set; }
        public string LembagaPeradilan { get; set; }
        public ICollection<Pihak> Tergugat { get; set; }
        public PenegakHukum Penuntut { get; set; }
        public ICollection<Pengacara> PengacaraTergugat { get; set; }
        public string Amar { get; set; }
        [DataType(DataType.MultilineText)]
        public string CatatanAmar { get; set; }
        public PenegakHukum HakimKetua { get; set; }
        public ICollection<PenegakHukum> HakimAnggota { get; set; }
        public bool Yurisprudensi { get; set; }
        public bool StatusTahanan { get; set; }
        public bool BerkekuatanHukumTetap { get; set; }
        public KaidahYuriprudensi Kaidah { get; set; }
        public string Ringkasan { get; set; }
       
    }
}