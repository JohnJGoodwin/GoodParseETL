# GoodParseETL
Simple scalable ETL tool
Written By John J. Goodwin

The system utilizes an SQL database table for ETL definitions. The SQL columns are used in the code as folllows;
ID              =     Passed on commandline to tell the code which ETL definition to use. 
FileLocation    =     The actual full path including full filename of the text file to be parsed. 
ParseType       =     The name of the parse method in the ParsingOperations code that will parse this file. 
DestSQLDB       =     The SQL database where the ETL results will be sent. 
DestSQLTable    =     Defined in the code but unused. Storeed Procedures are used instead. You can choose to use it. 
ParseString     =     The Regex string used to parse the text file in through the ParseType. 
DestSQLProc     =     This is the Stored Procedure used to take results from the ETL and store them in the destination table. 

Currently designed as a proof of concept. The destination SQL server has been hardcoded into the DBOperations class.
Recommend moving that into the ETL Management SQL tables for fully dynamic destination DB servers for each ETL

A stored procedure is used to read the ETL definition (profile) into the system based on the ID provided on the commandline at runtime.

Builds a queue of process results. Each result is transformed into a Stored Procedure call string within the queue.
When the file read is complete, the contents of the queue (All store procedure calls now) are sent to the destination database on mass.

You'll need to create an ETL Management database and modify the database connection string in the app.config file to point the application to your SQL server before attempting to run this program.


