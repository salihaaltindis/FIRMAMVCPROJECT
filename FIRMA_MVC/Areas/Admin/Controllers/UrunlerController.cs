using FIRMA_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    public class UrunlerController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Urunler
        public ActionResult Index()
        {
            List<URUN> liste = db.URUNs.ToList();
            return View(liste);
        }
    }
}