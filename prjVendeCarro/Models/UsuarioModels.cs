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
    [Table("Usuario")]
    public class UsuarioModels : Model
    {
        #region Property
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Required]
        [Display(Name="Nome")]
        public string Nome { get; set; }
        [Required]
        [Display(Name="Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name="CPF")]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:###.###.###-##}")]
        [MaxLength(14)]
        public string CPF { get; set; }
        [Required]
        [Display(Name="Tipo do Usuário")]
        [ForeignKey("Tipo")]
        public int idTipo { get; set; }
        public virtual UsuarioTipoModels Tipo {
            get { return new UsuarioTipoModels(idTipo); }
            set { this.idTipo = value.Id; }
        }
        [Display(Name="Telefone")]
        public string Telefone { get; set; }
        #endregion
        #region Constructor
        public UsuarioModels() : base() { }
        public UsuarioModels(int id) : base(id) { }
        public UsuarioModels(string login) : base()
        {
            UsuarioModels model = ((UsuarioBL)bl).GetByLogin(login);
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(model));
            }
        }
        public UsuarioModels(SqlDataReader reader) : base(reader) { }
        #endregion
        public override string ToString() { return Nome; }
    }
    [Table("UsuarioTipo")]
    public class UsuarioTipoModels : Model
    {
        #region Property
        [Required]
        [Display(Name="Tipo do Usuário")]
        public string Nome { get; set; }
        #endregion
        #region Constructor
        public UsuarioTipoModels() : base() { }
        public UsuarioTipoModels(int id) : base(id) { }
        public UsuarioTipoModels(SqlDataReader reader) : base(reader) { }
        #endregion
        public override string ToString() { return Nome; }
    }
    public enum TelefoneTipo
    {
        RESIDENCIAL,
        CELULAR,
        COMERCIAL,
        OUTRO
    }
}