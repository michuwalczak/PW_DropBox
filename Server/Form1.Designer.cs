namespace PW_DropBox
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvwLoggedClients = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbxLog = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxAssignedTasks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxRunningTasks = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnReloadConfig = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvwThreadsDetails = new System.Windows.Forms.ListView();
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwLoggedClients
            // 
            this.lvwLoggedClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader10,
            this.columnHeader12,
            this.columnHeader11,
            this.columnHeader5,
            this.columnHeader7});
            this.lvwLoggedClients.HideSelection = false;
            this.lvwLoggedClients.Location = new System.Drawing.Point(6, 19);
            this.lvwLoggedClients.Name = "lvwLoggedClients";
            this.lvwLoggedClients.Size = new System.Drawing.Size(484, 109);
            this.lvwLoggedClients.TabIndex = 1;
            this.lvwLoggedClients.UseCompatibleStateImageBehavior = false;
            this.lvwLoggedClients.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Client name";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Premium";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Assined tasks";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Running tasks";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Operation number";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Total load";
            // 
            // tbxLog
            // 
            this.tbxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLog.Location = new System.Drawing.Point(6, 19);
            this.tbxLog.Multiline = true;
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.Size = new System.Drawing.Size(559, 67);
            this.tbxLog.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvwThreadsDetails);
            this.groupBox1.Controls.Add(this.tbxAssignedTasks);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxRunningTasks);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(571, 373);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tasks";
            // 
            // tbxAssignedTasks
            // 
            this.tbxAssignedTasks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxAssignedTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAssignedTasks.Location = new System.Drawing.Point(141, 16);
            this.tbxAssignedTasks.Name = "tbxAssignedTasks";
            this.tbxAssignedTasks.ReadOnly = true;
            this.tbxAssignedTasks.Size = new System.Drawing.Size(100, 13);
            this.tbxAssignedTasks.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Assigned tasks:";
            // 
            // tbxRunningTasks
            // 
            this.tbxRunningTasks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxRunningTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxRunningTasks.Location = new System.Drawing.Point(349, 16);
            this.tbxRunningTasks.Name = "tbxRunningTasks";
            this.tbxRunningTasks.ReadOnly = true;
            this.tbxRunningTasks.Size = new System.Drawing.Size(100, 13);
            this.tbxRunningTasks.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(251, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Running tasks:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnReloadConfig);
            this.groupBox2.Controls.Add(this.lvwLoggedClients);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(571, 134);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Active clients";
            // 
            // btnReloadConfig
            // 
            this.btnReloadConfig.Location = new System.Drawing.Point(497, 45);
            this.btnReloadConfig.Name = "btnReloadConfig";
            this.btnReloadConfig.Size = new System.Drawing.Size(68, 45);
            this.btnReloadConfig.TabIndex = 2;
            this.btnReloadConfig.Text = "Reload config";
            this.btnReloadConfig.UseVisualStyleBackColor = true;
            this.btnReloadConfig.Click += new System.EventHandler(this.ReloadConfig_OnClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbxLog);
            this.groupBox3.Location = new System.Drawing.Point(3, 516);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(571, 91);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log";
            // 
            // lvwThreadsDetails
            // 
            this.lvwThreadsDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader9,
            this.columnHeader3,
            this.columnHeader8,
            this.columnHeader6});
            this.lvwThreadsDetails.HideSelection = false;
            this.lvwThreadsDetails.Location = new System.Drawing.Point(6, 33);
            this.lvwThreadsDetails.Name = "lvwThreadsDetails";
            this.lvwThreadsDetails.Size = new System.Drawing.Size(559, 338);
            this.lvwThreadsDetails.TabIndex = 5;
            this.lvwThreadsDetails.UseCompatibleStateImageBehavior = false;
            this.lvwThreadsDetails.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Id";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "User name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Operation name";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "File name";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Task status";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Progress";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Priority rate";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 610);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Server";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lvwLoggedClients;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.TextBox tbxLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.TextBox tbxRunningTasks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxAssignedTasks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Button btnReloadConfig;
        private System.Windows.Forms.ListView lvwThreadsDetails;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}

