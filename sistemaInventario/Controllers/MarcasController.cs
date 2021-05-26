using sistemaInventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistemaInventario.Controllers
{
    public class MarcasController : Controller
    {
        private db_inventarioEntities db = new db_inventarioEntities();

        //método para accededr a crear marca
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Marca marca)
        {
            //insert into Marca('nombre','descripcion') values ('nombre','descripcion')
            db.Marca.Add(marca);
            //guarda cambios en la base de datos
            db.SaveChanges();
            return View();
        }
        //lista de marcas
        public ActionResult Index()
        {
            //lista de todas las marcas
            var marcas = db.Marca.ToList();//select * from marca
            return View(marcas);
        }
    }
}