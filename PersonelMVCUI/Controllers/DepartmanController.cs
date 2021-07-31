using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonelMVCUI.Controllers
{
    public class DepartmanController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        // GET: Departman
        public ActionResult Index()
        {
           
            var model = db.Departmans.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            
            return View("DepartmanForm",new Departmans());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Departmans departmans)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }
            if(departmans.Id==0)
            {
                db.Departmans.Add(departmans);
            }
            else
            {
                var guncellenecekDepartman = db.Departmans.Find(departmans.Id);
                    if (guncellenecekDepartman==null)
                {
                    return HttpNotFound();
                }
                guncellenecekDepartman.Ad = departmans.Ad;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Departman");
        }
        public ActionResult Guncelle( int id)
        {
            var model = db.Departmans.Find(id);
            if (model == null)
                return HttpNotFound();
            return View("DepartmanForm",model);
        }
        public ActionResult Sil(int id)
        {
            var silincekDepartman = db.Departmans.Find(id);
            if (silincekDepartman==null)
            {
                return HttpNotFound();

            }
            db.Departmans.Remove(silincekDepartman);
           
            db.SaveChanges();
            return RedirectToAction("index");


        }
    }
}