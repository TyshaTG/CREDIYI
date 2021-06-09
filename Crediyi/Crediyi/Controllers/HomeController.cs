using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Crediyi.Models;

namespace Crediyi.Controllers
{
    public class HomeController : Controller
    {
        CrediYiEntities Context = new CrediYiEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuPrincipal()
        {
            return View();
        }


        [HttpGet] 
        public ActionResult Logueo()
        { return View(); }


        [HttpPost] 
        public ActionResult Logueo(Usuarios U)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    U = Context.Usuarios.Where(usu => usu.Usu == U.Usu && usu.Pass == U.Pass).FirstOrDefault();
                    return RedirectToAction("MenuPrincipal", "Home");
                }
                else
                    return View();
            
            }
            catch (Exception )
            {
                ViewBag.Mesaje = "Error al intentar Loguearse."; // ex.Message;
                return View();
            }
        }


    }
}