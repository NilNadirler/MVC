using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonelMVCUI.Controllers
{
    public class SecurityController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanicilar kullanici)
        {

            var kullanicinDb = db.Kullanicilar.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Sifre == kullanici.Sifre);
            if (kullanicinDb != null)
            {
                FormsAuthentication.SetAuthCookie(kullanicinDb.Ad, false);
                return View("Index", "Departman");
            }
            else
            {
                ViewBag.Mesaj = "Gecersiz Kullanici Adi ve sifre";
                return View();
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }

        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(Kullanicilar kullanici)
        {
            var kullanicilar = db.Kullanicilar.FirstOrDefault(i => i.Ad == kullanici.Ad);
            if (kullanicilar!=null)
            {
                return View();
            }
            FormsAuthentication.SetAuthCookie(kullanicilar.Ad, false);
            return View("Login", "Security");
        }
    }
}