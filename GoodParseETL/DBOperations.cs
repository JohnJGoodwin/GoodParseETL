using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace GoodParseETL
{
    /// <summary>
    /// Written By John J Goodwin 5/29/2019
    /// </summary>
    class DBOperations : IDisposable 
    {
        //Initialize some variables and objects
        private string connectionString = "";
        public SqlConnection conn;
        private SqlDataAdapter sqlDA = new SqlDataAdapter();
        private SqlCommand sqlCmd = new SqlCommand();
        DataSet ds = new DataSet();
        ParsingOperations Parser;

        public DBOperations(ParsingOperations InpParser)
        {
            //Go grab the database connection settings for the ETLManager database
            connectionString = ConfigurationManager.AppSettings["ConfigConnectionString"];
            Parser = InpParser;
        }

        //Grab the ETL configuration from the ETL Manager for this ETL task. The ID was given in the commandline (args[0])
        //Remember, every ETL ConfigID can represent completely different files, Parsing rules & database
        public void LoadConfig(int ConfigID)
        {
            sqlCmd.CommandType = CommandType.StoredProcedure;
            conn = new SqlConnection(connectionString);
            conn.Open();
            sqlCmd.Connection = conn;
            sqlCmd.CommandText = "LoadConfig";
            sqlCmd.Parameters.AddWithValue("@ParseID", ConfigID);
            sqlDA.SelectCommand = sqlCmd;
            sqlDA.Fill(ds);
            conn.Close();
            try
            {
                ParseConfig.m_Name = ds.Tables[0].Rows[0]["Name"].ToString().Trim();
                ParseConfig.m_FileLocation = ds.Tables[0].Rows[0]["FileLocation"].ToString().Trim();
                ParseConfig.m_ParseType = ds.Tables[0].Rows[0]["ParseType"].ToString().Trim();
                ParseConfig.m_DestSQLDb = ds.Tables[0].Rows[0]["DestSQLDb"].ToString().Trim();
                ParseConfig.m_DestSQLTable = ds.Tables[0].Rows[0]["DestSQLTable"].ToString().Trim();
                ParseConfig.m_ParseString = ds.Tables[0].Rows[0]["ParseString"].ToString().Trim();
                ParseConfig.m_DestSQLProc = ds.Tables[0].Rows[0]["DestSQLProc"].ToString().Trim();
            }
            catch(Exception ex)
            {
                Console.WriteLine("DBOperations: Attempt to store values from Query result failed{0}{1}", Environment.NewLine, ex.StackTrace);
            }
        }

        //Generic SQL connection method for reuse in all SQL connections
        public SqlConnection DBConnect(string SQLConStr)
        {
            SqlConnection sqlConn = new SqlConnection(SQLConStr);
            try
            {
                sqlConn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DBOperations: Attempt to connect to an SQL database failed{0}{1}", Environment.NewLine, ex.StackTrace);
            }
            return sqlConn;
        }

        //ETL Parsed results write operation. Writes the parsed data to the specified SQL database
        //I didn't model Server into the ETLManager so its hardcoded here. Server should be added to the ETLManager Schema
        public void WriteQue()
        {
            string server = @"(localdb)\ProjectsV13";
            String Db = ParseConfig.m_DestSQLDb;
            string conStr = "Data Source=" + @server + ";" + "Initial Catalog=" + @Db;
            
            if (Parser.QueueSize() > 0)
            {
                conn = DBConnect(@conStr);
                //conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.Text;
                
                foreach(Object obj in Parser.GetQueue())
                {
                    string sqlStr = obj.ToString();
                    sqlCmd.CommandText = @sqlStr;
                    try
                    {
                        sqlCmd.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("DBOperations: Attempt to call an SQL stored Procedure failed{0}{1}", Environment.NewLine, ex.StackTrace);
                    }
                }
                conn.Close();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                conn.Close();
                sqlCmd.Dispose();
                sqlDA.Dispose();
                ds.Dispose();
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
