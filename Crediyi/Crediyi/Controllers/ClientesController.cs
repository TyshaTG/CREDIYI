using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Crediyi.Models;

namespace Crediyi.Controllers
{
    public class ClientesController : Controller
    {

        CrediYiEntities Context = new CrediYiEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListarClientes()
        {
            try
            {
                List<Clientes> Lclientes = (from cc in Context.Clientes
                                            select cc).ToList();

                if (Lclientes.Count >= 1)
                {
                    return View(Lclientes.ToList());
                }
                else 
                    throw new Exception("No hay Clientes para mostrar");
                
            }
            catch (Exception )
            {

                ViewBag.Mesaje = "Error: No se pudieron listar los datos de clientes."; //ex.Message;
                return View(new List<CompraCabezal>());
            }
        }


        [HttpGet]
        public ActionResult ReporteDeCompras(int? NumDoc)
        {
            try
            {
                List<CompraCabezal> CompraxCli = (from compra in Context.CompraCabezal
                                                  where compra.Clientes.NumDoc == NumDoc.Value
                                                  orderby compra.Fecha 
                                                  select compra).ToList();

                if (CompraxCli.Count >= 1)
                {
                    return View(CompraxCli.ToList());
                }

                else
                {
                    ViewBag.Mesaje = "No hay Compras para dicha CI";
                    return View(new List<CompraCabezal>());
                }
            }
            catch (Exception )
            {

                ViewBag.Mesaje = "Error: No se pudo listar los Datos para dicha CI."; //ex.Message;
                return View(new List<CompraCabezal>());
            }

        }



        [HttpGet]
        public ActionResult DetalleLineas(string Serie, int Numero)
        {
            try
            {
                List<LineaCabezal> Dlineas = (from linea in Context.LineaCabezal
                                              where linea.Serie == Serie && linea.Numero == Numero
                                              select linea).ToList();

                if (Dlineas.Count >= 1)
                {
                    return View(Dlineas.ToList());
                }

                else
                {
                    ViewBag.Mesaje = "No hay Lineas para dicha Compra";
                    return View(new List<LineaCabezal>());
                }
                   
            }
            catch (Exception )
            {
                ViewBag.Mesaje = "Error: No se pudo obtener los Datos Detallados."; // ex.Message;
                return View(new List<LineaCabezal>());
            }
        }


        [HttpGet]
        public ActionResult ReporteDeComprasGenerico()
        {
            try
            {
                List<CompraCabezal> Compra = (from compra in Context.CompraCabezal
                                              orderby compra.Fecha
                                              select compra).ToList();

                if (Compra.Count >= 1)
                {
                    return View(Compra.ToList());
                }

                else
                {
                    ViewBag.Mesaje = "No hay Compras para visualizar.";
                    return View(new List<CompraCabezal>());
                }
            }
            catch (Exception)
            {

                ViewBag.Mesaje = "Error: No se pudo listar los Datos."; //ex.Message;
                return View(new List<CompraCabezal>());
            }

        }


        [HttpGet]
        public ActionResult DetalleLineasTotal(string Serie, int Numero)
        {
            try
            {
                List<LineaCabezal> Dlineas = (from linea in Context.LineaCabezal
                                              where linea.Serie == Serie && linea.Numero == Numero
                                              select linea).ToList();

                if (Dlineas.Count >= 1)
                {
                    return View(Dlineas.ToList());
                }

                else
                {
                    ViewBag.Mesaje = "No hay Lineas para dicha Compra";
                    return View(new List<LineaCabezal>());
                }

            }
            catch (Exception)
            {
                ViewBag.Mesaje = "Error: No se pudo obtener los Datos Detallados."; // ex.Message;
                return View(new List<LineaCabezal>());
            }
        }



    }
}