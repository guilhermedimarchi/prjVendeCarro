using prjVendeCarro.DAL;
using prjVendeCarro.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjVendeCarro.Models;
using System.Data.SqlClient;

namespace prjVendeCarro.BL
{
    public class CidadeBL : GenericBL
    {
        public List<CidadeModels> GetData()
        {
            List<Model> models = base.GetData();
            List<CidadeModels> list = new List<CidadeModels>();
            models.ForEach(row => list.Add((CidadeModels)row));
            return list;
        }
        public List<CidadeModels> GetDataByEstado(EstadoModels estado)
        {
            return GetDataByEstado(estado.Id);
        }
        public List<CidadeModels> GetDataByEstado(int idEstado)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idEstado", idEstado));
            List<Model> models = base.dal.GetByQuery(string.Format("SELECT {0} FROM {1} WHERE idEstado = @idEstado", dal.Columns, dal.Table), parameters);
            List<CidadeModels> list = new List<CidadeModels>();
            models.ForEach(row => list.Add((CidadeModels)row));
            return list;
        }
    }
    public class EstadoBL : GenericBL
    {
        public List<EstadoModels> GetData()
        {
            List<Model> models = base.GetData();
            List<EstadoModels> list = new List<EstadoModels>();
            models.ForEach(row => list.Add((EstadoModels)row));
            return list;
        }
    }
}