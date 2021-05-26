using sistemaInventario.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistemaInventario.Controllers
{
    public class CategoriasController : Controller
    {
        private db_inventarioEntities db = new db_inventarioEntities();
        //crear datos
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Categoria categoria)
        {
            db.Categoria.Add(categoria);
            db.SaveChanges();
            return View();
        }
        //listar los datos
        public ActionResult Index()
        {
            var categoria = db.Categoria.ToList();
            return View(categoria);
        }
        //editar los datos
        public ActionResult Edit(int? id) // ? permite que el entero sea nulo 
        {
            if (id != null)
            {
                //select * from Categoria Where id_categoria = 1
                var categoria = db.Categoria.Find(id);
                if (categoria != null)
                {
                    return View(categoria);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                //redirectToAction sirve para redireccionar hacia otra página 
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Categoria categoria)
        {
            //UPDATE categoria SET nombre = 'categoria.nombre', descripcion = 'categoria.descripcion'
            //WHERE id_categoria = categoria.id_categoria

            db.Entry(categoria).State = EntityState.Modified;//forma para editar los registros
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if(id != null)
            {
                var categoria = db.Categoria.Find(id);
                if(categoria != null)
                {
                    return View(categoria);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id_categoria)
        {
            var categoria = db.Categoria.Find(id_categoria);
            if (categoria != null)
            {
                //Remove => DELETE FROM categoria WHERE id_categoria = 2
                db.Categoria.Remove(categoria);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}