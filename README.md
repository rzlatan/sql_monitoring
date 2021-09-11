# sql_monitoring
Tool for monitoring and troubleshooting SQL Server issues

Comes with a client app which is used to configure which logs from the SQL server will be uploaded on the web server, while web server is used for:
 - Generating reports based on the uploaded logs for the given timerange on which different charts will be shown which will help DBA easily spot the issues  with the server
 - Check if some common SQL server issues occurred during the given time range and surface an insight to the customer telling how to fix this issue
 - Predicting the future workload based on the history of query execution
 - Comes with a T-SQL editor for some basic DML/DDL statements
