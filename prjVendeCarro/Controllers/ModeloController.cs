using prjVendeCarro.BL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjVendeCarro.Controllers
{
    public class ModeloController : Controller
    {
        // GET: Modelo
        #region Index
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this);
            return View(new ModeloBL().GetData());
        }
        #endregion

        // GET: Modelo/Details/5
        #region Details
        public ActionResult Details(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Details", id);
            ModeloModels modelo = new ModeloModels(id);
            return View(modelo);
        }
        #endregion

        // GET: Modelo/Create
        #region Create
        public ActionResult Create()
        {
            ViewBag.Marcas = SelectListModels.Marcas;
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Create");
            ModeloModels modelo = new ModeloModels();
            return View(modelo);
        }

        // POST: Modelo/Create
        [HttpPost]
        public ActionResult Create(ModeloModels modelo)
        {
            try
            {
                modelo.Insert();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Modelo/Edit/5
        #region Edit
        public ActionResult Edit(int id)
        {
            ViewBag.Marcas = SelectListModels.Marcas;
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Edit", id);
            ModeloModels modelo = new ModeloModels(id);
            return View(modelo);
        }

        // POST: Modelo/Edit/5
        [HttpPost]
        public ActionResult Edit(ModeloModels modelo)
        {
            try
            {
                modelo.Update();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Modelo/Delete/5
        #region Delete
        public ActionResult Delete(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Delete", id);
            ModeloModels modelo = new ModeloModels(id);
            return View(modelo);
        }

        // POST: Modelo/Delete/5
        [HttpPost]
        public ActionResult Delete(ModeloModels modelo)
        {
            try
            {
                modelo.Delete();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion
    }
}
