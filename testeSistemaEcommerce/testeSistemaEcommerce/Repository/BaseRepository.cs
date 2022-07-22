using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace testeSistemaEcommerce.Repository
{
    public class BaseRepository
    {
        private string sistema = System.Configuration.ConfigurationManager.AppSettings["sistema"];
        private SqlParameterCollection sqlParametros;
        protected ConnectionStringSettings connectionSetting = ConfigurationManager.ConnectionStrings[System.Configuration.ConfigurationManager.AppSettings["sistema"]];
        private static string connectionString;
        public BaseRepository()
        {
            sqlParametros = new SqlCommand().Parameters;
        }

        private SqlConnection getConnection()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings[sistema].ConnectionString;
            }
            catch
            {

            }

            return new SqlConnection(connectionString);
        }

        public DataSet getDataSet(string strCommand, CommandType cmdType)
        {
            using (SqlConnection sqlConn = getConnection())
            {
                sqlConn.Open();
                SqlCommand sqlCommand = sqlConn.CreateCommand();
                sqlCommand.CommandText = strCommand;
                sqlCommand.CommandType = cmdType;
                sqlCommand.CommandTimeout = 7200;
                setParameters(sqlCommand);
                SqlDataAdapter dtaDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                dtaDataAdapter.Fill(ds, "Consulta");
                return ds;
            }
        }

        public void cleanParameters()
        {
            sqlParametros.Clear();
        }
        public void addParameter(SqlParameter sqlParameter)
        {
            sqlParametros.Add(sqlParameter);
        }
        private void setParameters(SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.Clear();
            foreach (SqlParameter param in sqlParametros)
                sqlCommand.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
        }
    }
}