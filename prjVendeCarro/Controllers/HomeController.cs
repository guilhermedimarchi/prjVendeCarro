using prjVendeCarro.BL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace prjVendeCarro.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            VisitadoBL v = new VisitadoBL();
            List<VisitadoModels> lst = v.GetData();
            if (lst.Count >= 4)
            {
                ViewBag.car0 = lst[0].idCarro;
                ViewBag.car1 = lst[1].idCarro;
                ViewBag.car2 = lst[2].idCarro;
                ViewBag.car3 = lst[3].idCarro;
                ViewBag.Foto0 = GetFistFotos(lst[0].idCarro);
                ViewBag.Foto1 = GetFistFotos(lst[1].idCarro);
                ViewBag.Foto2 = GetFistFotos(lst[2].idCarro);
                ViewBag.Foto3 = GetFistFotos(lst[3].idCarro);
            }
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb();
            ViewBag.Message = "Your application description page."; 
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb();
            ViewBag.Message = "Your contact page.";
            return View();
        }


        #region GetFoto


        public List<string> GetFotos(int id)
        {
            FotoBL f = new FotoBL();
            Dictionary<string, string> lst = f.GetFotosByCarro(id);


            Dictionary<string, string> notfound = new Dictionary<string, string>
            {
                { "notfound", "notfound.jpg" }
            };
            if (lst.Count == 0)
                lst = notfound;


            List<string> d = lst.Values.ToList();

            return d;
        }



        public string GetFistFotos(int id)
        {
            FotoBL f = new FotoBL();
            Dictionary<string, string> lst = f.GetFotosByCarro(id);

            if (lst.Count == 0)
                return "notfound.jpg";


            return lst["" + id + "_0"];
        }




        #endregion


    }
}