using prjVendeCarro.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjVendeCarro.DAL
{
    public class CidadeDAL : GenericDAL
    {
        public override void setTable()
        {
            base.Table = "Cidade";
        }

        public override void setColumns()
        {
            addColumn("Id");
            addColumn("Nome");
            addColumn("idEstado");
        }
    }
    public class EstadoDAL : GenericDAL
    {
        public override void setTable()
        {
            base.Table = "Estado";
        }

        public override void setColumns()
        {
            addColumn("Id");
            addColumn("Nome");
        }
    }
}