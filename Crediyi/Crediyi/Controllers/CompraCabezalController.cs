using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/////////////////////////
using Crediyi.Models;
/////////////////////////


namespace Crediyi.Controllers
{
    public class CompraCabezalController : Controller
    {
        CrediYiEntities Context = new CrediYiEntities();

        // GET: CompraCabezal
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AltaCompraCabezal()
        {
            try
            {
                ViewBag.ListaClientes = new SelectList(Context.Clientes.ToList(), "IdCliente", "Nombre");

                return View();
            }
            catch (Exception )
            {
                List<Clientes> _Lista = new List<Clientes>();
                ViewBag.Mensaje = "No se pudo cargar el Formulario."; //ex.Message;
                ViewBag.ListaClientes = new SelectList(Context.Clientes.ToList(), "IdCliente", "Nombre");
                return View();
            }
        }


        [HttpPost]
        public ActionResult AltaCompraCabezal(CompraCabezal CC) 
        {
            try
            {
                CC.Clientes = Context.Clientes.Where(c => c.IdCliente == CC.IdCliente).FirstOrDefault();

                if (CC.Clientes == null)
                    {  throw new Exception("El Cliente especificado no existe");  }
                else
                {
                    CC.Fecha = DateTime.Now;

                    Session["CC"] = CC;

                    //Valido el modelo
                    CC.Validar();

                    Context.CompraCabezal.Add(CC);

                    Context.SaveChanges();

                    ViewBag.ListaClientes = new SelectList(Context.Clientes.ToList(), "IdCliente", "Nombre");
                    return RedirectToAction("AltaLineaCabezal", "LineaCabezal");
                
                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message; // "No se pudo dar de alta el Cabezal de Compra."; //ex.Message;
                Context.Entry(CC).State = System.Data.Entity.EntityState.Detached;
                ViewBag.ListaClientes = new SelectList(Context.Clientes.ToList(), "IdCliente", "Nombre");
                return View();
            }
        }



 


    }
}