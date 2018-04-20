using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MainForm
{
    class SqlConnect
    {

        const string CONNECTIONSTRING = @"Server=PL1\MTCDB;Database=AdventureWorks2012;Trusted_Connection=True;";
        SqlConnection sqlConn;

        private bool dataConnect(string connectionString = "")
        {
            bool returnValue;

            if (connectionString.Length == 0)
                connectionString = CONNECTIONSTRING;

            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                returnValue = true;
            }

            catch (Exception ex)
            {
                returnValue = false;
                throw ex;
            }
            finally
            {
                sqlConn.Close();
            }

            return returnValue;
        }




    }



        

}

