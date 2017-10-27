using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbid.Module.CustomCodes
{
    public class DataRetrieval
    {
        string connectionString;
        string queryString;
        string tableName;
        DataSet dataSet;
        List<ParameterDictionary> parameters = new List<ParameterDictionary>();

        public DataRetrieval()
        {
        }

        public DataRetrieval(string QueryString, string ConnectionString)
        {
            connectionString = ConnectionString;
            queryString = QueryString;
        }

        public DataRetrieval(string QueryString, string ConnectionString, List<ParameterDictionary> Parameters)
        {
            connectionString = ConnectionString;
            queryString = QueryString;
            parameters = Parameters;
        }

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        public List<ParameterDictionary> Parameters
        {
            get
            {
                return parameters;
            }
            set
            {
                parameters = value;
            }
        }

        SqlConnection GetSQLConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            return sqlConnection;
        }

        SqlConnection GetSQLConnection(string ConnectionString)
        {
            connectionString = ConnectionString;
            return GetSQLConnection();
        }

        SqlDataAdapter GetSqlDataAdapter()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(queryString, GetSQLConnection(connectionString));
            if (parameters.Count > 0)
            {
                foreach(ParameterDictionary param in parameters)
                {
                    dataAdapter.SelectCommand.Parameters.Add(new SqlParameter(param.ParameterName, param.ParameterValue));
                }
            }
            return dataAdapter;
        }

        SqlDataAdapter GetSqlDataAdapter(string QueryString, string ConnectionString)
        {
            queryString = QueryString;
            connectionString = ConnectionString;
            return GetSqlDataAdapter();
        }

        public string QueryString
        {
            get
            {
                return queryString;
            }
            set
            {
                queryString = value;
            }
        }

        public DataSet GetDataSet(string TableName, string QueryString, string ConnectionString, List<ParameterDictionary> Parameters, DataSet DataSet)
        {
            parameters = Parameters;
            queryString = QueryString;
            connectionString = ConnectionString;
            tableName = TableName;
            dataSet = DataSet;
            return GetDataSet();
        }

        public DataSet GetDataSet(string TableName, string QueryString, string ConnectionString, DataSet DataSet)
        {
            queryString = QueryString;
            connectionString = ConnectionString;
            tableName = TableName;
            dataSet = DataSet;
            return GetDataSet();
        }

        public DataSet GetDataSet(string TableName, DataSet DataSet)
        {
            tableName = TableName;
            dataSet = DataSet;
            return GetDataSet();
        }

        public DataSet GetDataSet()
        {
            SqlDataAdapter adapter = GetSqlDataAdapter();
            adapter.SelectCommand.CommandTimeout = 1000000;
            adapter.Fill(dataSet, tableName);
            return dataSet;
        }
    }

    public class ParameterDictionary
    {
        public string ParameterName { get; set; }
        public object ParameterValue { get; set; }
    }
}
