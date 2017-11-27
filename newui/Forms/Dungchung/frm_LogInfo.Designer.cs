namespace VNS.HIS.UI.Forms.Dungchung
{
    partial class frm_LogInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_LogInfo));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem4 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.GridEX.GridEXLayout grdLogInfomation_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtNhanvien = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboLogAction = new Janus.Windows.EditControls.UIComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdViewLog = new Janus.Windows.EditControls.UIButton();
            this.cmdSearchLog = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdLogInfomation = new Janus.Windows.GridEX.GridEX();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLogInfomation)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 502);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1003, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1003, 502);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
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
            this.splitContainer1.Panel2.Controls.Add(this.uiGroupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1003, 502);
            this.splitContainer1.SplitterDistance = 265;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.uiGroupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.uiGroupBox3);
            this.splitContainer2.Size = new System.Drawing.Size(265, 502);
            this.splitContainer2.SplitterDistance = 153;
            this.splitContainer2.TabIndex = 0;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.txtNhanvien);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.cboLogAction);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.dtpToDate);
            this.uiGroupBox1.Controls.Add(this.dtpFromDate);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(265, 153);
            this.uiGroupBox1.TabIndex = 1;
            this.uiGroupBox1.Text = "Search Information";
            // 
            // txtNhanvien
            // 
            this.txtNhanvien._backcolor = System.Drawing.SystemColors.Control;
            this.txtNhanvien._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhanvien._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNhanvien.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNhanvien.AutoCompleteList")));
            this.txtNhanvien.CaseSensitive = false;
            this.txtNhanvien.CompareNoID = true;
            this.txtNhanvien.DefaultCode = "-1";
            this.txtNhanvien.DefaultID = "-1";
            this.txtNhanvien.Drug_ID = null;
            this.txtNhanvien.ExtraWidth = 0;
            this.txtNhanvien.FillValueAfterSelect = false;
            this.txtNhanvien.Location = new System.Drawing.Point(88, 112);
            this.txtNhanvien.MaxHeight = -1;
            this.txtNhanvien.MinTypedCharacters = 2;
            this.txtNhanvien.MyCode = "-1";
            this.txtNhanvien.MyID = "-1";
            this.txtNhanvien.MyText = "";
            this.txtNhanvien.Name = "txtNhanvien";
            this.txtNhanvien.RaiseEvent = false;
            this.txtNhanvien.RaiseEventEnter = false;
            this.txtNhanvien.RaiseEventEnterWhenEmpty = false;
            this.txtNhanvien.SelectedIndex = -1;
            this.txtNhanvien.Size = new System.Drawing.Size(171, 21);
            this.txtNhanvien.splitChar = '@';
            this.txtNhanvien.splitCharIDAndCode = '#';
            this.txtNhanvien.TabIndex = 10;
            this.txtNhanvien.TakeCode = false;
            this.txtNhanvien.txtMyCode = null;
            this.txtNhanvien.txtMyCode_Edit = null;
            this.txtNhanvien.txtMyID = null;
            this.txtNhanvien.txtMyID_Edit = null;
            this.txtNhanvien.txtMyName = null;
            this.txtNhanvien.txtMyName_Edit = null;
            this.txtNhanvien.txtNext = null;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "- Log User";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "- Log Action";
            // 
            // cboLogAction
            // 
            this.cboLogAction.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Tất cả";
            uiComboBoxItem1.Value = ((short)(-1));
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Thêm mới (Create)";
            uiComboBoxItem2.Value = ((short)(0));
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "Sửa (Update)";
            uiComboBoxItem3.Value = ((short)(1));
            uiComboBoxItem4.FormatStyle.Alpha = 0;
            uiComboBoxItem4.IsSeparator = false;
            uiComboBoxItem4.Text = "Hủy và Xóa ";
            uiComboBoxItem4.Value = ((short)(2));
            this.cboLogAction.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2,
            uiComboBoxItem3,
            uiComboBoxItem4});
            this.cboLogAction.Location = new System.Drawing.Point(88, 84);
            this.cboLogAction.Name = "cboLogAction";
            this.cboLogAction.Size = new System.Drawing.Size(171, 21);
            this.cboLogAction.TabIndex = 6;
            this.cboLogAction.Text = "Chọn Action";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "- To Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "- From Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(88, 55);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(171, 23);
            this.dtpToDate.TabIndex = 3;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(88, 28);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(171, 23);
            this.dtpFromDate.TabIndex = 2;
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.cmdExit);
            this.uiGroupBox3.Controls.Add(this.cmdViewLog);
            this.uiGroupBox3.Controls.Add(this.cmdSearchLog);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(265, 345);
            this.uiGroupBox3.TabIndex = 0;
            this.uiGroupBox3.Text = "Menu";
            // 
            // cmdExit
            // 
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(46, 124);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(159, 33);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.Text = "Exit (Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdViewLog
            // 
            this.cmdViewLog.Image = ((System.Drawing.Image)(resources.GetObject("cmdViewLog.Image")));
            this.cmdViewLog.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdViewLog.Location = new System.Drawing.Point(46, 85);
            this.cmdViewLog.Name = "cmdViewLog";
            this.cmdViewLog.Size = new System.Drawing.Size(159, 33);
            this.cmdViewLog.TabIndex = 1;
            this.cmdViewLog.Text = "View Log (F4)";
            this.cmdViewLog.Click += new System.EventHandler(this.cmdViewLog_Click);
            // 
            // cmdSearchLog
            // 
            this.cmdSearchLog.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearchLog.Image")));
            this.cmdSearchLog.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSearchLog.Location = new System.Drawing.Point(46, 46);
            this.cmdSearchLog.Name = "cmdSearchLog";
            this.cmdSearchLog.Size = new System.Drawing.Size(159, 33);
            this.cmdSearchLog.TabIndex = 0;
            this.cmdSearchLog.Text = "Search Log (F3)";
            this.cmdSearchLog.Click += new System.EventHandler(this.cmdSearchLog_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdLogInfomation);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(734, 502);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Log Infomation";
            // 
            // grdLogInfomation
            // 
            this.grdLogInfomation.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdLogInfomation.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdLogInfomation_DesignTimeLayout.LayoutString = resources.GetString("grdLogInfomation_DesignTimeLayout.LayoutString");
            this.grdLogInfomation.DesignTimeLayout = grdLogInfomation_DesignTimeLayout;
            this.grdLogInfomation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLogInfomation.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdLogInfomation.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdLogInfomation.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdLogInfomation.FilterRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdLogInfomation.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdLogInfomation.GroupByBoxVisible = false;
            this.grdLogInfomation.Location = new System.Drawing.Point(3, 17);
            this.grdLogInfomation.Name = "grdLogInfomation";
            this.grdLogInfomation.RecordNavigator = true;
            this.grdLogInfomation.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdLogInfomation.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdLogInfomation.Size = new System.Drawing.Size(728, 482);
            this.grdLogInfomation.TabIndex = 0;
            // 
            // frm_LogInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 524);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_LogInfo";
            this.Text = "Log Infomation";
            this.Load += new System.EventHandler(this.frm_LogInfo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_LogInfo_KeyDown);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLogInfomation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grdLogInfomation;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox cboLogAction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdViewLog;
        private Janus.Windows.EditControls.UIButton cmdSearchLog;
        private HIS.UCs.AutoCompleteTextbox txtNhanvien;
    }
}