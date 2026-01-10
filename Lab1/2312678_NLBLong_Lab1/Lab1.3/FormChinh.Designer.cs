namespace Lab1._3
{
    partial class FormChinh
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTenMien = new System.Windows.Forms.TextBox();
            this.rTxtBoxThongTin = new System.Windows.Forms.RichTextBox();
            this.btn_PhanGiai = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập tên miền: ";
            // 
            // txtTenMien
            // 
            this.txtTenMien.Location = new System.Drawing.Point(151, 41);
            this.txtTenMien.Name = "txtTenMien";
            this.txtTenMien.Size = new System.Drawing.Size(126, 20);
            this.txtTenMien.TabIndex = 1;
            // 
            // rTxtBoxThongTin
            // 
            this.rTxtBoxThongTin.Location = new System.Drawing.Point(66, 80);
            this.rTxtBoxThongTin.Name = "rTxtBoxThongTin";
            this.rTxtBoxThongTin.Size = new System.Drawing.Size(211, 135);
            this.rTxtBoxThongTin.TabIndex = 4;
            this.rTxtBoxThongTin.Text = "";
            // 
            // btn_PhanGiai
            // 
            this.btn_PhanGiai.Location = new System.Drawing.Point(293, 39);
            this.btn_PhanGiai.Name = "btn_PhanGiai";
            this.btn_PhanGiai.Size = new System.Drawing.Size(75, 23);
            this.btn_PhanGiai.TabIndex = 5;
            this.btn_PhanGiai.Text = "Phân giải";
            this.btn_PhanGiai.UseVisualStyleBackColor = true;
            this.btn_PhanGiai.Click += new System.EventHandler(this.btn_PhanGiai_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 253);
            this.Controls.Add(this.btn_PhanGiai);
            this.Controls.Add(this.rTxtBoxThongTin);
            this.Controls.Add(this.txtTenMien);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Thông tin giao thức IP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTenMien;
        private System.Windows.Forms.RichTextBox rTxtBoxThongTin;
        private System.Windows.Forms.Button btn_PhanGiai;
    }
}

