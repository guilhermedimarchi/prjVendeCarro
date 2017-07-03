using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjVendeCarro.Controllers
{
    public class Breadcrumb
    {
        public string GetBreadcrumb(object classe = null)
        {
            Dictionary<string, string> breadcrumbs = new Dictionary<string, string>();
            breadcrumbs.Add("Home", "/");
            string breadcrumb = "";
            if (classe != null)
            {
                string nome = classe.GetType().Name.Replace("Controller", "");
                breadcrumbs.Add(nome, string.Format("{0}{1}", breadcrumbs["Home"], nome));
            }
            foreach (var item in breadcrumbs)
            {
                breadcrumb += string.Format("<li><a href='{1}'>{0}</a></li>", item.Key, item.Value);
            }
            return breadcrumb;
        }
        public string GetBreadcrumb(object classe, string action, object parameter = null)
        {
            string nome = classe.GetType().Name.Replace("Controller", "");
            Dictionary<string, string> breadcrumbs = new Dictionary<string, string>();
            breadcrumbs.Add("Home", "/");
            breadcrumbs.Add(nome, string.Format("{0}{1}", breadcrumbs["Home"], nome));
            breadcrumbs.Add(action, string.Format("{0}/{1}/{2}", breadcrumbs[nome], action, parameter == null ? "" : parameter));
            string breadcrumb = "";
            foreach (var item in breadcrumbs)
            {
                breadcrumb += string.Format("<li><a href='{1}'>{0}</a></li>", item.Key, item.Value);
            }
            return breadcrumb;
        }
    }
}