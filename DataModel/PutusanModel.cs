using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataModel
{
    public class Putusan
    {
    }

    public class PutusanPidana
    {
        public int ID { get; set; }
        [Display(Name="Nomor Putusan")]
        public string NomorPutusan { get; set; }
        public string NomorPutusanCompact { get; set; }
        public TingkatProses Proses { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]  
        [Display(Name = "Tanggal Registrasi")]
        public DateTime? TanggalRegister { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]  
        [Display(Name = "Tanggal Musyawarah")]
        public DateTime? TanggalMusyawarah { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]  
        [Display(Name = "Tanggal Dibacakan")]
        public DateTime? TanggalDibacakan { get; set; }
        public string Klasifikasi { get; set; }

        [Display(Name = "Sub Kalisifikasi")]
        public string SubKlasifikasi { get; set; }
        public string Panitera { get; set; }

        public Pengadilan Peradilan { get; set; }
        public int? PeradilanID { get; set; }

        public virtual ICollection<Pihak> Tergugat { get; set; }
        public string Tersangka { get; set; }
        public string ParaPihak { get; set; }
        
        public virtual ICollection<Pengacara> PengacaraTergugat { get; set; }
        public string Amar { get; set; }
        [Display(Name = "Catatan Amar")]
        public string CatatanAmar { get; set; }
        
        public virtual ICollection<PenegakHukum> HakimAnggota { get; set; }
        [Display(Name = "Hakim Anggota")]
        public string Anggota { get; set; }
        
        public PenegakHukum Penuntut { get; set; }
        public int? PenuntutID { get; set; }

        public PenegakHukum HakimKetua { get; set; }
        public int? HakimKetuaID { get; set; }

        public bool Yurisprudensi { get; set; }
        [Display(Name = "Status Tahanan")]
        public bool StatusTahanan { get; set; }
        [Display(Name = "Berkekuatan Hukum Tetap")]
        public bool BerkekuatanHukumTetap { get; set; }
        public KaidahYuriprudensi Kaidah { get; set; }
        public string Ringkasan { get; set; }
        public string Raw { get; set; }
        [UIHint("DetailData")]
        public DetailData DataDetail { get; set; }

        [Display(Name = "Nomor Putusan Pertama")]
        [UIHint("DisplayRelated")]
        public string PutusanPertama { get; set; }
        [Display(Name = "Nomor Putusan Banding")]
        [UIHint("DisplayRelated")]
        public string PutusanBanding{ get; set; }
        [Display(Name = "Nomor Putusan PK")]
        [UIHint("DisplayRelated")]
        public string PutusanPK { get; set; }
        [Display(Name = "Nomor Putusan Kasasi")]
        [UIHint("DisplayRelated")]
        public string PutusanKasasi { get; set; }
        [Display(Name = "Putusan Lain")]
        [UIHint("DisplayRelated")]
        public string PutusanLain { get; set; }
        [Display(Name = "Kasus Landmark")]
        public bool? KasusLandMark { get; set; }
        [Display(Name = "Lama Hukuman")]
        public int? LamaHukuman { get; set; }

        public virtual ICollection<OpiniData> Opini { get; set; }
    }

    public class DetailData
    {
        public int ID { get; set; }
        public string TextPutusan { get; set; }
        public string NomorPutusan { get; set; }
        public string NomorPutusanCompact { get; set; }
        public string FileName { get; set; }
        public virtual ICollection<UndangUndang> PasalPelanggaran { get; set; }
    }

    public class OpiniData
    {
        public int ID { get; set; }

        [AllowHtml]
        public string TextOpini { get; set; }
        public string PenulisOpini { get; set; }
        public string RangkumanOpini { get; set; }


    }

    public class PenegakHukum :Identitas
    {
        public int HukumPidanaID { get; set; }
        public virtual ICollection<PutusanPidana> kasusHakimKetua { get; set; }
        public virtual ICollection<PutusanPidana> kasusHakimAnggota { get; set; }
        public virtual ICollection<PutusanPidana> kasusPenuntut { get; set; }
        [Display(Name = "Jenis Penegak Hukum")]
        public JenisPenegakHukum JenisPenegak { get; set; }
        public string Catatan { get; set; }
    }

    public class UndangUndang
    {
        public int ID { get; set; }
        public string Pasal { get; set; }
        [Display(Name = "Text")]
        public string TextPasal { get; set; }
        [Display(Name = "Jenis UU")]
        public JenisUndangUndang JenisUU { get; set; }
        public int HukumanPenjara { get; set; }
        [Display(Name = "Masih Berlaku")]
        public bool? MasihBerlaku { get; set; }
        public string PasalKompak { get; set; }
    }

    public class Pengacara : Identitas
    {
        public virtual ICollection<PutusanPidana> kasus { get; set; }
        public string Perusahaan { get; set; }
        public string Catatan { get; set; }
    }

    public class Identitas
    {
        public int ID { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        [Display(Name = "Tanggal Lahir")]
        public DateTime? TanggalLahir { get; set; }
        [Display(Name = "Gender")]
        public JenisKelamin? Kelamin { get; set; }
        public string NamaCompact { get; set; }
        public string Agama { get; set; }
        public string Pendidikan { get; set; }
        public string TempatLahir { get; set; }
    }

    public class Pihak : Identitas
    {
        public string Catatan { get; set; }
        public string Pekerjaan { get; set; }
        public string Kebangsaan { get; set; }
        public string Alias { get; set; }
    }

    public class Pengadilan
    {
        public int ID { get; set; }
        public string Nama { get; set; }
        [Display(Name = "Lembaga Peradilan")]
        public string Alamat { get; set; }
        public string NamaCompact { get; set; }
        [Display(Name = "Jenis Lembaga Peradilan")]
        public JenisLembaga JenisLembagaPeradilan { get; set; }
        public virtual ICollection<PutusanPidana> Kasus { get; set; }

    }

    public class Propinsi
    {
        public int ID { get; set; }
        public string Nama { get; set; }
        public string NomorIso { get; set; }
        public virtual ICollection<Pengadilan> LembagaPengadilan { get; set; }
    }

    public enum JenisLembaga
    {
        PN,
        PT,
        MA,
        PTM,
        PM,
        PA,
        DILMILTAMA,
        BADILUM
    }

    public enum JenisUndangUndang
    {
        UUD,
        KUHP,
        KUHPER
    }

    public enum KaidahYuriprudensi
    {
       
        [Display(Name = "Pidana Khusus")]
        PidanaKhusus,
        [Display(Name = "Pidana Umum")]
        PidanaUmum,
        [Display(Name = "Perdata Khusus")]
        PerdataKhusus,
        [Display(Name = "Perdata Umum")]
        PerdataUmum
    }

    public enum JenisKelamin
    {
        Pria,
        Perempuan
    }

    public enum SatusTersangka
    {
        Tersangka,
        Terdakwa,
        Terpidana
    }

    public enum TingkatProses
    {
        Pertama,
        Banding,
        Kasasi,
        PK
    }

    public enum JenisPenegakHukum
    {
        Hakim,
        Penuntut

    }

    public enum Bulan
    {
        Bulan0,
        januari,
        februari, 
        maret, 
        april, 
        mei, 
        juni, 
        juli, 
        agustus, 
        september,
        oktober, 
        november, 
        desember

    }
}
