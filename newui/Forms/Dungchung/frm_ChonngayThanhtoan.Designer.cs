﻿namespace VNS.HIS.UI.Forms.Cauhinh
{
    partial class frm_ChonngayThanhtoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChonngayThanhtoan));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtCreateDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.radEditDate = new Janus.Windows.EditControls.UIRadioButton();
            this.radRegisterDate = new Janus.Windows.EditControls.UIRadioButton();
            this.radCurrentDate = new Janus.Windows.EditControls.UIRadioButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(482, 222);
            this.panel1.TabIndex = 3;
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
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(482, 222);
            this.splitContainer1.SplitterDistance = 61;
            this.splitContainer1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(85, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(397, 61);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hệ thống phát hiện ngày thanh toán khác với ngày đăng ký khám chữa bệnh của Bệnh " +
    "nhân. Mời bạn xác nhận ngày thanh toán thực tế";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(85, 61);
            this.panel2.TabIndex = 6;
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
            this.splitContainer2.Panel1.Controls.Add(this.uiGroupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.uiGroupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(482, 157);
            this.splitContainer2.SplitterDistance = 85;
            this.splitContainer2.TabIndex = 0;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.dtCreateDate);
            this.uiGroupBox1.Controls.Add(this.radEditDate);
            this.uiGroupBox1.Controls.Add(this.radRegisterDate);
            this.uiGroupBox1.Controls.Add(this.radCurrentDate);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(482, 85);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Ngày thanh toán:";
            // 
            // dtCreateDate
            // 
            this.dtCreateDate.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtCreateDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtCreateDate.DropDownCalendar.Name = "";
            this.dtCreateDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            this.dtCreateDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtCreateDate.Location = new System.Drawing.Point(194, 47);
            this.dtCreateDate.Name = "dtCreateDate";
            this.dtCreateDate.ReadOnly = true;
            this.dtCreateDate.ShowUpDown = true;
            this.dtCreateDate.Size = new System.Drawing.Size(195, 23);
            this.dtCreateDate.TabIndex = 8;
            this.dtCreateDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003;
            // 
            // radEditDate
            // 
            this.radEditDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEditDate.Location = new System.Drawing.Point(352, 14);
            this.radEditDate.Name = "radEditDate";
            this.radEditDate.Size = new System.Drawing.Size(97, 27);
            this.radEditDate.TabIndex = 7;
            this.radEditDate.TabStop = true;
            this.radEditDate.Text = "Tùy chỉnh";
            this.radEditDate.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // radRegisterDate
            // 
            this.radRegisterDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRegisterDate.Location = new System.Drawing.Point(194, 14);
            this.radRegisterDate.Name = "radRegisterDate";
            this.radRegisterDate.Size = new System.Drawing.Size(152, 27);
            this.radRegisterDate.TabIndex = 6;
            this.radRegisterDate.Text = "Ngày đăng ký KCB";
            this.radRegisterDate.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // radCurrentDate
            // 
            this.radCurrentDate.Checked = true;
            this.radCurrentDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radCurrentDate.Location = new System.Drawing.Point(65, 14);
            this.radCurrentDate.Name = "radCurrentDate";
            this.radCurrentDate.Size = new System.Drawing.Size(121, 27);
            this.radCurrentDate.TabIndex = 5;
            this.radCurrentDate.TabStop = true;
            this.radCurrentDate.Text = "Ngày hiện tại";
            this.radCurrentDate.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.cmdAccept);
            this.uiGroupBox2.Controls.Add(this.cmdExit);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(482, 68);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAccept.Location = new System.Drawing.Point(132, 20);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(115, 39);
            this.cmdAccept.TabIndex = 553;
            this.cmdAccept.Text = "Chấp nhận";
            this.cmdAccept.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(265, 20);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(117, 39);
            this.cmdExit.TabIndex = 554;
            this.cmdExit.Text = "Thoát(Esc)";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // frm_ChonngayThanhtoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 222);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChonngayThanhtoan";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin cấu hình ngày thanh toán";
            this.Load += new System.EventHandler(this.frm_ChonngayThanhtoan_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ChonngayThanhtoan_KeyDown);
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
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtCreateDate;
        private Janus.Windows.EditControls.UIRadioButton radEditDate;
        private Janus.Windows.EditControls.UIRadioButton radRegisterDate;
        private Janus.Windows.EditControls.UIRadioButton radCurrentDate;
    }
}