using prjVendeCarro.BL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjVendeCarro.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        #region Index
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this);
            return View(new UsuarioBL().GetData());
        }
        #endregion

        // GET: Usuario/Details/5
        #region Details
        public ActionResult Details(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Details", id);
            UsuarioModels usuario = new UsuarioModels(id);
            return View(usuario);
        }
        #endregion

        // GET: Usuario/Create
        #region Create
        public ActionResult Create()
        {
            ViewBag.UsuarioTipos = SelectListModels.UsuarioTipos;
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Create");
            UsuarioModels usuario = new UsuarioModels();
            return View(usuario);
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(UsuarioModels usuario)
        {
            try
            {
                usuario.Insert();
                
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Usuario/Edit/5
        #region Edit
        public ActionResult Edit(int id)
        {
            ViewBag.UsuarioTipos = SelectListModels.UsuarioTipos;
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Edit", id);
            UsuarioModels usuario = new UsuarioModels(id);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(UsuarioModels usuario)
        {
            try
            {
                usuario.Update();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Usuario/Delete/5
        #region Delete
        public ActionResult Delete(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Delete", id);
            UsuarioModels usuario = new UsuarioModels(id);
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(UsuarioModels usuario)
        {
            try
            {
                usuario.Delete();
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
