using prjVendeCarro.DAL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjVendeCarro.BL
{
    public class FotoBL : GenericBL
    {
        public List<FotoModels> GetData()
        {
            List<Model> models = base.GetData();
            List<FotoModels> list = new List<FotoModels>();
            models.ForEach(row => list.Add((FotoModels)row));
            return list;
        }
        public FotoModels GetByNome(string Nome)
        {
            return (FotoModels)dal.GetBy("Nome", Nome);
        }
        public void Insert(Model model, bool x)
        {
            ((FotoDAL)dal).Insert(model, x);
        }
        public Dictionary<string, string> GetFotosByCarro(CarroModels carro)
        {
            return GetFotosByCarro(carro.Id);
        }
        public Dictionary<string, string> GetFotosByCarro(int idCarro)
        {
            Dictionary<string, string> paths = new Dictionary<string, string>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idCarro", idCarro));
            List<Model> models = dal.GetByQuery(
                string.Format("SELECT {0} FROM {1} WHERE idCarro = @idCarro",
                dal.Columns, dal.Table), parameters);
            models.ForEach(row =>
            {
                FotoModels foto = (FotoModels)row;
                paths.Add(foto.Nome, foto.Local);
            });

            return paths;
        }
        public List<FotoModels> GetFotosByCarroM(int idCarro)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idCarro", idCarro));
            List<Model> models = base.dal.GetByQuery(string.Format("SELECT {0} FROM {1} WHERE idCarro = @idCarro", dal.Columns, dal.Table), parameters);
            List<FotoModels> list = new List<FotoModels>();
            models.ForEach(row => list.Add((FotoModels)row));
            return list;
        }


    }
}