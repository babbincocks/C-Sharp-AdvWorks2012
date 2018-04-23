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
    {   //This is the class that handles the connection to the SQL Server.

        //First the settings for the connection is created. A string holds all of the information, and is put into a SqlConnection.
        //const string CONNECTIONSTRING = @"user id=AdvWorks2012;pwd=AW2012;Server=PL1\MTCDB;Database=AdventureWorks2012;
        //                                    Trusted_Connection=False;";

        const string CONNECTIONSTRING = @"user id=AdvWorks2012;pwd=AW2012;Server=COMEAU-WIN7;Database=AdventureWorks2012;
                                            Trusted_Connection=False;";

        public static SqlConnection sqlConn = new SqlConnection(CONNECTIONSTRING);

        public static bool dataConnect()
        {       //This is the code for a test that checks if the program can connect to the database.

            //A Boolean variable that returns whether the connection was a success or not is created.
            bool returnValue;

            try
            {
                //Then it attempts to open the SqlConnection created outside of this test. If it works, it then makes the Boolean variable return
                //true.
                sqlConn.Open();
                returnValue = true;
            }

            catch
            {
                //If the connection attempt fails and raises an exception, it sets the Boolean variable to equal false, and it throws a 
                //new exception.
                returnValue = false;
                throw new Exception("Unable to connect to the AdventureWorks2012 database. Make sure the connection string is completely accurate, and try again.");
            }
            finally
            {
                //Regardless of whether the attempt succeeds or not, the connection is closed.
                sqlConn.Close();
            }
            //The result of the test is then returned.
            return returnValue;
        }




    }





}

