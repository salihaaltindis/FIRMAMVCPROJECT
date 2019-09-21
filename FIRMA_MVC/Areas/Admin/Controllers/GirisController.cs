using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FIRMA_MVC.Models;
namespace FIRMA_MVC.Areas.Admin.Controllers
{
    public class GirisController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Giris
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult CheckUser(string KULLANICI_ADI, string PAROLA)//INDEXTE YAZANIN ID NIN AYNISI OLMAK ZORUNDA.
        {
            KULLANICI kullanici = db.KULLANICIs.Where(k => k.KULLANICI_ADI == KULLANICI_ADI && k.PAROLA == PAROLA).SingleOrDefault();//eğer kullanıcı gelmiyorsa null olacak.
            if (kullanici != null)
            {

                FormsAuthentication.SetAuthCookie(KULLANICI_ADI, false);//giriş yapıldığı cookide tutulmasın diye false dedik.
                return RedirectToAction("index", "urunler");
            }
            else
            {
                return RedirectToAction("index");
            }
        }
        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index");
        }


    }
}
