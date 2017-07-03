using prjVendeCarro.BL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjVendeCarro.DAL
{
    public class VisitadoDAL : GenericDAL
    {
        public override void setTable()
        {
            base.Table = "Visitado";
        }

        public override void setColumns()
        {
            addColumn("Id");
            addColumn("idCarro");
            addColumn("Visitados");
        }
        public void Insert(Model model, bool x)
        {
            VisitadoModels mod = model as VisitadoModels;
            if (!x && mod.Carro.Id <= 0)
            {
                VisitadoBL bl = new VisitadoBL();
                mod = bl.GetByCarro(mod.Carro);
            }
            if (mod.Id <= 0)
            {
                Dictionary<string, object> listColumnsValues = new Dictionary<string, object>();
                listColumnsValues.Add("idCarro", mod.idCarro);
                Dictionary<string, string> listColumnsSql = new Dictionary<string, string>();
                listColumnsSql.Add("Visitados", "1");
                base.InsertByQuery(mod, listColumnsValues, listColumnsSql);
            }
            else
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", mod.Id));
                base.ActionByQuery(string.Format("UPDATE {0} SET Visitados = Visitados+1 WHERE Id = @Id", Table), parameters);
            }
        }
    }
}