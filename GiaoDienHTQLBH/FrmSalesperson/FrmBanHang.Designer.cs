namespace GiaoDienHTQLBH
{
    partial class FrmBanHang
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
            this.components = new System.ComponentModel.Container();
            this.grbTable = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblBan = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvDH = new System.Windows.Forms.DataGridView();
            this.btnInHD = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTienThua = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTienDua = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtThanhTien = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTC = new System.Windows.Forms.TextBox();
            this.nmrVAT = new System.Windows.Forms.NumericUpDown();
            this.btnTT = new System.Windows.Forms.Button();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.cbLoai = new System.Windows.Forms.ComboBox();
            this.flpRight = new System.Windows.Forms.FlowLayoutPanel();
            this.flpDSBan = new System.Windows.Forms.FlowLayoutPanel();
            this.flpSP = new System.Windows.Forms.FlowLayoutPanel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrVAT)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // grbTable
            // 
            this.grbTable.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grbTable.ForeColor = System.Drawing.Color.Maroon;
            this.grbTable.Location = new System.Drawing.Point(13, 13);
            this.grbTable.Margin = new System.Windows.Forms.Padding(4);
            this.grbTable.Name = "grbTable";
            this.grbTable.Padding = new System.Windows.Forms.Padding(4);
            this.grbTable.Size = new System.Drawing.Size(323, 621);
            this.grbTable.TabIndex = 14;
            this.grbTable.TabStop = false;
            this.grbTable.Text = "Danh sách bàn";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblBan);
            this.groupBox4.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox4.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox4.Location = new System.Drawing.Point(351, 13);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(593, 76);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = " Bàn đang chọn";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // lblBan
            // 
            this.lblBan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblBan.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblBan.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblBan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBan.Location = new System.Drawing.Point(0, 31);
            this.lblBan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBan.Name = "lblBan";
            this.lblBan.Size = new System.Drawing.Size(593, 42);
            this.lblBan.TabIndex = 0;
            this.lblBan.Text = "Chưa chọn bàn";
            this.lblBan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgvDH);
            this.groupBox6.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox6.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox6.Location = new System.Drawing.Point(351, 97);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(593, 357);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Đồ uống được chọn";
            // 
            // dgvDH
            // 
            this.dgvDH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDH.BackgroundColor = System.Drawing.Color.White;
            this.dgvDH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDH.Location = new System.Drawing.Point(0, 34);
            this.dgvDH.Name = "dgvDH";
            this.dgvDH.RowHeadersVisible = false;
            this.dgvDH.RowHeadersWidth = 50;
            this.dgvDH.RowTemplate.Height = 24;
            this.dgvDH.Size = new System.Drawing.Size(593, 323);
            this.dgvDH.TabIndex = 7;
            // 
            // btnInHD
            // 
            this.btnInHD.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInHD.ForeColor = System.Drawing.Color.Maroon;
            this.btnInHD.Location = new System.Drawing.Point(779, 585);
            this.btnInHD.Name = "btnInHD";
            this.btnInHD.Size = new System.Drawing.Size(133, 49);
            this.btnInHD.TabIndex = 41;
            this.btnInHD.Text = "In hóa đơn";
            this.btnInHD.UseVisualStyleBackColor = true;
            this.btnInHD.Click += new System.EventHandler(this.btnInHD_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(353, 544);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 23);
            this.label4.TabIndex = 40;
            this.label4.Text = "Thành tiền:";
            // 
            // txtTienThua
            // 
            this.txtTienThua.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTienThua.Location = new System.Drawing.Point(800, 502);
            this.txtTienThua.Name = "txtTienThua";
            this.txtTienThua.Size = new System.Drawing.Size(134, 30);
            this.txtTienThua.TabIndex = 39;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(644, 508);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 23);
            this.label3.TabIndex = 38;
            this.label3.Text = "Tiền thừa:";
            // 
            // txtTienDua
            // 
            this.txtTienDua.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTienDua.Location = new System.Drawing.Point(800, 461);
            this.txtTienDua.Name = "txtTienDua";
            this.txtTienDua.Size = new System.Drawing.Size(134, 30);
            this.txtTienDua.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(644, 467);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 23);
            this.label1.TabIndex = 36;
            this.label1.Text = "Tiền khách đưa:";
            // 
            // txtThanhTien
            // 
            this.txtThanhTien.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtThanhTien.Location = new System.Drawing.Point(463, 541);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.Size = new System.Drawing.Size(145, 30);
            this.txtThanhTien.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(353, 505);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 23);
            this.label2.TabIndex = 34;
            this.label2.Text = "VAT:";
            // 
            // txtTC
            // 
            this.txtTC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTC.Location = new System.Drawing.Point(463, 464);
            this.txtTC.Name = "txtTC";
            this.txtTC.Size = new System.Drawing.Size(145, 30);
            this.txtTC.TabIndex = 33;
            // 
            // nmrVAT
            // 
            this.nmrVAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.nmrVAT.ForeColor = System.Drawing.Color.Maroon;
            this.nmrVAT.Location = new System.Drawing.Point(463, 503);
            this.nmrVAT.Name = "nmrVAT";
            this.nmrVAT.Size = new System.Drawing.Size(80, 28);
            this.nmrVAT.TabIndex = 32;
            this.nmrVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnTT
            // 
            this.btnTT.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnTT.ForeColor = System.Drawing.Color.Maroon;
            this.btnTT.Location = new System.Drawing.Point(388, 585);
            this.btnTT.Name = "btnTT";
            this.btnTT.Size = new System.Drawing.Size(101, 49);
            this.btnTT.TabIndex = 31;
            this.btnTT.Text = "Tính tiền";
            this.btnTT.UseVisualStyleBackColor = true;
            this.btnTT.Click += new System.EventHandler(this.btnTT_Click);
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongTien.ForeColor = System.Drawing.Color.Maroon;
            this.lblTongTien.Location = new System.Drawing.Point(353, 467);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(104, 23);
            this.lblTongTien.TabIndex = 30;
            this.lblTongTien.Text = "Tổng cộng:";
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLuu.ForeColor = System.Drawing.Color.Maroon;
            this.btnLuu.Location = new System.Drawing.Point(574, 585);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(138, 49);
            this.btnLuu.TabIndex = 42;
            this.btnLuu.Text = "Lưu dữ liệu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnLoc);
            this.groupBox7.Controls.Add(this.cbLoai);
            this.groupBox7.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox7.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox7.Location = new System.Drawing.Point(952, 13);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(369, 87);
            this.groupBox7.TabIndex = 43;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Loại sản phẩm";
            // 
            // btnLoc
            // 
            this.btnLoc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLoc.ForeColor = System.Drawing.Color.Maroon;
            this.btnLoc.Location = new System.Drawing.Point(290, 25);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(72, 45);
            this.btnLoc.TabIndex = 44;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // cbLoai
            // 
            this.cbLoai.BackColor = System.Drawing.Color.White;
            this.cbLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoai.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbLoai.FormattingEnabled = true;
            this.cbLoai.Location = new System.Drawing.Point(8, 31);
            this.cbLoai.Margin = new System.Windows.Forms.Padding(4);
            this.cbLoai.Name = "cbLoai";
            this.cbLoai.Size = new System.Drawing.Size(257, 33);
            this.cbLoai.TabIndex = 1;
            // 
            // flpRight
            // 
            this.flpRight.AutoScroll = true;
            this.flpRight.BackColor = System.Drawing.Color.MistyRose;
            this.flpRight.Location = new System.Drawing.Point(931, 108);
            this.flpRight.Margin = new System.Windows.Forms.Padding(4);
            this.flpRight.MaximumSize = new System.Drawing.Size(933, 0);
            this.flpRight.Name = "flpRight";
            this.flpRight.Size = new System.Drawing.Size(390, 0);
            this.flpRight.TabIndex = 44;
            // 
            // flpDSBan
            // 
            this.flpDSBan.AutoScroll = true;
            this.flpDSBan.BackColor = System.Drawing.Color.MistyRose;
            this.flpDSBan.Location = new System.Drawing.Point(12, 47);
            this.flpDSBan.Name = "flpDSBan";
            this.flpDSBan.Size = new System.Drawing.Size(324, 587);
            this.flpDSBan.TabIndex = 0;
            // 
            // flpSP
            // 
            this.flpSP.AutoScroll = true;
            this.flpSP.BackColor = System.Drawing.Color.MistyRose;
            this.flpSP.Location = new System.Drawing.Point(952, 108);
            this.flpSP.Name = "flpSP";
            this.flpSP.Size = new System.Drawing.Size(369, 526);
            this.flpSP.TabIndex = 45;
            // 
            // FormSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(1334, 647);
            this.Controls.Add(this.flpSP);
            this.Controls.Add(this.flpDSBan);
            this.Controls.Add(this.flpRight);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnInHD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTienThua);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTienDua);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtThanhTien);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTC);
            this.Controls.Add(this.nmrVAT);
            this.Controls.Add(this.btnTT);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.grbTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSale";
            this.Text = "FormSale";
            this.Load += new System.EventHandler(this.FormSale_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrVAT)).EndInit();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbTable;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblBan;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnInHD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTienThua;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTienDua;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtThanhTien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTC;
        private System.Windows.Forms.NumericUpDown nmrVAT;
        private System.Windows.Forms.Button btnTT;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cbLoai;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.FlowLayoutPanel flpRight;
        private System.Windows.Forms.FlowLayoutPanel flpDSBan;
        private System.Windows.Forms.FlowLayoutPanel flpSP;
        private System.Windows.Forms.DataGridView dgvDH;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}