using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Controllers
{
    public class MesajController : Controller
    {
        // GET: Mesaj
        FIRMAMODEL db = new FIRMAMODEL();
        public ActionResult Goster(string m)
        {

            return View(m);
        }
    }
}
