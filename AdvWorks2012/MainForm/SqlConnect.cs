using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Databoy
{
    public class SqlConnect
    {
        const string CONNECTIONSTRING = @"user id=AdvWorks2012;pwd=AW2012;Server=DBPLAPTOP01\LT_SERVER;Database=AdventureWorks2012;Trusted_Connection=False;";

        public static SqlConnection sqlConn = new SqlConnection(CONNECTIONSTRING);

        public static bool dataConnect()
        {
            bool returnValue;

            try
            {
                
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

