namespace RSKKMS.WinForms
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnDeploy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKMSContractAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEtherAmount = new System.Windows.Forms.TextBox();
            this.btnEtherAmount = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCertificateThumbprint = new System.Windows.Forms.TextBox();
            this.btnLoadCertificate = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKeyName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnKMSSetItem = new System.Windows.Forms.Button();
            this.btnKMSGetItem = new System.Windows.Forms.Button();
            this.txtStoredKeyValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnDeploy
            // 
            this.btnDeploy.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnDeploy.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeploy.ForeColor = System.Drawing.Color.White;
            this.btnDeploy.Location = new System.Drawing.Point(591, 204);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(257, 73);
            this.btnDeploy.TabIndex = 0;
            this.btnDeploy.Text = "Deploy Contract";
            this.btnDeploy.UseVisualStyleBackColor = false;
            this.btnDeploy.Click += new System.EventHandler(this.btnDeploy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "KMS Contract Address:";
            // 
            // txtKMSContractAddress
            // 
            this.txtKMSContractAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKMSContractAddress.Location = new System.Drawing.Point(304, 144);
            this.txtKMSContractAddress.Name = "txtKMSContractAddress";
            this.txtKMSContractAddress.Size = new System.Drawing.Size(439, 34);
            this.txtKMSContractAddress.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "R-BTC Amount: ";
            // 
            // txtEtherAmount
            // 
            this.txtEtherAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEtherAmount.Location = new System.Drawing.Point(304, 87);
            this.txtEtherAmount.Name = "txtEtherAmount";
            this.txtEtherAmount.Size = new System.Drawing.Size(439, 34);
            this.txtEtherAmount.TabIndex = 4;
            // 
            // btnEtherAmount
            // 
            this.btnEtherAmount.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnEtherAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEtherAmount.ForeColor = System.Drawing.Color.White;
            this.btnEtherAmount.Location = new System.Drawing.Point(303, 204);
            this.btnEtherAmount.Name = "btnEtherAmount";
            this.btnEtherAmount.Size = new System.Drawing.Size(257, 73);
            this.btnEtherAmount.TabIndex = 5;
            this.btnEtherAmount.Text = "Get R-BTC";
            this.btnEtherAmount.UseVisualStyleBackColor = false;
            this.btnEtherAmount.Click += new System.EventHandler(this.btnEtherAmount_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(256, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Certificate Thumbprint:";
            // 
            // txtCertificateThumbprint
            // 
            this.txtCertificateThumbprint.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCertificateThumbprint.Location = new System.Drawing.Point(304, 24);
            this.txtCertificateThumbprint.Name = "txtCertificateThumbprint";
            this.txtCertificateThumbprint.Size = new System.Drawing.Size(439, 34);
            this.txtCertificateThumbprint.TabIndex = 7;
            // 
            // btnLoadCertificate
            // 
            this.btnLoadCertificate.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnLoadCertificate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadCertificate.ForeColor = System.Drawing.Color.White;
            this.btnLoadCertificate.Location = new System.Drawing.Point(22, 204);
            this.btnLoadCertificate.Name = "btnLoadCertificate";
            this.btnLoadCertificate.Size = new System.Drawing.Size(257, 73);
            this.btnLoadCertificate.TabIndex = 8;
            this.btnLoadCertificate.Text = "Load Certificate";
            this.btnLoadCertificate.UseVisualStyleBackColor = false;
            this.btnLoadCertificate.Click += new System.EventHandler(this.btnLoadCertificate_Click);
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(22, 503);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(826, 284);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 29);
            this.label4.TabIndex = 10;
            this.label4.Text = "Key Name: ";
            // 
            // txtKeyName
            // 
            this.txtKeyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeyName.Location = new System.Drawing.Point(188, 308);
            this.txtKeyName.Name = "txtKeyName";
            this.txtKeyName.Size = new System.Drawing.Size(439, 34);
            this.txtKeyName.TabIndex = 11;
            this.txtKeyName.TextChanged += new System.EventHandler(this.txtKeyName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 362);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "New Value:";
            // 
            // txtValue
            // 
            this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.Location = new System.Drawing.Point(188, 362);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(439, 34);
            this.txtValue.TabIndex = 13;
            // 
            // btnKMSSetItem
            // 
            this.btnKMSSetItem.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnKMSSetItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKMSSetItem.ForeColor = System.Drawing.Color.White;
            this.btnKMSSetItem.Location = new System.Drawing.Point(867, 503);
            this.btnKMSSetItem.Name = "btnKMSSetItem";
            this.btnKMSSetItem.Size = new System.Drawing.Size(257, 73);
            this.btnKMSSetItem.TabIndex = 14;
            this.btnKMSSetItem.Text = "KMS - Set";
            this.btnKMSSetItem.UseVisualStyleBackColor = false;
            this.btnKMSSetItem.Click += new System.EventHandler(this.btnKMSSetItem_Click);
            // 
            // btnKMSGetItem
            // 
            this.btnKMSGetItem.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnKMSGetItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKMSGetItem.ForeColor = System.Drawing.Color.White;
            this.btnKMSGetItem.Location = new System.Drawing.Point(867, 612);
            this.btnKMSGetItem.Name = "btnKMSGetItem";
            this.btnKMSGetItem.Size = new System.Drawing.Size(257, 73);
            this.btnKMSGetItem.TabIndex = 15;
            this.btnKMSGetItem.Text = "KMS - Get";
            this.btnKMSGetItem.UseVisualStyleBackColor = false;
            this.btnKMSGetItem.Click += new System.EventHandler(this.btnKMSGetItem_Click);
            // 
            // txtStoredKeyValue
            // 
            this.txtStoredKeyValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStoredKeyValue.Location = new System.Drawing.Point(188, 424);
            this.txtStoredKeyValue.Name = "txtStoredKeyValue";
            this.txtStoredKeyValue.ReadOnly = true;
            this.txtStoredKeyValue.Size = new System.Drawing.Size(439, 34);
            this.txtStoredKeyValue.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 424);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 29);
            this.label6.TabIndex = 17;
            this.label6.Text = "Stored Value:";
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(867, 714);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(257, 73);
            this.btnRemove.TabIndex = 19;
            this.btnRemove.Text = "KMS - Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(24, 793);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(824, 36);
            this.progressBar1.TabIndex = 20;
            this.progressBar1.Value = 100;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 885);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.txtStoredKeyValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnKMSGetItem);
            this.Controls.Add(this.btnKMSSetItem);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtKeyName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnLoadCertificate);
            this.Controls.Add(this.txtCertificateThumbprint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnEtherAmount);
            this.Controls.Add(this.txtEtherAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKMSContractAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeploy);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RSK Key Management";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKMSContractAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEtherAmount;
        private System.Windows.Forms.Button btnEtherAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCertificateThumbprint;
        private System.Windows.Forms.Button btnLoadCertificate;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKeyName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnKMSSetItem;
        private System.Windows.Forms.Button btnKMSGetItem;
        private System.Windows.Forms.TextBox txtStoredKeyValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

