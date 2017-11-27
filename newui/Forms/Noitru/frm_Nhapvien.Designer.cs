namespace VNS.HIS.UI.NOITRU
{
    partial class frm_Nhapvien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Nhapvien));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtKhoanoitru = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtGhiChu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdTuSinh = new Janus.Windows.EditControls.UIButton();
            this.txtSoBenhAn = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtNgayNhapVien = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.cmdHUY_VAO_VIEN = new Janus.Windows.EditControls.UIButton();
            this.cmdPHIEU_VAOVIEN = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkThoatngaysaukhinhapvien = new Janus.Windows.EditControls.UICheckBox();
            this.chkInNgaySauKhiNhapVien = new Janus.Windows.EditControls.UICheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.chkInNgaySauKhiNhapVien);
            this.uiGroupBox1.Controls.Add(this.chkThoatngaysaukhinhapvien);
            this.uiGroupBox1.Controls.Add(this.txtKhoanoitru);
            this.uiGroupBox1.Controls.Add(this.lblMsg);
            this.uiGroupBox1.Controls.Add(this.txtGhiChu);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.cmdTuSinh);
            this.uiGroupBox1.Controls.Add(this.txtSoBenhAn);
            this.uiGroupBox1.Controls.Add(this.dtNgayNhapVien);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(583, 144);
            this.uiGroupBox1.TabIndex = 1;
            this.uiGroupBox1.Text = "Thông tin nhập viện nội trú";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // txtKhoanoitru
            // 
            this.txtKhoanoitru._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtKhoanoitru._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKhoanoitru._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKhoanoitru.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKhoanoitru.AutoCompleteList")));
            this.txtKhoanoitru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKhoanoitru.CaseSensitive = false;
            this.txtKhoanoitru.CompareNoID = true;
            this.txtKhoanoitru.DefaultCode = "-1";
            this.txtKhoanoitru.DefaultID = "-1";
            this.txtKhoanoitru.Drug_ID = null;
            this.txtKhoanoitru.ExtraWidth = 0;
            this.txtKhoanoitru.FillValueAfterSelect = false;
            this.txtKhoanoitru.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKhoanoitru.Location = new System.Drawing.Point(99, 48);
            this.txtKhoanoitru.MaxHeight = 289;
            this.txtKhoanoitru.MinTypedCharacters = 2;
            this.txtKhoanoitru.MyCode = "-1";
            this.txtKhoanoitru.MyID = "-1";
            this.txtKhoanoitru.MyText = "";
            this.txtKhoanoitru.Name = "txtKhoanoitru";
            this.txtKhoanoitru.RaiseEvent = true;
            this.txtKhoanoitru.RaiseEventEnter = true;
            this.txtKhoanoitru.RaiseEventEnterWhenEmpty = true;
            this.txtKhoanoitru.SelectedIndex = -1;
            this.txtKhoanoitru.Size = new System.Drawing.Size(471, 21);
            this.txtKhoanoitru.splitChar = '@';
            this.txtKhoanoitru.splitCharIDAndCode = '#';
            this.txtKhoanoitru.TabIndex = 2;
            this.txtKhoanoitru.TakeCode = false;
            this.txtKhoanoitru.txtMyCode = null;
            this.txtKhoanoitru.txtMyCode_Edit = null;
            this.txtKhoanoitru.txtMyID = null;
            this.txtKhoanoitru.txtMyID_Edit = null;
            this.txtKhoanoitru.txtMyName = null;
            this.txtKhoanoitru.txtMyName_Edit = null;
            this.txtKhoanoitru.txtNext = null;
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Location = new System.Drawing.Point(3, 126);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(577, 15);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.Text = "Msg";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(99, 76);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(471, 21);
            this.txtGhiChu.TabIndex = 3;
            this.txtGhiChu.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 34;
            this.label4.Text = "Ghi chú thêm:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(291, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Số bệnh án:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ngày nhập viện:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Khoa nội trú:";
            // 
            // cmdTuSinh
            // 
            this.cmdTuSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTuSinh.Image = ((System.Drawing.Image)(resources.GetObject("cmdTuSinh.Image")));
            this.cmdTuSinh.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdTuSinh.Location = new System.Drawing.Point(477, 16);
            this.cmdTuSinh.Name = "cmdTuSinh";
            this.cmdTuSinh.Size = new System.Drawing.Size(93, 27);
            this.cmdTuSinh.TabIndex = 6;
            this.cmdTuSinh.TabStop = false;
            this.cmdTuSinh.Text = "Refresh";
            this.cmdTuSinh.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdTuSinh.Click += new System.EventHandler(this.cmdTuSinh_Click);
            // 
            // txtSoBenhAn
            // 
            this.txtSoBenhAn.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoBenhAn.Location = new System.Drawing.Point(367, 19);
            this.txtSoBenhAn.Name = "txtSoBenhAn";
            this.txtSoBenhAn.Size = new System.Drawing.Size(104, 23);
            this.txtSoBenhAn.TabIndex = 1;
            this.txtSoBenhAn.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // dtNgayNhapVien
            // 
            this.dtNgayNhapVien.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtNgayNhapVien.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayNhapVien.DropDownCalendar.Name = "";
            this.dtNgayNhapVien.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtNgayNhapVien.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayNhapVien.Location = new System.Drawing.Point(99, 19);
            this.dtNgayNhapVien.Name = "dtNgayNhapVien";
            this.dtNgayNhapVien.ShowUpDown = true;
            this.dtNgayNhapVien.Size = new System.Drawing.Size(186, 23);
            this.dtNgayNhapVien.TabIndex = 0;
            this.dtNgayNhapVien.Value = new System.DateTime(2013, 1, 16, 0, 0, 0, 0);
            this.dtNgayNhapVien.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // cmdHUY_VAO_VIEN
            // 
            this.cmdHUY_VAO_VIEN.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHUY_VAO_VIEN.Image = ((System.Drawing.Image)(resources.GetObject("cmdHUY_VAO_VIEN.Image")));
            this.cmdHUY_VAO_VIEN.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdHUY_VAO_VIEN.Location = new System.Drawing.Point(319, 152);
            this.cmdHUY_VAO_VIEN.Name = "cmdHUY_VAO_VIEN";
            this.cmdHUY_VAO_VIEN.Size = new System.Drawing.Size(104, 33);
            this.cmdHUY_VAO_VIEN.TabIndex = 7;
            this.cmdHUY_VAO_VIEN.Text = "Hủy vào viện";
            this.cmdHUY_VAO_VIEN.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdHUY_VAO_VIEN.Click += new System.EventHandler(this.cmdHUY_VAO_VIEN_Click);
            // 
            // cmdPHIEU_VAOVIEN
            // 
            this.cmdPHIEU_VAOVIEN.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPHIEU_VAOVIEN.Image = ((System.Drawing.Image)(resources.GetObject("cmdPHIEU_VAOVIEN.Image")));
            this.cmdPHIEU_VAOVIEN.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPHIEU_VAOVIEN.Location = new System.Drawing.Point(209, 152);
            this.cmdPHIEU_VAOVIEN.Name = "cmdPHIEU_VAOVIEN";
            this.cmdPHIEU_VAOVIEN.Size = new System.Drawing.Size(104, 33);
            this.cmdPHIEU_VAOVIEN.TabIndex = 6;
            this.cmdPHIEU_VAOVIEN.Text = "In phiếu";
            this.cmdPHIEU_VAOVIEN.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdPHIEU_VAOVIEN.Click += new System.EventHandler(this.cmdPHIEU_VAOVIEN_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(429, 152);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(104, 33);
            this.cmdExit.TabIndex = 8;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAccept.Location = new System.Drawing.Point(99, 152);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(104, 33);
            this.cmdAccept.TabIndex = 6;
            this.cmdAccept.Tag = "0";
            this.cmdAccept.Text = "Nhập viện";
            this.cmdAccept.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // chkThoatngaysaukhinhapvien
            // 
            this.chkThoatngaysaukhinhapvien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkThoatngaysaukhinhapvien.Checked = true;
            this.chkThoatngaysaukhinhapvien.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThoatngaysaukhinhapvien.Font = new System.Drawing.Font("Arial", 9F);
            this.chkThoatngaysaukhinhapvien.Location = new System.Drawing.Point(99, 100);
            this.chkThoatngaysaukhinhapvien.Name = "chkThoatngaysaukhinhapvien";
            this.chkThoatngaysaukhinhapvien.Size = new System.Drawing.Size(190, 23);
            this.chkThoatngaysaukhinhapvien.TabIndex = 437;
            this.chkThoatngaysaukhinhapvien.TabStop = false;
            this.chkThoatngaysaukhinhapvien.Text = "Thoát ngay sau khi nhập viện?";
            this.chkThoatngaysaukhinhapvien.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // chkInNgaySauKhiNhapVien
            // 
            this.chkInNgaySauKhiNhapVien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInNgaySauKhiNhapVien.Checked = true;
            this.chkInNgaySauKhiNhapVien.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInNgaySauKhiNhapVien.Font = new System.Drawing.Font("Arial", 9F);
            this.chkInNgaySauKhiNhapVien.Location = new System.Drawing.Point(367, 100);
            this.chkInNgaySauKhiNhapVien.Name = "chkInNgaySauKhiNhapVien";
            this.chkInNgaySauKhiNhapVien.Size = new System.Drawing.Size(167, 23);
            this.chkInNgaySauKhiNhapVien.TabIndex = 438;
            this.chkInNgaySauKhiNhapVien.TabStop = false;
            this.chkInNgaySauKhiNhapVien.Text = "In ngay sau khi nhập viện";
            this.chkInNgaySauKhiNhapVien.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // frm_Nhapvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 193);
            this.Controls.Add(this.cmdPHIEU_VAOVIEN);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.cmdHUY_VAO_VIEN);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Nhapvien";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập viện nội trú";
            this.Load += new System.EventHandler(this.frm_Nhapvien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIButton cmdHUY_VAO_VIEN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIButton cmdTuSinh;
        internal Janus.Windows.GridEX.EditControls.EditBox txtSoBenhAn;
        public Janus.Windows.CalendarCombo.CalendarCombo dtNgayNhapVien;
        private Janus.Windows.EditControls.UIButton cmdPHIEU_VAOVIEN;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label4;
        internal Janus.Windows.GridEX.EditControls.EditBox txtGhiChu;
        private System.Windows.Forms.Label lblMsg;
        private UCs.AutoCompleteTextbox txtKhoanoitru;
        private Janus.Windows.EditControls.UICheckBox chkInNgaySauKhiNhapVien;
        private Janus.Windows.EditControls.UICheckBox chkThoatngaysaukhinhapvien;
    }
}