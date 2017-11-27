namespace VNS.HIS.UI.Forms.QuanlyMauKiemnghiem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBanGiaoMauKiemNghiem));
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.uiButton1 = new Janus.Windows.EditControls.UIButton();
            this.uiButton2 = new Janus.Windows.EditControls.UIButton();
            this.txtPatientName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDichvuKn = new VNS.HIS.UCs.AutoCompleteTextbox();
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
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 474);
            this.uiStatusBar1.Name = "uiStatusBar1";
            this.uiStatusBar1.Size = new System.Drawing.Size(748, 31);
            this.uiStatusBar1.TabIndex = 0;
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
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(748, 474);
            this.splitContainer1.SplitterDistance = 88;
            this.splitContainer1.TabIndex = 1;
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
            this.splitContainer2.Panel1.Controls.Add(this.uiGroupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.uiGroupBox3);
            this.splitContainer2.Size = new System.Drawing.Size(748, 382);
            this.splitContainer2.SplitterDistance = 305;
            this.splitContainer2.TabIndex = 0;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.txtDichvuKn);
            this.uiGroupBox1.Controls.Add(this.txtPatientName);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(748, 88);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin khách hàng";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(748, 305);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Thông tin bàn giao mẫu";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.uiButton2);
            this.uiGroupBox3.Controls.Add(this.uiButton1);
            this.uiGroupBox3.Controls.Add(this.cmdTimKiem);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(748, 73);
            this.uiGroupBox3.TabIndex = 1;
            this.uiGroupBox3.Text = "Chức năng";
            this.uiGroupBox3.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTimKiem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("cmdTimKiem.Image")));
            this.cmdTimKiem.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdTimKiem.Location = new System.Drawing.Point(188, 22);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(115, 40);
            this.cmdTimKiem.TabIndex = 7;
            this.cmdTimKiem.Text = "Bàn giao";
            this.cmdTimKiem.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // uiButton1
            // 
            this.uiButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiButton1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiButton1.Image = ((System.Drawing.Image)(resources.GetObject("uiButton1.Image")));
            this.uiButton1.ImageSize = new System.Drawing.Size(32, 32);
            this.uiButton1.Location = new System.Drawing.Point(450, 22);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(115, 40);
            this.uiButton1.TabIndex = 8;
            this.uiButton1.Text = "Thoát";
            this.uiButton1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // uiButton2
            // 
            this.uiButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiButton2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiButton2.Image = ((System.Drawing.Image)(resources.GetObject("uiButton2.Image")));
            this.uiButton2.ImageSize = new System.Drawing.Size(32, 32);
            this.uiButton2.Location = new System.Drawing.Point(319, 22);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(115, 40);
            this.uiButton2.TabIndex = 9;
            this.uiButton2.Text = "In mẫu";
            this.uiButton2.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            // 
            // txtPatientName
            // 
            this.txtPatientName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatientName.BackColor = System.Drawing.Color.White;
            this.txtPatientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.Location = new System.Drawing.Point(119, 22);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(609, 23);
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
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(15, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 603;
            this.label5.Text = "Dịch vụ KN:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.txtDichvuKn.SelectedIndex = -1;
            this.txtDichvuKn.Size = new System.Drawing.Size(609, 21);
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
            // FrmBanGiaoMauKiemNghiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 505);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.uiStatusBar1);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBanGiaoMauKiemNghiem";
            this.ShowIcon = false;
            this.Text = "Bàn giao mẫu kiểm nghiệm";
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
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.EditControls.UIButton uiButton2;
        private Janus.Windows.EditControls.UIButton uiButton1;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private Janus.Windows.GridEX.EditControls.EditBox txtPatientName;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private UCs.AutoCompleteTextbox txtDichvuKn;

    }
}