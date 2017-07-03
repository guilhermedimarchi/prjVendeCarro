using prjVendeCarro.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjVendeCarro.DAL
{
    public abstract class GenericDAL
    {
        #region Property
        private string connectionString { get; set; }
        public string Table { get; set; }
        public Dictionary<string, string> ListColumns { get; set; }
        public string Columns
        {
            get
            {
                string list = "";
                foreach (var row in ListColumns)
                {
                    list += string.Format("{0} {1},", row.Key, row.Value);
                }
                return list.Trim(',');
            }
        }
        #endregion
        #region Constructor
        public GenericDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ListColumns = new Dictionary<string,string>();
            setTable();
            setColumns();
        }
        #endregion
        #region Method
        public abstract void setTable();
        public abstract void setColumns();
        public List<Model> GetData()
        {
            string query = string.Format("SELECT {0} FROM {1}", Columns, Table);
            List<Model> list = new List<Model>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Model obj = (Model) Activator.CreateInstance(Type.GetType(this.GetType().ToString().Replace("DAL", "Models")), reader);
                        list.Add(obj);
                    }
                }
            }
            return list;
        }
        public Model GetById(int Id)
        {
            string query = string.Format("SELECT {0} FROM {1} WHERE Id=@Id", Columns, Table);
            Model obj = (Model)Activator.CreateInstance(Type.GetType(this.GetType().ToString().Replace("DAL", "Models")));
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Id", Id));
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    if (reader.Read())
                    {
                        obj = (Model)Activator.CreateInstance(Type.GetType(this.GetType().ToString().Replace("DAL", "Models")), reader);
                    }
                }
            }
            return obj;
        }
        public Model GetBy(string column, object value)
        {
            string query = string.Format("SELECT {0} FROM {1} WHERE {2}=@{2}", Columns, Table, column);
            Model obj = (Model)Activator.CreateInstance(Type.GetType(this.GetType().ToString().Replace("DAL", "Models")));
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@" + column, value));
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    if (reader.Read())
                    {
                        obj = (Model)Activator.CreateInstance(Type.GetType(this.GetType().ToString().Replace("DAL", "Models")), reader);
                    }
                }
            }
            return obj;
        }
        public List<Model> GetByQuery(string query, List<SqlParameter> parameters = null)
        {
            List<Model> list = new List<Model>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Model obj = (Model)Activator.CreateInstance(Type.GetType(this.GetType().ToString().Replace("DAL", "Models")), reader);
                        list.Add(obj);
                    }
                }
            }
            return list;
        }
        public void Insert(Model model)
        {
            List<string> listColumnsParameters = GetListColumnsParameters();
            List<string> listColumns = new List<string>(this.ListColumns.Keys);
            List<object> listValues = GetListPropertyValues(model);
            listColumnsParameters.Remove("@Id");
            listColumns.Remove("Id");
            listValues.Remove(listValues[0]);
            string strParameters = string.Join(",", listColumnsParameters);
            string columns = string.Join(",", listColumns);
            string query = string.Format("INSERT INTO {0} ({1}) OUTPUT INSERTED.Id VALUES ({2})", Table, columns, strParameters);
            
            model.Id = ActionByQuery(query, GetListSqlParameter(listColumnsParameters, listValues), true);
        }
        public void InsertByQuery(Model model, Dictionary<string, object> listColumnsValuesModel, Dictionary<string, string> listColumnsValuesSql = null)
        {
            List<string> listColumnsParameters = GetListColumnsParameters(listColumnsValuesModel.Keys.ToList());
            string strParameters = string.Join(",", listColumnsParameters);
            string columns = string.Join(",", listColumnsValuesModel.Keys.ToList());
            if (listColumnsValuesSql != null)
            {
                foreach (var sqlColumn in listColumnsValuesSql)
                {
                    columns += "," + sqlColumn.Key;
                    strParameters += "," + sqlColumn.Value;
                }
            }
            columns.Trim(',');
            strParameters.Trim(',');
            string query = string.Format("INSERT INTO {0} ({1}) OUTPUT INSERTED.Id VALUES ({2})", Table, columns, strParameters);

            model.Id = ActionByQuery(query, GetListSqlParameter(listColumnsParameters, listColumnsValuesModel.Values.ToList()), true);
        }
        public void Update(Model model)
        {
            List<string> listColumnsParameters = GetListColumnsParameters();
            List<string> listColumns = new List<string>(this.ListColumns.Keys);
            List<object> listValues = GetListPropertyValues(model);
            listColumnsParameters.Remove("@Id");
            listColumns.Remove("Id");
            listValues.Remove(listValues[0]);
            
            string fields = GetColumnsParametersSet(listColumns, listColumnsParameters);
            string query = string.Format("UPDATE {0} SET {1} WHERE Id = @Id", Table, fields);
            List<SqlParameter> parameters = GetListSqlParameter(listColumnsParameters, listValues);
            parameters.Add(new SqlParameter("@Id", model.Id));
            ActionByQuery(query, parameters);
        }
        public void Delete(Model model)
        {
            string query = string.Format("DELETE FROM {0} WHERE Id = @Id", Table);
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", model.Id));
            ActionByQuery(query, parameters);
        }
        public int ActionByQuery(string query, List<SqlParameter> parameters = null, bool retorno = false)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }
                    connection.Open();
                    if (retorno)
                        return (int) command.ExecuteScalar();
                    command.ExecuteNonQuery();
                    return 0;
                }
            }
        }
        // Pega todos os resultados dos atributos que tem no model
        public List<object> GetListPropertyValues(Model model)
        {
            List<object> values = new List<object>();
            foreach (string column in ListColumns.Values)
            {
                if (model.GetType().GetProperty(column) != null)
                {
                    values.Add(model.GetType().GetProperty(column).GetValue(model));
                }
            }
            return values;
        }
        // Pega todos os parâmetros (@column)
        public List<string> GetListColumnsParameters(List<string> list = null)
        {
            if (list == null)
                list = ListColumns.Values.ToList();
            List<string> parameters = new List<string>();
            foreach (string column in list)
            {
                parameters.Add(string.Format("@{0}", column));
            }
            return parameters;
        }
        // Pega parametros setados List<SqlParamenter>
        public List<SqlParameter> GetListSqlParameter(List<string> listColumnsParameters, List<object> listValues)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            int i = 0;
            foreach (object value in listValues)
            {
                parameters.Add(new SqlParameter(listColumnsParameters[i], value ?? DBNull.Value));
                i++;
            }
            return parameters;
        }
        // Pega colunas = parâmtros (usado no edit)
        public string GetColumnsParametersSet(List<string> listColumns, List<string> listColumnsParameters)
        {
            string fields = "";
            int i = 0;
            foreach (string column in listColumns)
            {
                fields += string.Format("{0} = {1},", column, listColumnsParameters[i]);
                i++;
            }
            fields = fields.Trim(',');
            return fields;
        }
        protected void addColumn(string key, string value = null)
        {
            if (value == null) value = key;
            ListColumns.Add(key, value);
        }
        #endregion
    }
}