namespace VMS.HIS.KSK.Forms
{
    partial class FrmThemMoiKhachHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThemMoiKhachHang));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtpNgayHopDong = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.cmdNew = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.txtStthienThi = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSohopDong = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtMaSoThue = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtNguoiDaiDien = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDiaChi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTenKhachHang = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtMaKhachHang = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdKhachHang = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.chkThemMoiLienTuc = new System.Windows.Forms.CheckBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStthienThi)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 325);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(589, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblMessage);
            this.splitContainer1.Panel2.Controls.Add(this.chkThemMoiLienTuc);
            this.splitContainer1.Panel2.Controls.Add(this.dtpNgayHopDong);
            this.splitContainer1.Panel2.Controls.Add(this.cmdNew);
            this.splitContainer1.Panel2.Controls.Add(this.cmdSave);
            this.splitContainer1.Panel2.Controls.Add(this.cmdExit);
            this.splitContainer1.Panel2.Controls.Add(this.chkTrangThai);
            this.splitContainer1.Panel2.Controls.Add(this.txtStthienThi);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.txtSohopDong);
            this.splitContainer1.Panel2.Controls.Add(this.txtMaSoThue);
            this.splitContainer1.Panel2.Controls.Add(this.txtNguoiDaiDien);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.txtSoDienThoai);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.txtDiaChi);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.txtTenKhachHang);
            this.splitContainer1.Panel2.Controls.Add(this.txtMaKhachHang);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.txtIdKhachHang);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Size = new System.Drawing.Size(589, 325);
            this.splitContainer1.SplitterDistance = 35;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // dtpNgayHopDong
            // 
            this.dtpNgayHopDong.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayHopDong.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayHopDong.DropDownCalendar.Name = "";
            this.dtpNgayHopDong.Location = new System.Drawing.Point(105, 154);
            this.dtpNgayHopDong.Name = "dtpNgayHopDong";
            this.dtpNgayHopDong.ShowUpDown = true;
            this.dtpNgayHopDong.Size = new System.Drawing.Size(163, 22);
            this.dtpNgayHopDong.TabIndex = 58;
            // 
            // cmdNew
            // 
            this.cmdNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdNew.Image")));
            this.cmdNew.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdNew.Location = new System.Drawing.Point(71, 236);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(138, 35);
            this.cmdNew.TabIndex = 57;
            this.cmdNew.Text = "Thêm mới";
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(216, 236);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(138, 35);
            this.cmdSave.TabIndex = 10;
            this.cmdSave.Text = "Ghi (Ctrl+S)";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(361, 236);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(138, 35);
            this.cmdExit.TabIndex = 11;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangThai.Location = new System.Drawing.Point(105, 184);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(68, 18);
            this.chkTrangThai.TabIndex = 8;
            this.chkTrangThai.TabStop = false;
            this.chkTrangThai.Text = "Hiển thị";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // txtStthienThi
            // 
            this.txtStthienThi.Location = new System.Drawing.Point(362, 155);
            this.txtStthienThi.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtStthienThi.Name = "txtStthienThi";
            this.txtStthienThi.Size = new System.Drawing.Size(67, 22);
            this.txtStthienThi.TabIndex = 9;
            this.txtStthienThi.TabStop = false;
            this.txtStthienThi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(322, 158);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 14);
            this.label10.TabIndex = 53;
            this.label10.Text = "STT:";
            // 
            // txtSohopDong
            // 
            this.txtSohopDong.Location = new System.Drawing.Point(362, 128);
            this.txtSohopDong.Name = "txtSohopDong";
            this.txtSohopDong.Size = new System.Drawing.Size(194, 22);
            this.txtSohopDong.TabIndex = 7;
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.Location = new System.Drawing.Point(105, 128);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.Size = new System.Drawing.Size(163, 22);
            this.txtMaSoThue.TabIndex = 6;
            // 
            // txtNguoiDaiDien
            // 
            this.txtNguoiDaiDien.Location = new System.Drawing.Point(362, 100);
            this.txtNguoiDaiDien.Name = "txtNguoiDaiDien";
            this.txtNguoiDaiDien.Size = new System.Drawing.Size(194, 22);
            this.txtNguoiDaiDien.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(274, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 19);
            this.label7.TabIndex = 10;
            this.label7.Text = "Người đại diện:";
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Location = new System.Drawing.Point(105, 100);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(163, 22);
            this.txtSoDienThoai.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(23, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 19);
            this.label6.TabIndex = 8;
            this.label6.Text = "Số điện thoại:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(105, 72);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(451, 22);
            this.txtDiaChi.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(61, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "Địa chỉ:";
            // 
            // txtTenKhachHang
            // 
            this.txtTenKhachHang.Location = new System.Drawing.Point(105, 44);
            this.txtTenKhachHang.Name = "txtTenKhachHang";
            this.txtTenKhachHang.Size = new System.Drawing.Size(451, 22);
            this.txtTenKhachHang.TabIndex = 2;
            // 
            // txtMaKhachHang
            // 
            this.txtMaKhachHang.Location = new System.Drawing.Point(362, 16);
            this.txtMaKhachHang.Name = "txtMaKhachHang";
            this.txtMaKhachHang.Size = new System.Drawing.Size(194, 22);
            this.txtMaKhachHang.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tên khách hàng:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(269, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mã khách hàng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Id ";
            // 
            // txtIdKhachHang
            // 
            this.txtIdKhachHang.Enabled = false;
            this.txtIdKhachHang.Location = new System.Drawing.Point(105, 16);
            this.txtIdKhachHang.Name = "txtIdKhachHang";
            this.txtIdKhachHang.ReadOnly = true;
            this.txtIdKhachHang.Size = new System.Drawing.Size(163, 22);
            this.txtIdKhachHang.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(34, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 19);
            this.label8.TabIndex = 14;
            this.label8.Text = "Mã số thuế:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(282, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 19);
            this.label9.TabIndex = 15;
            this.label9.Text = "Số hợp đồng:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(11, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 19);
            this.label11.TabIndex = 59;
            this.label11.Text = "Ngày hợp đồng:";
            // 
            // chkThemMoiLienTuc
            // 
            this.chkThemMoiLienTuc.AutoSize = true;
            this.chkThemMoiLienTuc.Checked = true;
            this.chkThemMoiLienTuc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThemMoiLienTuc.Location = new System.Drawing.Point(361, 184);
            this.chkThemMoiLienTuc.Name = "chkThemMoiLienTuc";
            this.chkThemMoiLienTuc.Size = new System.Drawing.Size(125, 18);
            this.chkThemMoiLienTuc.TabIndex = 60;
            this.chkThemMoiLienTuc.TabStop = false;
            this.chkThemMoiLienTuc.Text = "Thêm mới liên tục";
            this.chkThemMoiLienTuc.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblMessage.Location = new System.Drawing.Point(3, 208);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(574, 19);
            this.lblMessage.TabIndex = 61;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(589, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "THÔNG TIN KHÁCH HÀNG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmThemMoiKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 347);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThemMoiKhachHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông tin khách hàng";
            this.Load += new System.EventHandler(this.FrmThemMoiKhachHang_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmThemMoiKhachHang_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStthienThi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.GridEX.EditControls.EditBox txtNguoiDaiDien;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.GridEX.EditControls.EditBox txtSoDienThoai;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.GridEX.EditControls.EditBox txtDiaChi;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.EditControls.EditBox txtTenKhachHang;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaKhachHang;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtSohopDong;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaSoThue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown txtStthienThi;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private Janus.Windows.EditControls.UIButton cmdNew;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayHopDong;
        private System.Windows.Forms.Label label11;
        public Janus.Windows.GridEX.EditControls.EditBox txtIdKhachHang;
        private System.Windows.Forms.CheckBox chkThemMoiLienTuc;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label1;
    }
}