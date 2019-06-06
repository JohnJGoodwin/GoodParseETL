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

Currently designed as a proof of concept. The destination SQL SERVER name has been hardcoded into the DBOperations class.
Recommend moving that into the ETL Management SQL tables for fully dynamic destination DB servers for each ETL

A stored procedure is used to read the ETL definition (profile) into the system based on the ID provided on the commandline at runtime.

Builds a queue of process results. Each result is transformed into a Stored Procedure call string within the queue.
When the file read is complete, the contents of the queue (All store procedure calls now) are sent to the destination database on mass.

You'll need to create an ETL Management database and modify the database connection string in the app.config file to point the application to your SQL server before attempting to run this program. I've attached an SQL script that will construct all of the management side components you'll need to run the code. Here's how to use it;

        The script will;
        - Create an ETLManager database on your target MS SQL server.
        - Create an ETLManager table within the newly created ETLManager database on your chosen MS SQL server
        - Create a LoadConfig stored procedure within the newly created ETLManager database on your chosen MS SQL server

        To use this script, connect to your Microsoft SQL server of choice and execute the script.
        This script has been tested from within Visual Studio 2017 but should run equally well in Query Analyzer.

        This is an example project, thus this script is ridiculously simple. As a result it should be compatible with most older version    MS SQL servers servers.
        
Once the ETLManager is in place you will need to use it to identify EACH ETL you intend to run. Each ETL can have a different SQL database and table which you identify in the ETLManager columns. Each ETL can use a different source text file to read from which you specify in the FileLocation column and each ETL entry in the ETLManager table can use a different Parser (If you've built others) which you must specify in the ParseType field.
THe code works fastest if use with storted procedures in the desitinaion database for ETL results storage but it takes the output results as strings that will be executed as a complete batch so any string will do.

John Goodwin


