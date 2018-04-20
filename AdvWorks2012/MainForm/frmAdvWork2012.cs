﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace MainForm
{
    public partial class frmAdvWork2012 : Form
    {
        const string CONNECTIONSTRING = @"Server=PL1\MTCDB;Database=AdventureWorks2012;Trusted_Connection=True;";
        SqlConnection sqlConn = new SqlConnection(CONNECTIONSTRING);

        public frmAdvWork2012()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("dbo.sp_ActiveCustomerNames", sqlConn);
            DataTable dtCombo = new DataTable();

            int CustomerID;
            string CustomerName;

            try
            {
                sqlAdapter.Fill(dtCombo);

                foreach (DataRow drCustomers in dtCombo.Rows)
                {
                    CustomerID = int.Parse(drCustomers.ItemArray[0].ToString());
                    CustomerName = drCustomers.ItemArray[1].ToString();
                    cbCustomers.Items.Add(new ComboObject(CustomerID, CustomerName));
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cbCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboObject current =  (ComboObject) cbCustomers.SelectedItem;
            int CustomerID = current.CustomID;
            DataTable dtCustomerOrders = new DataTable();

            try
            {
                SqlCommand sqlCom = new SqlCommand("dbo.sp_CustomerSalesInfo", sqlConn);
                sqlCom.CommandType = CommandType.StoredProcedure;

                SqlParameter prmCustID = new SqlParameter("@CustID", CustomerID);
                sqlCom.Parameters.Add(prmCustID);

                SqlDataAdapter dataFeed = new SqlDataAdapter(sqlCom);

                dataFeed.Fill(dtCustomerOrders);

                dgResults.DataSource = dtCustomerOrders;
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }





        public class ComboObject
        {
            int custID;
            string custName;


            public ComboObject(int CustomID, string CustomName)
            {
                custID = CustomID;
                custName = CustomName;
            }


            public ComboObject(string CustomName)
            {
                custName = CustomName;
            }


            public int CustomID
            {
                get { return custID; }
                set { custID = value; }
            }


            public string CustomName
            {
                get { return custName; }
                set { custName = value; }
            }


            public override string ToString()
            {
                return CustomName;
            }

        }
    }
}
