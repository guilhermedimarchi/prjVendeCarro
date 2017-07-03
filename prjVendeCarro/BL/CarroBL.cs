using prjVendeCarro.DAL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjVendeCarro.BL
{
    public class CarroBL : GenericBL
    {
        public List<CarroModels> GetData()
        {
            List<Model> models = base.GetData();
            List<CarroModels> list = new List<CarroModels>();
            models.ForEach(row => list.Add((CarroModels)row));
            return list;
        }
        public void Insert(Model model)
        {
            base.Insert(model);

        }
        public List<CarroModels> GetCarrosByUsuario(UsuarioModels usuario)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@usuario", usuario.Id));
            List<Model> models = dal.GetByQuery(string.Format("SELECT {0} FROM {1} WHERE idUsuario = @usuario", dal.Columns, dal.Table), parameters);
            List<CarroModels> list = new List<CarroModels>();
            models.ForEach(row => list.Add((CarroModels)row));
            return list;
        }
    }
    public class MarcaBL : GenericBL
    {
        public List<MarcaModels> GetData()
        {
            List<Model> models = base.GetData();
            List<MarcaModels> list = new List<MarcaModels>();
            models.ForEach(row => list.Add((MarcaModels)row));
            return list;
        }
    }
    public class ModeloBL : GenericBL
    {
        public List<ModeloModels> GetData()
        {
            List<Model> models = base.GetData();
            List<ModeloModels> list = new List<ModeloModels>();
            models.ForEach(row => list.Add((ModeloModels)row));
            return list;
        }
        public List<ModeloModels> GetDataByMarca(MarcaModels marca)
        {
            return GetDataByMarca(marca.Id);
        }
        public List<ModeloModels> GetDataByMarca(int idMarca)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idMarca", idMarca));
            List<Model> models = base.dal.GetByQuery(string.Format("SELECT {0} FROM {1} WHERE idMarca = @idMarca", dal.Columns, dal.Table), parameters);
            List<ModeloModels> list = new List<ModeloModels>();
            models.ForEach(row => list.Add((ModeloModels)row));
            return list;
        }
    }
    public class CombustivelBL : GenericBL
    {
        public List<CombustivelModels> GetData()
        {
            List<Model> models = base.GetData();
            List<CombustivelModels> list = new List<CombustivelModels>();
            models.ForEach(row => list.Add((CombustivelModels)row));
            return list;
        }
    }
}