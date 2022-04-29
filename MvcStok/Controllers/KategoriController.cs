using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index(int sayfa=1)
        {
            //var kategoriler = db.TBLKATEGORILER.ToList();
            var kategoriler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa, 4); //Yukarıdaki satırdan farkı, verilerin sayfalanmış bir şekilde gelmesidir. Sondaki (1,4) ise 1 sayfada 4 veri olması demektir.
            return View(kategoriler);
        }

        //HttpGet ve HttpPost' ları ekleme sebebimiz, eklemediğimiz zaman YeniÜrünEkle butonuna bastığımızda ekleme işlemini yapmasak bile eklemeye devam etmesinden kaynaklıydı. Bu sebepten HttpGet (Yani sayfayı sadece açma işlemi) işleminde sadece return yaptırıyoruz. Ürün gerçekten eklendiğinde işlemlerin gerçekleştirilmesini HttpPost işleminde yapıyoruz.

        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORILER eklenecekKategori) //Yeni kategori eklenmesi
        {
            if (!ModelState.IsValid) //Kategori adının boş geçilip geçilmediğinin kontrolü
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(eklenecekKategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult kategoriSil(int id) //Seçilen kategorinin silinmesi
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult kategoriGetir(int id) //Güncellenecek olan kategorinin ID' sini getirme
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            return View("kategoriGetir",kategori);
        }

        public ActionResult kategoriGuncelle(TBLKATEGORILER guncellenecekKategori) //Kategori güncelleme işlemi
        {
            var kategori = db.TBLKATEGORILER.Find(guncellenecekKategori.KATEGORIID);
            kategori.KATEGORIAD = guncellenecekKategori.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}