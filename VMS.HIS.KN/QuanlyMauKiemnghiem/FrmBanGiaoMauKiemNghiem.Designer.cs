using VNS.HIS.UCs;

namespace VMS.HIS.KN.QuanlyMauKiemnghiem
{
    partial class FrmBanGiaoMauKiemNghiem
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
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel3 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBanGiaoMauKiemNghiem));
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.dtpNgaytraKQ = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label18 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPatientName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdThoat = new Janus.Windows.EditControls.UIButton();
            this.cmdBanGiao = new Janus.Windows.EditControls.UIButton();
            this.txtDichvuKn = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtthauphu_nguoinhan = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtthauphu_nguoigiao = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtvisinh_nguoinhan = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtvisinh_nguoigiao = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txthoaly_nguoinhan = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txthoaly_nguoigiao = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtthauphu_luongmau = new MaskedTextBox.MaskedTextBox();
            this.txtvisinh_luongmau = new MaskedTextBox.MaskedTextBox();
            this.txthoaly_luongmau = new MaskedTextBox.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAssignCode = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtThetichKhoiluong = new MaskedTextBox.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTinhtrangmau = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.calendarCombo1 = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label19 = new System.Windows.Forms.Label();
            this.lblMessge = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 302);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.Key = "";
            uiStatusBarPanel1.ProgressBarValue = 0;
            uiStatusBarPanel1.Text = "(F3: Bàn giao)";
            uiStatusBarPanel1.Width = 107;
            uiStatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel2.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel2.Key = "";
            uiStatusBarPanel2.ProgressBarValue = 0;
            uiStatusBarPanel2.Text = "(F4: In mẫu bàn giao)";
            uiStatusBarPanel2.Width = 152;
            uiStatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel3.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel3.Key = "";
            uiStatusBarPanel3.ProgressBarValue = 0;
            uiStatusBarPanel3.Text = "(Esc: Thoát)";
            uiStatusBarPanel3.Width = 94;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1,
            uiStatusBarPanel2,
            uiStatusBarPanel3});
            this.uiStatusBar1.Size = new System.Drawing.Size(843, 27);
            this.uiStatusBar1.TabIndex = 0;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
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
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(843, 302);
            this.splitContainer1.SplitterDistance = 77;
            this.splitContainer1.TabIndex = 1;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.dtpNgaytraKQ);
            this.uiGroupBox1.Controls.Add(this.label18);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.txtDichvuKn);
            this.uiGroupBox1.Controls.Add(this.txtPatientName);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(843, 77);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin khách hàng";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // dtpNgaytraKQ
            // 
            this.dtpNgaytraKQ.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaytraKQ.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaytraKQ.DropDownCalendar.Name = "";
            this.dtpNgaytraKQ.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtpNgaytraKQ.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaytraKQ.Location = new System.Drawing.Point(695, 51);
            this.dtpNgaytraKQ.Name = "dtpNgaytraKQ";
            this.dtpNgaytraKQ.ShowUpDown = true;
            this.dtpNgaytraKQ.Size = new System.Drawing.Size(128, 21);
            this.dtpNgaytraKQ.TabIndex = 631;
            this.dtpNgaytraKQ.Value = new System.DateTime(2014, 5, 11, 0, 0, 0, 0);
            this.dtpNgaytraKQ.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Location = new System.Drawing.Point(608, 52);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 17);
            this.label18.TabIndex = 632;
            this.label18.Text = "Ngày trả KQ:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(17, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 603;
            this.label5.Text = "Dịch vụ KN:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPatientName
            // 
            this.txtPatientName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatientName.BackColor = System.Drawing.Color.White;
            this.txtPatientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.Location = new System.Drawing.Point(119, 22);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(704, 23);
            this.txtPatientName.TabIndex = 512;
            this.txtPatientName.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(12, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 513;
            this.label3.Text = "Tên Khách hàng :";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.uiGroupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.uiGroupBox3);
            this.splitContainer2.Size = new System.Drawing.Size(843, 221);
            this.splitContainer2.SplitterDistance = 152;
            this.splitContainer2.TabIndex = 0;
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.lblMessge);
            this.uiGroupBox3.Controls.Add(this.label19);
            this.uiGroupBox3.Controls.Add(this.calendarCombo1);
            this.uiGroupBox3.Controls.Add(this.cmdPrint);
            this.uiGroupBox3.Controls.Add(this.cmdThoat);
            this.uiGroupBox3.Controls.Add(this.cmdBanGiao);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(843, 65);
            this.uiGroupBox3.TabIndex = 1;
            this.uiGroupBox3.Text = "Chức năng";
            this.uiGroupBox3.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdPrint.Location = new System.Drawing.Point(357, 20);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(106, 33);
            this.cmdPrint.TabIndex = 1;
            this.cmdPrint.Text = "In mẫu";
            this.cmdPrint.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // cmdThoat
            // 
            this.cmdThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdThoat.Location = new System.Drawing.Point(469, 20);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(106, 33);
            this.cmdThoat.TabIndex = 2;
            this.cmdThoat.Text = "Thoát";
            this.cmdThoat.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // cmdBanGiao
            // 
            this.cmdBanGiao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBanGiao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBanGiao.Image = ((System.Drawing.Image)(resources.GetObject("cmdBanGiao.Image")));
            this.cmdBanGiao.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdBanGiao.Location = new System.Drawing.Point(245, 20);
            this.cmdBanGiao.Name = "cmdBanGiao";
            this.cmdBanGiao.Size = new System.Drawing.Size(106, 33);
            this.cmdBanGiao.TabIndex = 0;
            this.cmdBanGiao.Text = "Bàn giao";
            this.cmdBanGiao.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdBanGiao.Click += new System.EventHandler(this.cmdBanGiao_Click);
            // 
            // txtDichvuKn
            // 
            this.txtDichvuKn._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtDichvuKn._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDichvuKn._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDichvuKn.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtDichvuKn.AutoCompleteList")));
            this.txtDichvuKn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDichvuKn.CaseSensitive = false;
            this.txtDichvuKn.CompareNoID = true;
            this.txtDichvuKn.DefaultCode = "-1";
            this.txtDichvuKn.DefaultID = "-1";
            this.txtDichvuKn.Drug_ID = null;
            this.txtDichvuKn.ExtraWidth = 0;
            this.txtDichvuKn.FillValueAfterSelect = false;
            this.txtDichvuKn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDichvuKn.Location = new System.Drawing.Point(119, 51);
            this.txtDichvuKn.MaxHeight = 289;
            this.txtDichvuKn.MinTypedCharacters = 2;
            this.txtDichvuKn.MyCode = "-1";
            this.txtDichvuKn.MyID = "-1";
            this.txtDichvuKn.MyText = "";
            this.txtDichvuKn.Name = "txtDichvuKn";
            this.txtDichvuKn.RaiseEvent = true;
            this.txtDichvuKn.RaiseEventEnter = true;
            this.txtDichvuKn.RaiseEventEnterWhenEmpty = true;
            this.txtDichvuKn.ReadOnly = true;
            this.txtDichvuKn.SelectedIndex = -1;
            this.txtDichvuKn.Size = new System.Drawing.Size(483, 21);
            this.txtDichvuKn.splitChar = '@';
            this.txtDichvuKn.splitCharIDAndCode = '#';
            this.txtDichvuKn.TabIndex = 602;
            this.txtDichvuKn.TakeCode = false;
            this.txtDichvuKn.txtMyCode = null;
            this.txtDichvuKn.txtMyCode_Edit = null;
            this.txtDichvuKn.txtMyID = null;
            this.txtDichvuKn.txtMyID_Edit = null;
            this.txtDichvuKn.txtMyName = null;
            this.txtDichvuKn.txtMyName_Edit = null;
            this.txtDichvuKn.txtNext = null;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.txtthauphu_nguoinhan);
            this.uiGroupBox2.Controls.Add(this.txtthauphu_nguoigiao);
            this.uiGroupBox2.Controls.Add(this.txtvisinh_nguoinhan);
            this.uiGroupBox2.Controls.Add(this.txtvisinh_nguoigiao);
            this.uiGroupBox2.Controls.Add(this.txthoaly_nguoinhan);
            this.uiGroupBox2.Controls.Add(this.txthoaly_nguoigiao);
            this.uiGroupBox2.Controls.Add(this.label17);
            this.uiGroupBox2.Controls.Add(this.label16);
            this.uiGroupBox2.Controls.Add(this.label15);
            this.uiGroupBox2.Controls.Add(this.txtthauphu_luongmau);
            this.uiGroupBox2.Controls.Add(this.txtvisinh_luongmau);
            this.uiGroupBox2.Controls.Add(this.txthoaly_luongmau);
            this.uiGroupBox2.Controls.Add(this.label14);
            this.uiGroupBox2.Controls.Add(this.label13);
            this.uiGroupBox2.Controls.Add(this.label12);
            this.uiGroupBox2.Controls.Add(this.label11);
            this.uiGroupBox2.Controls.Add(this.label9);
            this.uiGroupBox2.Controls.Add(this.label8);
            this.uiGroupBox2.Controls.Add(this.label6);
            this.uiGroupBox2.Controls.Add(this.label4);
            this.uiGroupBox2.Controls.Add(this.label2);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.txtAssignCode);
            this.uiGroupBox2.Controls.Add(this.txtThetichKhoiluong);
            this.uiGroupBox2.Controls.Add(this.label10);
            this.uiGroupBox2.Controls.Add(this.label7);
            this.uiGroupBox2.Controls.Add(this.txtTinhtrangmau);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(843, 152);
            this.uiGroupBox2.TabIndex = 633;
            this.uiGroupBox2.Text = "Thông tin bàn giao mẫu";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // txtthauphu_nguoinhan
            // 
            this.txtthauphu_nguoinhan._backcolor = System.Drawing.SystemColors.Control;
            this.txtthauphu_nguoinhan._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthauphu_nguoinhan._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtthauphu_nguoinhan.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtthauphu_nguoinhan.AutoCompleteList")));
            this.txtthauphu_nguoinhan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtthauphu_nguoinhan.CaseSensitive = false;
            this.txtthauphu_nguoinhan.CompareNoID = true;
            this.txtthauphu_nguoinhan.DefaultCode = "-1";
            this.txtthauphu_nguoinhan.DefaultID = "-1";
            this.txtthauphu_nguoinhan.Drug_ID = null;
            this.txtthauphu_nguoinhan.ExtraWidth = 0;
            this.txtthauphu_nguoinhan.FillValueAfterSelect = false;
            this.txtthauphu_nguoinhan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthauphu_nguoinhan.LOAI_DANHMUC = "KN_HOALY_NGUOIGIAO";
            this.txtthauphu_nguoinhan.Location = new System.Drawing.Point(632, 126);
            this.txtthauphu_nguoinhan.MaxHeight = 200;
            this.txtthauphu_nguoinhan.MinTypedCharacters = 2;
            this.txtthauphu_nguoinhan.MyCode = "-1";
            this.txtthauphu_nguoinhan.MyID = "-1";
            this.txtthauphu_nguoinhan.Name = "txtthauphu_nguoinhan";
            this.txtthauphu_nguoinhan.RaiseEvent = false;
            this.txtthauphu_nguoinhan.RaiseEventEnter = false;
            this.txtthauphu_nguoinhan.RaiseEventEnterWhenEmpty = false;
            this.txtthauphu_nguoinhan.SelectedIndex = -1;
            this.txtthauphu_nguoinhan.Size = new System.Drawing.Size(191, 21);
            this.txtthauphu_nguoinhan.splitChar = '@';
            this.txtthauphu_nguoinhan.splitCharIDAndCode = '#';
            this.txtthauphu_nguoinhan.TabIndex = 8;
            this.txtthauphu_nguoinhan.TakeCode = false;
            this.txtthauphu_nguoinhan.txtMyCode = null;
            this.txtthauphu_nguoinhan.txtMyCode_Edit = null;
            this.txtthauphu_nguoinhan.txtMyID = null;
            this.txtthauphu_nguoinhan.txtMyID_Edit = null;
            this.txtthauphu_nguoinhan.txtMyName = null;
            this.txtthauphu_nguoinhan.txtMyName_Edit = null;
            this.txtthauphu_nguoinhan.txtNext = null;
            // 
            // txtthauphu_nguoigiao
            // 
            this.txtthauphu_nguoigiao._backcolor = System.Drawing.SystemColors.Control;
            this.txtthauphu_nguoigiao._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthauphu_nguoigiao._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtthauphu_nguoigiao.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtthauphu_nguoigiao.AutoCompleteList")));
            this.txtthauphu_nguoigiao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtthauphu_nguoigiao.CaseSensitive = false;
            this.txtthauphu_nguoigiao.CompareNoID = true;
            this.txtthauphu_nguoigiao.DefaultCode = "-1";
            this.txtthauphu_nguoigiao.DefaultID = "-1";
            this.txtthauphu_nguoigiao.Drug_ID = null;
            this.txtthauphu_nguoigiao.ExtraWidth = 0;
            this.txtthauphu_nguoigiao.FillValueAfterSelect = false;
            this.txtthauphu_nguoigiao.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthauphu_nguoigiao.LOAI_DANHMUC = "KN_HOALY_NGUOIGIAO";
            this.txtthauphu_nguoigiao.Location = new System.Drawing.Point(354, 127);
            this.txtthauphu_nguoigiao.MaxHeight = 200;
            this.txtthauphu_nguoigiao.MinTypedCharacters = 2;
            this.txtthauphu_nguoigiao.MyCode = "-1";
            this.txtthauphu_nguoigiao.MyID = "-1";
            this.txtthauphu_nguoigiao.Name = "txtthauphu_nguoigiao";
            this.txtthauphu_nguoigiao.RaiseEvent = false;
            this.txtthauphu_nguoigiao.RaiseEventEnter = false;
            this.txtthauphu_nguoigiao.RaiseEventEnterWhenEmpty = false;
            this.txtthauphu_nguoigiao.SelectedIndex = -1;
            this.txtthauphu_nguoigiao.Size = new System.Drawing.Size(191, 21);
            this.txtthauphu_nguoigiao.splitChar = '@';
            this.txtthauphu_nguoigiao.splitCharIDAndCode = '#';
            this.txtthauphu_nguoigiao.TabIndex = 7;
            this.txtthauphu_nguoigiao.TakeCode = false;
            this.txtthauphu_nguoigiao.txtMyCode = null;
            this.txtthauphu_nguoigiao.txtMyCode_Edit = null;
            this.txtthauphu_nguoigiao.txtMyID = null;
            this.txtthauphu_nguoigiao.txtMyID_Edit = null;
            this.txtthauphu_nguoigiao.txtMyName = null;
            this.txtthauphu_nguoigiao.txtMyName_Edit = null;
            this.txtthauphu_nguoigiao.txtNext = null;
            // 
            // txtvisinh_nguoinhan
            // 
            this.txtvisinh_nguoinhan._backcolor = System.Drawing.SystemColors.Control;
            this.txtvisinh_nguoinhan._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvisinh_nguoinhan._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtvisinh_nguoinhan.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtvisinh_nguoinhan.AutoCompleteList")));
            this.txtvisinh_nguoinhan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtvisinh_nguoinhan.CaseSensitive = false;
            this.txtvisinh_nguoinhan.CompareNoID = true;
            this.txtvisinh_nguoinhan.DefaultCode = "-1";
            this.txtvisinh_nguoinhan.DefaultID = "-1";
            this.txtvisinh_nguoinhan.Drug_ID = null;
            this.txtvisinh_nguoinhan.ExtraWidth = 0;
            this.txtvisinh_nguoinhan.FillValueAfterSelect = false;
            this.txtvisinh_nguoinhan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvisinh_nguoinhan.LOAI_DANHMUC = "KN_HOALY_NGUOIGIAO";
            this.txtvisinh_nguoinhan.Location = new System.Drawing.Point(632, 94);
            this.txtvisinh_nguoinhan.MaxHeight = 200;
            this.txtvisinh_nguoinhan.MinTypedCharacters = 2;
            this.txtvisinh_nguoinhan.MyCode = "-1";
            this.txtvisinh_nguoinhan.MyID = "-1";
            this.txtvisinh_nguoinhan.Name = "txtvisinh_nguoinhan";
            this.txtvisinh_nguoinhan.RaiseEvent = false;
            this.txtvisinh_nguoinhan.RaiseEventEnter = false;
            this.txtvisinh_nguoinhan.RaiseEventEnterWhenEmpty = false;
            this.txtvisinh_nguoinhan.SelectedIndex = -1;
            this.txtvisinh_nguoinhan.Size = new System.Drawing.Size(191, 21);
            this.txtvisinh_nguoinhan.splitChar = '@';
            this.txtvisinh_nguoinhan.splitCharIDAndCode = '#';
            this.txtvisinh_nguoinhan.TabIndex = 5;
            this.txtvisinh_nguoinhan.TakeCode = false;
            this.txtvisinh_nguoinhan.txtMyCode = null;
            this.txtvisinh_nguoinhan.txtMyCode_Edit = null;
            this.txtvisinh_nguoinhan.txtMyID = null;
            this.txtvisinh_nguoinhan.txtMyID_Edit = null;
            this.txtvisinh_nguoinhan.txtMyName = null;
            this.txtvisinh_nguoinhan.txtMyName_Edit = null;
            this.txtvisinh_nguoinhan.txtNext = null;
            // 
            // txtvisinh_nguoigiao
            // 
            this.txtvisinh_nguoigiao._backcolor = System.Drawing.SystemColors.Control;
            this.txtvisinh_nguoigiao._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvisinh_nguoigiao._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtvisinh_nguoigiao.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtvisinh_nguoigiao.AutoCompleteList")));
            this.txtvisinh_nguoigiao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtvisinh_nguoigiao.CaseSensitive = false;
            this.txtvisinh_nguoigiao.CompareNoID = true;
            this.txtvisinh_nguoigiao.DefaultCode = "-1";
            this.txtvisinh_nguoigiao.DefaultID = "-1";
            this.txtvisinh_nguoigiao.Drug_ID = null;
            this.txtvisinh_nguoigiao.ExtraWidth = 0;
            this.txtvisinh_nguoigiao.FillValueAfterSelect = false;
            this.txtvisinh_nguoigiao.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvisinh_nguoigiao.LOAI_DANHMUC = "KN_HOALY_NGUOIGIAO";
            this.txtvisinh_nguoigiao.Location = new System.Drawing.Point(354, 94);
            this.txtvisinh_nguoigiao.MaxHeight = 200;
            this.txtvisinh_nguoigiao.MinTypedCharacters = 2;
            this.txtvisinh_nguoigiao.MyCode = "-1";
            this.txtvisinh_nguoigiao.MyID = "-1";
            this.txtvisinh_nguoigiao.Name = "txtvisinh_nguoigiao";
            this.txtvisinh_nguoigiao.RaiseEvent = false;
            this.txtvisinh_nguoigiao.RaiseEventEnter = false;
            this.txtvisinh_nguoigiao.RaiseEventEnterWhenEmpty = false;
            this.txtvisinh_nguoigiao.SelectedIndex = -1;
            this.txtvisinh_nguoigiao.Size = new System.Drawing.Size(191, 21);
            this.txtvisinh_nguoigiao.splitChar = '@';
            this.txtvisinh_nguoigiao.splitCharIDAndCode = '#';
            this.txtvisinh_nguoigiao.TabIndex = 4;
            this.txtvisinh_nguoigiao.TakeCode = false;
            this.txtvisinh_nguoigiao.txtMyCode = null;
            this.txtvisinh_nguoigiao.txtMyCode_Edit = null;
            this.txtvisinh_nguoigiao.txtMyID = null;
            this.txtvisinh_nguoigiao.txtMyID_Edit = null;
            this.txtvisinh_nguoigiao.txtMyName = null;
            this.txtvisinh_nguoigiao.txtMyName_Edit = null;
            this.txtvisinh_nguoigiao.txtNext = null;
            // 
            // txthoaly_nguoinhan
            // 
            this.txthoaly_nguoinhan._backcolor = System.Drawing.SystemColors.Control;
            this.txthoaly_nguoinhan._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txthoaly_nguoinhan._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txthoaly_nguoinhan.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txthoaly_nguoinhan.AutoCompleteList")));
            this.txthoaly_nguoinhan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txthoaly_nguoinhan.CaseSensitive = false;
            this.txthoaly_nguoinhan.CompareNoID = true;
            this.txthoaly_nguoinhan.DefaultCode = "-1";
            this.txthoaly_nguoinhan.DefaultID = "-1";
            this.txthoaly_nguoinhan.Drug_ID = null;
            this.txthoaly_nguoinhan.ExtraWidth = 0;
            this.txthoaly_nguoinhan.FillValueAfterSelect = false;
            this.txthoaly_nguoinhan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txthoaly_nguoinhan.LOAI_DANHMUC = "KN_HOALY_NGUOIGIAO";
            this.txthoaly_nguoinhan.Location = new System.Drawing.Point(632, 58);
            this.txthoaly_nguoinhan.MaxHeight = 200;
            this.txthoaly_nguoinhan.MinTypedCharacters = 2;
            this.txthoaly_nguoinhan.MyCode = "-1";
            this.txthoaly_nguoinhan.MyID = "-1";
            this.txthoaly_nguoinhan.Name = "txthoaly_nguoinhan";
            this.txthoaly_nguoinhan.RaiseEvent = false;
            this.txthoaly_nguoinhan.RaiseEventEnter = false;
            this.txthoaly_nguoinhan.RaiseEventEnterWhenEmpty = false;
            this.txthoaly_nguoinhan.SelectedIndex = -1;
            this.txthoaly_nguoinhan.Size = new System.Drawing.Size(191, 21);
            this.txthoaly_nguoinhan.splitChar = '@';
            this.txthoaly_nguoinhan.splitCharIDAndCode = '#';
            this.txthoaly_nguoinhan.TabIndex = 2;
            this.txthoaly_nguoinhan.TakeCode = false;
            this.txthoaly_nguoinhan.txtMyCode = null;
            this.txthoaly_nguoinhan.txtMyCode_Edit = null;
            this.txthoaly_nguoinhan.txtMyID = null;
            this.txthoaly_nguoinhan.txtMyID_Edit = null;
            this.txthoaly_nguoinhan.txtMyName = null;
            this.txthoaly_nguoinhan.txtMyName_Edit = null;
            this.txthoaly_nguoinhan.txtNext = null;
            // 
            // txthoaly_nguoigiao
            // 
            this.txthoaly_nguoigiao._backcolor = System.Drawing.SystemColors.Control;
            this.txthoaly_nguoigiao._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txthoaly_nguoigiao._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txthoaly_nguoigiao.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txthoaly_nguoigiao.AutoCompleteList")));
            this.txthoaly_nguoigiao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txthoaly_nguoigiao.CaseSensitive = false;
            this.txthoaly_nguoigiao.CompareNoID = true;
            this.txthoaly_nguoigiao.DefaultCode = "-1";
            this.txthoaly_nguoigiao.DefaultID = "-1";
            this.txthoaly_nguoigiao.Drug_ID = null;
            this.txthoaly_nguoigiao.ExtraWidth = 0;
            this.txthoaly_nguoigiao.FillValueAfterSelect = false;
            this.txthoaly_nguoigiao.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txthoaly_nguoigiao.LOAI_DANHMUC = "KN_HOALY_NGUOIGIAO";
            this.txthoaly_nguoigiao.Location = new System.Drawing.Point(354, 58);
            this.txthoaly_nguoigiao.MaxHeight = 200;
            this.txthoaly_nguoigiao.MinTypedCharacters = 2;
            this.txthoaly_nguoigiao.MyCode = "-1";
            this.txthoaly_nguoigiao.MyID = "-1";
            this.txthoaly_nguoigiao.Name = "txthoaly_nguoigiao";
            this.txthoaly_nguoigiao.RaiseEvent = false;
            this.txthoaly_nguoigiao.RaiseEventEnter = false;
            this.txthoaly_nguoigiao.RaiseEventEnterWhenEmpty = false;
            this.txthoaly_nguoigiao.SelectedIndex = -1;
            this.txthoaly_nguoigiao.Size = new System.Drawing.Size(191, 21);
            this.txthoaly_nguoigiao.splitChar = '@';
            this.txthoaly_nguoigiao.splitCharIDAndCode = '#';
            this.txthoaly_nguoigiao.TabIndex = 1;
            this.txthoaly_nguoigiao.TakeCode = false;
            this.txthoaly_nguoigiao.txtMyCode = null;
            this.txthoaly_nguoigiao.txtMyCode_Edit = null;
            this.txthoaly_nguoigiao.txtMyID = null;
            this.txthoaly_nguoigiao.txtMyID_Edit = null;
            this.txthoaly_nguoigiao.txtMyName = null;
            this.txthoaly_nguoigiao.txtMyName_Edit = null;
            this.txthoaly_nguoigiao.txtNext = null;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(551, 126);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(78, 21);
            this.label17.TabIndex = 624;
            this.label17.Text = "Người nhận:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(551, 93);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 21);
            this.label16.TabIndex = 623;
            this.label16.Text = "Người nhận:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label15.Location = new System.Drawing.Point(551, 57);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 21);
            this.label15.TabIndex = 622;
            this.label15.Text = "Người nhận:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtthauphu_luongmau
            // 
            this.txtthauphu_luongmau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtthauphu_luongmau.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthauphu_luongmau.Location = new System.Drawing.Point(154, 126);
            this.txtthauphu_luongmau.Masked = MaskedTextBox.Mask.Digit;
            this.txtthauphu_luongmau.Name = "txtthauphu_luongmau";
            this.txtthauphu_luongmau.Size = new System.Drawing.Size(100, 21);
            this.txtthauphu_luongmau.TabIndex = 6;
            this.txtthauphu_luongmau.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtvisinh_luongmau
            // 
            this.txtvisinh_luongmau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtvisinh_luongmau.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvisinh_luongmau.Location = new System.Drawing.Point(154, 93);
            this.txtvisinh_luongmau.Masked = MaskedTextBox.Mask.Digit;
            this.txtvisinh_luongmau.Name = "txtvisinh_luongmau";
            this.txtvisinh_luongmau.Size = new System.Drawing.Size(100, 21);
            this.txtvisinh_luongmau.TabIndex = 3;
            this.txtvisinh_luongmau.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txthoaly_luongmau
            // 
            this.txthoaly_luongmau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txthoaly_luongmau.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txthoaly_luongmau.Location = new System.Drawing.Point(154, 58);
            this.txthoaly_luongmau.Masked = MaskedTextBox.Mask.Digit;
            this.txthoaly_luongmau.Name = "txthoaly_luongmau";
            this.txthoaly_luongmau.Size = new System.Drawing.Size(100, 21);
            this.txthoaly_luongmau.TabIndex = 0;
            this.txthoaly_luongmau.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(270, 126);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 21);
            this.label14.TabIndex = 618;
            this.label14.Text = "Người giao:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(70, 126);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 21);
            this.label13.TabIndex = 617;
            this.label13.Text = "Lượng mẫu:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(6, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 19);
            this.label12.TabIndex = 616;
            this.label12.Text = "Thầu phụ:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(20, 93);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 19);
            this.label11.TabIndex = 615;
            this.label11.Text = "Vi sinh:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(270, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 21);
            this.label9.TabIndex = 614;
            this.label9.Text = "Người giao:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(70, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 21);
            this.label8.TabIndex = 613;
            this.label8.Text = "Lượng mẫu:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(270, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 21);
            this.label6.TabIndex = 612;
            this.label6.Text = "Người giao:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(70, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 21);
            this.label4.TabIndex = 611;
            this.label4.Text = "Lượng mẫu:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(21, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 610;
            this.label2.Text = "Hóa lý:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(563, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 21);
            this.label1.TabIndex = 609;
            this.label1.Text = "Mã phiếu:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAssignCode
            // 
            this.txtAssignCode.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.txtAssignCode.Enabled = false;
            this.txtAssignCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssignCode.Location = new System.Drawing.Point(632, 22);
            this.txtAssignCode.Name = "txtAssignCode";
            this.txtAssignCode.Size = new System.Drawing.Size(191, 21);
            this.txtAssignCode.TabIndex = 608;
            this.txtAssignCode.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtThetichKhoiluong
            // 
            this.txtThetichKhoiluong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtThetichKhoiluong.Enabled = false;
            this.txtThetichKhoiluong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThetichKhoiluong.Location = new System.Drawing.Point(154, 22);
            this.txtThetichKhoiluong.Masked = MaskedTextBox.Mask.Digit;
            this.txtThetichKhoiluong.Name = "txtThetichKhoiluong";
            this.txtThetichKhoiluong.Size = new System.Drawing.Size(100, 21);
            this.txtThetichKhoiluong.TabIndex = 604;
            this.txtThetichKhoiluong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(6, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 19);
            this.label10.TabIndex = 607;
            this.label10.Text = "Thể tích/KL mẫu:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(245, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 21);
            this.label7.TabIndex = 606;
            this.label7.Text = "Tình trạng mẫu:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTinhtrangmau
            // 
            this.txtTinhtrangmau._backcolor = System.Drawing.SystemColors.Control;
            this.txtTinhtrangmau._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhtrangmau._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTinhtrangmau.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTinhtrangmau.AutoCompleteList")));
            this.txtTinhtrangmau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTinhtrangmau.CaseSensitive = false;
            this.txtTinhtrangmau.CompareNoID = true;
            this.txtTinhtrangmau.DefaultCode = "-1";
            this.txtTinhtrangmau.DefaultID = "-1";
            this.txtTinhtrangmau.Drug_ID = null;
            this.txtTinhtrangmau.Enabled = false;
            this.txtTinhtrangmau.ExtraWidth = 0;
            this.txtTinhtrangmau.FillValueAfterSelect = false;
            this.txtTinhtrangmau.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhtrangmau.LOAI_DANHMUC = "TINHTRANGMAU";
            this.txtTinhtrangmau.Location = new System.Drawing.Point(354, 22);
            this.txtTinhtrangmau.MaxHeight = 200;
            this.txtTinhtrangmau.MinTypedCharacters = 2;
            this.txtTinhtrangmau.MyCode = "-1";
            this.txtTinhtrangmau.MyID = "-1";
            this.txtTinhtrangmau.Name = "txtTinhtrangmau";
            this.txtTinhtrangmau.RaiseEvent = false;
            this.txtTinhtrangmau.RaiseEventEnter = false;
            this.txtTinhtrangmau.RaiseEventEnterWhenEmpty = false;
            this.txtTinhtrangmau.SelectedIndex = -1;
            this.txtTinhtrangmau.Size = new System.Drawing.Size(191, 21);
            this.txtTinhtrangmau.splitChar = '@';
            this.txtTinhtrangmau.splitCharIDAndCode = '#';
            this.txtTinhtrangmau.TabIndex = 605;
            this.txtTinhtrangmau.TakeCode = false;
            this.txtTinhtrangmau.txtMyCode = null;
            this.txtTinhtrangmau.txtMyCode_Edit = null;
            this.txtTinhtrangmau.txtMyID = null;
            this.txtTinhtrangmau.txtMyID_Edit = null;
            this.txtTinhtrangmau.txtMyName = null;
            this.txtTinhtrangmau.txtMyName_Edit = null;
            this.txtTinhtrangmau.txtNext = null;
            // 
            // calendarCombo1
            // 
            this.calendarCombo1.CustomFormat = "dd/MM/yyyy";
            this.calendarCombo1.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.calendarCombo1.DropDownCalendar.Name = "";
            this.calendarCombo1.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.calendarCombo1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendarCombo1.Location = new System.Drawing.Point(98, 27);
            this.calendarCombo1.Name = "calendarCombo1";
            this.calendarCombo1.ShowUpDown = true;
            this.calendarCombo1.Size = new System.Drawing.Size(109, 21);
            this.calendarCombo1.TabIndex = 632;
            this.calendarCombo1.Value = new System.DateTime(2014, 5, 11, 0, 0, 0, 0);
            this.calendarCombo1.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label19.Location = new System.Drawing.Point(17, 27);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(78, 21);
            this.label19.TabIndex = 631;
            this.label19.Text = "Ngày giao:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMessge
            // 
            this.lblMessge.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessge.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblMessge.Location = new System.Drawing.Point(581, 27);
            this.lblMessge.Name = "lblMessge";
            this.lblMessge.Size = new System.Drawing.Size(256, 21);
            this.lblMessge.TabIndex = 633;
            this.lblMessge.Text = "lblMessge";
            this.lblMessge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmBanGiaoMauKiemNghiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 329);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.uiStatusBar1);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBanGiaoMauKiemNghiem";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bàn giao mẫu kiểm nghiệm";
            this.Load += new System.EventHandler(this.FrmBanGiaoMauKiemNghiem_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            this.uiGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdThoat;
        private Janus.Windows.EditControls.UIButton cmdBanGiao;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        public Janus.Windows.GridEX.EditControls.EditBox txtPatientName;
        public AutoCompleteTextbox txtDichvuKn;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgaytraKQ;
        private System.Windows.Forms.Label label18;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private AutoCompleteTextbox_Danhmucchung txtthauphu_nguoinhan;
        private AutoCompleteTextbox_Danhmucchung txtthauphu_nguoigiao;
        private AutoCompleteTextbox_Danhmucchung txtvisinh_nguoinhan;
        private AutoCompleteTextbox_Danhmucchung txtvisinh_nguoigiao;
        private AutoCompleteTextbox_Danhmucchung txthoaly_nguoinhan;
        private AutoCompleteTextbox_Danhmucchung txthoaly_nguoigiao;
        internal System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label16;
        internal System.Windows.Forms.Label label15;
        private MaskedTextBox.MaskedTextBox txtthauphu_luongmau;
        private MaskedTextBox.MaskedTextBox txtvisinh_luongmau;
        private MaskedTextBox.MaskedTextBox txthoaly_luongmau;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtAssignCode;
        private MaskedTextBox.MaskedTextBox txtThetichKhoiluong;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label7;
        private AutoCompleteTextbox_Danhmucchung txtTinhtrangmau;
        internal System.Windows.Forms.Label label19;
        private Janus.Windows.CalendarCombo.CalendarCombo calendarCombo1;
        internal System.Windows.Forms.Label lblMessge;

    }
}