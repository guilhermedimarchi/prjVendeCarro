using prjVendeCarro.BL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjVendeCarro.Controllers
{
    public class UsuarioTipoController : Controller
    {
        // GET: UsuarioTipo
        #region Index
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this);
            return View(new UsuarioTipoBL().GetData());
        }
        #endregion

        // GET: UsuarioTipo/Details/5
        #region Details
        public ActionResult Details(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Details", id);
            UsuarioTipoModels usuarioTipo = new UsuarioTipoModels(id);
            return View(usuarioTipo);
        }
        #endregion

        // GET: UsuarioTipo/Create
        #region Create
        public ActionResult Create()
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Create");
            UsuarioTipoModels usuarioTipo = new UsuarioTipoModels();
            return View(usuarioTipo);
        }

        // POST: UsuarioTipo/Create
        [HttpPost]
        public ActionResult Create(UsuarioTipoModels usuarioTipo)
        {
            try
            {
                usuarioTipo.Insert();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: UsuarioTipo/Edit/5
        #region Edit
        public ActionResult Edit(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Edit", id);
            UsuarioTipoModels usuarioTipo = new UsuarioTipoModels(id);
            return View(usuarioTipo);
        }

        // POST: UsuarioTipo/Edit/5
        [HttpPost]
        public ActionResult Edit(UsuarioTipoModels usuarioTipo)
        {
            try
            {
                usuarioTipo.Update();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: UsuarioTipo/Delete/5
        #region Delete
        public ActionResult Delete(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Delete", id);
            UsuarioTipoModels usuarioTipo = new UsuarioTipoModels(id);
            return View(usuarioTipo);
        }

        // POST: UsuarioTipo/Delete/5
        [HttpPost]
        public ActionResult Delete(UsuarioTipoModels usuarioTipo)
        {
            try
            {
                usuarioTipo.Delete();
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
