﻿namespace VNS.HIS.UI.DANHMUC
{
    partial class DMUC_KIEUDMUC
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
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DMUC_KIEUDMUC));
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.ribbonStatusBar1 = new Janus.Windows.Ribbon.RibbonStatusBar();
            this.labelCommand1 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand2 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand3 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand4 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand5 = new Janus.Windows.Ribbon.LabelCommand();
            this.labelCommand6 = new Janus.Windows.Ribbon.LabelCommand();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.chkAutoNew = new Janus.Windows.EditControls.UICheckBox();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdCancel = new Janus.Windows.EditControls.UIButton();
            this.cmdUpdate = new Janus.Windows.EditControls.UIButton();
            this.cmdNew = new Janus.Windows.EditControls.UIButton();
            this.cmdDelete = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.pnlView = new System.Windows.Forms.Panel();
            this.uiTab1 = new Janus.Windows.UI.Tab.UITab();
            this.uiTabPage1 = new Janus.Windows.UI.Tab.UITabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtMotathem = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.chkTrangthai = new Janus.Windows.EditControls.UICheckBox();
            this.txtTen = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMa = new Janus.Windows.GridEX.EditControls.EditBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlBottom.SuspendLayout();
            this.pnlView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).BeginInit();
            this.uiTab1.SuspendLayout();
            this.uiTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTOP
            // 
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.Location = new System.Drawing.Point(0, 0);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Size = new System.Drawing.Size(784, 0);
            this.pnlTOP.TabIndex = 0;
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ImageSize = new System.Drawing.Size(16, 16);
            this.ribbonStatusBar1.LeftPanelCommands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.labelCommand1,
            this.labelCommand2,
            this.labelCommand3,
            this.labelCommand4,
            this.labelCommand5,
            this.labelCommand6});
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 539);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Office2007ColorScheme = Janus.Windows.Ribbon.Office2007ColorScheme.Custom;
            this.ribbonStatusBar1.Office2007CustomColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ribbonStatusBar1.ShowToolTips = false;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(784, 23);
            // 
            // 
            // 
            this.ribbonStatusBar1.SuperTipComponent.AutoPopDelay = 2000;
            this.ribbonStatusBar1.SuperTipComponent.ImageList = null;
            this.ribbonStatusBar1.TabIndex = 1;
            this.ribbonStatusBar1.Text = "ribbonStatusBar1";
            this.ribbonStatusBar1.UseCompatibleTextRendering = false;
            // 
            // labelCommand1
            // 
            this.labelCommand1.Key = "labelCommand1";
            this.labelCommand1.Name = "labelCommand1";
            this.labelCommand1.Text = "Thêm mới(Ctrl+N)";
            // 
            // labelCommand2
            // 
            this.labelCommand2.Key = "labelCommand2";
            this.labelCommand2.Name = "labelCommand2";
            this.labelCommand2.Text = "Cập nhật(Ctrl+E)";
            // 
            // labelCommand3
            // 
            this.labelCommand3.Key = "labelCommand3";
            this.labelCommand3.Name = "labelCommand3";
            this.labelCommand3.Text = "Xóa(Ctrl+D)";
            // 
            // labelCommand4
            // 
            this.labelCommand4.Key = "labelCommand4";
            this.labelCommand4.Name = "labelCommand4";
            this.labelCommand4.Text = "Ghi(Ctrl+S)";
            // 
            // labelCommand5
            // 
            this.labelCommand5.Key = "labelCommand5";
            this.labelCommand5.Name = "labelCommand5";
            this.labelCommand5.Text = "In(Ctrl+P)";
            // 
            // labelCommand6
            // 
            this.labelCommand6.Key = "labelCommand6";
            this.labelCommand6.Name = "labelCommand6";
            this.labelCommand6.Text = "Thoát(Esc)";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.chkAutoNew);
            this.pnlBottom.Controls.Add(this.cmdPrint);
            this.pnlBottom.Controls.Add(this.cmdCancel);
            this.pnlBottom.Controls.Add(this.cmdUpdate);
            this.pnlBottom.Controls.Add(this.cmdNew);
            this.pnlBottom.Controls.Add(this.cmdDelete);
            this.pnlBottom.Controls.Add(this.cmdSave);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBottom.Location = new System.Drawing.Point(0, 496);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(784, 43);
            this.pnlBottom.TabIndex = 2;
            // 
            // chkAutoNew
            // 
            this.chkAutoNew.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoNew.Location = new System.Drawing.Point(115, 9);
            this.chkAutoNew.Name = "chkAutoNew";
            this.chkAutoNew.Size = new System.Drawing.Size(214, 23);
            this.chkAutoNew.TabIndex = 12;
            this.chkAutoNew.TabStop = false;
            this.chkAutoNew.Text = "Cho phép thêm mới liên tục?";
            this.chkAutoNew.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Location = new System.Drawing.Point(9, 8);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(89, 25);
            this.cmdPrint.TabIndex = 10;
            this.cmdPrint.Text = "Mở danh mục";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(677, 8);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(89, 25);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "Thoát";
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(487, 8);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(89, 25);
            this.cmdUpdate.TabIndex = 1;
            this.cmdUpdate.Text = "Cập nhật";
            // 
            // cmdNew
            // 
            this.cmdNew.Location = new System.Drawing.Point(392, 8);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(89, 25);
            this.cmdNew.TabIndex = 0;
            this.cmdNew.Text = "Thêm mới";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(582, 8);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(89, 25);
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Text = "Xóa";
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(582, 8);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(89, 25);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Ghi";
            this.cmdSave.Visible = false;
            // 
            // pnlView
            // 
            this.pnlView.Controls.Add(this.uiTab1);
            this.pnlView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlView.Location = new System.Drawing.Point(0, 348);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(784, 148);
            this.pnlView.TabIndex = 3;
            // 
            // uiTab1
            // 
            this.uiTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTab1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTab1.Location = new System.Drawing.Point(0, 0);
            this.uiTab1.Name = "uiTab1";
            this.uiTab1.Size = new System.Drawing.Size(784, 148);
            this.uiTab1.TabIndex = 0;
            this.uiTab1.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.uiTabPage1});
            this.uiTab1.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.VS2005;
            // 
            // uiTabPage1
            // 
            this.uiTabPage1.Controls.Add(this.label2);
            this.uiTabPage1.Controls.Add(this.txtID);
            this.uiTabPage1.Controls.Add(this.txtMotathem);
            this.uiTabPage1.Controls.Add(this.label4);
            this.uiTabPage1.Controls.Add(this.lblMsg);
            this.uiTabPage1.Controls.Add(this.chkTrangthai);
            this.uiTabPage1.Controls.Add(this.txtTen);
            this.uiTabPage1.Controls.Add(this.lblTen);
            this.uiTabPage1.Controls.Add(this.label1);
            this.uiTabPage1.Controls.Add(this.txtMa);
            this.uiTabPage1.Location = new System.Drawing.Point(1, 23);
            this.uiTabPage1.Name = "uiTabPage1";
            this.uiTabPage1.Size = new System.Drawing.Size(782, 124);
            this.uiTabPage1.TabStop = true;
            this.uiTabPage1.Text = "Thông tin";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(19, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 23);
            this.label2.TabIndex = 12;
            this.label2.Text = "ID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(114, 10);
            this.txtID.MaxLength = 20;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(147, 21);
            this.txtID.TabIndex = 11;
            this.txtID.Text = "(Tự sinh)";
            // 
            // txtMotathem
            // 
            this.txtMotathem.Location = new System.Drawing.Point(114, 67);
            this.txtMotathem.MaxLength = 255;
            this.txtMotathem.Name = "txtMotathem";
            this.txtMotathem.Size = new System.Drawing.Size(651, 21);
            this.txtMotathem.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(19, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Mô tả thêm:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Location = new System.Drawing.Point(308, 98);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(457, 23);
            this.lblMsg.TabIndex = 9;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkTrangthai
            // 
            this.chkTrangthai.BackColor = System.Drawing.Color.Transparent;
            this.chkTrangthai.Location = new System.Drawing.Point(114, 95);
            this.chkTrangthai.Name = "chkTrangthai";
            this.chkTrangthai.Size = new System.Drawing.Size(126, 23);
            this.chkTrangthai.TabIndex = 5;
            this.chkTrangthai.TabStop = false;
            this.chkTrangthai.Text = "Đang sử dụng?";
            this.chkTrangthai.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005;
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(311, 39);
            this.txtTen.MaxLength = 255;
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(454, 21);
            this.txtTen.TabIndex = 3;
            // 
            // lblTen
            // 
            this.lblTen.BackColor = System.Drawing.Color.Transparent;
            this.lblTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTen.Location = new System.Drawing.Point(267, 42);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(40, 16);
            this.lblTen.TabIndex = 2;
            this.lblTen.Text = "Tên:";
            this.lblTen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(19, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(114, 38);
            this.txtMa.MaxLength = 20;
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(147, 21);
            this.txtMa.TabIndex = 0;
            // 
            // grdList
            // 
            this.grdList.AutomaticSort = false;
            this.grdList.ContextMenuStrip = this.contextMenuStrip1;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.FrozenColumns = 2;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.IncrementalSearchMode = Janus.Windows.GridEX.IncrementalSearchMode.FirstCharacter;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(784, 348);
            this.grdList.TabIndex = 5;
            this.grdList.TabStop = false;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInsert,
            this.mnuUpdate,
            this.mnuDelete,
            this.toolStripMenuItem1,
            this.mnuPrint});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(142, 98);
            // 
            // mnuInsert
            // 
            this.mnuInsert.Name = "mnuInsert";
            this.mnuInsert.Size = new System.Drawing.Size(141, 22);
            this.mnuInsert.Text = "Thêm mới";
            // 
            // mnuUpdate
            // 
            this.mnuUpdate.Name = "mnuUpdate";
            this.mnuUpdate.Size = new System.Drawing.Size(141, 22);
            this.mnuUpdate.Text = "Cập nhật";
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(141, 22);
            this.mnuDelete.Text = "Xóa";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 6);
            // 
            // mnuPrint
            // 
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.Size = new System.Drawing.Size(141, 22);
            this.mnuPrint.Text = "In danh sách";
            // 
            // DMUC_KIEUDMUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.pnlView);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.pnlTOP);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "DMUC_KIEUDMUC";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục...";
            this.pnlBottom.ResumeLayout(false);
            this.pnlView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).EndInit();
            this.uiTab1.ResumeLayout(false);
            this.uiTabPage1.ResumeLayout(false);
            this.uiTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTOP;
        private Janus.Windows.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlView;
        public Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdCancel;
        private Janus.Windows.EditControls.UIButton cmdDelete;
        private Janus.Windows.EditControls.UIButton cmdUpdate;
        private Janus.Windows.EditControls.UIButton cmdNew;
        private Janus.Windows.UI.Tab.UITab uiTab1;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage1;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.Ribbon.LabelCommand labelCommand1;
        private Janus.Windows.Ribbon.LabelCommand labelCommand2;
        private Janus.Windows.GridEX.EditControls.EditBox txtTen;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtMa;
        private Janus.Windows.Ribbon.LabelCommand labelCommand3;
        private Janus.Windows.Ribbon.LabelCommand labelCommand4;
        private Janus.Windows.Ribbon.LabelCommand labelCommand5;
        private Janus.Windows.Ribbon.LabelCommand labelCommand6;
        private Janus.Windows.EditControls.UICheckBox chkTrangthai;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.GridEX.EditControls.EditBox txtMotathem;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UICheckBox chkAutoNew;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuInsert;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdate;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtID;
    }
}