namespace ChatApp_Client
{
    partial class frmSignup
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
            this.linklblSignup = new System.Windows.Forms.LinkLabel();
            this.btnSignup = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSignupPassword = new System.Windows.Forms.TextBox();
            this.txtSignupUsername = new System.Windows.Forms.TextBox();
            this.txtSignupIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtConfirmPass = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // linklblSignup
            // 
            this.linklblSignup.AutoSize = true;
            this.linklblSignup.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Underline);
            this.linklblSignup.ForeColor = System.Drawing.Color.Transparent;
            this.linklblSignup.Location = new System.Drawing.Point(329, 612);
            this.linklblSignup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linklblSignup.Name = "linklblSignup";
            this.linklblSignup.Size = new System.Drawing.Size(251, 26);
            this.linklblSignup.TabIndex = 5;
            this.linklblSignup.TabStop = true;
            this.linklblSignup.Text = "Already have an account?";
            this.linklblSignup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblSignup_LinkClicked);
            // 
            // btnSignup
            // 
            this.btnSignup.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnSignup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSignup.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnSignup.ForeColor = System.Drawing.Color.Transparent;
            this.btnSignup.Location = new System.Drawing.Point(102, 573);
            this.btnSignup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSignup.Name = "btnSignup";
            this.btnSignup.Size = new System.Drawing.Size(204, 65);
            this.btnSignup.TabIndex = 4;
            this.btnSignup.Text = "Sign up";
            this.btnSignup.UseVisualStyleBackColor = false;
            this.btnSignup.Click += new System.EventHandler(this.btnSignup_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.SteelBlue;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(262, 236);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 52);
            this.label4.TabIndex = 51;
            this.label4.Text = "Sign up";
            // 
            // txtSignupPassword
            // 
            this.txtSignupPassword.Location = new System.Drawing.Point(311, 442);
            this.txtSignupPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSignupPassword.Name = "txtSignupPassword";
            this.txtSignupPassword.Size = new System.Drawing.Size(312, 26);
            this.txtSignupPassword.TabIndex = 2;
            // 
            // txtSignupUsername
            // 
            this.txtSignupUsername.Location = new System.Drawing.Point(311, 379);
            this.txtSignupUsername.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSignupUsername.Name = "txtSignupUsername";
            this.txtSignupUsername.Size = new System.Drawing.Size(312, 26);
            this.txtSignupUsername.TabIndex = 1;
            // 
            // txtSignupIP
            // 
            this.txtSignupIP.Location = new System.Drawing.Point(311, 319);
            this.txtSignupIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSignupIP.Name = "txtSignupIP";
            this.txtSignupIP.Size = new System.Drawing.Size(312, 26);
            this.txtSignupIP.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(38, 435);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 32);
            this.label3.TabIndex = 47;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(38, 372);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 32);
            this.label2.TabIndex = 46;
            this.label2.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(38, 319);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 32);
            this.label1.TabIndex = 45;
            this.label1.Text = "Server IP";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ChatApp_Client.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(65, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(541, 221);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 54;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(38, 498);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(242, 32);
            this.label5.TabIndex = 47;
            this.label5.Text = "Confirm Password";
            // 
            // txtConfirmPass
            // 
            this.txtConfirmPass.Location = new System.Drawing.Point(311, 504);
            this.txtConfirmPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtConfirmPass.Name = "txtConfirmPass";
            this.txtConfirmPass.Size = new System.Drawing.Size(312, 26);
            this.txtConfirmPass.TabIndex = 3;
            // 
            // frmSignup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(665, 668);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.linklblSignup);
            this.Controls.Add(this.btnSignup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtConfirmPass);
            this.Controls.Add(this.txtSignupPassword);
            this.Controls.Add(this.txtSignupUsername);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSignupIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmSignup";
            this.Text = "frmSignup";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linklblSignup;
        private System.Windows.Forms.Button btnSignup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSignupPassword;
        private System.Windows.Forms.TextBox txtSignupUsername;
        private System.Windows.Forms.TextBox txtSignupIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtConfirmPass;
    }
}