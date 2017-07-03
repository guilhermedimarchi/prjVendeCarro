using prjVendeCarro.DAL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjVendeCarro.BL
{
    public class VisitadoBL : GenericBL
    {
        public List<VisitadoModels> GetData()
        {
            List<Model> models = dal.GetByQuery(string.Format("SELECT {0} FROM {1} ORDER BY Visitados DESC", dal.Columns, dal.Table));
            List<VisitadoModels> list = new List<VisitadoModels>();
            models.ForEach(row => list.Add((VisitadoModels)row));
            return list;
        }
        public void Insert(Model model, bool x)
        {
            ((VisitadoDAL)dal).Insert(model, x);
        }
        public VisitadoModels GetByCarro(int idCarro)
        {
            return (VisitadoModels)dal.GetBy("idCarro", idCarro);
        }
        public VisitadoModels GetByCarro(CarroModels carro)
        {
            return (VisitadoModels)dal.GetBy("idCarro", carro.Id);
        }
    }
}