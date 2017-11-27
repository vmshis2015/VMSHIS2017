namespace VNS.HIS.UI.Forms.NGOAITRU
{
    partial class FrmThongTinDonThuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThongTinDonThuoc));
            Janus.Windows.GridEX.GridEXLayout grdPresDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.chkTutuc = new Janus.Windows.EditControls.UICheckBox();
            this.txtMaBenhChinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtChanDoan = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtPres_ID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtPatientCode = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtPatientID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtYearBirth = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtSex = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtPatientName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdSavePres = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTenBenhChinh = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtchandoan_new = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdPresDetail = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPresDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 357);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.Key = "";
            uiStatusBarPanel1.ProgressBarValue = 0;
            uiStatusBarPanel1.Text = "Ctrl+S: Lưu lại";
            uiStatusBarPanel1.Width = 93;
            uiStatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel2.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel2.Key = "";
            uiStatusBarPanel2.ProgressBarValue = 0;
            uiStatusBarPanel2.Text = "Esc: Thoát";
            uiStatusBarPanel2.Width = 73;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1,
            uiStatusBarPanel2});
            this.uiStatusBar1.Size = new System.Drawing.Size(697, 23);
            this.uiStatusBar1.TabIndex = 1;
            this.uiStatusBar1.TabStop = false;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer1.Panel2.Controls.Add(this.uiGroupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(697, 357);
            this.splitContainer1.SplitterDistance = 77;
            this.splitContainer1.TabIndex = 2;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.chkTutuc);
            this.uiGroupBox1.Controls.Add(this.txtMaBenhChinh);
            this.uiGroupBox1.Controls.Add(this.txtChanDoan);
            this.uiGroupBox1.Controls.Add(this.txtPres_ID);
            this.uiGroupBox1.Controls.Add(this.txtPatientCode);
            this.uiGroupBox1.Controls.Add(this.txtPatientID);
            this.uiGroupBox1.Controls.Add(this.txtYearBirth);
            this.uiGroupBox1.Controls.Add(this.txtSex);
            this.uiGroupBox1.Controls.Add(this.txtPatientName);
            this.uiGroupBox1.Controls.Add(this.cmdSavePres);
            this.uiGroupBox1.Controls.Add(this.cmdExit);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.txtTenBenhChinh);
            this.uiGroupBox1.Controls.Add(this.txtchandoan_new);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(697, 77);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin đơn thuốc";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // chkTutuc
            // 
            this.chkTutuc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTutuc.ForeColor = System.Drawing.Color.Navy;
            this.chkTutuc.Location = new System.Drawing.Point(357, 21);
            this.chkTutuc.Name = "chkTutuc";
            this.chkTutuc.Size = new System.Drawing.Size(105, 23);
            this.chkTutuc.TabIndex = 598;
            this.chkTutuc.Text = "Thuốc tự túc";
            this.chkTutuc.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            this.chkTutuc.CheckedChanged += new System.EventHandler(this.chkTutuc_CheckedChanged);
            // 
            // txtMaBenhChinh
            // 
            this.txtMaBenhChinh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaBenhChinh.Location = new System.Drawing.Point(357, 49);
            this.txtMaBenhChinh.Name = "txtMaBenhChinh";
            this.txtMaBenhChinh.ReadOnly = true;
            this.txtMaBenhChinh.Size = new System.Drawing.Size(36, 21);
            this.txtMaBenhChinh.TabIndex = 587;
            this.txtMaBenhChinh.TabStop = false;
            this.txtMaBenhChinh.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtMaBenhChinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtChanDoan
            // 
            this.txtChanDoan.BackColor = System.Drawing.Color.White;
            this.txtChanDoan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChanDoan.ForeColor = System.Drawing.Color.Black;
            this.txtChanDoan.Location = new System.Drawing.Point(77, 49);
            this.txtChanDoan.Name = "txtChanDoan";
            this.txtChanDoan.Size = new System.Drawing.Size(277, 21);
            this.txtChanDoan.TabIndex = 585;
            this.txtChanDoan.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtPres_ID
            // 
            this.txtPres_ID.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPres_ID.Location = new System.Drawing.Point(383, 21);
            this.txtPres_ID.Name = "txtPres_ID";
            this.txtPres_ID.ReadOnly = true;
            this.txtPres_ID.Size = new System.Drawing.Size(10, 21);
            this.txtPres_ID.TabIndex = 584;
            this.txtPres_ID.Visible = false;
            this.txtPres_ID.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtPatientCode
            // 
            this.txtPatientCode.BackColor = System.Drawing.Color.Honeydew;
            this.txtPatientCode.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCode.Location = new System.Drawing.Point(383, 20);
            this.txtPatientCode.Name = "txtPatientCode";
            this.txtPatientCode.ReadOnly = true;
            this.txtPatientCode.Size = new System.Drawing.Size(10, 23);
            this.txtPatientCode.TabIndex = 583;
            this.txtPatientCode.TabStop = false;
            this.txtPatientCode.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPatientCode.Visible = false;
            this.txtPatientCode.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtPatientID
            // 
            this.txtPatientID.BackColor = System.Drawing.Color.Honeydew;
            this.txtPatientID.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientID.Location = new System.Drawing.Point(360, 21);
            this.txtPatientID.Name = "txtPatientID";
            this.txtPatientID.ReadOnly = true;
            this.txtPatientID.Size = new System.Drawing.Size(10, 21);
            this.txtPatientID.TabIndex = 582;
            this.txtPatientID.TabStop = false;
            this.txtPatientID.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPatientID.Visible = false;
            this.txtPatientID.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtYearBirth
            // 
            this.txtYearBirth.ButtonFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearBirth.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearBirth.Location = new System.Drawing.Point(303, 20);
            this.txtYearBirth.Name = "txtYearBirth";
            this.txtYearBirth.ReadOnly = true;
            this.txtYearBirth.Size = new System.Drawing.Size(51, 23);
            this.txtYearBirth.TabIndex = 572;
            this.txtYearBirth.TabStop = false;
            this.txtYearBirth.Text = "1990";
            this.txtYearBirth.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtYearBirth.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtSex
            // 
            this.txtSex.ButtonFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSex.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSex.Location = new System.Drawing.Point(264, 20);
            this.txtSex.Name = "txtSex";
            this.txtSex.ReadOnly = true;
            this.txtSex.Size = new System.Drawing.Size(39, 23);
            this.txtSex.TabIndex = 571;
            this.txtSex.TabStop = false;
            this.txtSex.Text = "Nam";
            this.txtSex.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtSex.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtPatientName
            // 
            this.txtPatientName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.Location = new System.Drawing.Point(77, 20);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(186, 23);
            this.txtPatientName.TabIndex = 569;
            this.txtPatientName.TabStop = false;
            this.txtPatientName.Text = "Nguyễn thị phương thanh";
            this.txtPatientName.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPatientName.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // cmdSavePres
            // 
            this.cmdSavePres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSavePres.Image = ((System.Drawing.Image)(resources.GetObject("cmdSavePres.Image")));
            this.cmdSavePres.Location = new System.Drawing.Point(473, 30);
            this.cmdSavePres.Name = "cmdSavePres";
            this.cmdSavePres.Size = new System.Drawing.Size(104, 29);
            this.cmdSavePres.TabIndex = 9;
            this.cmdSavePres.Text = "Sao chép";
            this.cmdSavePres.Visible = false;
            this.cmdSavePres.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdSavePres.Click += new System.EventHandler(this.cmdSavePres_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(581, 30);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(104, 29);
            this.cmdExit.TabIndex = 10;
            this.cmdExit.Text = "Thoát";
            this.cmdExit.Visible = false;
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 586;
            this.label1.Text = "Chẩn đoán:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 581;
            this.label4.Text = "Họ và tên:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTenBenhChinh
            // 
            this.txtTenBenhChinh.Font = new System.Drawing.Font("Arial", 9F);
            this.txtTenBenhChinh.Location = new System.Drawing.Point(357, 21);
            this.txtTenBenhChinh.Name = "txtTenBenhChinh";
            this.txtTenBenhChinh.Size = new System.Drawing.Size(10, 21);
            this.txtTenBenhChinh.TabIndex = 599;
            this.txtTenBenhChinh.TabStop = false;
            this.txtTenBenhChinh.Visible = false;
            this.txtTenBenhChinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            // 
            // txtchandoan_new
            // 
            this.txtchandoan_new._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtchandoan_new._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchandoan_new._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtchandoan_new.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtchandoan_new.AutoCompleteList")));
            this.txtchandoan_new.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtchandoan_new.CaseSensitive = false;
            this.txtchandoan_new.CompareNoID = true;
            this.txtchandoan_new.DefaultCode = "-1";
            this.txtchandoan_new.DefaultID = "-1";
            this.txtchandoan_new.Drug_ID = null;
            this.txtchandoan_new.ExtraWidth = 0;
            this.txtchandoan_new.FillValueAfterSelect = false;
            this.txtchandoan_new.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchandoan_new.LOAI_DANHMUC = "CHANDOAN";
            this.txtchandoan_new.Location = new System.Drawing.Point(357, 22);
            this.txtchandoan_new.MaxHeight = -1;
            this.txtchandoan_new.MinTypedCharacters = 2;
            this.txtchandoan_new.MyCode = "-1";
            this.txtchandoan_new.MyID = "-1";
            this.txtchandoan_new.Name = "txtchandoan_new";
            this.txtchandoan_new.RaiseEvent = false;
            this.txtchandoan_new.RaiseEventEnter = false;
            this.txtchandoan_new.RaiseEventEnterWhenEmpty = false;
            this.txtchandoan_new.SelectedIndex = -1;
            this.txtchandoan_new.Size = new System.Drawing.Size(10, 21);
            this.txtchandoan_new.splitChar = '@';
            this.txtchandoan_new.splitCharIDAndCode = '#';
            this.txtchandoan_new.TabIndex = 600;
            this.txtchandoan_new.TakeCode = false;
            this.txtchandoan_new.txtMyCode = null;
            this.txtchandoan_new.txtMyCode_Edit = null;
            this.txtchandoan_new.txtMyID = null;
            this.txtchandoan_new.txtMyID_Edit = null;
            this.txtchandoan_new.txtMyName = null;
            this.txtchandoan_new.txtMyName_Edit = null;
            this.txtchandoan_new.txtNext = null;
            this.txtchandoan_new.Visible = false;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdPresDetail);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(697, 276);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Danh sách thuốc";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdPresDetail
            // 
            grdPresDetail_DesignTimeLayout.LayoutString = resources.GetString("grdPresDetail_DesignTimeLayout.LayoutString");
            this.grdPresDetail.DesignTimeLayout = grdPresDetail_DesignTimeLayout;
            this.grdPresDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPresDetail.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPresDetail.GroupByBoxVisible = false;
            this.grdPresDetail.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdPresDetail.GroupTotalRowFormatStyle.BackColor = System.Drawing.Color.White;
            this.grdPresDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPresDetail.Location = new System.Drawing.Point(3, 17);
            this.grdPresDetail.Name = "grdPresDetail";
            this.grdPresDetail.RowFormatStyle.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPresDetail.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPresDetail.SelectedFormatStyle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.grdPresDetail.Size = new System.Drawing.Size(691, 256);
            this.grdPresDetail.TabIndex = 551;
            this.grdPresDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPresDetail.TotalRowFormatStyle.BackColor = System.Drawing.Color.White;
            this.grdPresDetail.TotalRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdPresDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPresDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdPresDetail.UseGroupRowSelector = true;
            this.grdPresDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            // 
            // FrmThongTinDonThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 380);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.uiStatusBar1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThongTinDonThuoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông tin đơn thuốc";
            this.Load += new System.EventHandler(this.FrmThongTinDonThuoc_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPresDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        internal Janus.Windows.EditControls.UIButton cmdSavePres;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.GridEX.GridEX grdPresDetail;
        public Janus.Windows.GridEX.EditControls.EditBox txtPatientName;
        public Janus.Windows.GridEX.EditControls.EditBox txtYearBirth;
        public Janus.Windows.GridEX.EditControls.EditBox txtSex;
        internal System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtChanDoan;
        internal System.Windows.Forms.Label label1;
        public Janus.Windows.GridEX.EditControls.EditBox txtPatientCode;
        public Janus.Windows.GridEX.EditControls.EditBox txtPatientID;
        public Janus.Windows.GridEX.EditControls.EditBox txtPres_ID;
        public Janus.Windows.GridEX.EditControls.EditBox txtMaBenhChinh;
        private Janus.Windows.EditControls.UICheckBox chkTutuc;
        public Janus.Windows.GridEX.EditControls.EditBox txtTenBenhChinh;
        public UCs.AutoCompleteTextbox_Danhmucchung txtchandoan_new;
    }
}