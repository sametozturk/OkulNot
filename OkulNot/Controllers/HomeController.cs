using OkulNot.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OkulNot.Controllers
{
    public class HomeController : Controller
    {
        OkulEntities db = new OkulEntities();
        public ActionResult Index()
        {
            var dersler = db.DERSLER.ToList();
            return View(dersler);
        }
        public ActionResult YeniDers()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDers(DERSLER ders)
        {
            db.DERSLER.Add(ders);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public ActionResult Sil(int id)
        {
            var ders = db.DERSLER.Find(id);
            db.DERSLER.Remove(ders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var ders = db.DERSLER.Find(id);
            return View("Guncelle", ders);
        }
        [HttpPost]
        public ActionResult Guncelle(DERSLER k)
        {
            var ders = db.DERSLER.Find(k.DERSID);
            ders.DERSAD = k.DERSAD;
            return RedirectToAction("Index", "Home");
        }
    }
}