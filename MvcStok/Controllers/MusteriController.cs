using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri

        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index(string parametre)
        {
            /*
            var musteriler = db.TBLMUSTERILER.ToList();
            return View(musteriler);
            */
            var musteriler = from m in db.TBLMUSTERILER select m;
            if (!string.IsNullOrEmpty(parametre))
            {
                musteriler = musteriler.Where(m => m.MUSTERIAD.Contains(parametre));
            }
            return View(musteriler.ToList());
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERILER eklenecekMusteri) //Yeni müşteri ekleme
        {
            if (!ModelState.IsValid) //Müşteri adının boş geçilip geçilmediğinin kontrolü
            {
                return View("YeniMusteri");
            }
            db.TBLMUSTERILER.Add(eklenecekMusteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSil(int id) //Müşteri silme
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id) //Güncellenecek olan müşterinin ID' sini getirme
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir",musteri);
        }

        public ActionResult MusteriGuncelle(TBLMUSTERILER guncellenecekMusteri) //Müşteri güncelleme işlemi
        {
            var musteri = db.TBLMUSTERILER.Find(guncellenecekMusteri.MUSTERIID);
            musteri.MUSTERIAD = guncellenecekMusteri.MUSTERIAD;
            musteri.MUSTERISOYAD = guncellenecekMusteri.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}