namespace GiaoDienHTQLBH
{
    partial class FrmĐang_nhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmĐang_nhap));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Btn_Thoat = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_TaiKhoan = new Guna.UI2.WinForms.Guna2TextBox();
            this.txt_MatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.Btn_DangNhap = new Guna.UI2.WinForms.Guna2Button();
            this.linkLabel_QuenMatKhau = new System.Windows.Forms.LinkLabel();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.ckbHienMK = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Thoat
            // 
            this.Btn_Thoat.BackColor = System.Drawing.Color.White;
            this.Btn_Thoat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Btn_Thoat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Btn_Thoat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Btn_Thoat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Btn_Thoat.FillColor = System.Drawing.Color.White;
            this.Btn_Thoat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Btn_Thoat.ForeColor = System.Drawing.Color.White;
            this.Btn_Thoat.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Thoat.Image")));
            this.Btn_Thoat.ImageSize = new System.Drawing.Size(30, 30);
            this.Btn_Thoat.Location = new System.Drawing.Point(434, 4);
            this.Btn_Thoat.Name = "Btn_Thoat";
            this.Btn_Thoat.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.Btn_Thoat.Size = new System.Drawing.Size(40, 40);
            this.Btn_Thoat.TabIndex = 6;
            this.Btn_Thoat.Click += new System.EventHandler(this.Btn_Thoat_Click);
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.BackColor = System.Drawing.Color.White;
            this.guna2CirclePictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2CirclePictureBox1.Image")));
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(154, 46);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(178, 153);
            this.guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2CirclePictureBox1.TabIndex = 1;
            this.guna2CirclePictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("#9Slide01 Tieu de ngan", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(93, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "ĐĂNG NHẬP HỆ THỐNG ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("#9Slide01 Tieu de ngan", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(111, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "QUẢN LÝ BÁN HÀNG";
            // 
            // txt_TaiKhoan
            // 
            this.txt_TaiKhoan.BackColor = System.Drawing.Color.White;
            this.txt_TaiKhoan.BorderColor = System.Drawing.Color.Silver;
            this.txt_TaiKhoan.BorderRadius = 15;
            this.txt_TaiKhoan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_TaiKhoan.DefaultText = "";
            this.txt_TaiKhoan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_TaiKhoan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_TaiKhoan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_TaiKhoan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_TaiKhoan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_TaiKhoan.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_TaiKhoan.ForeColor = System.Drawing.Color.Black;
            this.txt_TaiKhoan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_TaiKhoan.IconLeft = ((System.Drawing.Image)(resources.GetObject("txt_TaiKhoan.IconLeft")));
            this.txt_TaiKhoan.Location = new System.Drawing.Point(99, 297);
            this.txt_TaiKhoan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_TaiKhoan.Name = "txt_TaiKhoan";
            this.txt_TaiKhoan.PlaceholderText = "Tài khoản";
            this.txt_TaiKhoan.SelectedText = "";
            this.txt_TaiKhoan.Size = new System.Drawing.Size(293, 55);
            this.txt_TaiKhoan.TabIndex = 1;
            // 
            // txt_MatKhau
            // 
            this.txt_MatKhau.BackColor = System.Drawing.Color.White;
            this.txt_MatKhau.BorderColor = System.Drawing.Color.Silver;
            this.txt_MatKhau.BorderRadius = 15;
            this.txt_MatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_MatKhau.DefaultText = "";
            this.txt_MatKhau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_MatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_MatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_MatKhau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_MatKhau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_MatKhau.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_MatKhau.ForeColor = System.Drawing.Color.Black;
            this.txt_MatKhau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_MatKhau.IconLeft = ((System.Drawing.Image)(resources.GetObject("txt_MatKhau.IconLeft")));
            this.txt_MatKhau.Location = new System.Drawing.Point(99, 361);
            this.txt_MatKhau.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_MatKhau.Name = "txt_MatKhau";
            this.txt_MatKhau.PlaceholderText = "Mật khẩu";
            this.txt_MatKhau.SelectedText = "";
            this.txt_MatKhau.Size = new System.Drawing.Size(293, 55);
            this.txt_MatKhau.TabIndex = 2;
            // 
            // Btn_DangNhap
            // 
            this.Btn_DangNhap.BackColor = System.Drawing.Color.White;
            this.Btn_DangNhap.BorderRadius = 15;
            this.Btn_DangNhap.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Btn_DangNhap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Btn_DangNhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Btn_DangNhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Btn_DangNhap.FillColor = System.Drawing.Color.Brown;
            this.Btn_DangNhap.Font = new System.Drawing.Font("#9Slide01 Tieu de ngan", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Btn_DangNhap.ForeColor = System.Drawing.Color.White;
            this.Btn_DangNhap.Location = new System.Drawing.Point(99, 462);
            this.Btn_DangNhap.Name = "Btn_DangNhap";
            this.Btn_DangNhap.Size = new System.Drawing.Size(293, 55);
            this.Btn_DangNhap.TabIndex = 3;
            this.Btn_DangNhap.Text = "ĐĂNG NHẬP";
            this.Btn_DangNhap.Click += new System.EventHandler(this.Btn_DangNhap_Click);
            // 
            // linkLabel_QuenMatKhau
            // 
            this.linkLabel_QuenMatKhau.AutoSize = true;
            this.linkLabel_QuenMatKhau.BackColor = System.Drawing.Color.White;
            this.linkLabel_QuenMatKhau.Font = new System.Drawing.Font("#9Slide01 Tieu de ngan", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.linkLabel_QuenMatKhau.LinkColor = System.Drawing.Color.Black;
            this.linkLabel_QuenMatKhau.Location = new System.Drawing.Point(169, 520);
            this.linkLabel_QuenMatKhau.Name = "linkLabel_QuenMatKhau";
            this.linkLabel_QuenMatKhau.Size = new System.Drawing.Size(149, 24);
            this.linkLabel_QuenMatKhau.TabIndex = 5;
            this.linkLabel_QuenMatKhau.TabStop = true;
            this.linkLabel_QuenMatKhau.Text = "Quên mật khẩu";
            this.linkLabel_QuenMatKhau.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_QuenMatKhau_LinkClicked);
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.BackColor = System.Drawing.Color.Black;
            this.guna2CustomGradientPanel1.BorderColor = System.Drawing.Color.Silver;
            this.guna2CustomGradientPanel1.BorderRadius = 15;
            this.guna2CustomGradientPanel1.BorderThickness = 1;
            this.guna2CustomGradientPanel1.Controls.Add(this.ckbHienMK);
            this.guna2CustomGradientPanel1.Controls.Add(this.linkLabel_QuenMatKhau);
            this.guna2CustomGradientPanel1.Controls.Add(this.Btn_DangNhap);
            this.guna2CustomGradientPanel1.Controls.Add(this.txt_MatKhau);
            this.guna2CustomGradientPanel1.Controls.Add(this.txt_TaiKhoan);
            this.guna2CustomGradientPanel1.Controls.Add(this.label2);
            this.guna2CustomGradientPanel1.Controls.Add(this.label1);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2CirclePictureBox1);
            this.guna2CustomGradientPanel1.Controls.Add(this.Btn_Thoat);
            this.guna2CustomGradientPanel1.ForeColor = System.Drawing.Color.Black;
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(-15, -6);
            this.guna2CustomGradientPanel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(488, 593);
            this.guna2CustomGradientPanel1.TabIndex = 0;
            this.guna2CustomGradientPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2CustomGradientPanel1_Paint);
            // 
            // ckbHienMK
            // 
            this.ckbHienMK.AutoSize = true;
            this.ckbHienMK.BackColor = System.Drawing.Color.White;
            this.ckbHienMK.Font = new System.Drawing.Font("#9Slide02 Noi dung dai", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ckbHienMK.Location = new System.Drawing.Point(108, 423);
            this.ckbHienMK.Name = "ckbHienMK";
            this.ckbHienMK.Size = new System.Drawing.Size(141, 23);
            this.ckbHienMK.TabIndex = 7;
            this.ckbHienMK.Text = "Hiện mật khẩu";
            this.ckbHienMK.UseVisualStyleBackColor = false;
            this.ckbHienMK.CheckedChanged += new System.EventHandler(this.ckbHienMK_CheckedChanged);
            // 
            // FrmĐang_nhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(461, 588);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmĐang_nhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Đang_nhap_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Guna.UI2.WinForms.Guna2CircleButton Btn_Thoat;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txt_TaiKhoan;
        private Guna.UI2.WinForms.Guna2TextBox txt_MatKhau;
        private Guna.UI2.WinForms.Guna2Button Btn_DangNhap;
        private System.Windows.Forms.LinkLabel linkLabel_QuenMatKhau;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private System.Windows.Forms.CheckBox ckbHienMK;
    }
}

