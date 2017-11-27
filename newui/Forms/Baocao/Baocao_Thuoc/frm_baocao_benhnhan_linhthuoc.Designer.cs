﻿namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    partial class frm_baocao_benhnhan_linhthuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_baocao_benhnhan_linhthuoc));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.cboStock = new Janus.Windows.EditControls.UIComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboDoiTuong = new Janus.Windows.EditControls.UIComboBox();
            this.lblMoneyCharacter = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.label1 = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.dtNgayInPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdInPhieuXN = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.baocaO_TIEUDE1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 68);
            this.panel1.TabIndex = 84;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "THUOC_BCDSACH_BNHANLINHTHUOC";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(784, 68);
            this.baocaO_TIEUDE1.TabIndex = 0;
            this.baocaO_TIEUDE1.TIEUDE = "BÁO CÁO CẤP PHÁT THUỐC BỆNH NHÂN";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.cboStock);
            this.uiGroupBox2.Controls.Add(this.label8);
            this.uiGroupBox2.Controls.Add(this.cboDoiTuong);
            this.uiGroupBox2.Controls.Add(this.lblMoneyCharacter);
            this.uiGroupBox2.Controls.Add(this.label5);
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.dtToDate);
            this.uiGroupBox2.Controls.Add(this.dtFromDate);
            this.uiGroupBox2.Controls.Add(this.chkByDate);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox2.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox2.Image")));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 68);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(784, 446);
            this.uiGroupBox2.TabIndex = 85;
            this.uiGroupBox2.Text = "Thông tin tìm kiếm";
            // 
            // cboStock
            // 
            this.cboStock.Font = new System.Drawing.Font("Arial", 9F);
            this.cboStock.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboStock.Location = new System.Drawing.Point(445, 50);
            this.cboStock.Name = "cboStock";
            this.cboStock.SelectInDataSource = true;
            this.cboStock.Size = new System.Drawing.Size(221, 21);
            this.cboStock.TabIndex = 33;
            this.cboStock.Text = "Kho thuốc";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.Location = new System.Drawing.Point(377, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 15);
            this.label8.TabIndex = 32;
            this.label8.Text = "Kho thuốc";
            // 
            // cboDoiTuong
            // 
            this.cboDoiTuong.Font = new System.Drawing.Font("Arial", 9F);
            this.cboDoiTuong.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboDoiTuong.Location = new System.Drawing.Point(127, 50);
            this.cboDoiTuong.Name = "cboDoiTuong";
            this.cboDoiTuong.SelectInDataSource = true;
            this.cboDoiTuong.Size = new System.Drawing.Size(244, 21);
            this.cboDoiTuong.TabIndex = 27;
            this.cboDoiTuong.Text = "Đối tượng";
            // 
            // lblMoneyCharacter
            // 
            this.lblMoneyCharacter.AutoSize = true;
            this.lblMoneyCharacter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoneyCharacter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblMoneyCharacter.Location = new System.Drawing.Point(85, 424);
            this.lblMoneyCharacter.Name = "lblMoneyCharacter";
            this.lblMoneyCharacter.Size = new System.Drawing.Size(15, 15);
            this.lblMoneyCharacter.TabIndex = 24;
            this.lblMoneyCharacter.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F);
            this.label5.Location = new System.Drawing.Point(9, 424);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 15);
            this.label5.TabIndex = 23;
            this.label5.Text = "Bằng chữ :";
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 88);
            this.grdList.Name = "grdList";
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(775, 327);
            this.grdList.TabIndex = 20;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(34, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Đối tượng";
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.Font = new System.Drawing.Font("Arial", 9F);
            this.dtToDate.Location = new System.Drawing.Point(271, 21);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(130, 21);
            this.dtToDate.TabIndex = 15;
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.Font = new System.Drawing.Font("Arial", 9F);
            this.dtFromDate.Location = new System.Drawing.Point(129, 21);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(127, 21);
            this.dtFromDate.TabIndex = 14;
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Font = new System.Drawing.Font("Arial", 9F);
            this.chkByDate.Location = new System.Drawing.Point(12, 21);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(111, 23);
            this.chkByDate.TabIndex = 13;
            this.chkByDate.Text = "Ngày chốt từ";
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(364, 520);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(133, 30);
            this.cmdExportToExcel.TabIndex = 118;
            this.cmdExportToExcel.Text = "Xuất Excel";
            this.cmdExportToExcel.Click += new System.EventHandler(this.cmdExportToExcel_Click_1);
            // 
            // dtNgayInPhieu
            // 
            this.dtNgayInPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtNgayInPhieu.CustomFormat = "dd/MM/yyyy";
            this.dtNgayInPhieu.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayInPhieu.DropDownCalendar.Name = "";
            this.dtNgayInPhieu.Font = new System.Drawing.Font("Arial", 9F);
            this.dtNgayInPhieu.Location = new System.Drawing.Point(99, 526);
            this.dtNgayInPhieu.Name = "dtNgayInPhieu";
            this.dtNgayInPhieu.ShowUpDown = true;
            this.dtNgayInPhieu.Size = new System.Drawing.Size(200, 21);
            this.dtNgayInPhieu.TabIndex = 116;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(11, 531);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 115;
            this.label3.Text = "Ngày in phiếu";
            // 
            // cmdInPhieuXN
            // 
            this.cmdInPhieuXN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInPhieuXN.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdInPhieuXN.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieuXN.Image")));
            this.cmdInPhieuXN.Location = new System.Drawing.Point(503, 520);
            this.cmdInPhieuXN.Name = "cmdInPhieuXN";
            this.cmdInPhieuXN.Size = new System.Drawing.Size(133, 30);
            this.cmdInPhieuXN.TabIndex = 114;
            this.cmdInPhieuXN.Text = "In báo cáo";
            this.cmdInPhieuXN.Click += new System.EventHandler(this.cmdInPhieuXN_Click_1);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(647, 520);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(133, 30);
            this.cmdExit.TabIndex = 113;
            this.cmdExit.Text = "Thoát (Esc)";
            // 
            // gridEXExporter1
            // 
            this.gridEXExporter1.GridEX = this.grdList;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // frm_baocao_benhnhan_linhthuoc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.cmdExportToExcel);
            this.Controls.Add(this.dtNgayInPhieu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdInPhieuXN);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_baocao_benhnhan_linhthuoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THỐNG KÊ THUỐC ĐÃ PHÁT CHO BỆNH NHÂN";
            this.Load += new System.EventHandler(this.frm_baocao_benhnhan_linhthuoc_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_baocao_benhnhan_linhthuoc_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIComboBox cboStock;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.EditControls.UIComboBox cboDoiTuong;
        private System.Windows.Forms.Label lblMoneyCharacter;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayInPhieu;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdInPhieuXN;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE baocaO_TIEUDE1;
    }
}