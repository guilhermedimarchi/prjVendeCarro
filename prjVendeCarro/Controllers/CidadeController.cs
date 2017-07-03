using prjVendeCarro.BL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjVendeCarro.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade
        #region Index
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this);
            return View(new CidadeBL().GetData());
        }
        #endregion

        // GET: Cidade/Details/5
        #region Details
        public ActionResult Details(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Details", id);
            CidadeModels cidade = new CidadeModels(id);
            return View(cidade);
        }
        #endregion

        // GET: Cidade/Create
        #region Create
        public ActionResult Create()
        {
            ViewBag.Estados = SelectListModels.Estados;
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Create");
            CidadeModels cidade = new CidadeModels();
            return View(cidade);
        }

        // POST: Cidade/Create
        [HttpPost]
        public ActionResult Create(CidadeModels cidade)
        {
            try
            {
                cidade.Insert();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Cidade/Edit/5
        #region Edit
        public ActionResult Edit(int id)
        {
            ViewBag.Estados = SelectListModels.Estados;
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Edit", id);
            CidadeModels cidade = new CidadeModels(id);
            return View(cidade);
        }

        // POST: Cidade/Edit/5
        [HttpPost]
        public ActionResult Edit(CidadeModels cidade)
        {
            try
            {
                cidade.Update();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Cidade/Delete/5
        #region Delete
        public ActionResult Delete(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Delete", id);
            CidadeModels cidade = new CidadeModels(id);
            return View(cidade);
        }

        // POST: Cidade/Delete/5
        [HttpPost]
        public ActionResult Delete(CidadeModels cidade)
        {
            try
            {
                cidade.Delete();
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
