using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;


namespace GoodParseETL
{
    /// <summary>
    /// Written By John J Goodwin 5/29/2019
    /// ParsingOperations isn't much here but it lays the ground work for some real posibilities
    /// The ParseRow method is a switch statement that directs program flow to the parser method specified in the ETLManager profile
    /// In theory you could have hundreds of different parse routines here to handle all of your parsing needs.
    /// Each entry in the ETLManager would dictate which parser method is used with which input file.
    /// </summary>
    class ParsingOperations : ParseConfig
    {
        Queue ParsedRows = new Queue();
        public ParsingOperations()
        {
            //Nothing to set up 
        }

        public int QueueSize()
        {
            return ParsedRows.Count;
        }

        public Queue GetQueue()
        {
            return ParsedRows;
        }

        //Read the Parser Type from the ETLManager and direct the data to the correct method to perform the data parse
        public void SelectParse(string Line)
        {
            string switchVal = ParseConfig.m_ParseType.ToUpper().Trim();
            switch (switchVal)
            {
                case "CHARACTER5":
                    ParseRxChar(Line);
                    break;
            }
        }

        //This is the parser for my 5 column test table & test file.
        //I could have dozens of ETL profiles that extract 5 columns but each using a completely different seperator between the Columns
        //In fact, this code would service any 5 column extract even if it had hundreds of characters seperating each of the 5 values in the iput file
        private void ParseRxChar(string Line)
        {
            StringBuilder queString = new StringBuilder();
            string[] matches = Regex.Split(Line, ParseConfig.m_ParseString);

            queString.AppendFormat("{0} '{1}','{2}','{3}','{4}','{5}'", ParseConfig.m_DestSQLProc, matches[0], matches[1], matches[2], matches[3], matches[4]);
            queString.ToString();
            ParsedRows.Enqueue(queString);
        }

    }
}
