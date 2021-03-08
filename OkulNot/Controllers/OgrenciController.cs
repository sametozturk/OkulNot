using OkulNot.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OkulNot.Controllers
{
    public class OgrenciController : Controller
    {
        OkulEntities db = new OkulEntities();
        public ActionResult Index()
        {
            var ogrenci = db.OGRENCILER.ToList();
            return View(ogrenci);
        }
        public ActionResult YeniOgrenci()
        {
            List<SelectListItem> degerler = (from i in db.KULUPLER.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.KULUPAD,
                                                Value=i.KULUPID.ToString()
                                            }).ToList();
            ViewBag.deger = degerler;
            List<SelectListItem> degerler1 = (from i in db.OGRENCILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.OGRCINSIYET,
                                                 Value = i.OGRID.ToString()
                                             }).ToList();
            ViewBag.deger1 = degerler1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniOgrenci(OGRENCILER ogrenci)
        {
            var kulup = db.KULUPLER.Where(m => m.KULUPID == ogrenci.KULUPLER.KULUPID).FirstOrDefault();
            ogrenci.KULUPLER = kulup;
            db.OGRENCILER.Add(ogrenci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var ogr = db.OGRENCILER.Find(id);
            db.OGRENCILER.Remove(ogr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var ogr = db.OGRENCILER.Find(id);
            List<SelectListItem> degerler = (from i in db.KULUPLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString()
                                             }).ToList();
            ViewBag.deger = degerler;
            return View("Guncelle", ogr);
        }

        [HttpPost]
        public ActionResult Guncelle(OGRENCILER k)
        {
            var kulup = db.KULUPLER.Where(m => m.KULUPID == k.KULUPLER.KULUPID).FirstOrDefault();
            var ogr = db.OGRENCILER.Find(k.OGRID);
            ogr.OGRAD = k.OGRAD;
            ogr.OGRSOYAD = k.OGRSOYAD;
            ogr.OGRFOTO = k.OGRFOTO;
            ogr.OGRCINSIYET = k.OGRCINSIYET;
            ogr.KULUPLER = kulup;
            db.SaveChanges(); 
            return RedirectToAction("Index", "Ogrenci");
        }
    }
}