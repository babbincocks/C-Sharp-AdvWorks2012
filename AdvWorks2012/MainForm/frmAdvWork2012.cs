using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Databoy;

namespace MainForm
{
    public partial class frmAdvWork2012 : Form
    {
        //Here is the code for the main form where all user interaction will occur.
        

        public frmAdvWork2012()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   //This is what happens when the form first loads. First we check if there is a connection that can be formed to the database we have.
            try
            {   //Then it's checked to see if there is a connection to the database.
                if (SqlConnect.dataConnect() == true)
                {
                    //Then an adapter is created that accesses the stored procedure that accesses all of the customers, using the connection that's
                    //created in the data class. A datatable is also created that will hold the data later.
                SqlDataAdapter sqlAdapter = new SqlDataAdapter("dbo.sp_ActiveCustomerNames", SqlConnect.sqlConn);
                DataTable dtCombo = new DataTable();
                    //A couple of variables are created that will hold the information that will later be converted and put into our combo box.
                int CustomerID;
                string CustomerName;

                //The data adapter then populates our datatable with the data it retrieved from the stored procedure.
                sqlAdapter.Fill(dtCombo);

                    //A foreach loop is created that will run as long as there is another row to access in the recently-populated datatable.
                    foreach (DataRow drCustomers in dtCombo.Rows)
                    {
                        /*
                        While the loop is going, it puts values into the two variables created earlier. These values are the values in the
                        first and second cells of whatever row the foreach loop is currently on, the first cell going into CustomerID, and the
                        second into CustomerName. Then the variables are put into the combobox as a Combo Object (which is a custom class
                        created farther down.)
                        */
                        CustomerID = int.Parse(drCustomers.ItemArray[0].ToString());
                        CustomerName = drCustomers.ItemArray[1].ToString();
                        cbCustomers.Items.Add(new ComboObject(CustomerID, CustomerName));
                    }

                }
                else
                {   //This is for the occasion that the program is unable to connect to the SQL database. An exception is thrown telling the user
                    //that the program failed to connect to the database.
                    throw new Exception("Unable to connect to the AdventureWorks2012 database.");
                }

            }
           

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        

        private void cbCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {   //Here is the code for when the user clicks an object in the form's combo box.

            try
            {
                //First we check to see if a connection can be established to the database.
                if (SqlConnect.dataConnect() == true)
                {

                    //Then a Combo Object object is created, which holds the values of what was clicked on by the user. Then the value of the CustomerID
                    //in that object is put into another variable.
                    ComboObject current = (ComboObject)cbCustomers.SelectedItem;
                    int CustomerID = current.CustomID;

                    //Then real quick, a datatable is created that will populate the datagrid on the form later on.
                    DataTable dtCustomerOrders = new DataTable();


                    //Next, as there will be a parameter playing into what results are returned, a SqlCommand is created using the
                    //other stored procedure that was created for this assignment. It's then categorized as a stored procedure, so it can have
                    //parameters set to it.
                    SqlCommand sqlCom = new SqlCommand("dbo.sp_CustomerSalesInfo", SqlConnect.sqlConn);
                    sqlCom.CommandType = CommandType.StoredProcedure;

                    //Here a parameter is created, where it states what parameter in the stored procedure it's supplying a value for, and what variable
                    //in this program currently contains the value that will be supplied to the parameter. The parameter is then added to the parameters
                    //of the SQL command.
                    SqlParameter prmCustID = new SqlParameter("@CustID", CustomerID);
                    sqlCom.Parameters.Add(prmCustID);

                    //A data adapter then gets the information from the database based on the conditions set.
                    SqlDataAdapter dataFeed = new SqlDataAdapter(sqlCom);

                    //The adapter then fills the data table created earlier.
                    dataFeed.Fill(dtCustomerOrders);

                    //Finally, the data table populates the data grid on the form.
                    dgResults.DataSource = dtCustomerOrders;
                }
                else
                {   //This is for the occasion that the program is unable to connect to the SQL database. An exception is thrown telling the user
                    //that the program failed to connect to the database.
                    throw new Exception("Unable to connect to the AdventureWorks2012 database.");
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dgResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {       //This is the code for when the user clicks on a cell in the data grid once it's populated.
            try
            {

                //It's a bit redundant, but just to be safe, I put what'll be done in an if statement that checks that a cell in the data grid
                //has indeed been chosen.
                if (dgResults.SelectedCells.Count > 0)
                {
                    //A variable is created that gets the numeric representative of what row the selected cell is on.
                    string a = dgResults.CurrentCell.RowIndex.ToString();

                    //Then another variable gets the value of the first cell of the row that the currently selected cell is in by using the variable
                    //that was just previously called. The first cell is being retrieved, as we need to get the SalesOrderID, and that's the first
                    //column of the stored procedure's results.
                    string output = dgResults.Rows[int.Parse(a)].Cells[0].Value.ToString();

                    //Finally, the last variable (which has the SalesOrderID) is put into the text box below the data grid of the main form.
                    txtSalesID.Text = output.ToString();

               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        //This is the code for the custom class that was used up above in the different form events.
        public class ComboObject
        {
            //First a couple of variables are created that the class focuses on.
            int custID;
            string custName;


            public ComboObject(int CustomID, string CustomName)
            {       //Here, an overload is created for the class, which just sets the variables in the class to what the user designates. 
                custID = CustomID;
                custName = CustomName;
            }

            //This sets the properties for the first of the two argument variables for what will be done when it's being retrieved or having a value 
            //set to it. If it's being retrieved, it returns the value of the variable custID, while putting a value into the argument CustomID puts
            //that value into the custID variable, so it can be used later.
            public int CustomID
            {
                get { return custID; }
                set { custID = value; }
            }

            //This sets the properties for the second of the two argument variables. Pretty much the exact same thing is done with this as the last
            //one.
            public string CustomName
            {
                get { return custName; }
                set { custName = value; }
            }

            //This makes it so if ToString() is used in relation to at least one of the values of a Combo Object, while the class does have both of
            //the values, it will only show the second value: the customer name. Really the only example of this being used in this program is when
            //putting values in the combo box, where this makes it so only the customer name is displayed.
            public override string ToString()
            {
                return CustomName;
            }

        }


    }
}
