namespace VMS.HIS.KSK.Forms
{
    partial class frm_ImportExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ImportExcel));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdServiceDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdChiDinh_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout cboKieuKham_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdPhongkham_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdDanhSachCLS_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdThongtinChidinh_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtDoanhNghiep = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cmdThemMoiKH = new Janus.Windows.EditControls.UIButton();
            this.cmdSeachByLo = new Janus.Windows.EditControls.UIButton();
            this.cmdDeletebyLo = new Janus.Windows.EditControls.UIButton();
            this.cmdChooseFile = new Janus.Windows.EditControls.UIButton();
            this.dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSolo = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSourceFile = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabKSK = new Janus.Windows.UI.Tab.UITab();
            this.tabDanhSach = new Janus.Windows.UI.Tab.UITabPage();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.tabLog = new Janus.Windows.UI.Tab.UITabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.grdServiceDetail = new Janus.Windows.GridEX.GridEX();
            this.cmdInPhieu = new Janus.Windows.EditControls.UIButton();
            this.cmdDeleteDichVu = new Janus.Windows.EditControls.UIButton();
            this.cmdAddChiDinh = new Janus.Windows.EditControls.UIButton();
            this.grdChiDinh = new Janus.Windows.GridEX.GridEX();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.cboKieuKham = new Janus.Windows.GridEX.EditControls.MultiColumnCombo();
            this.txtIDKieuKham = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtIDPkham = new Janus.Windows.GridEX.EditControls.EditBox();
            this.chkChiDinhNhanh = new Janus.Windows.EditControls.UICheckBox();
            this.cmdAddDvuKCB = new Janus.Windows.EditControls.UIButton();
            this.txtExamtypeCode = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.grdPhongkham = new Janus.Windows.GridEX.GridEX();
            this.tabChiDinh = new Janus.Windows.UI.Tab.UITabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.txtNhomCD = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdTaonhom = new Janus.Windows.EditControls.UIButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFilterName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdDanhSachCLS = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox4 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdThongtinChidinh = new Janus.Windows.GridEX.GridEX();
            this.tabChiDinhOld = new Janus.Windows.UI.Tab.UITabPage();
            this.rtxtLogs = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDelLog = new System.Windows.Forms.ToolStripMenuItem();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.cmdConfig = new Janus.Windows.EditControls.UIButton();
            this.cmdThoat = new Janus.Windows.EditControls.UIButton();
            this.cmdSentToLis = new Janus.Windows.EditControls.UIButton();
            this.cmdImportToHis = new Janus.Windows.EditControls.UIButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabKSK)).BeginInit();
            this.tabKSK.SuspendLayout();
            this.tabDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.tabLog.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdServiceDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdChiDinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboKieuKham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPhongkham)).BeginInit();
            this.tabChiDinh.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDanhSachCLS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).BeginInit();
            this.uiGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdThongtinChidinh)).BeginInit();
            this.tabChiDinhOld.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 619);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1036, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 619);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1036, 619);
            this.splitContainer1.SplitterDistance = 107;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.txtDoanhNghiep);
            this.uiGroupBox1.Controls.Add(this.cmdThemMoiKH);
            this.uiGroupBox1.Controls.Add(this.cmdSeachByLo);
            this.uiGroupBox1.Controls.Add(this.cmdDeletebyLo);
            this.uiGroupBox1.Controls.Add(this.cmdChooseFile);
            this.uiGroupBox1.Controls.Add(this.dtpNgayNhap);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.txtSolo);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtSourceFile);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1036, 107);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin import";
            // 
            // txtDoanhNghiep
            // 
            this.txtDoanhNghiep._backcolor = System.Drawing.SystemColors.Control;
            this.txtDoanhNghiep._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoanhNghiep._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDoanhNghiep.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtDoanhNghiep.AutoCompleteList")));
            this.txtDoanhNghiep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDoanhNghiep.CaseSensitive = true;
            this.txtDoanhNghiep.CompareNoID = true;
            this.txtDoanhNghiep.DefaultCode = "-1";
            this.txtDoanhNghiep.DefaultID = "-1";
            this.txtDoanhNghiep.Drug_ID = null;
            this.txtDoanhNghiep.ExtraWidth = 0;
            this.txtDoanhNghiep.FillValueAfterSelect = false;
            this.txtDoanhNghiep.Location = new System.Drawing.Point(163, 80);
            this.txtDoanhNghiep.MaxHeight = 289;
            this.txtDoanhNghiep.MinTypedCharacters = 2;
            this.txtDoanhNghiep.Multiline = true;
            this.txtDoanhNghiep.MyCode = "-1";
            this.txtDoanhNghiep.MyID = "-1";
            this.txtDoanhNghiep.MyText = "";
            this.txtDoanhNghiep.Name = "txtDoanhNghiep";
            this.txtDoanhNghiep.RaiseEvent = true;
            this.txtDoanhNghiep.RaiseEventEnter = true;
            this.txtDoanhNghiep.RaiseEventEnterWhenEmpty = true;
            this.txtDoanhNghiep.SelectedIndex = -1;
            this.txtDoanhNghiep.Size = new System.Drawing.Size(457, 23);
            this.txtDoanhNghiep.splitChar = '@';
            this.txtDoanhNghiep.splitCharIDAndCode = '#';
            this.txtDoanhNghiep.TabIndex = 13;
            this.txtDoanhNghiep.TakeCode = false;
            this.txtDoanhNghiep.txtMyCode = null;
            this.txtDoanhNghiep.txtMyCode_Edit = null;
            this.txtDoanhNghiep.txtMyID = null;
            this.txtDoanhNghiep.txtMyID_Edit = null;
            this.txtDoanhNghiep.txtMyName = null;
            this.txtDoanhNghiep.txtMyName_Edit = null;
            this.txtDoanhNghiep.txtNext = null;
            // 
            // cmdThemMoiKH
            // 
            this.cmdThemMoiKH.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThemMoiKH.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemMoiKH.Image")));
            this.cmdThemMoiKH.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdThemMoiKH.Location = new System.Drawing.Point(621, 79);
            this.cmdThemMoiKH.Name = "cmdThemMoiKH";
            this.cmdThemMoiKH.Size = new System.Drawing.Size(25, 25);
            this.cmdThemMoiKH.TabIndex = 12;
            this.cmdThemMoiKH.TabStop = false;
            this.cmdThemMoiKH.Click += new System.EventHandler(this.cmdThemMoiKH_Click);
            // 
            // cmdSeachByLo
            // 
            this.cmdSeachByLo.Image = ((System.Drawing.Image)(resources.GetObject("cmdSeachByLo.Image")));
            this.cmdSeachByLo.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSeachByLo.Location = new System.Drawing.Point(805, 69);
            this.cmdSeachByLo.Name = "cmdSeachByLo";
            this.cmdSeachByLo.Size = new System.Drawing.Size(109, 32);
            this.cmdSeachByLo.TabIndex = 10;
            this.cmdSeachByLo.Text = "Tìm kiếm";
            this.cmdSeachByLo.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdSeachByLo.Click += new System.EventHandler(this.cmdSeachByLo_Click);
            // 
            // cmdDeletebyLo
            // 
            this.cmdDeletebyLo.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeletebyLo.Image")));
            this.cmdDeletebyLo.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdDeletebyLo.Location = new System.Drawing.Point(920, 69);
            this.cmdDeletebyLo.Name = "cmdDeletebyLo";
            this.cmdDeletebyLo.Size = new System.Drawing.Size(109, 32);
            this.cmdDeletebyLo.TabIndex = 9;
            this.cmdDeletebyLo.Text = "Xóa lô";
            this.cmdDeletebyLo.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdDeletebyLo.Click += new System.EventHandler(this.cmdDeletebyLo_Click);
            // 
            // cmdChooseFile
            // 
            this.cmdChooseFile.Image = ((System.Drawing.Image)(resources.GetObject("cmdChooseFile.Image")));
            this.cmdChooseFile.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdChooseFile.Location = new System.Drawing.Point(920, 32);
            this.cmdChooseFile.Name = "cmdChooseFile";
            this.cmdChooseFile.Size = new System.Drawing.Size(109, 32);
            this.cmdChooseFile.TabIndex = 8;
            this.cmdChooseFile.Text = "Chọn file";
            this.cmdChooseFile.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdChooseFile.Click += new System.EventHandler(this.cmdChooseFile_Click);
            // 
            // dtpNgayNhap
            // 
            this.dtpNgayNhap.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayNhap.Location = new System.Drawing.Point(482, 53);
            this.dtpNgayNhap.Name = "dtpNgayNhap";
            this.dtpNgayNhap.Size = new System.Drawing.Size(138, 23);
            this.dtpNgayNhap.TabIndex = 7;
            this.dtpNgayNhap.Value = new System.DateTime(2016, 1, 25, 23, 14, 56, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(374, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ngày đăng ký: ";
            // 
            // txtSolo
            // 
            this.txtSolo.Location = new System.Drawing.Point(163, 54);
            this.txtSolo.Name = "txtSolo";
            this.txtSolo.Size = new System.Drawing.Size(205, 23);
            this.txtSolo.TabIndex = 4;
            this.txtSolo.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tên doanh nghiệp: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Thông tin số lô: ";
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.Location = new System.Drawing.Point(163, 28);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.ReadOnly = true;
            this.txtSourceFile.Size = new System.Drawing.Size(457, 23);
            this.txtSourceFile.TabIndex = 1;
            this.txtSourceFile.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn đường dẫn: ";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabKSK);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.uiGroupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(1036, 507);
            this.splitContainer2.SplitterDistance = 399;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabKSK
            // 
            this.tabKSK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabKSK.Location = new System.Drawing.Point(0, 0);
            this.tabKSK.Margin = new System.Windows.Forms.Padding(4);
            this.tabKSK.Name = "tabKSK";
            this.tabKSK.Size = new System.Drawing.Size(1036, 399);
            this.tabKSK.TabIndex = 0;
            this.tabKSK.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.tabDanhSach,
            this.tabLog,
            this.tabChiDinh,
            this.tabChiDinhOld});
            // 
            // tabDanhSach
            // 
            this.tabDanhSach.Controls.Add(this.grdList);
            this.tabDanhSach.Location = new System.Drawing.Point(1, 25);
            this.tabDanhSach.Margin = new System.Windows.Forms.Padding(4);
            this.tabDanhSach.Name = "tabDanhSach";
            this.tabDanhSach.Size = new System.Drawing.Size(1032, 371);
            this.tabDanhSach.TabStop = true;
            this.tabDanhSach.Text = "Danh sách bệnh nhân (F1)";
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1032, 371);
            this.grdList.TabIndex = 0;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.panel3);
            this.tabLog.Location = new System.Drawing.Point(1, 25);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(1032, 371);
            this.tabLog.TabStop = true;
            this.tabLog.Text = "Thông tin chỉ định(F2)";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitContainer5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1032, 371);
            this.panel3.TabIndex = 0;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.splitContainer6);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer8);
            this.splitContainer5.Size = new System.Drawing.Size(1032, 371);
            this.splitContainer5.SplitterDistance = 242;
            this.splitContainer5.TabIndex = 0;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.splitContainer7);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.grdChiDinh);
            this.splitContainer6.Size = new System.Drawing.Size(1032, 242);
            this.splitContainer6.SplitterDistance = 477;
            this.splitContainer6.TabIndex = 0;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.grdServiceDetail);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.cmdInPhieu);
            this.splitContainer7.Panel2.Controls.Add(this.cmdDeleteDichVu);
            this.splitContainer7.Panel2.Controls.Add(this.cmdAddChiDinh);
            this.splitContainer7.Size = new System.Drawing.Size(477, 242);
            this.splitContainer7.SplitterDistance = 423;
            this.splitContainer7.TabIndex = 0;
            // 
            // grdServiceDetail
            // 
            this.grdServiceDetail.BackColor = System.Drawing.SystemColors.Control;
            this.grdServiceDetail.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdServiceDetail_DesignTimeLayout.LayoutString = resources.GetString("grdServiceDetail_DesignTimeLayout.LayoutString");
            this.grdServiceDetail.DesignTimeLayout = grdServiceDetail_DesignTimeLayout;
            this.grdServiceDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdServiceDetail.DynamicFiltering = true;
            this.grdServiceDetail.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdServiceDetail.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdServiceDetail.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdServiceDetail.FocusCellFormatStyle.BackColor = System.Drawing.Color.LightCyan;
            this.grdServiceDetail.FocusCellFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdServiceDetail.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdServiceDetail.Font = new System.Drawing.Font("Arial", 9F);
            this.grdServiceDetail.GroupByBoxVisible = false;
            this.grdServiceDetail.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdServiceDetail.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdServiceDetail.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdServiceDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdServiceDetail.Location = new System.Drawing.Point(0, 0);
            this.grdServiceDetail.Name = "grdServiceDetail";
            this.grdServiceDetail.RecordNavigator = true;
            this.grdServiceDetail.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdServiceDetail.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdServiceDetail.Size = new System.Drawing.Size(423, 242);
            this.grdServiceDetail.TabIndex = 8;
            this.grdServiceDetail.TableSpacing = 0;
            this.grdServiceDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdServiceDetail.UseGroupRowSelector = true;
            this.grdServiceDetail.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdServiceDetail_CellValueChanged);
            this.grdServiceDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdServiceDetail_KeyDown);
            // 
            // cmdInPhieu
            // 
            this.cmdInPhieu.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieu.Image")));
            this.cmdInPhieu.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdInPhieu.Location = new System.Drawing.Point(2, 158);
            this.cmdInPhieu.Name = "cmdInPhieu";
            this.cmdInPhieu.Size = new System.Drawing.Size(47, 41);
            this.cmdInPhieu.TabIndex = 2;
            // 
            // cmdDeleteDichVu
            // 
            this.cmdDeleteDichVu.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeleteDichVu.Image")));
            this.cmdDeleteDichVu.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdDeleteDichVu.Location = new System.Drawing.Point(2, 105);
            this.cmdDeleteDichVu.Name = "cmdDeleteDichVu";
            this.cmdDeleteDichVu.Size = new System.Drawing.Size(47, 41);
            this.cmdDeleteDichVu.TabIndex = 1;
            // 
            // cmdAddChiDinh
            // 
            this.cmdAddChiDinh.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddChiDinh.Image")));
            this.cmdAddChiDinh.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdAddChiDinh.Location = new System.Drawing.Point(2, 52);
            this.cmdAddChiDinh.Name = "cmdAddChiDinh";
            this.cmdAddChiDinh.Size = new System.Drawing.Size(47, 41);
            this.cmdAddChiDinh.TabIndex = 0;
            this.cmdAddChiDinh.Click += new System.EventHandler(this.cmdAddChiDinh_Click);
            // 
            // grdChiDinh
            // 
            this.grdChiDinh.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdChiDinh.ColumnAutoResize = true;
            this.grdChiDinh.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdChiDinh_DesignTimeLayout.LayoutString = resources.GetString("grdChiDinh_DesignTimeLayout.LayoutString");
            this.grdChiDinh.DesignTimeLayout = grdChiDinh_DesignTimeLayout;
            this.grdChiDinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChiDinh.DynamicFiltering = true;
            this.grdChiDinh.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdChiDinh.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdChiDinh.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdChiDinh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdChiDinh.GroupByBoxVisible = false;
            this.grdChiDinh.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdChiDinh.Location = new System.Drawing.Point(0, 0);
            this.grdChiDinh.Name = "grdChiDinh";
            this.grdChiDinh.RecordNavigator = true;
            this.grdChiDinh.Size = new System.Drawing.Size(551, 242);
            this.grdChiDinh.TabIndex = 3;
            this.grdChiDinh.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdChiDinh.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdChiDinh.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            // 
            // splitContainer8
            // 
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.cboKieuKham);
            this.splitContainer8.Panel1.Controls.Add(this.txtIDKieuKham);
            this.splitContainer8.Panel1.Controls.Add(this.txtIDPkham);
            this.splitContainer8.Panel1.Controls.Add(this.chkChiDinhNhanh);
            this.splitContainer8.Panel1.Controls.Add(this.cmdAddDvuKCB);
            this.splitContainer8.Panel1.Controls.Add(this.txtExamtypeCode);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.grdPhongkham);
            this.splitContainer8.Size = new System.Drawing.Size(1032, 125);
            this.splitContainer8.SplitterDistance = 474;
            this.splitContainer8.TabIndex = 0;
            // 
            // cboKieuKham
            // 
            this.cboKieuKham.AllowDrop = true;
            this.cboKieuKham.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKieuKham.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            cboKieuKham_DesignTimeLayout.LayoutString = resources.GetString("cboKieuKham_DesignTimeLayout.LayoutString");
            this.cboKieuKham.DesignTimeLayout = cboKieuKham_DesignTimeLayout;
            this.cboKieuKham.DisplayMember = "_name";
            this.cboKieuKham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKieuKham.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
            this.cboKieuKham.Location = new System.Drawing.Point(11, 13);
            this.cboKieuKham.Name = "cboKieuKham";
            this.cboKieuKham.SelectedIndex = -1;
            this.cboKieuKham.SelectedItem = null;
            this.cboKieuKham.Size = new System.Drawing.Size(409, 21);
            this.cboKieuKham.TabIndex = 26;
            this.cboKieuKham.Text = "CHỌN DỊCH VỤ KCB";
            this.cboKieuKham.ValueMember = "ID";
            this.cboKieuKham.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtIDKieuKham
            // 
            this.txtIDKieuKham.BackColor = System.Drawing.Color.White;
            this.txtIDKieuKham.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDKieuKham.Location = new System.Drawing.Point(22, 73);
            this.txtIDKieuKham.Name = "txtIDKieuKham";
            this.txtIDKieuKham.Size = new System.Drawing.Size(12, 23);
            this.txtIDKieuKham.TabIndex = 541;
            this.txtIDKieuKham.TabStop = false;
            this.txtIDKieuKham.Visible = false;
            // 
            // txtIDPkham
            // 
            this.txtIDPkham.BackColor = System.Drawing.Color.White;
            this.txtIDPkham.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDPkham.Location = new System.Drawing.Point(40, 73);
            this.txtIDPkham.Name = "txtIDPkham";
            this.txtIDPkham.Size = new System.Drawing.Size(10, 23);
            this.txtIDPkham.TabIndex = 542;
            this.txtIDPkham.TabStop = false;
            this.txtIDPkham.Visible = false;
            // 
            // chkChiDinhNhanh
            // 
            this.chkChiDinhNhanh.Checked = true;
            this.chkChiDinhNhanh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChiDinhNhanh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkChiDinhNhanh.Location = new System.Drawing.Point(11, 40);
            this.chkChiDinhNhanh.Name = "chkChiDinhNhanh";
            this.chkChiDinhNhanh.Size = new System.Drawing.Size(457, 27);
            this.chkChiDinhNhanh.TabIndex = 28;
            this.chkChiDinhNhanh.TabStop = false;
            this.chkChiDinhNhanh.Text = "Thêm ngay dịch vụ sau khi chọn?";
            // 
            // cmdAddDvuKCB
            // 
            this.cmdAddDvuKCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddDvuKCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAddDvuKCB.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddDvuKCB.Image")));
            this.cmdAddDvuKCB.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAddDvuKCB.Location = new System.Drawing.Point(435, 10);
            this.cmdAddDvuKCB.Name = "cmdAddDvuKCB";
            this.cmdAddDvuKCB.Size = new System.Drawing.Size(29, 27);
            this.cmdAddDvuKCB.TabIndex = 27;
            // 
            // txtExamtypeCode
            // 
            this.txtExamtypeCode._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtExamtypeCode._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExamtypeCode._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtExamtypeCode.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtExamtypeCode.AutoCompleteList")));
            this.txtExamtypeCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExamtypeCode.CaseSensitive = false;
            this.txtExamtypeCode.CompareNoID = true;
            this.txtExamtypeCode.DefaultCode = "-1";
            this.txtExamtypeCode.DefaultID = "-1";
            this.txtExamtypeCode.Drug_ID = null;
            this.txtExamtypeCode.ExtraWidth = 400;
            this.txtExamtypeCode.FillValueAfterSelect = false;
            this.txtExamtypeCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExamtypeCode.Location = new System.Drawing.Point(11, 13);
            this.txtExamtypeCode.MaxHeight = 289;
            this.txtExamtypeCode.MinTypedCharacters = 2;
            this.txtExamtypeCode.MyCode = "-1";
            this.txtExamtypeCode.MyID = "-1";
            this.txtExamtypeCode.MyText = "";
            this.txtExamtypeCode.Name = "txtExamtypeCode";
            this.txtExamtypeCode.RaiseEvent = true;
            this.txtExamtypeCode.RaiseEventEnter = false;
            this.txtExamtypeCode.RaiseEventEnterWhenEmpty = false;
            this.txtExamtypeCode.SelectedIndex = -1;
            this.txtExamtypeCode.Size = new System.Drawing.Size(53, 21);
            this.txtExamtypeCode.splitChar = '@';
            this.txtExamtypeCode.splitCharIDAndCode = '#';
            this.txtExamtypeCode.TabIndex = 25;
            this.txtExamtypeCode.TakeCode = true;
            this.txtExamtypeCode.txtMyCode = null;
            this.txtExamtypeCode.txtMyCode_Edit = null;
            this.txtExamtypeCode.txtMyID = null;
            this.txtExamtypeCode.txtMyID_Edit = null;
            this.txtExamtypeCode.txtMyName = null;
            this.txtExamtypeCode.txtMyName_Edit = null;
            this.txtExamtypeCode.txtNext = null;
            // 
            // grdPhongkham
            // 
            this.grdPhongkham.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdPhongkham.AlternatingColors = true;
            grdPhongkham_DesignTimeLayout.LayoutString = resources.GetString("grdPhongkham_DesignTimeLayout.LayoutString");
            this.grdPhongkham.DesignTimeLayout = grdPhongkham_DesignTimeLayout;
            this.grdPhongkham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPhongkham.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPhongkham.GroupByBoxVisible = false;
            this.grdPhongkham.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPhongkham.Location = new System.Drawing.Point(0, 0);
            this.grdPhongkham.Name = "grdPhongkham";
            this.grdPhongkham.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPhongkham.SelectedInactiveFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdPhongkham.Size = new System.Drawing.Size(554, 125);
            this.grdPhongkham.TabIndex = 596;
            this.grdPhongkham.TabStop = false;
            this.grdPhongkham.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPhongkham.TotalRowFormatStyle.BackColor = System.Drawing.SystemColors.Control;
            this.grdPhongkham.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdPhongkham.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // tabChiDinh
            // 
            this.tabChiDinh.Controls.Add(this.panel2);
            this.tabChiDinh.Location = new System.Drawing.Point(1, 25);
            this.tabChiDinh.Margin = new System.Windows.Forms.Padding(4);
            this.tabChiDinh.Name = "tabChiDinh";
            this.tabChiDinh.Size = new System.Drawing.Size(1032, 375);
            this.tabChiDinh.TabStop = true;
            this.tabChiDinh.TabVisible = false;
            this.tabChiDinh.Text = "Chỉ định";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1032, 375);
            this.panel2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.uiGroupBox4);
            this.splitContainer3.Size = new System.Drawing.Size(1032, 375);
            this.splitContainer3.SplitterDistance = 462;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.txtNhomCD);
            this.splitContainer4.Panel1.Controls.Add(this.cmdTaonhom);
            this.splitContainer4.Panel1.Controls.Add(this.cmdAccept);
            this.splitContainer4.Panel1.Controls.Add(this.label6);
            this.splitContainer4.Panel1.Controls.Add(this.label5);
            this.splitContainer4.Panel1.Controls.Add(this.txtFilterName);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.uiGroupBox3);
            this.splitContainer4.Size = new System.Drawing.Size(462, 375);
            this.splitContainer4.SplitterDistance = 54;
            this.splitContainer4.TabIndex = 0;
            // 
            // txtNhomCD
            // 
            this.txtNhomCD._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtNhomCD._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhomCD._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNhomCD.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNhomCD.AutoCompleteList")));
            this.txtNhomCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNhomCD.CaseSensitive = false;
            this.txtNhomCD.CompareNoID = true;
            this.txtNhomCD.DefaultCode = "-1";
            this.txtNhomCD.DefaultID = "-1";
            this.txtNhomCD.Drug_ID = null;
            this.txtNhomCD.ExtraWidth = 0;
            this.txtNhomCD.FillValueAfterSelect = false;
            this.txtNhomCD.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhomCD.Location = new System.Drawing.Point(45, 3);
            this.txtNhomCD.MaxHeight = 289;
            this.txtNhomCD.MinTypedCharacters = 2;
            this.txtNhomCD.Multiline = true;
            this.txtNhomCD.MyCode = "-1";
            this.txtNhomCD.MyID = "-1";
            this.txtNhomCD.MyText = "";
            this.txtNhomCD.Name = "txtNhomCD";
            this.txtNhomCD.RaiseEvent = false;
            this.txtNhomCD.RaiseEventEnter = true;
            this.txtNhomCD.RaiseEventEnterWhenEmpty = false;
            this.txtNhomCD.SelectedIndex = -1;
            this.txtNhomCD.Size = new System.Drawing.Size(274, 26);
            this.txtNhomCD.splitChar = '@';
            this.txtNhomCD.splitCharIDAndCode = '#';
            this.txtNhomCD.TabIndex = 516;
            this.txtNhomCD.TakeCode = true;
            this.txtNhomCD.txtMyCode = null;
            this.txtNhomCD.txtMyCode_Edit = null;
            this.txtNhomCD.txtMyID = null;
            this.txtNhomCD.txtMyID_Edit = null;
            this.txtNhomCD.txtMyName = null;
            this.txtNhomCD.txtMyName_Edit = null;
            this.txtNhomCD.txtNext = this.cmdAccept;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.Location = new System.Drawing.Point(328, 3);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(31, 27);
            this.cmdAccept.TabIndex = 514;
            // 
            // cmdTaonhom
            // 
            this.cmdTaonhom.Image = ((System.Drawing.Image)(resources.GetObject("cmdTaonhom.Image")));
            this.cmdTaonhom.Location = new System.Drawing.Point(328, 29);
            this.cmdTaonhom.Name = "cmdTaonhom";
            this.cmdTaonhom.Size = new System.Drawing.Size(31, 27);
            this.cmdTaonhom.TabIndex = 515;
            this.cmdTaonhom.Click += new System.EventHandler(this.cmdTaonhom_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "F2: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "F3: ";
            // 
            // txtFilterName
            // 
            this.txtFilterName.Location = new System.Drawing.Point(45, 30);
            this.txtFilterName.Name = "txtFilterName";
            this.txtFilterName.Size = new System.Drawing.Size(274, 23);
            this.txtFilterName.TabIndex = 13;
            this.txtFilterName.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.txtFilterName.TextChanged += new System.EventHandler(this.txtFilterName_TextChanged);
            this.txtFilterName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilterName_KeyDown);
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.grdDanhSachCLS);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(462, 317);
            this.uiGroupBox3.TabIndex = 1;
            this.uiGroupBox3.Text = "Danh mục chỉ định";
            // 
            // grdDanhSachCLS
            // 
            this.grdDanhSachCLS.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdDanhSachCLS_DesignTimeLayout.LayoutString = resources.GetString("grdDanhSachCLS_DesignTimeLayout.LayoutString");
            this.grdDanhSachCLS.DesignTimeLayout = grdDanhSachCLS_DesignTimeLayout;
            this.grdDanhSachCLS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDanhSachCLS.DynamicFiltering = true;
            this.grdDanhSachCLS.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdDanhSachCLS.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdDanhSachCLS.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdDanhSachCLS.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdDanhSachCLS.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDanhSachCLS.GroupByBoxVisible = false;
            this.grdDanhSachCLS.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdDanhSachCLS.Location = new System.Drawing.Point(3, 19);
            this.grdDanhSachCLS.Name = "grdDanhSachCLS";
            this.grdDanhSachCLS.RecordNavigator = true;
            this.grdDanhSachCLS.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDanhSachCLS.Size = new System.Drawing.Size(456, 295);
            this.grdDanhSachCLS.TabIndex = 1;
            this.grdDanhSachCLS.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdDanhSachCLS_CellValueChanged);
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.grdThongtinChidinh);
            this.uiGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox4.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Size = new System.Drawing.Size(566, 375);
            this.uiGroupBox4.TabIndex = 1;
            this.uiGroupBox4.Text = "Thông tin chỉ định ";
            // 
            // grdThongtinChidinh
            // 
            this.grdThongtinChidinh.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdThongtinChidinh.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdThongtinChidinh_DesignTimeLayout.LayoutString = resources.GetString("grdThongtinChidinh_DesignTimeLayout.LayoutString");
            this.grdThongtinChidinh.DesignTimeLayout = grdThongtinChidinh_DesignTimeLayout;
            this.grdThongtinChidinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdThongtinChidinh.DynamicFiltering = true;
            this.grdThongtinChidinh.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdThongtinChidinh.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdThongtinChidinh.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdThongtinChidinh.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdThongtinChidinh.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdThongtinChidinh.GroupByBoxVisible = false;
            this.grdThongtinChidinh.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdThongtinChidinh.Location = new System.Drawing.Point(3, 19);
            this.grdThongtinChidinh.Name = "grdThongtinChidinh";
            this.grdThongtinChidinh.RecordNavigator = true;
            this.grdThongtinChidinh.Size = new System.Drawing.Size(560, 353);
            this.grdThongtinChidinh.TabIndex = 1;
            this.grdThongtinChidinh.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThongtinChidinh.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdThongtinChidinh.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            // 
            // tabChiDinhOld
            // 
            this.tabChiDinhOld.Controls.Add(this.rtxtLogs);
            this.tabChiDinhOld.Location = new System.Drawing.Point(1, 25);
            this.tabChiDinhOld.Margin = new System.Windows.Forms.Padding(4);
            this.tabChiDinhOld.Name = "tabChiDinhOld";
            this.tabChiDinhOld.Size = new System.Drawing.Size(1032, 373);
            this.tabChiDinhOld.TabStop = true;
            this.tabChiDinhOld.Text = "Tab log (F3)";
            // 
            // rtxtLogs
            // 
            this.rtxtLogs.ContextMenuStrip = this.contextMenuStrip2;
            this.rtxtLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLogs.Location = new System.Drawing.Point(0, 0);
            this.rtxtLogs.Name = "rtxtLogs";
            this.rtxtLogs.Size = new System.Drawing.Size(1032, 373);
            this.rtxtLogs.TabIndex = 0;
            this.rtxtLogs.Text = "";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.mnuDelLog});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(115, 32);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(111, 6);
            // 
            // mnuDelLog
            // 
            this.mnuDelLog.Name = "mnuDelLog";
            this.mnuDelLog.Size = new System.Drawing.Size(114, 22);
            this.mnuDelLog.Text = "Xóa log";
            this.mnuDelLog.Click += new System.EventHandler(this.mnuDelLog_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.prgBar);
            this.uiGroupBox2.Controls.Add(this.cmdConfig);
            this.uiGroupBox2.Controls.Add(this.cmdThoat);
            this.uiGroupBox2.Controls.Add(this.cmdSentToLis);
            this.uiGroupBox2.Controls.Add(this.cmdImportToHis);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1036, 103);
            this.uiGroupBox2.TabIndex = 0;
            this.uiGroupBox2.Text = "Chức năng";
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(3, 22);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(1030, 27);
            this.prgBar.TabIndex = 32;
            this.prgBar.Visible = false;
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdConfig.Location = new System.Drawing.Point(993, 68);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(43, 34);
            this.cmdConfig.TabIndex = 31;
            this.cmdConfig.TabStop = false;
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            // 
            // cmdThoat
            // 
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdThoat.Location = new System.Drawing.Point(608, 55);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(109, 32);
            this.cmdThoat.TabIndex = 11;
            this.cmdThoat.Text = "Exit";
            this.cmdThoat.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // cmdSentToLis
            // 
            this.cmdSentToLis.Image = ((System.Drawing.Image)(resources.GetObject("cmdSentToLis.Image")));
            this.cmdSentToLis.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSentToLis.Location = new System.Drawing.Point(467, 55);
            this.cmdSentToLis.Name = "cmdSentToLis";
            this.cmdSentToLis.Size = new System.Drawing.Size(109, 32);
            this.cmdSentToLis.TabIndex = 10;
            this.cmdSentToLis.Text = "Sent to Lis";
            this.cmdSentToLis.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdSentToLis.Click += new System.EventHandler(this.cmdSentToLis_Click);
            // 
            // cmdImportToHis
            // 
            this.cmdImportToHis.Image = ((System.Drawing.Image)(resources.GetObject("cmdImportToHis.Image")));
            this.cmdImportToHis.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdImportToHis.Location = new System.Drawing.Point(329, 55);
            this.cmdImportToHis.Name = "cmdImportToHis";
            this.cmdImportToHis.Size = new System.Drawing.Size(109, 32);
            this.cmdImportToHis.TabIndex = 9;
            this.cmdImportToHis.Text = "Import";
            this.cmdImportToHis.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdImportToHis.Click += new System.EventHandler(this.cmdImportToHis_Click);
            // 
            // frm_ImportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 641);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_ImportExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chức năng import bệnh nhân khám sức khỏe";
            this.Load += new System.EventHandler(this.frm_ImportExcel_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabKSK)).EndInit();
            this.tabKSK.ResumeLayout(false);
            this.tabDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.tabLog.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdServiceDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdChiDinh)).EndInit();
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel1.PerformLayout();
            this.splitContainer8.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboKieuKham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPhongkham)).EndInit();
            this.tabChiDinh.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDanhSachCLS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).EndInit();
            this.uiGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdThongtinChidinh)).EndInit();
            this.tabChiDinhOld.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Janus.Windows.UI.Tab.UITab tabKSK;
        private Janus.Windows.UI.Tab.UITabPage tabDanhSach;
        private Janus.Windows.UI.Tab.UITabPage tabChiDinh;
        private Janus.Windows.UI.Tab.UITabPage tabChiDinhOld;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtSourceFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private Janus.Windows.EditControls.UIButton cmdDeletebyLo;
        private Janus.Windows.EditControls.UIButton cmdChooseFile;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtSolo;
        private Janus.Windows.EditControls.UIButton cmdThoat;
        private Janus.Windows.EditControls.UIButton cmdSentToLis;
        private Janus.Windows.EditControls.UIButton cmdImportToHis;
        private Janus.Windows.EditControls.UIButton cmdConfig;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private Janus.Windows.GridEX.EditControls.EditBox txtFilterName;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.GridEX.GridEX grdDanhSachCLS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.EditControls.UIButton cmdTaonhom;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox4;
        private Janus.Windows.GridEX.GridEX grdThongtinChidinh;
        private VNS.HIS.UCs.AutoCompleteTextbox txtNhomCD;
        private System.Windows.Forms.ProgressBar prgBar;
        private Janus.Windows.UI.Tab.UITabPage tabLog;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private Janus.Windows.EditControls.UIButton cmdSeachByLo;
        private System.Windows.Forms.RichTextBox rtxtLogs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuDelLog;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private Janus.Windows.GridEX.GridEX grdChiDinh;
        private Janus.Windows.GridEX.GridEX grdServiceDetail;
        private Janus.Windows.EditControls.UIButton cmdInPhieu;
        private Janus.Windows.EditControls.UIButton cmdDeleteDichVu;
        private Janus.Windows.EditControls.UIButton cmdAddChiDinh;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private VNS.HIS.UCs.AutoCompleteTextbox txtExamtypeCode;
        private Janus.Windows.GridEX.EditControls.MultiColumnCombo cboKieuKham;
        private Janus.Windows.EditControls.UIButton cmdAddDvuKCB;
        private Janus.Windows.EditControls.UICheckBox chkChiDinhNhanh;
        private Janus.Windows.GridEX.EditControls.EditBox txtIDKieuKham;
        private Janus.Windows.GridEX.EditControls.EditBox txtIDPkham;
        private Janus.Windows.GridEX.GridEX grdPhongkham;
        private Janus.Windows.EditControls.UIButton cmdThemMoiKH;
        private VNS.HIS.UCs.AutoCompleteTextbox txtDoanhNghiep;

    }
}