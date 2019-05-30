using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodParseETL
{
    /// <summary>
    /// Written By John J Goodwin 5/29/2019
    /// </summary>
    class ParseConfig
    {
        //These are the values stored in the ETLManager db.
        //Every record in the ETLManager database can define a completely different ETL/Input file/SQL result database.
        //This should be expanded along with the ETL Manager to handle different SQL servers and login credentials like I have in my full version.
        //These values are referenced by all of the other classes.
        public static string m_Name { get; set; }
        public static string m_FileLocation { get; set; }
        public static string m_ParseType { get; set; }
        public static string m_DestSQLDb { get; set; }
        public static string m_DestSQLTable { get; set; }
        public static string m_ParseString { get; set; }
        public static string m_DestSQLProc { get; set; }

        //To expand this list;
        //      1 - Add the column you want to your ETLManager table
        //      2 - Add the new field name to your ETLManager 'LoadConfig' Stored Procedure
        //      3 - Add the new column to this list with an "m_" prefix to show its a Member 
        //      4 - Add the value initializatio to the LoadConfig method of the DBOperations class

    }
}
