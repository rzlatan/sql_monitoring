using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Query
{
    public class InsertQuery : QueryBase
    {
        public InsertQuery(string queryText, string connectionString) 
            : base(queryText, connectionString)
        {
            this.QueryType = QueryType.INSERT;
        }

        public override string Execute()
        {
            string result = "INSERT-";
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();

                var command = new SqlCommand(QueryText, connection);
                command.ExecuteNonQuery();
                result += "1";

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
