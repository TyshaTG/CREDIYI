using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Crediyi.Models;

namespace Crediyi.Controllers
{
    public class LineaCabezalController : Controller
    {

        CrediYiEntities Context = new CrediYiEntities();

        // GET: LineaCabezal
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AltaLineaCabezal()
        {
            try
            {
                var CC = (CompraCabezal)Session["CC"];

                ViewBag.ListaSerie = CC.Serie;
                ViewBag.ListaNumero = CC.Numero;

                ViewBag.ListaProductos = new SelectList(Context.Productos.ToList(), "IdProd", "DscProd");

                return View();
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "No se pudo cargar el formulario.";  //ex.Message;
                ViewBag.ListaProductos = new SelectList(Context.Productos.ToList(), "IdProd", "DscProd");
                return View();
            }
        }


        [HttpPost]
        public ActionResult AltaLineaCabezal(LineaCabezal Lin)
        {
            try
            {
                var CC = (CompraCabezal)Session["CC"];

                Lin.Productos = Context.Productos.Where(p => p.IdProd == Lin.IdProd).FirstOrDefault();

                if (Lin.Productos == null)
                    { throw new Exception("El Producto especificado no existe"); }
                else
                {
                 
                    Lin.Serie = CC.Serie;
                    Lin.Numero = CC.Numero;

                    Lin.IdLinea = CC.LineaCabezal.Count + 1;

                    // Lin.impLinea = Lin.Cantidad * ((int)Lin.Productos.Importe);

                    //Valido el modelo
                    Lin.Validar();

                    Context.LineaCabezal.Add(Lin);
                    CC.LineaCabezal.Add(Lin);

                    Context.SaveChanges();

                    Session["CC"] = CC;

                    ViewBag.ListaProductos = new SelectList(Context.Productos.ToList(), "IdProd", "DscProd");

                    return RedirectToAction("AltaLineaCabezal", "LineaCabezal");

                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message; // "No se pudo dar de alta la Linea."; //ex.Message;
                Context.Entry(Lin).State = System.Data.Entity.EntityState.Detached;
                ViewBag.ListaProductos = new SelectList(Context.Productos.ToList(), "IdProd", "DscProd");
                return View();
            }
        }


    }
}