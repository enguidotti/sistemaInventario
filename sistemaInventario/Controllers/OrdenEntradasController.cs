using sistemaInventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistemaInventario.Controllers
{
    public class OrdenEntradasController : Controller
    {
        private db_inventarioEntities db = new db_inventarioEntities();
        // GET: OrdenEntradas
        public ActionResult OrdenEntrada()
        {
            return View();
        }
        //vertifica la existencia del un producto en base a su código 
        [HttpPost]
        public JsonResult CodigoExiste(string codigo)
        {
            //consulta que pregunta si el código existe en la tabla producto
            //db.Producto.Find(id)<-busca por id del producto, entrega un registro
            //db.Producto.FirstOrDefault<-buscar por el campo que se desea, entrega un registro
            //db.Producto.Where<- busca por el campo que se desea, entrega n registra(una lista)
            var producto = db.Producto.FirstOrDefault(p => p.codigo == codigo);
            if(producto != null)
            {
                return Json(new { producto.id_producto, producto.nombre },JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }
        [HttpPost]
        public JsonResult GuardarOrden(OrdenEntrada orden,List<DetalleEntrada> detalle)
        {
            orden.fecha = DateTime.Now;
            orden.id_user = 1;//luego se debe cambiar por el usuario logeado en el sistema 
            db.OrdenEntrada.Add(orden);
            db.SaveChanges();
            //capturar el id autoincrementable que se crea al guardar el registro
            var id_entrada = orden.id_entrada;

            //con foreach recorremos la lista de detalle para su guardado
            foreach (var item in detalle)
            {
                item.id_entrada = id_entrada;
                db.DetalleEntrada.Add(item);
                db.SaveChanges();
            }

            return Json("");
        }
    }
}