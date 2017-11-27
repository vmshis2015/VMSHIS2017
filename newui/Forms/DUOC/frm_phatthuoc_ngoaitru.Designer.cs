namespace VNS.HIS.UI.THUOC
{
    partial class frm_phatthuoc_ngoaitru
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
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel2 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_phatthuoc_ngoaitru));
            Janus.Windows.GridEX.GridEXLayout grdPres_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdPresDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiStatusBar2 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.txtPID = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromdate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.txtTenBN = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtPres_ID = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmdConfig = new Janus.Windows.EditControls.UIButton();
            this.label3 = new System.Windows.Forms.Label();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdPhatThuoc = new Janus.Windows.EditControls.UIButton();
            this.cmdKiemTraSoLuong = new Janus.Windows.EditControls.UIButton();
            this.cmdHuyDonThuoc = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.dtNgayPhatThuoc = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.radTatCa = new Janus.Windows.EditControls.UIRadioButton();
            this.radDaXacNhan = new Janus.Windows.EditControls.UIRadioButton();
            this.radChuaXacNhan = new Janus.Windows.EditControls.UIRadioButton();
            this.grdPres = new Janus.Windows.GridEX.GridEX();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cboKho = new Janus.Windows.EditControls.UIComboBox();
            this.grdPresDetail = new Janus.Windows.GridEX.GridEX();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPres)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPresDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // uiStatusBar2
            // 
            this.uiStatusBar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiStatusBar2.Location = new System.Drawing.Point(0, 713);
            this.uiStatusBar2.Name = "uiStatusBar2";
            uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.Key = "";
            uiStatusBarPanel1.ProgressBarValue = 0;
            uiStatusBarPanel1.Text = "Esc: Thoát Form hiện tại";
            uiStatusBarPanel1.Width = 147;
            uiStatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            uiStatusBarPanel2.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel2.Key = "";
            uiStatusBarPanel2.ProgressBarValue = 0;
            uiStatusBarPanel2.Width = 858;
            this.uiStatusBar2.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1,
            uiStatusBarPanel2});
            this.uiStatusBar2.Size = new System.Drawing.Size(1013, 23);
            this.uiStatusBar2.TabIndex = 3;
            this.uiStatusBar2.TabStop = false;
            // 
            // txtPID
            // 
            this.txtPID.BackColor = System.Drawing.Color.White;
            this.txtPID.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtPID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPID.Location = new System.Drawing.Point(147, 59);
            this.txtPID.MaxLength = 50;
            this.txtPID.Name = "txtPID";
            this.txtPID.Numeric = true;
            this.txtPID.Size = new System.Drawing.Size(129, 22);
            this.txtPID.TabIndex = 4;
            this.txtPID.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPID.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(46, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 16);
            this.label10.TabIndex = 461;
            this.label10.Text = "Mã lượt khám:";
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtToDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDate.Location = new System.Drawing.Point(385, 31);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(129, 22);
            this.dtToDate.TabIndex = 2;
            this.dtToDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // dtFromdate
            // 
            this.dtFromdate.CustomFormat = "dd/MM/yyyy";
            this.dtFromdate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromdate.DropDownCalendar.Name = "";
            this.dtFromdate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtFromdate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromdate.Location = new System.Drawing.Point(147, 30);
            this.dtFromdate.Name = "dtFromdate";
            this.dtFromdate.ShowUpDown = true;
            this.dtFromdate.Size = new System.Drawing.Size(129, 22);
            this.dtFromdate.TabIndex = 1;
            this.dtFromdate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdSearch.Location = new System.Drawing.Point(783, 33);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(153, 43);
            this.cmdSearch.TabIndex = 6;
            this.cmdSearch.Text = "Tìm kiếm (F3)";
            this.cmdSearch.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(284, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tên bệnh nhân:";
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkByDate.Location = new System.Drawing.Point(22, 30);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(119, 23);
            this.chkByDate.TabIndex = 0;
            this.chkByDate.TabStop = false;
            this.chkByDate.Text = "Ngày kê đơn từ:";
            // 
            // txtTenBN
            // 
            this.txtTenBN.BackColor = System.Drawing.Color.White;
            this.txtTenBN.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtTenBN.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenBN.Location = new System.Drawing.Point(385, 59);
            this.txtTenBN.Name = "txtTenBN";
            this.txtTenBN.Size = new System.Drawing.Size(357, 22);
            this.txtTenBN.TabIndex = 5;
            this.txtTenBN.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtPres_ID
            // 
            this.txtPres_ID.BackColor = System.Drawing.Color.White;
            this.txtPres_ID.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtPres_ID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPres_ID.Location = new System.Drawing.Point(609, 31);
            this.txtPres_ID.Name = "txtPres_ID";
            this.txtPres_ID.Numeric = true;
            this.txtPres_ID.Size = new System.Drawing.Size(133, 22);
            this.txtPres_ID.TabIndex = 3;
            this.txtPres_ID.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPres_ID.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(520, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID đơn thuốc:";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.cmdConfig);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.txtPID);
            this.panel4.Controls.Add(this.dtToDate);
            this.panel4.Controls.Add(this.dtFromdate);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.cmdSearch);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.txtPres_ID);
            this.panel4.Controls.Add(this.txtTenBN);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.chkByDate);
            this.panel4.Controls.Add(this.vbLine1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1013, 93);
            this.panel4.TabIndex = 0;
            this.panel4.TabStop = true;
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdConfig.Location = new System.Drawing.Point(973, -1);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(39, 33);
            this.cmdConfig.TabIndex = 16;
            this.cmdConfig.TabStop = false;
            this.cmdConfig.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(316, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 16);
            this.label3.TabIndex = 462;
            this.label3.Text = "đến ngày:";
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.vbLine1.Location = new System.Drawing.Point(4, 4);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(962, 22);
            this.vbLine1.TabIndex = 0;
            this.vbLine1.TabStop = false;
            this.vbLine1.YourText = "Tìm kiếm đơn thuốc bệnh nhân";
            // 
            // cmdPhatThuoc
            // 
            this.cmdPhatThuoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPhatThuoc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPhatThuoc.Image = ((System.Drawing.Image)(resources.GetObject("cmdPhatThuoc.Image")));
            this.cmdPhatThuoc.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPhatThuoc.Location = new System.Drawing.Point(615, 9);
            this.cmdPhatThuoc.Name = "cmdPhatThuoc";
            this.cmdPhatThuoc.Size = new System.Drawing.Size(124, 30);
            this.cmdPhatThuoc.TabIndex = 13;
            this.cmdPhatThuoc.Text = "Phát thuốc";
            this.toolTip1.SetToolTip(this.cmdPhatThuoc, "Nhấn vào đây để thực hiện phát thuốc Bệnh nhân(Phím tắt Ctrl+S)");
            this.cmdPhatThuoc.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdPhatThuoc.Click += new System.EventHandler(this.cmdPhatThuoc_Click);
            // 
            // cmdKiemTraSoLuong
            // 
            this.cmdKiemTraSoLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdKiemTraSoLuong.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdKiemTraSoLuong.Image = ((System.Drawing.Image)(resources.GetObject("cmdKiemTraSoLuong.Image")));
            this.cmdKiemTraSoLuong.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdKiemTraSoLuong.Location = new System.Drawing.Point(479, 9);
            this.cmdKiemTraSoLuong.Name = "cmdKiemTraSoLuong";
            this.cmdKiemTraSoLuong.Size = new System.Drawing.Size(124, 30);
            this.cmdKiemTraSoLuong.TabIndex = 12;
            this.cmdKiemTraSoLuong.Text = "Kiểm tra thuốc";
            this.toolTip1.SetToolTip(this.cmdKiemTraSoLuong, "Nhấn vào đây để kiểm tra thuốc tồn trong kho");
            this.cmdKiemTraSoLuong.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // cmdHuyDonThuoc
            // 
            this.cmdHuyDonThuoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHuyDonThuoc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHuyDonThuoc.Image = ((System.Drawing.Image)(resources.GetObject("cmdHuyDonThuoc.Image")));
            this.cmdHuyDonThuoc.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdHuyDonThuoc.Location = new System.Drawing.Point(746, 9);
            this.cmdHuyDonThuoc.Name = "cmdHuyDonThuoc";
            this.cmdHuyDonThuoc.Size = new System.Drawing.Size(124, 30);
            this.cmdHuyDonThuoc.TabIndex = 14;
            this.cmdHuyDonThuoc.Text = "Hủy phát thuốc";
            this.toolTip1.SetToolTip(this.cmdHuyDonThuoc, "Nhấn vào đây để hủy phát thuốc Bệnh nhân");
            this.cmdHuyDonThuoc.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdHuyDonThuoc.Click += new System.EventHandler(this.cmdHuyDonThuoc_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(880, 9);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(124, 30);
            this.cmdExit.TabIndex = 15;
            this.cmdExit.Text = "Thoát";
            this.toolTip1.SetToolTip(this.cmdExit, "Nhấn vào đây để thoát khỏi chức năng( phím tắt Esc)");
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // dtNgayPhatThuoc
            // 
            this.dtNgayPhatThuoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dtNgayPhatThuoc.CustomFormat = "dd/MM/yyyy";
            this.dtNgayPhatThuoc.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayPhatThuoc.DropDownCalendar.Name = "";
            this.dtNgayPhatThuoc.DropDownCalendar.Visible = false;
            this.dtNgayPhatThuoc.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtNgayPhatThuoc.Enabled = false;
            this.dtNgayPhatThuoc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayPhatThuoc.Location = new System.Drawing.Point(110, 10);
            this.dtNgayPhatThuoc.Name = "dtNgayPhatThuoc";
            this.dtNgayPhatThuoc.ShowUpDown = true;
            this.dtNgayPhatThuoc.Size = new System.Drawing.Size(134, 22);
            this.dtNgayPhatThuoc.TabIndex = 11;
            this.dtNgayPhatThuoc.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(5, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 16);
            this.label9.TabIndex = 454;
            this.label9.Text = "Ngày xác nhận:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmdExit);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.cmdHuyDonThuoc);
            this.panel3.Controls.Add(this.dtNgayPhatThuoc);
            this.panel3.Controls.Add(this.cmdKiemTraSoLuong);
            this.panel3.Controls.Add(this.cmdPhatThuoc);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 668);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1013, 45);
            this.panel3.TabIndex = 100;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1013, 575);
            this.panel1.TabIndex = 101;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1013, 575);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel7);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.grdPres);
            this.splitContainer2.Size = new System.Drawing.Size(340, 575);
            this.splitContainer2.SplitterDistance = 33;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.radTatCa);
            this.panel7.Controls.Add(this.radDaXacNhan);
            this.panel7.Controls.Add(this.radChuaXacNhan);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(340, 32);
            this.panel7.TabIndex = 91;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "Trạng thái đơn thuốc:";
            // 
            // radTatCa
            // 
            this.radTatCa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radTatCa.Location = new System.Drawing.Point(152, 4);
            this.radTatCa.Name = "radTatCa";
            this.radTatCa.Size = new System.Drawing.Size(60, 23);
            this.radTatCa.TabIndex = 6;
            this.radTatCa.Text = "Tất cả";
            this.radTatCa.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // radDaXacNhan
            // 
            this.radDaXacNhan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDaXacNhan.Location = new System.Drawing.Point(316, 4);
            this.radDaXacNhan.Name = "radDaXacNhan";
            this.radDaXacNhan.Size = new System.Drawing.Size(83, 23);
            this.radDaXacNhan.TabIndex = 8;
            this.radDaXacNhan.Text = "Đã phát";
            this.radDaXacNhan.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // radChuaXacNhan
            // 
            this.radChuaXacNhan.Checked = true;
            this.radChuaXacNhan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radChuaXacNhan.Location = new System.Drawing.Point(218, 4);
            this.radChuaXacNhan.Name = "radChuaXacNhan";
            this.radChuaXacNhan.Size = new System.Drawing.Size(92, 23);
            this.radChuaXacNhan.TabIndex = 7;
            this.radChuaXacNhan.TabStop = true;
            this.radChuaXacNhan.Text = "Chưa phát";
            this.radChuaXacNhan.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // grdPres
            // 
            this.grdPres.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Tìm" +
    " kiếm thông tin đơn thuốc</FilterRowInfoText></LocalizableData>";
            grdPres_DesignTimeLayout.LayoutString = resources.GetString("grdPres_DesignTimeLayout.LayoutString");
            this.grdPres.DesignTimeLayout = grdPres_DesignTimeLayout;
            this.grdPres.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPres.DynamicFiltering = true;
            this.grdPres.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdPres.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdPres.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPres.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdPres.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPres.GroupByBoxVisible = false;
            this.grdPres.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPres.Location = new System.Drawing.Point(0, 0);
            this.grdPres.Name = "grdPres";
            this.grdPres.RecordNavigator = true;
            this.grdPres.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPres.Size = new System.Drawing.Size(340, 538);
            this.grdPres.TabIndex = 6;
            this.grdPres.TabStop = false;
            this.grdPres.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.grdPres.SelectionChanged += new System.EventHandler(this.grdPres_SelectionChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.grdPresDetail);
            this.splitContainer3.Size = new System.Drawing.Size(669, 575);
            this.splitContainer3.SplitterDistance = 34;
            this.splitContainer3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cboKho);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(669, 32);
            this.panel2.TabIndex = 460;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Kho thuốc-Vt:";
            // 
            // cboKho
            // 
            this.cboKho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKho.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKho.Location = new System.Drawing.Point(103, 3);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(554, 23);
            this.cboKho.TabIndex = 10;
            this.cboKho.Text = "Kho";
            this.cboKho.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cboKho.SelectedIndexChanged += new System.EventHandler(this.cboKho_SelectedIndexChanged);
            // 
            // grdPresDetail
            // 
            this.grdPresDetail.AlternatingColors = true;
            grdPresDetail_DesignTimeLayout.LayoutString = resources.GetString("grdPresDetail_DesignTimeLayout.LayoutString");
            this.grdPresDetail.DesignTimeLayout = grdPresDetail_DesignTimeLayout;
            this.grdPresDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPresDetail.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdPresDetail.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPresDetail.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdPresDetail.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdPresDetail.GroupByBoxVisible = false;
            this.grdPresDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPresDetail.Location = new System.Drawing.Point(0, 0);
            this.grdPresDetail.Name = "grdPresDetail";
            this.grdPresDetail.RecordNavigator = true;
            this.grdPresDetail.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPresDetail.Size = new System.Drawing.Size(669, 537);
            this.grdPresDetail.TabIndex = 460;
            this.grdPresDetail.TabStop = false;
            this.grdPresDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPresDetail.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPresDetail.TotalRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdPresDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPresDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdPresDetail.UpdateMode = Janus.Windows.GridEX.UpdateMode.CellUpdate;
            this.grdPresDetail.UseGroupRowSelector = true;
            this.grdPresDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // frm_phatthuoc_ngoaitru
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1013, 736);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.uiStatusBar2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_phatthuoc_ngoaitru";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phát thuốc bệnh nhân ngoại trú";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPres)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPresDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar2;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtTenBN;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtPres_ID;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.EditControls.UIButton cmdSearch;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromdate;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtPID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIButton cmdPhatThuoc;
        private Janus.Windows.EditControls.UIButton cmdKiemTraSoLuong;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayPhatThuoc;
        private Janus.Windows.EditControls.UIButton cmdHuyDonThuoc;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdConfig;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.EditControls.UIRadioButton radTatCa;
        private Janus.Windows.EditControls.UIRadioButton radDaXacNhan;
        private Janus.Windows.EditControls.UIRadioButton radChuaXacNhan;
        private Janus.Windows.GridEX.GridEX grdPres;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIComboBox cboKho;
        private Janus.Windows.GridEX.GridEX grdPresDetail;
    }
}