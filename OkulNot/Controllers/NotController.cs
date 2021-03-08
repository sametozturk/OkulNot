using OkulNot.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OkulNot.Models;

namespace OkulNot.Controllers
{
    public class NotController : Controller
    {
        OkulEntities db = new OkulEntities();
        public ActionResult Index()
        {
            var not = db.NOTLAR.ToList();
            return View(not);
        }
        public ActionResult YeniSinav()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniSinav(NOTLAR not)
        {
            var sonuc = (not.SINAV1 + not.SINAV2 + not.SINAV3 +not.PROJE)/4;
            not.ORTALAMA = sonuc;
            db.NOTLAR.Add(not);
            db.SaveChanges();
            return View();
        }
        public ActionResult Guncelle(int id)
        {
            var not = db.NOTLAR.Find(id);
            return View("Guncelle", not);
        }
        [HttpPost]
        public ActionResult Guncelle(Hesap model,NOTLAR not,int SINAV1=0,int SINAV2=0,int SINAV3=0,int PROJE=0)
        {
            if(model.islem=="Hesapla")
            {
                int ortalama = (SINAV1 + SINAV2 + SINAV3 + PROJE) / 4;
                ViewBag.ort = ortalama;
            }
            if (model.islem == "Guncelle")
            {
                var sinav = db.NOTLAR.Find(not.NOTID);
                sinav.SINAV1 = not.SINAV1;
                sinav.SINAV2 = not.SINAV2;
                sinav.SINAV3 = not.SINAV3;
                sinav.PROJE = not.PROJE;
                sinav.ORTALAMA = not.ORTALAMA;
                db.SaveChanges();
                return RedirectToAction("Index","Not");
            }
            return View();
        }
    }
   
}