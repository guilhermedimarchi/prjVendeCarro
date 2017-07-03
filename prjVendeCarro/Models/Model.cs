using prjVendeCarro.BL;
using prjVendeCarro.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace prjVendeCarro.Models
{
    public class Model
    {
        #region Property
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        protected GenericBL bl { get; set; }
        #endregion
        #region Constructor
        public Model()
        {
            bl = (GenericBL)Activator.CreateInstance(Type.GetType(this.GetType().ToString().Replace("Models", "BL")));
        }
        public Model(int id) : this()
        {
            Model model = bl.GetById(id);
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(model));
            }
        }
        public Model(SqlDataReader reader) : this()
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                PropertyInfo property = this.GetType().GetProperty(reader.GetName(i));
                if (property != null)
                {
                    property.SetValue(this, reader[reader.GetName(i)] == System.DBNull.Value ? null : reader[reader.GetName(i)]);
                }
            }
        }
        public void Insert()
        {
            bl.Insert(this);
        }
        public void Update()
        {
            bl.Update(this);
        }
        public void Delete()
        {
            bl.Delete(this);
        }
        #endregion
    }
}