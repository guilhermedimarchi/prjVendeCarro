using prjVendeCarro.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjVendeCarro.Models
{
    public enum Cambio { Manual, Automático }
    [Table("Marca")]
    public class MarcaModels : Model
    {
        #region Property
        [Required]
        [Display(Name="Marca")]
        public string Nome { get; set; }
        #endregion
        #region Constructor
        public MarcaModels() : base() { }
        public MarcaModels(int id) : base(id) { }
        public MarcaModels(SqlDataReader reader) : base(reader) { }
        #endregion
        public override string ToString() { return Nome; }
    }
    [Table("Modelo")]
    public class ModeloModels : Model
    {
        #region Property
        [Required]
        [Display(Name="Modelo")]
        public string Nome { get; set; }
        [Required]
        [Display(Name="Marca")]
        [ForeignKey("Marca")]
        public int idMarca { get; set; }
        public virtual MarcaModels Marca {
            get { return new MarcaModels(idMarca); }
            set { this.idMarca = value.Id; }
        }
        #endregion
        #region Constructor
        public ModeloModels() : base() { }
        public ModeloModels(int id) : base(id) { }
        public ModeloModels(SqlDataReader reader) : base(reader) { }
        #endregion
        public override string ToString() { return Nome; }
    }
    [Table("Combustivel")]
    public class CombustivelModels : Model
    {
        #region Property
        [Required]
        [Display(Name="Combustivel")]
        public string Nome { get; set; }
        #endregion
        #region Constructor
        public CombustivelModels() : base() { }
        public CombustivelModels(int id) : base(id) { }
        public CombustivelModels(SqlDataReader reader) : base(reader) { }
        #endregion
        public override string ToString() { return Nome; }
    }
    [Table("Carro")]
    public class CarroModels : Model
    {
        #region Property
        [Required]
        [Display(Name="Carro")]
        public string Nome { get; set; }
        #region Usuario
        [Required]
        [Display(Name="Usuario")]
        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }
        public virtual UsuarioModels Usuario
        {
            get { return new UsuarioModels(idUsuario); }
            set { this.idUsuario = value.Id; }
        }
        #endregion
        #region Marca
        [Required]
        [Display(Name="Marca")]
        public int idMarca { get; set; }
        public virtual MarcaModels Marca
        {
            get { return new MarcaModels(idMarca); }
            set { this.idMarca = value.Id; }
        }
        #endregion
        #region Modelo
        [Required]
        [Display(Name="Modelo")]
        [ForeignKey("Modelo")]
        public int idModelo { get; set; }
        public virtual ModeloModels Modelo
        {
            get { return new ModeloModels(idModelo); }
            set { this.idModelo = value.Id; }
        }
        #endregion
        #region Combustível
        [Display(Name="Combustível")]
        [ForeignKey("Combustivel")]
        public int idCombustivel { get; set; }
        public virtual CombustivelModels Combustivel
        {
            get { return new CombustivelModels(idCombustivel); }
            set { this.idCombustivel = value.Id; }
        }
        #endregion
        [Display(Name="Foto")]
        [HiddenInput]
        public string Foto { get; set; }
        #region Cidade
        [Required]
        [Display(Name="Cidade")]
        [ForeignKey("Cidade")]
        public int idCidade { get; set; }
        public virtual CidadeModels Cidade
        {
            get { return new CidadeModels(idCidade); }
            set { this.idCidade = value.Id; }
        }
        #endregion
        #region Estado
        [Required]
        [Display(Name = "Estado")]
        [ForeignKey("Estado")]
        public int idEstado { get; set; }
        public virtual EstadoModels Estado
        {
            get { return new EstadoModels(idEstado); }
            set { this.idEstado = value.Id; }
        }
        #endregion
        [Display(Name="Ano")]
        [Range(1900, 2999)]
        public int Ano { get; set; }
        [Display(Name="Cor")]
        public string Cor { get; set; }
        [Display(Name="KM")]
        public decimal Km { get; set; }
        [Display(Name="Portas")]
        public int QtdePorta { get; set; }
        [Display(Name="Versão")]
        public string Versao { get; set; }
        #region Câmbio
        public string strCambio { get; set; }
        [Display(Name="Câmbio")]
        public virtual Cambio Cambio
        {
            get
            {
                switch (strCambio)
                {
                    case "M": return Cambio.Manual;
                    case "A": return Cambio.Automático;
                    default: return new Cambio();
                }
            }
            set
            {
                strCambio = value.ToString().Substring(0, 1);
            }
        }
        #endregion
        [Display(Name="Preço")]
        public decimal Preco { get; set; }
        
        [Display(Name="Detalhe")]
        [StringLength(290, ErrorMessage = "Max 250 caracteres!")]
        public string Detalhe { get; set; }
        #endregion
        #region Constructor
        public CarroModels() : base() { }
        public CarroModels(int id) : base(id) { }
        public CarroModels(SqlDataReader reader) : base(reader) { }
        #endregion
        public override string ToString() { return Nome; }
    }
}