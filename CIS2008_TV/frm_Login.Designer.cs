namespace VNSCore
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem4 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem5 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.txtUserName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtPassWord = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdLogin = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.chkRemember = new Janus.Windows.EditControls.UICheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lnkHelp = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdSettings = new Janus.Windows.EditControls.UIButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cbogiaodien = new Janus.Windows.EditControls.UIComboBox();
            this.cboKhoaKCB = new Janus.Windows.EditControls.UIComboBox();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.White;
            this.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(125, 132);
            this.txtUserName.MaxLength = 100;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(292, 21);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            this.txtUserName.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassWord.Location = new System.Drawing.Point(125, 159);
            this.txtPassWord.MaxLength = 100;
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(292, 21);
            this.txtPassWord.TabIndex = 2;
            this.txtPassWord.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            this.txtPassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassWord_KeyDown);
            // 
            // cmdLogin
            // 
            this.cmdLogin.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLogin.Location = new System.Drawing.Point(152, 218);
            this.cmdLogin.Name = "cmdLogin";
            this.cmdLogin.Size = new System.Drawing.Size(101, 34);
            this.cmdLogin.TabIndex = 3;
            this.cmdLogin.Text = "Đăng nhập";
            this.cmdLogin.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdLogin.Click += new System.EventHandler(this.cmdLogin_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(268, 218);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(101, 34);
            this.cmdExit.TabIndex = 4;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // chkRemember
            // 
            this.chkRemember.BackColor = System.Drawing.Color.Transparent;
            this.chkRemember.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRemember.Location = new System.Drawing.Point(126, 223);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(286, 23);
            this.chkRemember.TabIndex = 3;
            this.chkRemember.TabStop = false;
            this.chkRemember.Text = "Ghi lại thông tin cho lần đăng nhập kế tiếp?";
            this.chkRemember.Visible = false;
            this.chkRemember.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            this.chkRemember.CheckedChanged += new System.EventHandler(this.chkRemember_CheckedChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(114, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(303, 32);
            this.label1.TabIndex = 7;
            this.label1.Text = "ĐĂNG NHẬP HỆ THỐNG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(4, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(104, 84);
            this.panel1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "Tên đăng nhập:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "Mật khẩu:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(111, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(301, 26);
            this.label4.TabIndex = 11;
            this.label4.Text = "Mời bạn nhập Tên đăng nhập+mật khẩu";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.SystemColors.HotTrack;
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(0, 84);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(473, 22);
            this.vbLine1.TabIndex = 12;
            this.vbLine1.YourText = "Thông tin đăng nhập";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 267);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(445, 16);
            this.progressBar1.TabIndex = 14;
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(0, 246);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(445, 21);
            this.lblMsg.TabIndex = 15;
            this.lblMsg.Text = "Msg";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 23);
            this.label5.TabIndex = 16;
            this.label5.Text = "Chọn giao diện:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkHelp
            // 
            this.lnkHelp.AutoSize = true;
            this.lnkHelp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkHelp.Location = new System.Drawing.Point(393, 252);
            this.lnkHelp.Name = "lnkHelp";
            this.lnkHelp.Size = new System.Drawing.Size(53, 15);
            this.lnkHelp.TabIndex = 17;
            this.lnkHelp.TabStop = true;
            this.lnkHelp.Text = "Trợ giúp";
            this.lnkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHelp_LinkClicked);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // cmdSettings
            // 
            this.cmdSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSettings.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSettings.Image = ((System.Drawing.Image)(resources.GetObject("cmdSettings.Image")));
            this.cmdSettings.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Center;
            this.cmdSettings.ImageSize = new System.Drawing.Size(23, 23);
            this.cmdSettings.Location = new System.Drawing.Point(415, 0);
            this.cmdSettings.Name = "cmdSettings";
            this.cmdSettings.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdSettings.Office2007CustomColor = System.Drawing.Color.White;
            this.cmdSettings.Size = new System.Drawing.Size(35, 28);
            this.cmdSettings.TabIndex = 546;
            this.cmdSettings.TabStop = false;
            this.toolTip1.SetToolTip(this.cmdSettings, "Cấu hình ứng dụng");
            this.cmdSettings.ToolTipText = "Settings";
            this.cmdSettings.Visible = false;
            this.cmdSettings.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007;
            this.cmdSettings.Click += new System.EventHandler(this.cmdSettings_Click_1);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(27, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 23);
            this.label6.TabIndex = 19;
            this.label6.Text = "Khoa làm việc:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbogiaodien
            // 
            this.cbogiaodien.BackColor = System.Drawing.SystemColors.Menu;
            this.cbogiaodien.BorderStyle = Janus.Windows.UI.BorderStyle.Flat;
            this.cbogiaodien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Giao diện menu truyền thống";
            uiComboBoxItem1.Value = 1;
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "Giao diện chức năng Outlook";
            uiComboBoxItem2.Value = 0;
            this.cbogiaodien.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2});
            this.cbogiaodien.Location = new System.Drawing.Point(125, 105);
            this.cbogiaodien.Name = "cbogiaodien";
            this.cbogiaodien.Size = new System.Drawing.Size(292, 21);
            this.cbogiaodien.TabIndex = 610;
            this.cbogiaodien.TabStop = false;
            this.cbogiaodien.Text = "Chọn giao diện hiển thị";
            this.cbogiaodien.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // cboKhoaKCB
            // 
            this.cboKhoaKCB.BackColor = System.Drawing.SystemColors.Menu;
            this.cboKhoaKCB.BorderStyle = Janus.Windows.UI.BorderStyle.Flat;
            this.cboKhoaKCB.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "Nữ";
            uiComboBoxItem3.Value = 1;
            uiComboBoxItem4.FormatStyle.Alpha = 0;
            uiComboBoxItem4.IsSeparator = false;
            uiComboBoxItem4.Text = "Nam";
            uiComboBoxItem4.Value = 0;
            uiComboBoxItem5.FormatStyle.Alpha = 0;
            uiComboBoxItem5.IsSeparator = false;
            uiComboBoxItem5.Text = "Khác";
            uiComboBoxItem5.Value = 2;
            this.cboKhoaKCB.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem3,
            uiComboBoxItem4,
            uiComboBoxItem5});
            this.cboKhoaKCB.Location = new System.Drawing.Point(125, 186);
            this.cboKhoaKCB.Name = "cboKhoaKCB";
            this.cboKhoaKCB.Size = new System.Drawing.Size(292, 21);
            this.cboKhoaKCB.TabIndex = 611;
            this.cboKhoaKCB.TabStop = false;
            this.cboKhoaKCB.Text = "Chọn khoa làm việc";
            this.cboKhoaKCB.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003;
            // 
            // FrmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(445, 283);
            this.ControlBox = false;
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdLogin);
            this.Controls.Add(this.cboKhoaKCB);
            this.Controls.Add(this.cbogiaodien);
            this.Controls.Add(this.cmdSettings);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lnkHelp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.vbLine1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkRemember);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Người dùng";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frm_Login_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Login_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.GridEX.EditControls.EditBox txtUserName;
        private Janus.Windows.GridEX.EditControls.EditBox txtPassWord;
        private Janus.Windows.EditControls.UIButton cmdLogin;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UICheckBox chkRemember;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel lnkHelp;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.EditControls.UIButton cmdSettings;
        private Janus.Windows.EditControls.UIComboBox cbogiaodien;
        private Janus.Windows.EditControls.UIComboBox cboKhoaKCB;


    }
}