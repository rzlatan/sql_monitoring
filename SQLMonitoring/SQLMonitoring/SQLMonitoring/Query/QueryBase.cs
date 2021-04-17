using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitoring.Query
{
    public enum QueryType
    {
        INSERT,
        UPDATE,
        DELETE,
        SELECT
    }

    public abstract class QueryBase
    {
        public static QueryBase GenerateQuery(string queryText, string connectionString)
        {
            string query = queryText.Trim()
                                    .Replace(System.Environment.NewLine, " ");

            string statementType = query.Split(" ")[0].ToUpper();

            switch (statementType)
            {
                case "INSERT":
                    return new InsertQuery(query, connectionString);
                    break;
                case "UPDATE":
                    return new UpdateQuery(query, connectionString);
                    break;
                case "DELETE":
                    return new DeleteQuery(query, connectionString);
                    break;
                case "SELECT":
                    break;
            }

            return null;
        }

        public QueryBase(string queryText, string connectionString)
        {
            QueryText = queryText;
            ConnectionString = connectionString;
        }

        public abstract string Execute();

        protected QueryType QueryType
        {
            get; set;
        }

        protected string QueryText
        {
            get; set;
        }

        protected string ConnectionString
        {
            get; set;
        }

    }
}
