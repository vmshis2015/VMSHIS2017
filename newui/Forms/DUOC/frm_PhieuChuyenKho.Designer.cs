﻿namespace VNS.HIS.UI.THUOC
{
    partial class frm_PhieuChuyenKho
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
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel26 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel27 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel28 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel29 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel30 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PhieuChuyenKho));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdPhieuNhapChiTiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiStatusBar2 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdThemPhieuNhap = new System.Windows.Forms.ToolStripButton();
            this.cmdUpdatePhieuNhap = new System.Windows.Forms.ToolStripButton();
            this.cmdXoaPhieuNhap = new System.Windows.Forms.ToolStripButton();
            this.cmdNhapKho = new System.Windows.Forms.ToolStripButton();
            this.cmdHuychuyenkho = new System.Windows.Forms.ToolStripButton();
            this.cmdInPhieuNhapKho = new System.Windows.Forms.ToolStripButton();
            this.cmdCauhinh = new System.Windows.Forms.ToolStripButton();
            this.cmdExit = new System.Windows.Forms.ToolStripButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtTenthuoc = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKhoXuat = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtKhoNhap = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtNhanvien = new VNS.HIS.UCs.AutoCompleteTextbox_Nhanvien();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboKhoXuat = new Janus.Windows.EditControls.UIComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboKhoNhap = new Janus.Windows.EditControls.UIComboBox();
            this.radChuaNhapKho = new Janus.Windows.EditControls.UIRadioButton();
            this.radDaNhap = new Janus.Windows.EditControls.UIRadioButton();
            this.radTatCa = new Janus.Windows.EditControls.UIRadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cboNhanVien = new Janus.Windows.EditControls.UIComboBox();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.txtSoPhieu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox4 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdPhieuNhapChiTiet = new Janus.Windows.GridEX.GridEX();
            this.cmdConfig = new Janus.Windows.EditControls.UIButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromdate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).BeginInit();
            this.uiGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPhieuNhapChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // uiStatusBar2
            // 
            this.uiStatusBar2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiStatusBar2.Location = new System.Drawing.Point(0, 676);
            this.uiStatusBar2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uiStatusBar2.Name = "uiStatusBar2";
            uiStatusBarPanel26.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel26.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel26.Key = "";
            uiStatusBarPanel26.ProgressBarValue = 0;
            uiStatusBarPanel26.Text = "Ctrl+N: Thêm mới";
            uiStatusBarPanel26.Width = 113;
            uiStatusBarPanel27.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel27.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel27.Key = "";
            uiStatusBarPanel27.ProgressBarValue = 0;
            uiStatusBarPanel27.Text = "Ctr+U: Sửa phiếu";
            uiStatusBarPanel27.Width = 110;
            uiStatusBarPanel28.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel28.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel28.Key = "";
            uiStatusBarPanel28.ProgressBarValue = 0;
            uiStatusBarPanel28.Text = "Ctrl+D:Xóa phiếu";
            uiStatusBarPanel28.Width = 108;
            uiStatusBarPanel29.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel29.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel29.Key = "";
            uiStatusBarPanel29.ProgressBarValue = 0;
            uiStatusBarPanel29.Text = "Esc: Thoát";
            uiStatusBarPanel29.Width = 73;
            uiStatusBarPanel30.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel30.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel30.FormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            uiStatusBarPanel30.FormatStyle.ForeColor = System.Drawing.Color.Navy;
            uiStatusBarPanel30.Key = "MSG";
            uiStatusBarPanel30.ProgressBarValue = 0;
            uiStatusBarPanel30.Text = "Thông báo";
            uiStatusBarPanel30.Width = 74;
            this.uiStatusBar2.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel26,
            uiStatusBarPanel27,
            uiStatusBarPanel28,
            uiStatusBarPanel29,
            uiStatusBarPanel30});
            this.uiStatusBar2.Size = new System.Drawing.Size(1008, 26);
            this.uiStatusBar2.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.toolStrip1.Font = new System.Drawing.Font("Arial", 10F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdThemPhieuNhap,
            this.cmdUpdatePhieuNhap,
            this.cmdXoaPhieuNhap,
            this.cmdNhapKho,
            this.cmdHuychuyenkho,
            this.cmdInPhieuNhapKho,
            this.cmdCauhinh,
            this.cmdExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 31);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdThemPhieuNhap
            // 
            this.cmdThemPhieuNhap.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemPhieuNhap.Image")));
            this.cmdThemPhieuNhap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdThemPhieuNhap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdThemPhieuNhap.Name = "cmdThemPhieuNhap";
            this.cmdThemPhieuNhap.Size = new System.Drawing.Size(111, 28);
            this.cmdThemPhieuNhap.Text = "Thêm phiếu";
            this.cmdThemPhieuNhap.Click += new System.EventHandler(this.cmdThemPhieuNhap_Click);
            // 
            // cmdUpdatePhieuNhap
            // 
            this.cmdUpdatePhieuNhap.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdatePhieuNhap.Image")));
            this.cmdUpdatePhieuNhap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdUpdatePhieuNhap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUpdatePhieuNhap.Name = "cmdUpdatePhieuNhap";
            this.cmdUpdatePhieuNhap.Size = new System.Drawing.Size(92, 28);
            this.cmdUpdatePhieuNhap.Text = "Sửa phiếu";
            this.cmdUpdatePhieuNhap.Click += new System.EventHandler(this.cmdUpdatePhieuNhap_Click);
            // 
            // cmdXoaPhieuNhap
            // 
            this.cmdXoaPhieuNhap.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoaPhieuNhap.Image")));
            this.cmdXoaPhieuNhap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdXoaPhieuNhap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdXoaPhieuNhap.Name = "cmdXoaPhieuNhap";
            this.cmdXoaPhieuNhap.Size = new System.Drawing.Size(90, 28);
            this.cmdXoaPhieuNhap.Text = "Xóa phiếu";
            this.cmdXoaPhieuNhap.Click += new System.EventHandler(this.cmdXoaPhieuNhap_Click);
            // 
            // cmdNhapKho
            // 
            this.cmdNhapKho.Image = ((System.Drawing.Image)(resources.GetObject("cmdNhapKho.Image")));
            this.cmdNhapKho.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdNhapKho.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdNhapKho.Name = "cmdNhapKho";
            this.cmdNhapKho.Size = new System.Drawing.Size(86, 28);
            this.cmdNhapKho.Text = "Xác nhận";
            this.cmdNhapKho.Click += new System.EventHandler(this.cmdNhapKho_Click);
            // 
            // cmdHuychuyenkho
            // 
            this.cmdHuychuyenkho.Image = ((System.Drawing.Image)(resources.GetObject("cmdHuychuyenkho.Image")));
            this.cmdHuychuyenkho.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdHuychuyenkho.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdHuychuyenkho.Name = "cmdHuychuyenkho";
            this.cmdHuychuyenkho.Size = new System.Drawing.Size(108, 28);
            this.cmdHuychuyenkho.Text = "Hủy xác nhận";
            this.cmdHuychuyenkho.Click += new System.EventHandler(this.cmdHuychuyenkho_Click);
            // 
            // cmdInPhieuNhapKho
            // 
            this.cmdInPhieuNhapKho.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieuNhapKho.Image")));
            this.cmdInPhieuNhapKho.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdInPhieuNhapKho.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdInPhieuNhapKho.Name = "cmdInPhieuNhapKho";
            this.cmdInPhieuNhapKho.Size = new System.Drawing.Size(79, 28);
            this.cmdInPhieuNhapKho.Text = "In phiếu";
            this.cmdInPhieuNhapKho.Click += new System.EventHandler(this.cmdInPhieuNhapKho_Click);
            // 
            // cmdCauhinh
            // 
            this.cmdCauhinh.Image = ((System.Drawing.Image)(resources.GetObject("cmdCauhinh.Image")));
            this.cmdCauhinh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdCauhinh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCauhinh.Name = "cmdCauhinh";
            this.cmdCauhinh.Size = new System.Drawing.Size(85, 28);
            this.cmdCauhinh.Text = "Cấu hình";
            this.cmdCauhinh.Click += new System.EventHandler(this.cmdCauhinh_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(95, 28);
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.dtFromdate);
            this.uiGroupBox1.Controls.Add(this.txtTenthuoc);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.txtKhoXuat);
            this.uiGroupBox1.Controls.Add(this.txtKhoNhap);
            this.uiGroupBox1.Controls.Add(this.txtNhanvien);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.cboKhoXuat);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.cboKhoNhap);
            this.uiGroupBox1.Controls.Add(this.radChuaNhapKho);
            this.uiGroupBox1.Controls.Add(this.radDaNhap);
            this.uiGroupBox1.Controls.Add(this.radTatCa);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.cboNhanVien);
            this.uiGroupBox1.Controls.Add(this.cmdSearch);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.txtSoPhieu);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 31);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 139);
            this.uiGroupBox1.TabIndex = 6;
            this.uiGroupBox1.Text = "Tìm kiếm thông tin ";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // txtTenthuoc
            // 
            this.txtTenthuoc._backcolor = System.Drawing.SystemColors.Control;
            this.txtTenthuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenthuoc._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTenthuoc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTenthuoc.AutoCompleteList")));
            this.txtTenthuoc.BackColor = System.Drawing.Color.Honeydew;
            this.txtTenthuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenthuoc.CaseSensitive = false;
            this.txtTenthuoc.CompareNoID = true;
            this.txtTenthuoc.DefaultCode = "-1";
            this.txtTenthuoc.DefaultID = "-1";
            this.txtTenthuoc.Drug_ID = null;
            this.txtTenthuoc.ExtraWidth = 0;
            this.txtTenthuoc.FillValueAfterSelect = false;
            this.txtTenthuoc.Location = new System.Drawing.Point(94, 108);
            this.txtTenthuoc.MaxHeight = -1;
            this.txtTenthuoc.MinTypedCharacters = 2;
            this.txtTenthuoc.MyCode = "-1";
            this.txtTenthuoc.MyID = "-1";
            this.txtTenthuoc.MyText = "";
            this.txtTenthuoc.Name = "txtTenthuoc";
            this.txtTenthuoc.RaiseEvent = false;
            this.txtTenthuoc.RaiseEventEnter = false;
            this.txtTenthuoc.RaiseEventEnterWhenEmpty = false;
            this.txtTenthuoc.SelectedIndex = -1;
            this.txtTenthuoc.Size = new System.Drawing.Size(454, 21);
            this.txtTenthuoc.splitChar = '@';
            this.txtTenthuoc.splitCharIDAndCode = '#';
            this.txtTenthuoc.TabIndex = 132;
            this.txtTenthuoc.TakeCode = false;
            this.txtTenthuoc.txtMyCode = null;
            this.txtTenthuoc.txtMyCode_Edit = null;
            this.txtTenthuoc.txtMyID = null;
            this.txtTenthuoc.txtMyID_Edit = null;
            this.txtTenthuoc.txtMyName = null;
            this.txtTenthuoc.txtMyName_Edit = null;
            this.txtTenthuoc.txtNext = null;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(29, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 131;
            this.label6.Text = "Tên thuốc:";
            // 
            // txtKhoXuat
            // 
            this.txtKhoXuat._backcolor = System.Drawing.SystemColors.Control;
            this.txtKhoXuat._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKhoXuat._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKhoXuat.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKhoXuat.AutoCompleteList")));
            this.txtKhoXuat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKhoXuat.CaseSensitive = false;
            this.txtKhoXuat.CompareNoID = true;
            this.txtKhoXuat.DefaultCode = "-1";
            this.txtKhoXuat.DefaultID = "-1";
            this.txtKhoXuat.Drug_ID = null;
            this.txtKhoXuat.ExtraWidth = 0;
            this.txtKhoXuat.FillValueAfterSelect = false;
            this.txtKhoXuat.Location = new System.Drawing.Point(470, 51);
            this.txtKhoXuat.MaxHeight = -1;
            this.txtKhoXuat.MinTypedCharacters = 2;
            this.txtKhoXuat.MyCode = "-1";
            this.txtKhoXuat.MyID = "-1";
            this.txtKhoXuat.MyText = "";
            this.txtKhoXuat.Name = "txtKhoXuat";
            this.txtKhoXuat.RaiseEvent = false;
            this.txtKhoXuat.RaiseEventEnter = false;
            this.txtKhoXuat.RaiseEventEnterWhenEmpty = false;
            this.txtKhoXuat.SelectedIndex = -1;
            this.txtKhoXuat.Size = new System.Drawing.Size(367, 21);
            this.txtKhoXuat.splitChar = '@';
            this.txtKhoXuat.splitCharIDAndCode = '#';
            this.txtKhoXuat.TabIndex = 20;
            this.txtKhoXuat.TakeCode = false;
            this.txtKhoXuat.txtMyCode = null;
            this.txtKhoXuat.txtMyCode_Edit = null;
            this.txtKhoXuat.txtMyID = null;
            this.txtKhoXuat.txtMyID_Edit = null;
            this.txtKhoXuat.txtMyName = null;
            this.txtKhoXuat.txtMyName_Edit = null;
            this.txtKhoXuat.txtNext = null;
            // 
            // txtKhoNhap
            // 
            this.txtKhoNhap._backcolor = System.Drawing.SystemColors.Control;
            this.txtKhoNhap._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKhoNhap._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKhoNhap.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKhoNhap.AutoCompleteList")));
            this.txtKhoNhap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKhoNhap.CaseSensitive = false;
            this.txtKhoNhap.CompareNoID = true;
            this.txtKhoNhap.DefaultCode = "-1";
            this.txtKhoNhap.DefaultID = "-1";
            this.txtKhoNhap.Drug_ID = null;
            this.txtKhoNhap.ExtraWidth = 0;
            this.txtKhoNhap.FillValueAfterSelect = false;
            this.txtKhoNhap.Location = new System.Drawing.Point(94, 52);
            this.txtKhoNhap.MaxHeight = -1;
            this.txtKhoNhap.MinTypedCharacters = 2;
            this.txtKhoNhap.MyCode = "-1";
            this.txtKhoNhap.MyID = "-1";
            this.txtKhoNhap.MyText = "";
            this.txtKhoNhap.Name = "txtKhoNhap";
            this.txtKhoNhap.RaiseEvent = false;
            this.txtKhoNhap.RaiseEventEnter = false;
            this.txtKhoNhap.RaiseEventEnterWhenEmpty = false;
            this.txtKhoNhap.SelectedIndex = -1;
            this.txtKhoNhap.Size = new System.Drawing.Size(291, 21);
            this.txtKhoNhap.splitChar = '@';
            this.txtKhoNhap.splitCharIDAndCode = '#';
            this.txtKhoNhap.TabIndex = 19;
            this.txtKhoNhap.TakeCode = false;
            this.txtKhoNhap.txtMyCode = null;
            this.txtKhoNhap.txtMyCode_Edit = null;
            this.txtKhoNhap.txtMyID = null;
            this.txtKhoNhap.txtMyID_Edit = null;
            this.txtKhoNhap.txtMyName = null;
            this.txtKhoNhap.txtMyName_Edit = null;
            this.txtKhoNhap.txtNext = null;
            // 
            // txtNhanvien
            // 
            this.txtNhanvien._backcolor = System.Drawing.SystemColors.Control;
            this.txtNhanvien._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhanvien.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNhanvien.AutoCompleteList")));
            this.txtNhanvien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNhanvien.CaseSensitive = false;
            this.txtNhanvien.CompareNoID = true;
            this.txtNhanvien.DefaultCode = "-1";
            this.txtNhanvien.DefaultID = "-1";
            this.txtNhanvien.Drug_ID = null;
            this.txtNhanvien.ExtraWidth = 0;
            this.txtNhanvien.FillValueAfterSelect = false;
            this.txtNhanvien.LOAI_NHANVIEN = null;
            this.txtNhanvien.Location = new System.Drawing.Point(470, 23);
            this.txtNhanvien.MaxHeight = 200;
            this.txtNhanvien.MinTypedCharacters = 2;
            this.txtNhanvien.MyCode = "-1";
            this.txtNhanvien.MyID = "-1";
            this.txtNhanvien.Name = "txtNhanvien";
            this.txtNhanvien.RaiseEvent = false;
            this.txtNhanvien.RaiseEventEnter = false;
            this.txtNhanvien.RaiseEventEnterWhenEmpty = false;
            this.txtNhanvien.SelectedIndex = -1;
            this.txtNhanvien.Size = new System.Drawing.Size(367, 21);
            this.txtNhanvien.splitChar = '@';
            this.txtNhanvien.splitCharIDAndCode = '#';
            this.txtNhanvien.TabIndex = 18;
            this.txtNhanvien.TakeCode = false;
            this.txtNhanvien.txtMyCode = null;
            this.txtNhanvien.txtMyCode_Edit = null;
            this.txtNhanvien.txtMyID = null;
            this.txtNhanvien.txtMyID_Edit = null;
            this.txtNhanvien.txtMyName = null;
            this.txtNhanvien.txtMyName_Edit = null;
            this.txtNhanvien.txtNext = null;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(392, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 10);
            this.label5.TabIndex = 15;
            this.label5.Text = "Trạng thái";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(400, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 14;
            this.label4.Text = "Kho xuất:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboKhoXuat
            // 
            this.cboKhoXuat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKhoXuat.Location = new System.Drawing.Point(827, 51);
            this.cboKhoXuat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboKhoXuat.MaxDropDownItems = 15;
            this.cboKhoXuat.Name = "cboKhoXuat";
            this.cboKhoXuat.Size = new System.Drawing.Size(10, 21);
            this.cboKhoXuat.TabIndex = 13;
            this.cboKhoXuat.Text = "Kho thuốc";
            this.cboKhoXuat.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Kho nhập:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboKhoNhap
            // 
            this.cboKhoNhap.Location = new System.Drawing.Point(395, 53);
            this.cboKhoNhap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboKhoNhap.MaxDropDownItems = 15;
            this.cboKhoNhap.Name = "cboKhoNhap";
            this.cboKhoNhap.Size = new System.Drawing.Size(10, 21);
            this.cboKhoNhap.TabIndex = 11;
            this.cboKhoNhap.Text = "Kho thuốc";
            this.cboKhoNhap.Visible = false;
            // 
            // radChuaNhapKho
            // 
            this.radChuaNhapKho.Location = new System.Drawing.Point(694, 79);
            this.radChuaNhapKho.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radChuaNhapKho.Name = "radChuaNhapKho";
            this.radChuaNhapKho.Size = new System.Drawing.Size(143, 26);
            this.radChuaNhapKho.TabIndex = 10;
            this.radChuaNhapKho.Text = "Chưa chuyển kho";
            this.radChuaNhapKho.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // radDaNhap
            // 
            this.radDaNhap.Location = new System.Drawing.Point(565, 80);
            this.radDaNhap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radDaNhap.Name = "radDaNhap";
            this.radDaNhap.Size = new System.Drawing.Size(124, 26);
            this.radDaNhap.TabIndex = 9;
            this.radDaNhap.Text = "Đã chuyển kho";
            this.radDaNhap.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // radTatCa
            // 
            this.radTatCa.Checked = true;
            this.radTatCa.Location = new System.Drawing.Point(470, 79);
            this.radTatCa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radTatCa.Name = "radTatCa";
            this.radTatCa.Size = new System.Drawing.Size(89, 26);
            this.radTatCa.TabIndex = 8;
            this.radTatCa.TabStop = true;
            this.radTatCa.Text = "Tất cả";
            this.radTatCa.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(400, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nhân viên:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboNhanVien.Location = new System.Drawing.Point(827, 24);
            this.cboNhanVien.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboNhanVien.MaxDropDownItems = 15;
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(10, 21);
            this.cboNhanVien.TabIndex = 6;
            this.cboNhanVien.Text = "Nhân viên";
            this.cboNhanVien.Visible = false;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSearch.Location = new System.Drawing.Point(844, 20);
            this.cmdSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(153, 56);
            this.cmdSearch.TabIndex = 5;
            this.cmdSearch.Text = "Tìm kiếm(F3)";
            this.cmdSearch.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(26, 77);
            this.chkByDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(68, 26);
            this.chkByDate.TabIndex = 3;
            this.chkByDate.Text = "Từ ngày";
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // txtSoPhieu
            // 
            this.txtSoPhieu.BackColor = System.Drawing.Color.White;
            this.txtSoPhieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoPhieu.Location = new System.Drawing.Point(94, 23);
            this.txtSoPhieu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSoPhieu.Name = "txtSoPhieu";
            this.txtSoPhieu.Size = new System.Drawing.Size(291, 21);
            this.txtSoPhieu.TabIndex = 1;
            this.txtSoPhieu.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            this.txtSoPhieu.TextChanged += new System.EventHandler(this.txtSoPhieu_TextChanged);
            this.txtSoPhieu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSoPhieu_KeyDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số phiếu:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 170);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uiGroupBox4);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 506);
            this.splitContainer1.SplitterDistance = 549;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 7;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(547, 504);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Thông tin phiếu chuyển kho";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdList
            // 
            this.grdList.AlternatingColors = true;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin phiếu nhập khoa</FilterRowInfoText></LocalizableData>";
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 17);
            this.grdList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(541, 484);
            this.grdList.TabIndex = 2;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.grdPhieuNhapChiTiet);
            this.uiGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox4.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox4.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Size = new System.Drawing.Size(452, 504);
            this.uiGroupBox4.TabIndex = 2;
            this.uiGroupBox4.Text = "Chi tiết phiếu chuyển kho đang chọn";
            this.uiGroupBox4.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdPhieuNhapChiTiet
            // 
            this.grdPhieuNhapChiTiet.AlternatingColors = true;
            grdPhieuNhapChiTiet_DesignTimeLayout.LayoutString = resources.GetString("grdPhieuNhapChiTiet_DesignTimeLayout.LayoutString");
            this.grdPhieuNhapChiTiet.DesignTimeLayout = grdPhieuNhapChiTiet_DesignTimeLayout;
            this.grdPhieuNhapChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPhieuNhapChiTiet.DynamicFiltering = true;
            this.grdPhieuNhapChiTiet.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdPhieuNhapChiTiet.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPhieuNhapChiTiet.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdPhieuNhapChiTiet.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPhieuNhapChiTiet.GroupByBoxVisible = false;
            this.grdPhieuNhapChiTiet.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPhieuNhapChiTiet.Location = new System.Drawing.Point(3, 17);
            this.grdPhieuNhapChiTiet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdPhieuNhapChiTiet.Name = "grdPhieuNhapChiTiet";
            this.grdPhieuNhapChiTiet.RecordNavigator = true;
            this.grdPhieuNhapChiTiet.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPhieuNhapChiTiet.Size = new System.Drawing.Size(446, 484);
            this.grdPhieuNhapChiTiet.TabIndex = 2;
            this.grdPhieuNhapChiTiet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPhieuNhapChiTiet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdPhieuNhapChiTiet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.Location = new System.Drawing.Point(970, 3);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(39, 33);
            this.cmdConfig.TabIndex = 460;
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // dtToDate
            // 
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtToDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDate.Location = new System.Drawing.Point(259, 80);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(126, 21);
            this.dtToDate.TabIndex = 134;
            this.dtToDate.Value = new System.DateTime(2014, 9, 12, 0, 0, 0, 0);
            this.dtToDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // dtFromdate
            // 
            // 
            // 
            // 
            this.dtFromdate.DropDownCalendar.Name = "";
            this.dtFromdate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtFromdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromdate.Location = new System.Drawing.Point(94, 80);
            this.dtFromdate.Name = "dtFromdate";
            this.dtFromdate.ShowUpDown = true;
            this.dtFromdate.Size = new System.Drawing.Size(126, 21);
            this.dtFromdate.TabIndex = 133;
            this.dtFromdate.Value = new System.DateTime(2014, 9, 12, 0, 0, 0, 0);
            this.dtFromdate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // frm_PhieuChuyenKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 702);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.uiStatusBar2);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_PhieuChuyenKho";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu chuyển kho";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_PhieuChuyenKho_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).EndInit();
            this.uiGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPhieuNhapChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdThemPhieuNhap;
        private System.Windows.Forms.ToolStripButton cmdUpdatePhieuNhap;
        private System.Windows.Forms.ToolStripButton cmdXoaPhieuNhap;
        private System.Windows.Forms.ToolStripButton cmdNhapKho;
        private System.Windows.Forms.ToolStripButton cmdInPhieuNhapKho;
        private System.Windows.Forms.ToolStripButton cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIComboBox cboKhoXuat;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox cboKhoNhap;
        private Janus.Windows.EditControls.UIRadioButton radChuaNhapKho;
        private Janus.Windows.EditControls.UIRadioButton radDaNhap;
        private Janus.Windows.EditControls.UIRadioButton radTatCa;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIComboBox cboNhanVien;
        private Janus.Windows.EditControls.UIButton cmdSearch;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.GridEX.EditControls.EditBox txtSoPhieu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox4;
        private System.Windows.Forms.ToolStripButton cmdHuychuyenkho;
        private Janus.Windows.EditControls.UIButton cmdConfig;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.GridEX.GridEX grdPhieuNhapChiTiet;
        private System.Windows.Forms.ToolStripButton cmdCauhinh;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
        private UCs.AutoCompleteTextbox_Nhanvien txtNhanvien;
        private UCs.AutoCompleteTextbox txtKhoXuat;
        private UCs.AutoCompleteTextbox txtKhoNhap;
        private UCs.AutoCompleteTextbox txtTenthuoc;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromdate;
    }
}