using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjVendeCarro.DAL
{
    public class MarcaDAL : GenericDAL
    {
        public override void setTable()
        {
            base.Table = "Marca";
        }
        public override void setColumns()
        {
            addColumn("Id");
            addColumn("Nome");
        }
    }
    public class ModeloDAL : GenericDAL
    {
        public override void setTable()
        {
            base.Table = "Modelo";
        }
        public override void setColumns()
        {
            addColumn("Id");
            addColumn("Nome");
            addColumn("idMarca");
        }
    }
    public class CombustivelDAL : GenericDAL
    {
        public override void setTable()
        {
            base.Table = "Combustivel";
        }
        public override void setColumns()
        {
            addColumn("Id");
            addColumn("Nome");
        }
    }
    public class CarroDAL : GenericDAL
    {
        public override void setTable()
        {
            base.Table = "Carro";
        }
        public override void setColumns()
        {
            addColumn("Id");
            addColumn("Nome");
            addColumn("idUsuario");
            addColumn("idModelo");
            addColumn("idCombustivel");
            addColumn("idCidade");
            addColumn("Ano");
            addColumn("Cor");
            addColumn("Km");
            addColumn("QtdePorta");
            addColumn("Cambio", "strCambio");
            addColumn("Versao");
            addColumn("Preco");
            addColumn("Detalhe");
        }
    }
}