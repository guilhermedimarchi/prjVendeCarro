using prjVendeCarro.BL;
using prjVendeCarro.DAL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjVendeCarro.BL
{
    public class UsuarioBL : GenericBL
    {
        public List<UsuarioModels> GetData()
        {
            List<Model> models = base.GetData();
            List<UsuarioModels> list = new List<UsuarioModels>();
            models.ForEach(row => list.Add((UsuarioModels)row));
            return list;
        }
        public UsuarioModels GetByLogin(string Login)
        {
            return (UsuarioModels) dal.GetBy("Login", Login);
        }
    }
    public class UsuarioTipoBL : GenericBL
    {
        public List<UsuarioTipoModels> GetData()
        {
            List<Model> models = base.GetData();
            List<UsuarioTipoModels> list = new List<UsuarioTipoModels>();
            models.ForEach(row => list.Add((UsuarioTipoModels)row));
            return list;
        }
    }
}