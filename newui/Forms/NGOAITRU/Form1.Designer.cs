namespace VNS.HIS.UI.Forms.NGOAITRU
{
    partial class Form1
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
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel8 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel9 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel10 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel11 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel12 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel13 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel14 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            this.statusStrip1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.uiPanelManager1 = new Janus.Windows.UI.Dock.UIPanelManager(this.components);
            this.uiPanel0 = new Janus.Windows.UI.Dock.UIPanel();
            this.uiPanel0Container = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
            this.uiPanel1 = new Janus.Windows.UI.Dock.UIPanel();
            this.uiPanel1Container = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
            this.ribbonContextMenu1 = new Janus.Windows.Ribbon.RibbonContextMenu(this.components);
            this.dropDownCommand1 = new Janus.Windows.Ribbon.DropDownCommand();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabThongtin = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtxtLogs = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanel0)).BeginInit();
            this.uiPanel0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanel1)).BeginInit();
            this.uiPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Location = new System.Drawing.Point(0, 369);
            this.statusStrip1.Name = "statusStrip1";
            uiStatusBarPanel8.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel8.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel8.Key = "lblHospital";
            uiStatusBarPanel8.ProgressBarValue = 0;
            uiStatusBarPanel8.Text = "Bệnh viện:";
            uiStatusBarPanel8.Width = 72;
            uiStatusBarPanel9.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel9.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel9.Key = "lblDepartment";
            uiStatusBarPanel9.ProgressBarValue = 0;
            uiStatusBarPanel9.Text = "Khoa-phòng:";
            uiStatusBarPanel9.Width = 86;
            uiStatusBarPanel10.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel10.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel10.Key = "lblUser";
            uiStatusBarPanel10.ProgressBarValue = 0;
            uiStatusBarPanel10.Text = "Người dùng: ";
            uiStatusBarPanel10.Width = 87;
            uiStatusBarPanel11.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel11.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel11.Key = "lblIP";
            uiStatusBarPanel11.ProgressBarValue = 0;
            uiStatusBarPanel11.Text = "IP:";
            uiStatusBarPanel11.Width = 29;
            uiStatusBarPanel12.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel12.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel12.Key = "lblTime";
            uiStatusBarPanel12.ProgressBarValue = 0;
            uiStatusBarPanel12.Text = "Time";
            uiStatusBarPanel12.Width = 43;
            uiStatusBarPanel13.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            uiStatusBarPanel13.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            uiStatusBarPanel13.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel13.Key = "lblCopyright";
            uiStatusBarPanel13.ProgressBarValue = 0;
            uiStatusBarPanel13.Text = "COPYRIGHT © Công ty cổ phần CNTT VMS Việt Nam";
            uiStatusBarPanel13.Width = 617;
            uiStatusBarPanel14.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            uiStatusBarPanel14.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel14.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel14.Key = "lblUpdateVersion";
            uiStatusBarPanel14.ProgressBarValue = 0;
            uiStatusBarPanel14.Text = "Cập nhật phiên bản";
            uiStatusBarPanel14.Width = 123;
            this.statusStrip1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel8,
            uiStatusBarPanel9,
            uiStatusBarPanel10,
            uiStatusBarPanel11,
            uiStatusBarPanel12,
            uiStatusBarPanel13,
            uiStatusBarPanel14});
            this.statusStrip1.Size = new System.Drawing.Size(1090, 23);
            this.statusStrip1.TabIndex = 550;
            this.statusStrip1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // uiPanelManager1
            // 
            this.uiPanelManager1.ContainerControl = this;
            this.uiPanel0.Id = new System.Guid("18bdf251-58af-4d36-8144-ce12620b1b13");
            this.uiPanelManager1.Panels.Add(this.uiPanel0);
            this.uiPanel1.Id = new System.Guid("e27c2f94-7e0d-4267-8236-b883fcffc4b6");
            this.uiPanelManager1.Panels.Add(this.uiPanel1);
            // 
            // Design Time Panel Info:
            // 
            this.uiPanelManager1.BeginPanelInfo();
            this.uiPanelManager1.AddDockPanelInfo(new System.Guid("18bdf251-58af-4d36-8144-ce12620b1b13"), Janus.Windows.UI.Dock.PanelDockStyle.Left, new System.Drawing.Size(200, 104), true);
            this.uiPanelManager1.AddDockPanelInfo(new System.Guid("e27c2f94-7e0d-4267-8236-b883fcffc4b6"), Janus.Windows.UI.Dock.PanelDockStyle.Left, new System.Drawing.Size(200, 200), true);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("18bdf251-58af-4d36-8144-ce12620b1b13"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("e27c2f94-7e0d-4267-8236-b883fcffc4b6"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.uiPanelManager1.EndPanelInfo();
            // 
            // uiPanel0
            // 
            this.uiPanel0.Closed = true;
            this.uiPanel0.InnerContainer = this.uiPanel0Container;
            this.uiPanel0.Location = new System.Drawing.Point(3, 262);
            this.uiPanel0.Name = "uiPanel0";
            this.uiPanel0.Size = new System.Drawing.Size(200, 104);
            this.uiPanel0.TabIndex = 4;
            this.uiPanel0.Text = "Panel 0";
            // 
            // uiPanel0Container
            // 
            this.uiPanel0Container.Location = new System.Drawing.Point(0, 0);
            this.uiPanel0Container.Name = "uiPanel0Container";
            this.uiPanel0Container.Size = new System.Drawing.Size(200, 104);
            this.uiPanel0Container.TabIndex = 0;
            // 
            // uiPanel1
            // 
            this.uiPanel1.AutoHide = true;
            this.uiPanel1.InnerContainer = this.uiPanel1Container;
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(200, 200);
            this.uiPanel1.TabIndex = 4;
            this.uiPanel1.Text = "Panel 1";
            // 
            // uiPanel1Container
            // 
            this.uiPanel1Container.Location = new System.Drawing.Point(1, 23);
            this.uiPanel1Container.Name = "uiPanel1Container";
            this.uiPanel1Container.Size = new System.Drawing.Size(194, 176);
            this.uiPanel1Container.TabIndex = 0;
            // 
            // ribbonContextMenu1
            // 
            this.ribbonContextMenu1.Commands.AddRange(new Janus.Windows.Ribbon.CommandBase[] {
            this.dropDownCommand1});
            this.ribbonContextMenu1.Name = "ribbonContextMenu1";
            // 
            // dropDownCommand1
            // 
            this.dropDownCommand1.Key = "dropDownCommand1";
            this.dropDownCommand1.Name = "dropDownCommand1";
            this.dropDownCommand1.Text = "fsdf";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabThongtin);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(21, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1066, 363);
            this.tabControl1.TabIndex = 551;
            // 
            // tabThongtin
            // 
            this.tabThongtin.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabThongtin.Location = new System.Drawing.Point(4, 27);
            this.tabThongtin.Name = "tabThongtin";
            this.tabThongtin.Padding = new System.Windows.Forms.Padding(3);
            this.tabThongtin.Size = new System.Drawing.Size(1058, 332);
            this.tabThongtin.TabIndex = 0;
            this.tabThongtin.Text = "Danh sách nhân viên";
            this.tabThongtin.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtxtLogs);
            this.tabPage2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1058, 332);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtxtLogs
            // 
            this.rtxtLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLogs.Location = new System.Drawing.Point(3, 3);
            this.rtxtLogs.Name = "rtxtLogs";
            this.rtxtLogs.Size = new System.Drawing.Size(1052, 326);
            this.rtxtLogs.TabIndex = 1;
            this.rtxtLogs.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 392);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.uiPanel0);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanel0)).EndInit();
            this.uiPanel0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiPanel1)).EndInit();
            this.uiPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar statusStrip1;
        private Janus.Windows.UI.Dock.UIPanelManager uiPanelManager1;
        private Janus.Windows.UI.Dock.UIPanel uiPanel0;
        private Janus.Windows.UI.Dock.UIPanelInnerContainer uiPanel0Container;
        private Janus.Windows.UI.Dock.UIPanel uiPanel1;
        private Janus.Windows.UI.Dock.UIPanelInnerContainer uiPanel1Container;
        private Janus.Windows.Ribbon.RibbonContextMenu ribbonContextMenu1;
        private Janus.Windows.Ribbon.DropDownCommand dropDownCommand1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabThongtin;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtxtLogs;



    }
}