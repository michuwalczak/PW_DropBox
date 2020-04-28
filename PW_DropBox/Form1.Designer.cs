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
            this.lvwThreadsDetails = new System.Windows.Forms.ListView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvwThreadsDetails
            // 
            this.lvwThreadsDetails.HideSelection = false;
            this.lvwThreadsDetails.Location = new System.Drawing.Point(418, 44);
            this.lvwThreadsDetails.Name = "lvwThreadsDetails";
            this.lvwThreadsDetails.Size = new System.Drawing.Size(283, 147);
            this.lvwThreadsDetails.TabIndex = 0;
            this.lvwThreadsDetails.UseCompatibleStateImageBehavior = false;
            this.lvwThreadsDetails.View = System.Windows.Forms.View.List;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(418, 296);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "button1";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lvwThreadsDetails);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwThreadsDetails;
        private System.Windows.Forms.Button btnUpdate;
    }
}

