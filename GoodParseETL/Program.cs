using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace GoodParseETL
{
    /// <summary>
    /// Written By John J Goodwin 5/29/2019
    /// This program was designed completely from scratch with a couple points in mind;
    /// 1 - Its going to be a free give-away so we're not looking to change the world.
    /// 2 - How much can be done in 1 day. This was started 5/29/2019 at 6AM and successfullly ran on 5/29/2019 at 2:47 PM
    /// 3 - Show some scalability & performance gains through SQL server/Code server partnership
    /// EVERYTHING in this program could easily be transposed into Java
    /// 
    /// My large version of this same system incorporates Threading & has handled as much as 500gb of text files a night.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
        //Set up our queue
        

        //Check which ETLManager profile we were asked to load (Record ID)
        //This is the ETL definition (Which file to read, how to parse it, how to process the results, etc)
        Int16 configVal;
            if (Int16.TryParse(args[0], out configVal))
            {
                //Go load our ETL profile
                ParsingOperations Parser = new ParsingOperations();
                DBOperations DBOps = new DBOperations(Parser);
                string connectionString = ConfigurationManager.AppSettings["ConfigConnectionString"];
                DBOps.LoadConfig(configVal);

                //Go process the file and parse it into the destination database/table
                FileOperations FileOps = new FileOperations(ParseConfig.m_FileLocation );
                FileOps.ParseRows(Parser);
                DBOps.WriteQue();
            }

        }
    }
}
