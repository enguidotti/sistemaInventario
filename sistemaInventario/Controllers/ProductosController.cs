﻿using System;
using System.Collections.Generic;
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
            var productos = db.Producto.ToList();
            return View(productos);
        }
    }
}