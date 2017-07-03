using prjVendeCarro.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace prjVendeCarro.Models
{
    [Table("Cidade")]
    public class CidadeModels : Model
    {
        #region Property
        [Required]
        [Display(Name="Cidade")]
        public string Nome { get; set; }
        [Required]
        [Display(Name="Estado")]
        [ForeignKey("Estado")]
        public int idEstado { get; set; }
        public virtual EstadoModels Estado {
            get { return new EstadoModels(idEstado); }
            set { this.idEstado = value.Id; }
        }
        #endregion
        #region Constructor
        public CidadeModels() : base() { }
        public CidadeModels(int id) : base(id) { }
        public CidadeModels(SqlDataReader reader) : base(reader) { }
        #endregion
        public override string ToString() { return Nome; }
    }
    [Table("Estado")]
    public class EstadoModels : Model
    {
        #region Property
        [Required]
        [Display(Name="Estado")]
        public string Nome { get; set; }
        #endregion
        #region Constructor
        public EstadoModels() : base() { }
        public EstadoModels(int id) : base(id) { }
        public EstadoModels(SqlDataReader reader) : base(reader) { }
        #endregion
        public override string ToString() { return Nome; }
    }
   
}
