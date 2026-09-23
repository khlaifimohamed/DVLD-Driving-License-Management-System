namespace DVLD.Users
{
    partial class ctrlUserCard
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlPersonCard1 = new DVLD.People.Controls.ctrlPersonCard();
            this.lblLoginInfo = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblUserIDValue = new System.Windows.Forms.Label();
            this.lblUserNameValue = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblIsActiveValue = new System.Windows.Forms.Label();
            this.lblIsActive = new System.Windows.Forms.Label();
            this.panelLoginInfo = new System.Windows.Forms.Panel();
            this.panelLoginInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlPersonCard1
            // 
            this.ctrlPersonCard1.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.ctrlPersonCard1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ctrlPersonCard1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 225);
            this.ctrlPersonCard1.Location = new System.Drawing.Point(15, 15);
            this.ctrlPersonCard1.Name = "ctrlPersonCard1";
            this.ctrlPersonCard1.Size = new System.Drawing.Size(850, 400);
            this.ctrlPersonCard1.TabIndex = 0;
            // 
            // lblLoginInfo
            // 
            this.lblLoginInfo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLoginInfo.ForeColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.lblLoginInfo.Location = new System.Drawing.Point(0, 15);
            this.lblLoginInfo.Name = "lblLoginInfo";
            this.lblLoginInfo.Size = new System.Drawing.Size(850, 35);
            this.lblLoginInfo.TabIndex = 1;
            this.lblLoginInfo.Text = "🔐  Login Information";
            this.lblLoginInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUserID
            // 
            this.lblUserID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserID.ForeColor = System.Drawing.Color.FromArgb(180, 180, 185);
            this.lblUserID.Location = new System.Drawing.Point(50, 68);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(110, 25);
            this.lblUserID.TabIndex = 2;
            this.lblUserID.Text = "User ID:";
            this.lblUserID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUserIDValue
            // 
            this.lblUserIDValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUserIDValue.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.lblUserIDValue.Location = new System.Drawing.Point(160, 68);
            this.lblUserIDValue.Name = "lblUserIDValue";
            this.lblUserIDValue.Size = new System.Drawing.Size(120, 25);
            this.lblUserIDValue.TabIndex = 3;
            this.lblUserIDValue.Text = "?????";
            this.lblUserIDValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUserName
            // 
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(180, 180, 185);
            this.lblUserName.Location = new System.Drawing.Point(380, 68);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(110, 25);
            this.lblUserName.TabIndex = 4;
            this.lblUserName.Text = "Username:";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUserNameValue
            // 
            this.lblUserNameValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUserNameValue.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.lblUserNameValue.Location = new System.Drawing.Point(490, 68);
            this.lblUserNameValue.Name = "lblUserNameValue";
            this.lblUserNameValue.Size = new System.Drawing.Size(120, 25);
            this.lblUserNameValue.TabIndex = 5;
            this.lblUserNameValue.Text = "?????";
            this.lblUserNameValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIsActive
            // 
            this.lblIsActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblIsActive.ForeColor = System.Drawing.Color.FromArgb(180, 180, 185);
            this.lblIsActive.Location = new System.Drawing.Point(650, 68);
            this.lblIsActive.Name = "lblIsActive";
            this.lblIsActive.Size = new System.Drawing.Size(90, 25);
            this.lblIsActive.TabIndex = 6;
            this.lblIsActive.Text = "Is Active:";
            this.lblIsActive.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIsActiveValue
            // 
            this.lblIsActiveValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblIsActiveValue.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.lblIsActiveValue.Location = new System.Drawing.Point(740, 68);
            this.lblIsActiveValue.Name = "lblIsActiveValue";
            this.lblIsActiveValue.Size = new System.Drawing.Size(80, 25);
            this.lblIsActiveValue.TabIndex = 7;
            this.lblIsActiveValue.Text = "?????";
            this.lblIsActiveValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelLoginInfo
            // 
            this.panelLoginInfo.BackColor = System.Drawing.Color.FromArgb(50, 50, 54);
            this.panelLoginInfo.Controls.Add(this.lblLoginInfo);
            this.panelLoginInfo.Controls.Add(this.lblIsActiveValue);
            this.panelLoginInfo.Controls.Add(this.lblUserID);
            this.panelLoginInfo.Controls.Add(this.lblIsActive);
            this.panelLoginInfo.Controls.Add(this.lblUserIDValue);
            this.panelLoginInfo.Controls.Add(this.lblUserNameValue);
            this.panelLoginInfo.Controls.Add(this.lblUserName);
            this.panelLoginInfo.Controls.Add(this.lblUserNameValue);
            this.panelLoginInfo.Location = new System.Drawing.Point(15, 430);
            this.panelLoginInfo.Name = "panelLoginInfo";
            this.panelLoginInfo.Padding = new System.Windows.Forms.Padding(15);
            this.panelLoginInfo.Size = new System.Drawing.Size(850, 110);
            this.panelLoginInfo.TabIndex = 8;
            // 
            // ctrlUserCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.Controls.Add(this.panelLoginInfo);
            this.Controls.Add(this.ctrlPersonCard1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(220, 220, 225);
            this.Name = "ctrlUserCard";
            this.Size = new System.Drawing.Size(880, 560);
            this.panelLoginInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private People.Controls.ctrlPersonCard ctrlPersonCard1;
        private System.Windows.Forms.Label lblLoginInfo;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblUserIDValue;
        private System.Windows.Forms.Label lblUserNameValue;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblIsActiveValue;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.Panel panelLoginInfo;
    }
}