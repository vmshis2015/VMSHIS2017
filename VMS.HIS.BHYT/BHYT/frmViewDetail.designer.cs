namespace VMS.HIS.BHYT
{
    partial class frmViewDetail
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
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewDetail));
            Janus.Windows.GridEX.GridEXLayout gridThuoc_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout gridDVKT_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.ribbonStatusBar1 = new Janus.Windows.Ribbon.RibbonStatusBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.gridThuoc = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.gridDVKT = new Janus.Windows.GridEX.GridEX();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridThuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDVKT)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ImageSize = new System.Drawing.Size(16, 16);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 644);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Office2007CustomColor = System.Drawing.Color.Empty;
            this.ribbonStatusBar1.ShowToolTips = false;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1025, 23);
            // 
            // 
            // 
            this.ribbonStatusBar1.SuperTipComponent.AutoPopDelay = 2000;
            this.ribbonStatusBar1.SuperTipComponent.ImageList = null;
            this.ribbonStatusBar1.TabIndex = 0;
            this.ribbonStatusBar1.Text = "ribbonStatusBar1";
            this.ribbonStatusBar1.UseCompatibleTextRendering = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1025, 644);
            this.panel1.TabIndex = 1;
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
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1025, 644);
            this.splitContainer1.SplitterDistance = 86;
            this.splitContainer1.TabIndex = 0;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1025, 86);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "XML1 (Thông tin người bệnh)";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // grdList
            // 
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.None;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 19);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1019, 64);
            this.grdList.TabIndex = 2;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.grdList.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdList_CellUpdated);
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
            this.splitContainer2.Size = new System.Drawing.Size(1025, 554);
            this.splitContainer2.SplitterDistance = 279;
            this.splitContainer2.TabIndex = 0;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.gridThuoc);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1025, 279);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "XML2 (Thông tin thuốc và chế phẩm máu)";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // gridThuoc
            // 
            this.gridThuoc.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            gridThuoc_DesignTimeLayout.LayoutString = resources.GetString("gridThuoc_DesignTimeLayout.LayoutString");
            this.gridThuoc.DesignTimeLayout = gridThuoc_DesignTimeLayout;
            this.gridThuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridThuoc.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.gridThuoc.GroupByBoxVisible = false;
            this.gridThuoc.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.gridThuoc.Location = new System.Drawing.Point(3, 19);
            this.gridThuoc.Name = "gridThuoc";
            this.gridThuoc.RecordNavigator = true;
            this.gridThuoc.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridThuoc.Size = new System.Drawing.Size(1019, 257);
            this.gridThuoc.TabIndex = 3;
            this.gridThuoc.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridThuoc.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.gridThuoc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.gridThuoc.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(this.gridThuoc_CellUpdated);
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.gridDVKT);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(1025, 271);
            this.uiGroupBox3.TabIndex = 1;
            this.uiGroupBox3.Text = "XML3 (Thông tin dịch vụ kỹ thuật và VTYT tiêu hao)";
            this.uiGroupBox3.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // gridDVKT
            // 
            this.gridDVKT.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            gridDVKT_DesignTimeLayout.LayoutString = resources.GetString("gridDVKT_DesignTimeLayout.LayoutString");
            this.gridDVKT.DesignTimeLayout = gridDVKT_DesignTimeLayout;
            this.gridDVKT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDVKT.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.gridDVKT.GroupByBoxVisible = false;
            this.gridDVKT.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.gridDVKT.Location = new System.Drawing.Point(3, 19);
            this.gridDVKT.Name = "gridDVKT";
            this.gridDVKT.RecordNavigator = true;
            this.gridDVKT.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridDVKT.Size = new System.Drawing.Size(1019, 249);
            this.gridDVKT.TabIndex = 3;
            this.gridDVKT.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.gridDVKT.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.gridDVKT.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            this.gridDVKT.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(this.gridDVKT_CellUpdated);
            // 
            // frmViewDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 667);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewDetail";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "THÔNG TIN XML";
            this.Load += new System.EventHandler(this.frmViewDetail_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridThuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDVKT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.GridEX.GridEX gridThuoc;
        private Janus.Windows.GridEX.GridEX gridDVKT;
    }
}