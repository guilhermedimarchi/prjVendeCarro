using prjVendeCarro.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace prjVendeCarro.Models
{
    [Table("Foto")]
    public class FotoModels : Model
    {
        #region Property
        //[Key, ForeignKey("Carro")]
        //public  int Id { get; set; }
        #region Carro
        [Required]
        [Display(Name="Carro")]
        [ForeignKey("Carro")]
        public int idCarro { get; set; }
        public virtual CarroModels Carro
        {
            get { return new CarroModels(idCarro); }
            set { this.idCarro = value.Id; }
        }
        #endregion
        [Required]
        [Display(Name="Nome")]
        public string Nome { get; set; }
        [Required]
        [Display(Name="Data")]
        public DateTime Data { get; set; }
        [Required]
        [Display(Name = "Local")]
        public string Local { get; set; }
        [Required]
        [HiddenInput(DisplayValue = false)]
        public string Tipo { get; set; }
        
        #endregion
        #region Constructor
        public FotoModels() : base() { }
        public FotoModels(int id) : base(id) { }
        public FotoModels(string Nome) : base()
        {
            FotoModels model = ((FotoBL)bl).GetByNome(Nome);
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(model));
            }
        }
        public FotoModels(SqlDataReader reader) : base(reader) { }
        #endregion
        public void Insert()
        {
            ((FotoBL)bl).Insert(this, true);
        }
        public override string ToString() { return Nome; }
    }
}