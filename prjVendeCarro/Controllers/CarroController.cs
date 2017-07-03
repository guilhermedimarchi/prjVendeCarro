using prjVendeCarro.Controllers;
using prjVendeCarro.BL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;

namespace prjVendeCarro.Controllers
{
    public class CarroController : Controller
    {
       
        // GET: Carro
        #region Index
        public ActionResult Index()
        {
            ViewBag.Cidades = SelectListModels.Cidades(0);
            ViewBag.Estados = SelectListModels.Estados;
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this);
            return View(new CarroBL().GetData());
        }
        #endregion

        #region List
        [Authorize]
        public ActionResult List()
        {
            UsuarioModels usuario = new UsuarioModels(User.Identity.Name);
            ViewBag.Cidades = SelectListModels.Cidades(0);
            ViewBag.Estados = SelectListModels.Estados;
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this);
            
            return View(new CarroBL().GetCarrosByUsuario(usuario));
        }
        #endregion
        // GET: Carro/Details/5
        #region Details
        public ActionResult Details(int id)
        {
            ViewBag.Fotos = GetFotos(id);
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Details", id);
            CarroModels carro = new CarroModels(id);
            bool vendedor = carro.Usuario.Login.Equals(User.Identity.Name);
            ViewBag.vendedor = vendedor;
            try
            {
                if (!vendedor && Session["visitado" + id] == null)
                {
                    Session["visitado" + id] = true;
                    VisitadoModels visitado = new VisitadoModels(carro);
                    visitado.Insert();
                }
            }
            catch
            {

            }
            return View(carro);
        }
        #endregion

        // GET: Carro/Create
        #region Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Marcas = SelectListModels.Marcas;
            ViewBag.Modelos = SelectListModels.Modelos(0);
            ViewBag.Combustiveis = SelectListModels.Combustiveis;
            ViewBag.Cidades = SelectListModels.Cidades(0);
            ViewBag.Estados = SelectListModels.Estados;
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Create");
            CarroModels carro = new CarroModels();
            carro.Usuario = new UsuarioModels(User.Identity.Name);
            return View(carro);
        }

        // POST: Carro/Create
        [HttpPost]
        public ActionResult Create(CarroModels carro, List<HttpPostedFileBase> files)
        {
            try
            {   
                carro.Insert();
                 
                if (!string.IsNullOrWhiteSpace(carro.Foto))
                {
                    string path = HttpContext.Server.MapPath("~/Images/Fotos/temp/" + carro.Foto);
                    if (Directory.Exists(path))
                    {
                        foreach (string file in Directory.GetFiles(path))
                        {
                            System.IO.File.Delete(file);
                        }
                        Directory.Delete(path);
                    }
                    if (files != null)
                    {
                        foreach (HttpPostedFileBase file in files)
                        {
                            if (file == null)
                                break;
                            FotoModels foto = new FotoModels();
                            foto.Carro = carro;
                            string ext = Path.GetExtension(file.FileName);
                            path = HttpContext.Server.MapPath("~/Images/Fotos/" + carro.Id);
                            int ultimo = 0;
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                                foto.Tipo = "P";
                            }
                            else
                            {
                                foto.Tipo = "S";
                                string temppath = Directory.GetFiles(path).Last();
                                string ex = Path.GetExtension(temppath);
                                ultimo = Convert.ToInt32(Path.GetFileName(temppath).Replace(ex, ""));
                            }
                            string imagem = string.Format("{0}{1}", ultimo + 1, ext);
                            string caminho = string.Format("{0}\\{1}", path, imagem);
                            foto.Nome = carro.Id + "_" + ultimo;
                            foto.Local = carro.Id + "/" + imagem;
                            foto.Insert();
                            file.SaveAs(caminho);
                        }
                    }
                }   
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Carro/Edit/5
        #region Edit
        [Authorize]
        public ActionResult Edit(int id)
        {
            CarroModels carro = new CarroModels(id);
            carro.idEstado = carro.Cidade.idEstado;
            carro.idMarca = carro.Modelo.idMarca;
            if (!carro.Usuario.Login.Equals(User.Identity.Name))
            {
                Redirect("/Carro");
            }
            ViewBag.Marcas = SelectListModels.Marcas;
            ViewBag.Modelos = SelectListModels.Modelos(carro.idMarca);
            ViewBag.Combustiveis = SelectListModels.Combustiveis;
            ViewBag.Cidades = SelectListModels.Cidades(carro.idEstado);
            ViewBag.Estados = SelectListModels.Estados;
            ViewBag.Fotos = SelectListModels.Fotos(id);
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Edit", id);
            return View(carro);
        }

        // POST: Carro/Edit/5
        [HttpPost]
        public ActionResult Edit(CarroModels carro, List<HttpPostedFileBase> files, List<string> removeFoto)
        {
            try
            {
                carro.Update();
                if (!string.IsNullOrWhiteSpace(carro.Foto))
                {
                    string path = HttpContext.Server.MapPath("~/Images/Fotos/temp/" + carro.Foto);
                    if (Directory.Exists(path))
                    {
                        foreach (string file in Directory.GetFiles(path))
                        {
                            System.IO.File.Delete(file);
                        }
                        Directory.Delete(path);
                    }
                }
                if (removeFoto != null && removeFoto.Count > 0)
                {
                    string path = HttpContext.Server.MapPath("~/Images/Fotos/");
                    if (Directory.Exists(path))
                    {
                        foreach (string file in removeFoto)
                        {
                            string strfoto = file.Replace("img-", "");
                            FotoModels foto = new FotoModels(strfoto);
                            strfoto = "\\" + foto.Local.Replace("/", "\\");
                            if (System.IO.File.Exists(path + strfoto))
                            {
                                System.IO.File.Delete(path + strfoto);
                            }
                            foto.Delete();
                        }
                    }
                }
                if (files != null)
                {
                    foreach (HttpPostedFileBase file in files)
                    {
                        if (file == null)
                            break;
                        FotoModels foto = new FotoModels();
                        foto.Carro = carro;
                        string ext = Path.GetExtension(file.FileName);
                        string path = HttpContext.Server.MapPath("~/Images/Fotos/" + carro.Id);
                        int ultimo = 0;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                            foto.Tipo = "P";
                        }
                        else
                        {
                            string temppath;
                            foreach (string x in Directory.GetFiles(path).Reverse())
                            {
                                try
                                {
                                    temppath = x;
                                    string ex = Path.GetExtension(x);
                                    ultimo = Convert.ToInt32(Path.GetFileName(temppath).Replace(ex, ""));
                                    break;
                                }
                                catch
                                {

                                }
                            }
                            foto.Tipo = "S";
                        }
                        string imagem = string.Format("{0}{1}", ultimo + 1, ext);
                        string caminho = string.Format("{0}\\{1}", path, imagem);
                        foto.Nome = carro.Id + "_" + ultimo;
                        foto.Local = carro.Id + "/" + imagem;
                        foto.Insert();
                        file.SaveAs(caminho);
                    }
                }   
                
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        // GET: Carro/Delete/5
        #region Delete
        [Authorize]
        public ActionResult Delete(int id)
        {
            ViewBag.Breadcrumb = new Breadcrumb().GetBreadcrumb(this, "Delete", id);
            CarroModels carro = new CarroModels(id);
            if (!carro.Usuario.Login.Equals(User.Identity.Name))
            {
                Redirect("/Carro");
            }
            return PartialView(carro);
        }

        // POST: Carro/Delete/5
        [HttpPost]
        public ActionResult Delete(CarroModels carro)
        {
            //CarroBL c = new CarroBL();
            //carro = (CarroModels)c.GetById(carro.Id);
            try
            {
                VisitadoBL v = new VisitadoBL();
                v.Delete(carro);

                FotoBL f = new FotoBL();

                List<FotoModels> lst = f.GetFotosByCarroM(carro.Id);
                foreach (FotoModels a in lst)
                    f.Delete(a);


                carro.Delete();
                return RedirectToAction("Index");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        #endregion

        #region GetModelos
        [HttpPost]
        public ActionResult GetModelos(int idMarca)
        {
            SelectList modelos = SelectListModels.Modelos(idMarca);
           return Json(modelos, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetMarcas
        [HttpPost]
        public ActionResult GetMarcas()
        {
            SelectList marcas = SelectListModels.Marcas;
            return Json(marcas, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetCidades
        [HttpPost]
        public ActionResult GetCidades(int idEstado)
        {
            SelectList cidades = SelectListModels.Cidades(idEstado);
            return Json(cidades, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetEstados
        [HttpPost]
        public ActionResult GetEstados()
        {
            SelectList estados = SelectListModels.Estados;
            return Json(estados, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UploadFoto
        [HttpPost]
        public ActionResult UploadFoto()
        {
            
            string ext;
            ext = Path.GetExtension(Request.Files[0].FileName);
            string nome = new Random().Next(10000000, 99999999).ToString();
            int i = 0;
            if (string.Equals(Request.Files.Keys[0], "_0"))
            {
                i = Request.Files.Count - 1;
                while (Directory.Exists(HttpContext.Server.MapPath("~/Images/Fotos/temp/" + nome)))
                {
                    nome = new Random().Next(10000000, 99999999).ToString();
                }
                Directory.CreateDirectory(HttpContext.Server.MapPath("~/Images/Fotos/temp/" + nome));
            }
            else
            {
                string[] temp = Request.Files.Keys[0].Split('_');
                i = Convert.ToInt32(temp[1]) + 1;
                nome = temp[0];
            }
            string imgs = "";
            //if (fotos != null)
            //{
            //    foreach (string foto in fotos)
            //    {
            //        i = Convert.ToInt32(foto.Split('_')[1]);
            //        imgs += string.Format("<div><button type='button' onclick='remove({0})'>Remover</button><img src='/Images/Fotos/{1}' width='200'></div>", i, foto);
            //    }
            //}
            //for (int j = 0; j < Request.Files.Count; j++)
            //{
                HttpPostedFileBase file = Request.Files[0];
                string imagem = string.Format("temp_{0}{1}", i, Path.GetExtension(file.FileName));
                //fotos.Add(imagem);
                string caminho = string.Format("{0}\\{1}\\{2}", HttpContext.Server.MapPath("~/Images/Fotos/temp"), nome, imagem);
                file.SaveAs(caminho);
                imgs += string.Format("<div style='margin-top:10px;'><button class='remove' type='button' onclick='this.parentNode.remove(); document.getElementById({0}).remove();'>Remover</button><img data-num='{0}' data-nome='{1}' src='/Images/Fotos/temp/{1}/{2}' width='200' height='120'></div>", i, nome, imagem);
                i++;
            //}
            //Session["tempFoto"] = fotos;
            return Json(new { nome = nome, imgs = imgs, num = i }, JsonRequestBehavior.AllowGet);
            //return Json("/Images/Fotos/" + imagem, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region RemoverFoto
        [HttpPost]
        public ActionResult RemoveFotos(string foto)
        {

            string path = HttpContext.Server.MapPath("~/Images/Fotos/temp/" + foto);
            if (!string.IsNullOrWhiteSpace(foto) && Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    System.IO.File.Delete(file);
                }
                Directory.Delete(path);
            }
            
            return View();
        }
        #endregion

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


            return lst[""+id+"_0"];
        }




        #endregion
    }
}
