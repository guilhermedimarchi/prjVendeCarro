using prjVendeCarro.BL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjVendeCarro.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        #region Index
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this);
            return View(new MarcaBL().GetData());
        }
        #endregion

        // GET: Marca/Details/5
        #region Details
        public ActionResult Details(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Details", id);
            MarcaModels marca = new MarcaModels(id);
            return View(marca);
        }
        #endregion

        // GET: Marca/Create
        #region Create
        public ActionResult Create()
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Create");
            MarcaModels marca = new MarcaModels();
            return View(marca);
        }

        // POST: Marca/Create
        [HttpPost]
        public ActionResult Create(MarcaModels marca)
        {
            try
            {
                marca.Insert();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Marca/Edit/5
        #region Edit
        public ActionResult Edit(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Edit", id);
            MarcaModels marca = new MarcaModels(id);
            return View(marca);
        }

        // POST: Marca/Edit/5
        [HttpPost]
        public ActionResult Edit(MarcaModels marca)
        {
            try
            {
                marca.Update();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Marca/Delete/5
        #region Delete
        public ActionResult Delete(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Delete", id);
            MarcaModels marca = new MarcaModels(id);
            return View(marca);
        }

        // POST: Marca/Delete/5
        [HttpPost]
        public ActionResult Delete(MarcaModels marca)
        {
            try
            {
                marca.Delete();
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
