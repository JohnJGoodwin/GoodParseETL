using System;
using System.Collections;
using System.Text;
using System.IO;

namespace GoodParseETL
{
    /// <summary>
    /// Written By John J Goodwin 5/29/2019
    /// FileOperations is all about source file handling for the ETL. Currently designed for text files only.
    /// FileOpertations is very under developed here. 
    /// My full version has directory management & file targeting Regex processes to selectivly target large groups of files.
    /// This is really just a starting point for you to expand.
    /// </summary>
    class FileOperations
    {
        //Set up some initial variables
        private string path = "";

        public FileOperations(String filePath)
        {
            //INitialize the variables at instantiation
            path = filePath;
        }

        //Read a row from the text file and send it to ParsingOperations to be parsed into its values.
        public void ParseRows(ParsingOperations SendToParse)
        {
            string row;
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while ((row = sr.ReadLine()) != null)
                    {
                        SendToParse.SelectParse(row);
                    }
                }
            }
        }

    }
}
