namespace VMS.HIS.KSK.Forms
{
    partial class FrmDanhMucKhachHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDanhMucKhachHang));
            Janus.Windows.GridEX.GridEXLayout grdkhachhang_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.sysColor = new System.Windows.Forms.ToolStrip();
            this.cmdNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdThoat = new System.Windows.Forms.ToolStripButton();
            this.grpKhoaPhong = new Janus.Windows.EditControls.UIGroupBox();
            this.grdkhachhang = new Janus.Windows.GridEX.GridEX();
            this.sysColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpKhoaPhong)).BeginInit();
            this.grpKhoaPhong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdkhachhang)).BeginInit();
            this.SuspendLayout();
            // 
            // sysColor
            // 
            this.sysColor.BackColor = System.Drawing.SystemColors.Control;
            this.sysColor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdNew,
            this.toolStripSeparator1,
            this.cmdEdit,
            this.toolStripSeparator2,
            this.cmdDelete,
            this.toolStripSeparator4,
            this.cmdThoat});
            this.sysColor.Location = new System.Drawing.Point(0, 0);
            this.sysColor.Name = "sysColor";
            this.sysColor.Size = new System.Drawing.Size(1172, 39);
            this.sysColor.TabIndex = 5;
            this.sysColor.Text = "toolStrip1";
            // 
            // cmdNew
            // 
            this.cmdNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmdNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdNew.Image")));
            this.cmdNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(144, 36);
            this.cmdNew.Text = "Thêm mới (Ctrl+N)";
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
            this.cmdEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(107, 36);
            this.cmdEdit.Text = "Sửa(Ctrl+E)";
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(107, 36);
            this.cmdDelete.Text = "Xoá(Ctrl+D)";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdThoat
            // 
            this.cmdThoat.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdThoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(104, 36);
            this.cmdThoat.Text = "Thoát(Esc)";
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // grpKhoaPhong
            // 
            this.grpKhoaPhong.Controls.Add(this.grdkhachhang);
            this.grpKhoaPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpKhoaPhong.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpKhoaPhong.Location = new System.Drawing.Point(0, 39);
            this.grpKhoaPhong.Name = "grpKhoaPhong";
            this.grpKhoaPhong.Size = new System.Drawing.Size(1172, 578);
            this.grpKhoaPhong.TabIndex = 30;
            this.grpKhoaPhong.Text = "&Thông tin khách hàng";
            // 
            // grdkhachhang
            // 
            this.grdkhachhang.AlternatingColors = true;
            this.grdkhachhang.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin khoa phòng</FilterRowInfoText></LocalizableData>";
            grdkhachhang_DesignTimeLayout.LayoutString = resources.GetString("grdkhachhang_DesignTimeLayout.LayoutString");
            this.grdkhachhang.DesignTimeLayout = grdkhachhang_DesignTimeLayout;
            this.grdkhachhang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdkhachhang.DynamicFiltering = true;
            this.grdkhachhang.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdkhachhang.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdkhachhang.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdkhachhang.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdkhachhang.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grdkhachhang.GroupByBoxVisible = false;
            this.grdkhachhang.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdkhachhang.Location = new System.Drawing.Point(3, 18);
            this.grdkhachhang.Name = "grdkhachhang";
            this.grdkhachhang.RecordNavigator = true;
            this.grdkhachhang.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdkhachhang.Size = new System.Drawing.Size(1166, 557);
            this.grdkhachhang.TabIndex = 31;
            this.grdkhachhang.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // FrmDanhMucKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 617);
            this.Controls.Add(this.grpKhoaPhong);
            this.Controls.Add(this.sysColor);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmDanhMucKhachHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh mục khách hàng";
            this.Load += new System.EventHandler(this.FrmDanhMucKhachHang_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDanhMucKhachHang_KeyDown);
            this.sysColor.ResumeLayout(false);
            this.sysColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpKhoaPhong)).EndInit();
            this.grpKhoaPhong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdkhachhang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip sysColor;
        private System.Windows.Forms.ToolStripButton cmdNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton cmdThoat;
        private Janus.Windows.EditControls.UIGroupBox grpKhoaPhong;
        private Janus.Windows.GridEX.GridEX grdkhachhang;

    }
}