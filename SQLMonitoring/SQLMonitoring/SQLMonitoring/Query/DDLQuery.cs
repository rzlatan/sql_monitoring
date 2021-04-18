using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Query
{
    public class DDLQuery : QueryBase
    {
        public DDLQuery(string queryText, string connectionString)
            : base(queryText, connectionString)
        {
            this.QueryType = QueryType.DDL;
        }

        public override string Execute()
        {
            string result = "DDL-";
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();

                var command = new SqlCommand(QueryText, connection);
                var affectedRows = command.ExecuteNonQuery();
                result += affectedRows.ToString();

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result += "Error occured while executing query";
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return result;
        }
    }
}
