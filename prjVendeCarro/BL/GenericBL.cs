using prjVendeCarro.DAL;
using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjVendeCarro.BL
{
    public class GenericBL
    {
        #region Property
        protected GenericDAL dal { get; set; }
        #endregion
        #region Constructor
        public GenericBL()
        {
            dal = (GenericDAL)Activator.CreateInstance(Type.GetType(this.GetType().ToString().Replace("BL", "DAL")));
        }
        #endregion
        #region Method
        public Model GetById(int id)
        {
            return dal.GetById(id);
        }
        public List<Model> GetData()
        {
            return dal.GetData();
        }
        public void Insert(Model model)
        {
            dal.Insert(model);
        }
        public void Update(Model model)
        {
            dal.Update(model);
        }
        public void Delete(Model model)
        {
            dal.Delete(model);
        }
        #endregion
    }
}