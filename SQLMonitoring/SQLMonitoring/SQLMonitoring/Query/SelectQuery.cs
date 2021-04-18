using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Query
{
    public class SelectQuery : QueryBase
    {
        public SelectQuery(string queryText, string connectionString)
            : base(queryText, connectionString)
        {
            this.QueryType = QueryType.SELECT;
        }

        public override string Execute()
        {
            string result = "SELECT-";
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();

                var command = new SqlCommand(QueryText, connection);

                SqlDataReader dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    result += "COLUMNS~";

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        result += dr.GetName(i);
                        
                        if (i < dr.FieldCount)
                        {
                            result += "~";
                        }
                    }

                    while (dr.Read())
                    {
                        result += "NEWROW~";

                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            result += dr.GetValue(i).ToString();

                            if (i < dr.FieldCount)
                            {
                                result += "~";
                            }
                        }
                    }
                }

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
