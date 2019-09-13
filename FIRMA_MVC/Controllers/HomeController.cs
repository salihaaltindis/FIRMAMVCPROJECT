using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;
namespace FIRMA_MVC.Controllers
{
    public class HomeController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        public ActionResult Index()
        {
            ViewData["slider"] = db.SLIDERs.Where(s => s.DURUMU == true).ToList();
            return View();
        }

        public ActionResult Kurumsal()
        {

            ViewData["sayfa"] = db.SAYFAs.Where(s=>s.BASLIK == "kurumsal").FirstOrDefault();
            return View();
        }

        public ActionResult Iletisim()
        {

            ViewData["sayfa"] = db.SAYFAs.Where(s => s.BASLIK == "iletisim").FirstOrDefault();
            return View("Kurumsal");
        }
    }
}
