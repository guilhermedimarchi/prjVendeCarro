using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjVendeCarro.DAL
{
    public class FotoDAL : GenericDAL
    {
        public override void setTable()
        {
            base.Table = "Foto";
        }

        public override void setColumns()
        {
            addColumn("Id");
            addColumn("idCarro");
            addColumn("Nome");
            addColumn("Tipo");
            addColumn("Local");
            addColumn("Data");
        }
        public void Insert(Model mod, bool x)
        {
            FotoModels model = mod as FotoModels;
            Dictionary<string, object> listColumnsValues = new Dictionary<string, object>();
            listColumnsValues.Add("idCarro", model.idCarro);
            listColumnsValues.Add("Nome", model.Nome);
            listColumnsValues.Add("Tipo", model.Tipo);
            listColumnsValues.Add("Local", model.Local);
            Dictionary<string, string> listColumnsSql = new Dictionary<string, string>();
            listColumnsSql.Add("Data", "GETDATE()");
            base.InsertByQuery(model, listColumnsValues, listColumnsSql);
        }
    }
}