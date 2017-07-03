using prjVendeCarro.BL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjVendeCarro.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estado
        #region Index
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this);
            return View(new EstadoBL().GetData());
        }
        #endregion

        // GET: Estado/Details/5
        #region Details
        public ActionResult Details(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Details", id);
            EstadoModels estado = new EstadoModels(id);
            return View(estado);
        }
        #endregion

        // GET: Estado/Create
        #region Create
        public ActionResult Create()
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Create");
            EstadoModels estado = new EstadoModels();
            return View(estado);
        }

        // POST: Estado/Create
        [HttpPost]
        public ActionResult Create(EstadoModels estado)
        {
            try
            {
                estado.Insert();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Estado/Edit/5
        #region Edit
        public ActionResult Edit(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Edit", id);
            EstadoModels estado = new EstadoModels(id);
            return View(estado);
        }

        // POST: Estado/Edit/5
        [HttpPost]
        public ActionResult Edit(EstadoModels estado)
        {
            try
            {
                //EstadoModels estado = new EstadoModels(collection);
                estado.Update();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Estado/Delete/5
        #region Delete
        public ActionResult Delete(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Delete", id);
            EstadoModels estado = new EstadoModels(id);
            return View(estado);
        }

        // POST: Estado/Delete/5
        [HttpPost]
        public ActionResult Delete(EstadoModels estado)
        {
            try
            {
                estado.Delete();
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
