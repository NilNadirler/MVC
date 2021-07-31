using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonelMVCUI.ViewModels;
using System.Reflection;

namespace PersonelMVCUI.Controllers
{
    public class PersonelController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        public ActionResult Index()
        {
            var model = db.Personels.Include(x => x.Departmans).ToList();
            return View(model);
        }

        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel() {
                Departmanlar = db.Departmans.ToList(),
                Personeller=new Personels()
            };

            return View("PersonelForm", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(PersonelFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
           {

            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departmans.ToList(),
                Personeller= viewModel.Personeller
            };
            return View("PersonelForm",model);
           }

            if (viewModel.Personeller.Id == 0)
            {
                db.Personels.Add(viewModel.Personeller);
            }
            else
            {
                db.Entry(viewModel.Personeller).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departmans.ToList(),
                Personeller = db.Personels.Find(id)
            };
            return View("PersonelForm",model);
        }
        public ActionResult Sil(int id)
        {
            var silinecekPersonel = db.Personels.Find(id);
            if (silinecekPersonel==null)
            {
                return HttpNotFound();

            }
            db.Personels.Remove(silinecekPersonel);
            db.SaveChanges();

           
            return RedirectToAction("Index");

        }
    }
}