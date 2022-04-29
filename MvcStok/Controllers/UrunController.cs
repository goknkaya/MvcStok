using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun

        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index(string parametre)
        {
            var urunler = db.TBLURUNLER.ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            //Dropdownlist' e verileri (KATEGORIAD) çekme

            List<SelectListItem> kategoriler = (from i in db.TBLKATEGORILER.ToList() //Linq Sorgulara göz at
                                            select new SelectListItem
                                            {
                                                Text = i.KATEGORIAD,
                                                Value = i.KATEGORIID.ToString()
                                            }).ToList();
            ViewBag.tasinanDegerler = kategoriler; //YeniUrun sayfasına veri taşımak için
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER eklenecekUrun) //Yeni ürün ekleme
        {
            //Dropdownlist' ten seçilen kategoriyi ekleme
            var kategori = db.TBLKATEGORILER.Where(m => m.KATEGORIID == eklenecekUrun.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            eklenecekUrun.TBLKATEGORILER = kategori;
            db.TBLURUNLER.Add(eklenecekUrun);
            db.SaveChanges(); //ExecuteNonQuery' e tekabul eder
            return RedirectToAction("Index"); //Kaydettikten sonra anasayfaya gel
        }

        public ActionResult UrunSil(int id) //Seçilen ürünün silinmesi
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);

            //Dropdownlist' e verileri (KATEGORIAD) çekme
            List<SelectListItem> kategoriler = (from i in db.TBLKATEGORILER.ToList() //Linq Sorgulara göz at
                                                select new SelectListItem
                                                {
                                                    Text = i.KATEGORIAD,
                                                    Value = i.KATEGORIID.ToString()
                                                }).ToList();
            ViewBag.tasinanDegerler = kategoriler; //YeniUrun sayfasına veri taşımak için

            return View("UrunGetir",urun);
        }

        public ActionResult UrunGuncelle(TBLURUNLER guncellenecekUrun)
        {
            var urun = db.TBLURUNLER.Find(guncellenecekUrun.URUNID);
            urun.URUNAD = guncellenecekUrun.URUNAD;
            urun.MARKA = guncellenecekUrun.MARKA;
            urun.FIYAT = guncellenecekUrun.FIYAT;
            urun.STOK = guncellenecekUrun.STOK;
            //urun.URUNKATEGORI = guncellenecekUrun.URUNKATEGORI;
            var kategori = db.TBLKATEGORILER.Where(m => m.KATEGORIID == guncellenecekUrun.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = kategori.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}