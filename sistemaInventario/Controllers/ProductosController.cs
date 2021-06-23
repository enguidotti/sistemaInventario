using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistemaInventario.Models;

namespace sistemaInventario.Controllers
{
    public class ProductosController : Controller
    {
        private db_inventarioEntities db = new db_inventarioEntities();
        public ActionResult Create()
        {
            //sirve para llenar un select a través del helper Html.DropDownList
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "nombre");
            //select * from marca m order by m.nombre desc
            ViewBag.marca = new SelectList(db.Marca.OrderBy(m=>m.nombre), "id_marca", "nombre");
            ViewBag.proveedor = new SelectList(db.Proveedor, "id_proveedor", "nombre");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Producto producto, int marca, int proveedor)
        {
            producto.id_marca = marca;
            producto.id_proveedor = proveedor;
            db.Producto.Add(producto);
            db.SaveChanges();

            //se vuelven a cargar la lista con datos
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "nombre");
            ViewBag.marca = new SelectList(db.Marca.OrderBy(m => m.nombre), "id_marca", "nombre");
            ViewBag.proveedor = new SelectList(db.Proveedor, "id_proveedor", "nombre");
            return View();
        }
        //método para verificar si el código del producto ya esta ingresado
        [HttpPost]
        public ActionResult CodigoExiste(string codigo)
        {
            //pregunta si el string es distinto de nullo vacío
            if (!string.IsNullOrEmpty(codigo))
            {
                //select * from producto p where LOWER(p.codigo) = LOWER(codigo)
                var q = db.Producto.FirstOrDefault(p=>p.codigo.ToLower().Equals(codigo.ToLower()));
                if(q != null)
                {
                    string nombre = q.nombre;//devuelve el nombre del producto cuyo código esta ingresado
                    return Json(nombre);
                }
            }
            return Json("");
        }
        public ActionResult Index()
        {
            //viewbag para los select
            ViewBag.proveedor = new SelectList(db.Proveedor, "id_proveedor", "nombre");
            ViewBag.marca = new SelectList(db.Marca, "id_marca", "nombre");
            ViewBag.categoria = new SelectList(db.Categoria, "id_categoria", "nombre");
            return View();
        }

        public ActionResult Edit(int id)
        {
            //consulta el detalla del producto en base al id 
            var producto = db.Producto.Find(id);
            if(producto != null)
            {
                ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "nombre", producto.id_categoria);
                ViewBag.id_marca = new SelectList(db.Marca, "id_marca", "nombre", producto.id_marca);
                ViewBag.id_proveedor = new SelectList(db.Proveedor, "id_proveedor", "nombre", producto.id_proveedor);
                return PartialView("_Edit",producto);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Producto producto)
        {
            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();
            return Json("ok");
        }
        //buscar según filtros
        public ActionResult ListarProductos(int? proveedor, int? marca, int? categoria)
        {
            //retornar todos los productos
            var productos = db.Producto.ToList();
            if (proveedor != null)
                productos = productos.Where(p => p.id_proveedor == proveedor).ToList();

            if (marca != null)
                productos = productos.Where(p => p.id_marca == marca).ToList();

            if (categoria != null)
                productos = productos.Where(p => p.id_categoria == categoria).ToList();

            return PartialView("_ListarProductos",productos);
        }
    }
}