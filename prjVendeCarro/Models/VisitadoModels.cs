using prjVendeCarro.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace prjVendeCarro.Models
{
    [Table("Combustivel")]
    public class VisitadoModels : Model
    {
        #region Property
        [Required]
        [Display(Name = "Carro")]
        [ForeignKey("Carro")]
        public int idCarro { get; set; }
        public virtual CarroModels Carro
        {
            get { return new CarroModels(idCarro); }
            set { this.idCarro = value.Id; }
        }
        public int Visitados { get; set; }
        #endregion
        #region Constructor
        public VisitadoModels() : base() { }
        public VisitadoModels(int id) : base(id) { }
        public VisitadoModels(CarroModels carro) : base()
        {
            VisitadoModels model = ((VisitadoBL)bl).GetByCarro(carro);
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(model));
            }
            this.Carro = carro;
        }
        public VisitadoModels(SqlDataReader reader) : base(reader) { }
        #endregion
        public void Insert()
        {
            ((VisitadoBL)bl).Insert(this, Id > 0);
        }
        public override string ToString() { return string.Format("{1}[{0}] - Visitados[{2}]", idCarro, Carro.Nome, Visitados); }
    }
}