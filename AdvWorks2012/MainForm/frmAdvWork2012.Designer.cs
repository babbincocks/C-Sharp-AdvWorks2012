namespace MainForm
{
    partial class frmAdvWork2012
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbCustomers = new System.Windows.Forms.ComboBox();
            this.dgResults = new System.Windows.Forms.DataGridView();
            this.txtSalesID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgResults)).BeginInit();
            this.SuspendLayout();
            // 
            // cbCustomers
            // 
            this.cbCustomers.FormattingEnabled = true;
            this.cbCustomers.Location = new System.Drawing.Point(12, 25);
            this.cbCustomers.Name = "cbCustomers";
            this.cbCustomers.Size = new System.Drawing.Size(165, 21);
            this.cbCustomers.TabIndex = 0;
            this.cbCustomers.SelectedIndexChanged += new System.EventHandler(this.cbCustomers_SelectedIndexChanged);
            // 
            // dgResults
            // 
            this.dgResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResults.Location = new System.Drawing.Point(183, 25);
            this.dgResults.MultiSelect = false;
            this.dgResults.Name = "dgResults";
            this.dgResults.Size = new System.Drawing.Size(561, 340);
            this.dgResults.TabIndex = 1;
            this.dgResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgResults_CellClick);
            // 
            // txtSalesID
            // 
            this.txtSalesID.Location = new System.Drawing.Point(548, 385);
            this.txtSalesID.Name = "txtSalesID";
            this.txtSalesID.Size = new System.Drawing.Size(196, 20);
            this.txtSalesID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Customer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(466, 388);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sales Order ID";
            // 
            // frmAdvWork2012
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 417);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSalesID);
            this.Controls.Add(this.dgResults);
            this.Controls.Add(this.cbCustomers);
            this.Name = "frmAdvWork2012";
            this.Text = "AdventureWorks";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCustomers;
        private System.Windows.Forms.DataGridView dgResults;
        private System.Windows.Forms.TextBox txtSalesID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

