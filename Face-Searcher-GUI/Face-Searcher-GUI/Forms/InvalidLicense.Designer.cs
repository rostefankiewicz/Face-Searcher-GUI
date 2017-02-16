namespace Face_Searcher_GUI.Forms
{
    partial class InvalidLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvalidLicense));
            this.continueButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.errorReason = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.machineKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // continueButton
            // 
            this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.Location = new System.Drawing.Point(319, 181);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(200, 50);
            this.continueButton.TabIndex = 0;
            this.continueButton.Text = "CONTINUE";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(525, 181);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(200, 50);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "EXIT";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // errorReason
            // 
            this.errorReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorReason.Location = new System.Drawing.Point(13, 13);
            this.errorReason.Name = "errorReason";
            this.errorReason.Size = new System.Drawing.Size(712, 91);
            this.errorReason.TabIndex = 2;
            this.errorReason.Text = "label1";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Machine Key";
            // 
            // machineKey
            // 
            this.machineKey.BackColor = System.Drawing.Color.White;
            this.machineKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.machineKey.Location = new System.Drawing.Point(17, 139);
            this.machineKey.Name = "machineKey";
            this.machineKey.ReadOnly = true;
            this.machineKey.Size = new System.Drawing.Size(708, 36);
            this.machineKey.TabIndex = 4;
            this.machineKey.Text = "...machine key...";
            this.machineKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // InvalidLicense
            // 
            this.ClientSize = new System.Drawing.Size(737, 243);
            this.ControlBox = false;
            this.Controls.Add(this.machineKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.errorReason);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.continueButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "InvalidLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invalid License";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label errorReason;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox machineKey;
    }
}