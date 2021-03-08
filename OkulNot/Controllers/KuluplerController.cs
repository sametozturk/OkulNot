using OkulNot.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OkulNot.Controllers
{
    public class KuluplerController : Controller
    {
        OkulEntities db = new OkulEntities();
        public ActionResult Index()
        {
            var kulup = db.KULUPLER.ToList();
            return View(kulup);
        }
        public ActionResult YeniKulup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKulup(KULUPLER kulup)
        {
            db.KULUPLER.Add(kulup);
            db.SaveChanges();
            return RedirectToAction("Index","Kulupler");
        }
        public ActionResult Sil(int id)
        {
            var kulup = db.KULUPLER.Find(id);
            db.KULUPLER.Remove(kulup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var kulup = db.KULUPLER.Find(id);
            return View("Guncelle",kulup);
        }
        [HttpPost]
        public ActionResult Guncelle(KULUPLER k)
        {
            var kulup = db.KULUPLER.Find(k.KULUPID);
            kulup.KULUPAD = k.KULUPAD;
            return RedirectToAction("Index","Kulupler");
        }
    }
}