using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.HIS.NGHIEPVU;
using VNS.Libs;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_phanbuonggiuong : Form
    {
        public bool BCancel = true;
        private DataTable _mDtDataRoom = new DataTable();
        private DataTable _mDtDatabed = new DataTable();
        private KcbLuotkham _objPatientExam;
        public NoitruPhanbuonggiuong ObjPhanbuonggiuong;
        public DataTable PDanhSachPhanBuongGiuong = new DataTable();

        public frm_phanbuonggiuong()
        {
            InitializeComponent();
            InitEvents();
            dtNgayChuyen.Value = globalVariables.SysDate;
            lblGiaBG.Visible =
                txtGia.Visible =
                    cboGia.Visible =
                        THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_APGIABUONGGIUONG_THEODANHMUCGIA", "0", false) =="0";
        }

        private void InitEvents()
        {
            Load += frm_phanbuonggiuong_Load;
            KeyDown += frm_phanbuonggiuong_KeyDown;
            cmdExit.Click += cmdExit_Click;
            cmdSave.Click += cmdSave_Click;
            dtNgayChuyen.TextChanged += dtNgayChuyen_TextChanged;
            dtNgayChuyen.ValueChanged += dtNgayChuyen_ValueChanged;
            txtMaLanKham.KeyDown += txtMaLanKham_KeyDown;
            cmdChonBNmoi.Click += cmdChonBNmoi_Click;
            txtGia._OnSelectionChanged += txtGia__OnSelectionChanged;

            txtRoom_code._OnEnterMe += txtRoom_code__OnEnterMe;
            grdBuong.SelectionChanged += grdBuong_SelectionChanged;
            grdBuong.KeyDown += grdBuong_KeyDown;
            grdGiuong.KeyDown += grdGiuong_KeyDown;
            grdGiuong.SelectionChanged += grdGiuong_SelectionChanged;
            grdGiuong.MouseDoubleClick += grdGiuong_MouseDoubleClick;
            txtBedCode._OnEnterMe += txtBedCode__OnEnterMe;
        }

        private void grdGiuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdGiuong)) return;
            ChonGiuong();
        }

        private void ChonGiuong()
        {
            string idGiuong = Utility.sDbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong), -1);
            txtBedCode.SetId(idGiuong);
        }

        private void txtBedCode__OnEnterMe()
        {
            Utility.GotoNewRowJanus(grdGiuong, NoitruDmucGiuongbenh.Columns.IdGiuong,
                Utility.sDbnull(txtBedCode.MyID, ""));
        }

        private void txtRoom_code__OnEnterMe()
        {
            Utility.GotoNewRowJanus(grdBuong, NoitruDmucBuong.Columns.IdBuong, Utility.sDbnull(txtRoom_code.MyID, ""));
        }

        private void grdGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdGiuong_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }

        private void grdGiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.isValidGrid(grdGiuong))
            {
                cmdSave_Click(cmdSave, new EventArgs());
            }
        }

        private void grdBuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBedCode.Focus();
                txtRoom_code.Text = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.TenBuong));
            }
        }

        private void grdBuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdBuong)) return;
            ChonBuong();
        }

        private void ChonBuong()
        {
            string idBuong = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong), -1);
            txtRoom_code.SetId(idBuong);
            _mDtDatabed = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(Utility.Int32Dbnull(txtDepartment_ID.Text),
                Utility.Int32Dbnull(idBuong));
            Utility.SetDataSourceForDataGridEx_Basic(grdGiuong, _mDtDatabed, true, true, "1=1",
                "isFull asc,dang_nam ASC,ten_giuong");
            string oldBed = txtBedCode.MyCode;
            txtBedCode.Init(_mDtDatabed,
                new List<string>
                {
                    NoitruDmucGiuongbenh.Columns.IdGiuong,
                    NoitruDmucGiuongbenh.Columns.MaGiuong,
                    NoitruDmucGiuongbenh.Columns.TenGiuong
                });
            txtBedCode.SetCode(oldBed);
            if (grdGiuong.DataSource != null)
            {
                grdGiuong.MoveFirst();
            }
            if (txtBedCode.MyCode == "-1")
            {
                string idGiuong = Utility.sDbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong), -1);
                txtBedCode.SetId(idGiuong);
            }
        }

        private void txtGia__OnSelectionChanged()
        {
            cboGia.Text = txtGia.MyText;
        }

        private void cmdChonBNmoi_Click(object sender, EventArgs e)
        {
            ClearControl();
            txtMaLanKham.Focus();
        }

        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaLanKham.Text) != "")
            {
                ObjPhanbuonggiuong = null;
                string patientCode = Utility.AutoFullPatientCode(txtMaLanKham.Text);
                ClearControl();
                txtMaLanKham.Text = patientCode;
                BindData();
            }
        }

        private void ClearControl()
        {
            foreach (Control ctrl in grpThongTinBN.Controls)
            {
                if (ctrl is EditBox)
                {
                    ((EditBox) ctrl).Clear();
                }
            }
        }


        private void dtNgayChuyen_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGio.Text))
            {
                txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
            }
            if (string.IsNullOrEmpty(txtPhut.Text))
            {
                txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
            }
        }

        private void dtNgayChuyen_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGio.Text))
            {
                txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
            }
            if (string.IsNullOrEmpty(txtPhut.Text))
            {
                txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
            }
        }

        /// <summary>
        ///     hàm thực hiện thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            //if (!InValBenhNhan.ExistBN(objPhanbuonggiuong.MaLuotkham)) return;
            if (!IsValidData()) return;
            PerformAction();
        }

        private void PerformAction()
        {
            //if (Utility.AcceptQuestion("Bạn có muốn chuyển bệnh nhân vào Buồng và giường đang chọn không", "Thông báo",
            //    true))
            //{
            var ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                Utility.Int32Dbnull(txtPhut.Text), 00);
            _objPatientExam =
                new Select().From<KcbLuotkham>()
                    .Where(KcbLuotkham.Columns.MaLuotkham)
                    .IsEqualTo(ObjPhanbuonggiuong.MaLuotkham)
                    .And(KcbLuotkham.Columns.IdBenhnhan)
                    .IsEqualTo(ObjPhanbuonggiuong.IdBenhnhan)
                    .ExecuteSingle<KcbLuotkham>();
            if (_objPatientExam != null)
            {
                ObjPhanbuonggiuong.Id = Utility.Int32Dbnull(txtPatientDept_ID.Text);
                ObjPhanbuonggiuong.NgayVaokhoa = ngaychuyenkhoa;
                ObjPhanbuonggiuong.IdBuong = Utility.Int16Dbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong));
                ObjPhanbuonggiuong.IdGiuong = Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong));
                ObjPhanbuonggiuong.IsGhepGiuong = Utility.ByteDbnull(chkGhepgiuong.Checked);
                if(chkGhepgiuong.Checked)  ObjPhanbuonggiuong.SoLuongGhep = Utility.Int16Dbnull(txtsoluongghep.Text,0);
                ObjPhanbuonggiuong.IdGia = Utility.Int32Dbnull(txtGia.MyID, -1);
                ActionResult actionResult = new noitru_nhapvien().PhanGiuongDieuTri(ObjPhanbuonggiuong,
                    _objPatientExam,
                    ngaychuyenkhoa,
                    Utility.Int16Dbnull(
                        grdBuong.GetValue(
                            NoitruDmucBuong.Columns.IdBuong)),
                    Utility.Int16Dbnull(
                        grdGiuong.GetValue(
                            NoitruDmucGiuongbenh.Columns.IdGiuong)));
                switch (actionResult)
                {
                    case ActionResult.Success:
                        txtPatientDept_ID.Text = Utility.sDbnull(ObjPhanbuonggiuong.Id);
                        Utility.SetMsg(lblMsg, "Bạn chuyển bênh nhân vào giường thành công", true);
                        ProcessChuyenKhoa();
                        BCancel = false;
                        Close();

                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình phân buồng giường", "Thông báo", MessageBoxIcon.Error);
                        break;
                }
            }
            //  }
        }

        private bool IsValidData()
        {
            KcbLuotkham objKcbLuotkham = Utility.getKcbLuotkham(ObjPhanbuonggiuong.IdBenhnhan,
                ObjPhanbuonggiuong.MaLuotkham);
            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru < 1)
            {
                Utility.ShowMsg("Bệnh nhân này chưa nhập viện nên không được phép phân buồng giường", "Thông báo",
                    MessageBoxIcon.Warning);
                return false;
            }


            if (!Utility.isValidGrid(grdBuong))
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn Buồng cần chuyển", true);
                txtRoom_code.Focus();
                txtRoom_code.SelectAll();
                return false;
            }


            if (!Utility.isValidGrid(grdGiuong))
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn giường cần chuyển", true);
                txtBedCode.Focus();
                txtBedCode.SelectAll();
                return false;
            }
            if (lblGiaBG.Visible && Utility.Int32Dbnull(txtGia.MyID, -1) == -1)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn giá buồng giường", true);
                txtGia.Focus();
                txtGia.SelectAll();
                return false;
            }
            DataTable dt = new noitru_nhapvien().NoitruKiemtraBuongGiuong(ObjPhanbuonggiuong.IdKhoanoitru,
                Utility.Int16Dbnull(grdBuong.GetValue(NoitruDmucGiuongbenh.Columns.IdBuong)),
                Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)));

            if (dt != null && dt.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg,
                    string.Format("Giường này đang được nằm bởi bệnh nhân: {0}. Mời bạn chọn giường khác",
                        Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan])), true);
                txtBedCode.Focus();
                txtBedCode.SelectAll();
                return false;
            }
            return true;
        }

        /// <summary>
        ///     hàm thực hiện việc xử lý thông tin chuyển khoa
        /// </summary>
        private void ProcessChuyenKhoa()
        {
            DataRow query = (from khoa in PDanhSachPhanBuongGiuong.AsEnumerable()
                where
                    Utility.Int32Dbnull(khoa[NoitruPhanbuonggiuong.Columns.Id]) ==
                    Utility.Int32Dbnull(Utility.Int32Dbnull(txtPatientDept_ID.Text))
                select khoa).FirstOrDefault();
            if (query != null)
            {
                NoitruDmucBuong objRoom =
                    NoitruDmucBuong.FetchByID(Utility.Int32Dbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong)));
                if (objRoom != null)
                {
                    query[NoitruDmucBuong.Columns.IdBuong] = Utility.Int32Dbnull(objRoom.IdBuong, -1);
                    query[NoitruDmucBuong.Columns.TenBuong] = Utility.sDbnull(objRoom.TenBuong);
                }
                NoitruDmucGiuongbenh objBed =
                    NoitruDmucGiuongbenh.FetchByID(
                        Utility.Int32Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)));
                if (objBed != null)
                {
                    query[NoitruDmucGiuongbenh.Columns.IdGiuong] = Utility.Int32Dbnull(objBed.IdGiuong, -1);
                    query[NoitruDmucGiuongbenh.Columns.TenGiuong] = Utility.sDbnull(objBed.TenGiuong);
                }
                query[NoitruPhanbuonggiuong.Columns.Id] = Utility.sDbnull(txtPatientDept_ID.Text);
                query.AcceptChanges();
            }
        }

        private void BindData()
        {
            SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaLanKham.Text);
            if (sqlQuery.GetRecordCount() > 0)
            {
                _objPatientExam = sqlQuery.ExecuteSingle<KcbLuotkham>();
                if (_objPatientExam != null)
                {
                    if (ObjPhanbuonggiuong == null)
                        ObjPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(_objPatientExam.IdRavien, 0));
                    txtMaLanKham.Text = Utility.sDbnull(_objPatientExam.MaLuotkham);
                    txtSoBHYT.Text = string.Format("{0}-{1}{2}", Utility.sDbnull(_objPatientExam.MatheBhyt),
                        Utility.sDbnull(_objPatientExam.MaNoicapBhyt), Utility.sDbnull(_objPatientExam.MaKcbbd));
                    txtphantramhuong.Text = Utility.sDbnull(_objPatientExam.PtramBhyt);
                    DmucKhoaphong objLDepartment = DmucKhoaphong.FetchByID(_objPatientExam.IdKhoanoitru);
                    if (objLDepartment != null)
                    {
                        txtDepartment_ID.Text = Utility.sDbnull(objLDepartment.IdKhoaphong);
                        txtDepartmentName.Tag = Utility.sDbnull(objLDepartment.IdKhoaphong);
                        txtDepartmentName.Text = Utility.sDbnull(objLDepartment.TenKhoaphong);
                        txtKhoadieutri.Text = txtDepartmentName.Text;
                    }
                    KcbDanhsachBenhnhan objPatientInfo = KcbDanhsachBenhnhan.FetchByID(_objPatientExam.IdBenhnhan);
                    if (objPatientInfo != null)
                    {
                        txtPatient_Name.Text = Utility.sDbnull(objPatientInfo.TenBenhnhan);
                        txtPatient_ID.Text = Utility.sDbnull(_objPatientExam.IdBenhnhan);
                        txtNamSinh.Text = Utility.sDbnull(objPatientInfo.NamSinh);
                        txtTuoi.Text = Utility.sDbnull(DateTime.Now.Year - objPatientInfo.NamSinh);
                        txtPatientSex.Text = objPatientInfo.GioiTinh;
                            // Utility.Int32Dbnull(objPatientInfo.) == 0 ? "Nam" : "Nữ";
                    }
                    if (ObjPhanbuonggiuong != null)
                    {
                        txtPatientDept_ID.Text = Utility.sDbnull(ObjPhanbuonggiuong.Id);
                        txtsoluongghep.Text = Utility.sDbnull(ObjPhanbuonggiuong.SoLuongGhep);
                        chkGhepgiuong.Checked = Utility.Byte2Bool(ObjPhanbuonggiuong.IsGhepGiuong);
                    }
                    DataTable dtGia = new dmucgiagiuong_busrule().dsGetList("-1").Tables[0];
                    dtGia.DefaultView.Sort = NoitruGiabuonggiuong.Columns.SttHthi + "," +
                                             NoitruGiabuonggiuong.Columns.TenGia;
                    txtGia.Init(dtGia,
                        new List<string>
                        {
                            NoitruGiabuonggiuong.Columns.IdGia,
                            NoitruGiabuonggiuong.Columns.MaGia,
                            NoitruGiabuonggiuong.Columns.TenGia
                        });
                    cboGia.DataSource = dtGia;
                    cboGia.DataMember = NoitruGiabuonggiuong.Columns.IdGia;
                    cboGia.ValueMember = NoitruGiabuonggiuong.Columns.IdGia;
                    cboGia.DisplayMember = NoitruGiabuonggiuong.Columns.TenGia;
                    _mDtDataRoom = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtDepartment_ID.Text));

                    Utility.SetDataSourceForDataGridEx_Basic(grdBuong, _mDtDataRoom, true, true, "1=1",
                        "sluong_giuong_trong desc,ten_buong");
                    txtRoom_code.Init(_mDtDataRoom,
                        new List<string>
                        {
                            NoitruDmucBuong.Columns.IdBuong,
                            NoitruDmucBuong.Columns.MaBuong,
                            NoitruDmucBuong.Columns.TenBuong
                        });
                    if (grdBuong.DataSource != null)
                    {
                        grdBuong.MoveFirst();
                    }
                }
                else
                {
                    string tempt = txtMaLanKham.Text;
                    ClearControl();
                    if (_mDtDataRoom != null) _mDtDataRoom.Clear();
                    if (_mDtDatabed != null) if (_mDtDataRoom != null) _mDtDataRoom.Clear();
                    txtMaLanKham.Text = tempt;
                    txtMaLanKham.SelectAll();
                    txtMaLanKham.Focus();
                }
            }
        }


        private void frm_phanbuonggiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2) txtRoom_code.Focus();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.F2)
            {
                txtMaLanKham.Focus();
                txtMaLanKham.SelectAll();
            }
            if (e.KeyCode == Keys.F3)
            {
                ShowPatientList();
            }
            if (e.KeyCode == Keys.F5)
            {
                grdBuong_SelectionChanged(grdBuong, new EventArgs());
            }
        }

        private void ShowPatientList()
        {
            var frm = new frm_TimKiem_BN();
            frm.SearchByDate = false;
            frm.txtPatientCode.Text = txtMaLanKham.Text;
            frm.ShowDialog();
            if (!frm.m_blnCancel)
            {
                txtMaLanKham.Text = Utility.sDbnull(frm.MaLuotkham);
                txtMaLanKham_KeyDown(txtMaLanKham, new KeyEventArgs(Keys.Enter));
            }
        }


        private void frm_phanbuonggiuong_Load(object sender, EventArgs e)
        {
            if (ObjPhanbuonggiuong != null)
            {
                txtMaLanKham.Text = ObjPhanbuonggiuong.MaLuotkham;
                txtPatientDept_ID.Text = Utility.sDbnull(ObjPhanbuonggiuong.Id);
                txtDepartment_ID.Text = Utility.sDbnull(ObjPhanbuonggiuong.IdKhoanoitru);
                DmucKhoaphong objDepartment = DmucKhoaphong.FetchByID(Utility.Int32Dbnull(txtDepartment_ID.Text));
                if (objDepartment != null)
                {
                    txtDepartmentName.Text = Utility.sDbnull(objDepartment.TenKhoaphong);
                    txtDepartmentName.Tag = Utility.sDbnull(objDepartment.IdKhoaphong);
                }
                dtNgayChuyen.Value = globalVariables.SysDate;
                txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
                txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
                BindData();
                txtRoom_code.Focus();
                txtRoom_code.SelectAll();
            }
        }

        private void chkGhepgiuong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGhepgiuong.Checked)
            {
                lblsoluongghep.Visible = true;
                txtsoluongghep.Visible = true;
            }
            else
            {
                lblsoluongghep.Visible = false;
                txtsoluongghep.Visible = false;
            }
        }
    }
}