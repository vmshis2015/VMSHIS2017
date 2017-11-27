using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Janus.Windows.UI.StatusBar;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.Libs;
using VNS.Libs.AppType;
using VNS.Properties;

namespace VMS.HIS.KN.NGOAITRU
{
    public partial class frm_Dangky_Kiemnghiem : Form
    {
        public delegate void OnActionSuccess();

        public delegate void OnAssign();

        private readonly string Args = "ALL";

        private readonly KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        private readonly AutoCompleteStringCollection namesCollectionThanhPho = new AutoCompleteStringCollection();
        private readonly string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfig.txt";

        private readonly string strSaveandprintPath1 = Application.StartupPath +
                                                       @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";

        private bool AutoLoad = false;

        private string MA_DTUONG = "DV";
        private decimal PtramBhytCu;
        private decimal PtramBhytGocCu;
        private string SoBHYT = "";
        private string TrongGio = "";
        private DMUC_CHUNG _DMUC_CHUNG = new DMUC_CHUNG();
        private Int16 _IdDoituongKcb = 1;
        private byte _IdLoaidoituongKcb = 1;
        private KCB_QMS _KCB_QMS = new KCB_QMS();
        private string _MaDoituongKcb = "DV";
        private string _TenDoituongKcb = "Dịch vụ";
        private bool b_HasLoaded;
        private bool b_HasSecondScreen;
        private bool b_NhapNamSinh;

        public GridEX grdList;
        private bool hasjustpressBACKKey;
        private bool isAutoFinding;
        private DataTable m_DC;
        public bool m_blnCancel;
        private bool m_blnHasJustInsert;
        private DataTable m_dtDoiTuong = new DataTable();
        public DataTable m_dtPatient = new DataTable();
        public action m_enAction = action.Insert;
        private string m_strDefaultLazerPrinterName = "";

        private string m_strMaluotkham = "";
            //Lưu giá trị patientcode khi cập nhật để người dùng ko được phép gõ Patient_code lung tung

        private DataTable mdt_DataQuyenhuyen;
        private DmucDoituongkcb objDoituongKCB;
        private KnDangkyXn objDangkyXn;

        public frm_Dangky_Kiemnghiem(string Args)
        {
            InitializeComponent();
            this.Args = Args;
            dtCreateDate.Value = globalVariables.SysDate;

            InitEvents();
            CauHinhKCB();
        }

        public event OnActionSuccess _OnActionSuccess;
        public event OnAssign _OnAssign;

        private void InitEvents()
        {
            FormClosing += frm_Dangky_Kiemnghiem_FormClosing;
            Load += frm_Dangky_Kiemnghiem_Load;
            KeyDown += frm_Dangky_Kiemnghiem_KeyDown;
            txtMaBN.KeyDown += txtMaBN_KeyDown;
            txtMaLankham.KeyDown += txtMaLankham_KeyDown;
            txtTenKH.TextChanged += txtTEN_BN_TextChanged;
            cmdConfig.Click += cmdConfig_Click;
            chkTudongthemmoi.CheckedChanged += chkTudongthemmoi_CheckedChanged;
            cboDoituongKCB.SelectedIndexChanged += cboDoituongKCB_SelectedIndexChanged;
            cmdThemmoiDiachinh.Click += cmdThemmoiDiachinh_Click;
            txtMaLankham.LostFocus += txtMaLankham_LostFocus;
            txtTenKH._OnShowData += txtTenKH__OnShowData;
        }

        private void txtTenKH__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtTenKH.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTenKH.myCode;
                txtTenKH.Init();
                txtTenKH.SetCode(oldCode);
                txtTenKH.Focus();
            }
        }

        private void txtMaLankham_LostFocus(object sender, EventArgs e)
        {
            if (Utility.DoTrim(txtMaLankham.Text).Length >= 8 && Utility.DoTrim(txtMaLankham.Text) != m_strMaluotkham)
                //Đã bị thay đổi do nhập tay
            {
                int reval = 0;
                StoredProcedure spitem = SPs.KcbKiemtraMalankhamNhaptay(globalVariables.UserName, 1, m_strMaluotkham,
                    Utility.DoTrim(txtMaLankham.Text), reval);
                spitem.Execute();
                reval = Utility.Int32Dbnull(spitem.OutputValues[0], -1);
                if (reval != 0)
                {
                    Utility.ShowMsg(
                        string.Format(
                            "Mã lượt khám bạn vừa nhập {0} không có trong danh mục hoặc đang được sử dụng bởi người dùng khác. Hãy nhấn OK để hệ thống tự động sinh mã lần khám mới nhất",
                            Utility.DoTrim(txtMaLankham.Text)));
                    SinhMaLanKham();
                    txtMaLankham.SelectAll();
                    txtMaLankham.Focus();
                }
                else
                {
                    m_strMaluotkham = Utility.DoTrim(txtMaLankham.Text);
                }
            }
        }
        private void cmdThemmoiDiachinh_Click(object sender, EventArgs e)
        {
            var _themmoi_diachinh = new frm_themmoi_diachinh_new();
            _themmoi_diachinh.ShowDialog();
            if (_themmoi_diachinh.m_blnHasChanged)
            {
                AddAutoCompleteDiaChi();
            }
        }

        private void cboDoituongKCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowTextChanged) return;
                _MaDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
                objDoituongKCB =
                    new Select().From(DmucDoituongkcb.Schema)
                        .Where(DmucDoituongkcb.MaDoituongKcbColumn)
                        .IsEqualTo(_MaDoituongKcb)
                        .ExecuteSingle<DmucDoituongkcb>();
                ChangeObjectRegion();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        private void chkTudongthemmoi_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._KCBProperties.Tudongthemmoi = chkTudongthemmoi.Checked;
            PropertyLib.SaveProperty(PropertyLib._KCBProperties);
        }


        private void cmdConfig_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._KCBProperties);
            frm.ShowDialog();
            CauHinhKCB();
        }


        private void txtMaLankham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaLankham.Text.Trim() != "")
            {
                isAutoFinding = true;
                string patient_ID = Utility.GetYY(globalVariables.SysDate) +
                                    Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLankham.Text, 0), "000000");
                txtMaLankham.Text = patient_ID;
                ResetLuotkham();
                FindPatientIDbyMaLanKham(txtMaLankham.Text.Trim());
                isAutoFinding = false;
            }
        }


        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaBN.Text.Trim() != "")
            {
                isAutoFinding = true;
                FindPatient(txtMaBN.Text.Trim());
                isAutoFinding = false;
            }
        }

        private bool NotPayment(string patient_ID, ref string NgayKhamGanNhat)
        {
            try
            {
                DataTable temdt =
                    _KCB_DANGKY.KcbLaythongtinBenhnhan(
                        Utility.Int64Dbnull(grdList.GetValue(KnDangkyXn.Columns.IdKhachhang)));
                if (temdt != null && temdt.Rows.Count <= 0)
                {
                    NgayKhamGanNhat = "NOREG";
                    //Chưa đăngký khám lần nào(mới gõ thông tin BN)-->Trạng thái sửa
                    return true; //Chưa thanh toán-->Cho về trạng thái sửa
                }
                return false;
            }
            catch (Exception ex)
            {
                return false; //Đã thanh toán--.Thêm lần đăng ký mới
            }
        }

        private void FindPatient(string patient_ID)
        {
            try
            {
                QueryCommand cmd = KnDanhsachKhachhang.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql =
                    "Select id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan where id_benhnhan like '%" +
                    patient_ID + "%'";

                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(
                        temdt.Rows[0][KnDanhsachKhachhang.Columns.IdKhachhang].ToString(), string.Empty);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select("id_benhnhan=" + patient_ID);
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(
                            arrDr[0][KnDanhsachKhachhang.Columns.IdKhachhang].ToString(), string.Empty);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void FindPatientIDbyMaLanKham(string malankham)
        {
            try
            {
                QueryCommand cmd = KnDanhsachKhachhang.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql =
                    "Select id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan p where exists(select 1 from kcb_luotkham where id_benhnhan=P.id_benhnhan and ma_luotkham like '%" +
                    malankham + "%')";
                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(
                        temdt.Rows[0][KnDanhsachKhachhang.Columns.IdKhachhang].ToString(), string.Empty);
                }
                else //Show dialog for select
                {
                    var _ChonBN = new frm_CHON_BENHNHAN();
                    _ChonBN.temdt = temdt;
                    _ChonBN.ShowDialog();
                    if (!_ChonBN.mv_bCancel)
                    {
                        AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void AutoFindLastExamandFetchIntoControls(string patientID, string sobhyt)
        {
            try
            {
                if (!Utility.CheckLockObject(m_strMaluotkham, "Tiếp đón", "TD"))
                    return;
                //Trả lại mã lượt khám nếu chưa được dùng đến
                ResetLuotkham();

                SqlQuery sqlQuery = new Select().From(KnDangkyXn.Schema)
                    .Where(KnDangkyXn.Columns.IdKhachhang).IsEqualTo(patientID);
                var objDangkyXn = sqlQuery.ExecuteSingle<KnDangkyXn>();
                if (objDangkyXn != null)
                {
                    txtMaBN.Text = patientID;
                    txtMaLankham.Text = Utility.sDbnull(objDangkyXn.MaDangky);
                    m_strMaluotkham = objDangkyXn.MaDangky;
                    m_enAction = action.Update;
                    AllowTextChanged = false;
                    LoadThongtinBenhnhan();
                    string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
                    if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))
                        //Đã thanh toán-->Kiểm tra ngày hẹn xem có được phép khám tiếp
                    {
                        var _Info =
                            new Select().From(KcbChandoanKetluan.Schema)
                                .Where(KcbChandoanKetluan.MaLuotkhamColumn)
                                .IsEqualTo(objDangkyXn.MaDangky)
                                .ExecuteSingle<KcbChandoanKetluan>();
                        if (_Info != null && _Info.SongayDieutri != null)
                        {
                            int SoNgayDieuTri = 0;
                            if (_Info.SongayDieutri.Value.ToString() == "")
                            {
                                SoNgayDieuTri = 0;
                            }
                            else
                            {
                                SoNgayDieuTri = _Info.SongayDieutri.Value;
                            }
                            DateTime ngaykhamcu = _Info.NgayTao;
                            ;
                            DateTime ngaykhammoi = globalVariables.SysDate;
                            TimeSpan songay = ngaykhammoi - ngaykhamcu;

                            int kt = songay.Days;
                            int kt1 = SoNgayDieuTri - kt;
                            kt1 = Utility.Int32Dbnull(kt1);
                            // nếu khoảng cách từ lần đăng ký trước đến ngày hiện tại lớn hơn ngày điều trị.
                            if (kt >= SoNgayDieuTri)
                            {
                                m_enAction = action.Add;
                                SinhMaLanKham();
                                chkPhongchuyenmon.Focus();
                            }
                            else if (kt < SoNgayDieuTri)
                            {
                                DialogResult dialogResult =
                                    MessageBox.Show(
                                        "Bác Sỹ hẹn :  " + SoNgayDieuTri + "ngày" + "\nNgày khám gần nhất:  " +
                                        ngaykhamcu + "\nCòn: " + kt1 + " ngày nữa mới được tái khám" +
                                        "\nBạn có muốn tiếp tục thêm lần đăng ký ", "Thông Báo", MessageBoxButtons.YesNo);

                                if (dialogResult == DialogResult.Yes)
                                {
                                    m_enAction = action.Add;
                                    SinhMaLanKham();
                                    chkPhongchuyenmon.Focus();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    ClearControl();
                                    return;
                                }
                            }
                        }
                    }
                    else //Còn lần đăng ký chưa thanh toán-->Kiểm tra
                    {
                        //nếu là ngày hiện tại thì đặt về trạng thái sửa
                        if (ngay_kham == "NOREG" || ngay_kham == globalVariables.SysDate.ToString("dd/MM/yyyy"))
                        {
                            //LoadThongtinBenhnhan();
                            if (ngay_kham == "NOREG") //Bn chưa đăng ký phòng khám nào cả. 
                            {
                                //Nếu ngày hệ thống=Ngày đăng ký gần nhất-->Sửa
                                if (globalVariables.SysDate.ToString("dd/MM/yyyy") ==
                                    dtpInputDate.Value.ToString("dd/MM/yyyy"))
                                {
                                    m_enAction = action.Update;

                                    Utility.ShowMsg(
                                        "Khách hàng vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                    //LaydanhsachdangkyKCB();
                                    txtTenKH.Select();
                                }
                                else //Thêm lần đăng ký cho ngày mới
                                {
                                    m_enAction = action.Add;
                                    SinhMaLanKham();
                                }
                            }
                            else //Quay về trạng thái sửa
                            {
                                m_enAction = action.Update;

                                Utility.ShowMsg(
                                    "Khách hàng vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                //LaydanhsachdangkyKCB();
                                txtTenKH.Select();
                            }
                        }
                        else //Không cho phép thêm lần đăng ký khác nếu chưa thanh toán lần đăng ký của ngày hôm trước
                        {
                            Utility.ShowMsg(
                                "Khách hàng đang có lần đăng ký chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Khách hàng trước khi thêm lần đăng ký mới. Nhấn OK để hệ thống chuyển về trạng thái thêm mới Khách hàng");
                            cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                        }
                    }
                    ModifyCommand();
                }
                else
                {
                    Utility.ShowMsg(
                        "Khách hàng này chưa có lần đăng ký nào-->Có thể bị lỗi dữ liệu. Đề nghị liên hệ với VNS để được giải đáp");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("AutoFindLastExam().Exception-->" + ex.Message);
            }
            finally
            {
                SetActionStatus();
                AllowTextChanged = true;
            }
        }


        // private  b_QMSStop=false;
        /// <summary>
        ///     hàm thực hiện việc lấy thông tin của phần dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Dangky_Kiemnghiem_Load(object sender, EventArgs e)
        {
            try
            {
                AllowTextChanged = false;
                b_HasLoaded = false;
                Utility.SetColor(lblDiachiBN,
                    THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BENHNHAN", "1", false) == "1"
                        ? lblHoten.ForeColor
                        : lblMaKH.ForeColor);
                AddAutoCompleteDiaChi();
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                    DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "", false);
                objDoituongKCB =
                    new Select().From(DmucDoituongkcb.Schema)
                        .Where(DmucDoituongkcb.MaDoituongKcbColumn)
                        .IsEqualTo(_MaDoituongKcb)
                        .ExecuteSingle<DmucDoituongkcb>();
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                ChangeObjectRegion();
                if (m_enAction == action.Insert) //Thêm mới BN
                {
                    objDangkyXn = null;
                    SinhMaLanKham();
                    txtTenKH.Select();
                }
                else if (m_enAction == action.Update) //Cập nhật thông tin Khách hàng
                {
                    LoadThongtinBenhnhan();
                    txtTenKH.Select();
                }
                else if (m_enAction == action.Add) //Thêm mới lần đăng ký
                {
                    objDangkyXn = null;
                    string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
                    if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))
                        //Nếu đã thanh toán xong hết thì thêm lần đăng ký mới
                    {
                        SinhMaLanKham();
                        LoadThongtinBenhnhan();
                        chkPhongchuyenmon.Focus();
                    }
                    else //Còn lần đăng ký chưa thanh toán-->Kiểm tra
                    {
                        //nếu là ngày hiện tại thì đặt về trạng thái sửa
                        if (ngay_kham == "NOREG" || ngay_kham == globalVariables.SysDate.ToString("dd/MM/yyyy"))
                        {
                            LoadThongtinBenhnhan();
                            if (ngay_kham == "NOREG") //Bn chưa đăng ký phòng khám nào cả. 
                            {
                                //Nếu ngày hệ thống=Ngày đăng ký gần nhất-->Sửa
                                if (globalVariables.SysDate.ToString("dd/MM/yyyy") ==
                                    dtpInputDate.Value.ToString("dd/MM/yyyy"))
                                {
                                    m_enAction = action.Update;

                                    Utility.ShowMsg(
                                        "Khách hàng vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                    txtTenKH.Select();
                                }
                                else //Thêm lần đăng ký cho ngày mới
                                {
                                    m_enAction = action.Add;
                                    SinhMaLanKham();
                                    chkPhongchuyenmon.Focus();
                                }
                            }
                            else //Quay về trạng thái sửa
                            {
                                m_enAction = action.Update;

                                Utility.ShowMsg(
                                    "Khách hàng vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                txtTenKH.Select();
                            }
                        }
                        else //Không cho phép thêm lần đăng ký khác nếu chưa thanh toán lần đăng ký của ngày hôm trước
                        {
                            Utility.ShowMsg(
                                "Khách hàng đang có lần đăng ký chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Khách hàng trước khi thêm lần đăng ký mới. Nhấn OK để hệ thống chuyển về trạng thái thêm mới Khách hàng");
                            cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                        }
                    }
                }
                ModifyCommand();
                AllowTextChanged = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
                
            }
            finally
            {
                if (PropertyLib._ConfigProperties.HIS_AppMode != AppEnum.AppMode.License)
                    Text = @"Đăng ký KCB -->Demo 1500";
                SetActionStatus();
                ModifyCommand();
                b_HasLoaded = true;
            }
        }

        private void LoadThongtinBenhnhan()
        {
            PtramBhytCu = 0m;
            PtramBhytGocCu = 0m;
            AllowTextChanged = false;
            KnDanhsachKhachhang objKhachhang = KnDanhsachKhachhang.FetchByID(txtMaBN.Text);
            if (objKhachhang != null)
            {
                txtTenKH._Text = Utility.sDbnull(objKhachhang.TenKhachhang);
                txtSoDT.Text = Utility.sDbnull(objKhachhang.DienThoai);
                txtDiachi._Text = Utility.sDbnull(objKhachhang.DiaChi);

                txtEmail.Text = Utility.sDbnull(objKhachhang.Email);
                txtFax.Text = objKhachhang.Fax;
                txtNguoiLienhe.Text = objKhachhang.NguoiLienhe;
                txtSoDT.Text = objKhachhang.DienThoai;

                objDangkyXn = new Select().From(KnDangkyXn.Schema)
                    .Where(KnDangkyXn.Columns.MaDangky).IsEqualTo(txtMaLankham.Text)
                    .And(KnDangkyXn.Columns.IdKhachhang).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text, -1)).ExecuteSingle
                    <KnDangkyXn>();
                if (objDangkyXn != null)
                {
                    m_strMaluotkham = objDangkyXn.MaDangky;
                    txtSolankham.Text = Utility.sDbnull(objKhachhang.SolanDky);
                    dtpInputDate.Value = objDangkyXn.NgayDangky;
                    dtCreateDate.Value = objDangkyXn.NgayDangky;
                    chkPhongchuyenmon.Checked = Utility.Byte2Bool(objDangkyXn.TrakqTaiphong);
                    chkFax.Checked = Utility.Byte2Bool(objDangkyXn.TrakqFax);
                    chkMail.Checked = Utility.Byte2Bool(objDangkyXn.TrakqGuithu);
                    chkEmail.Checked = Utility.Byte2Bool(objDangkyXn.TrakqEmail);
                    chkSosanh.Checked = Utility.Byte2Bool(objDangkyXn.KqQcvn);
                    txtMotathem.Text = objDangkyXn.MotaThem;
                    txtEmail.Text = objKhachhang.Email;
                    txtMotathem.Text = objKhachhang.MotaThem;
                    txtyeucaukhac.Text = objDangkyXn.YeucauKhac;
                }
            }
        }

        private void AddAutoCompleteDiaChi()
        {
            txtDiachi.dtData = globalVariables.dtAutocompleteAddress.Copy();
            txtDiachi.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
            txtDiachi.CaseSensitive = false;
            txtDiachi.MinTypedCharacters = 1;
        }

        private void CreateTable()
        {
            if (m_DC == null || m_DC.Columns.Count <= 0)
            {
                m_DC = new DataTable();
                m_DC.Columns.AddRange(new[]
                {
                    new DataColumn("ShortCutXP", typeof (string)),
                    new DataColumn("ShortCutQH", typeof (string)),
                    new DataColumn("ShortCutTP", typeof (string)),
                    new DataColumn("Value", typeof (string)),
                    new DataColumn("ComparedValue", typeof (string))
                });
            }
        }


        private void SinhMaLanKham()
        {
            txtSolankham.Text = string.Empty;
            if (m_enAction == action.Insert)
            {
                txtMaBN.Text = @"Tự sinh";
            }
            // Tý xem phần này
            txtMaLankham.Text = THU_VIEN_CHUNG.KN_SINH_MADANGKY(1);
            m_strMaluotkham = txtMaLankham.Text;
            //Tạm bỏ
            SqlQuery sqlQuery = new Select(Aggregate.Count(KnDangkyXn.Columns.MaDangky)).From(KnDangkyXn.Schema)
                .Where(KnDangkyXn.Columns.IdKhachhang).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text, -1));
            var soThuTuKham = sqlQuery.ExecuteScalar<Int32>();
            txtSolankham.Text = Utility.sDbnull(soThuTuKham + 1);
        }


        /// <summary>
        ///     hàm thực hiện việc làm sách thông tin của Khách hàng
        /// </summary>
        private void ClearControl()
        {
            setMsg(uiStatusBar1.Panels["MSG"], "", false);
            m_blnHasJustInsert = false;
            txtSolankham.Text = "1";
            txtTenKH.ResetText();
            txtDiachi.Clear();
            txtSoDT.Clear();
            txtNguoiLienhe.Clear();
            txtEmail.Clear();
            txtFax.Clear();
            txtMotathem.Clear();
            ModifyCommand();
            AllowTextChanged = false;
            _MaDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
            objDoituongKCB =
                new Select().From(DmucDoituongkcb.Schema)
                    .Where(DmucDoituongkcb.MaDoituongKcbColumn)
                    .IsEqualTo(_MaDoituongKcb)
                    .ExecuteSingle<DmucDoituongkcb>();
            if (objDoituongKCB == null) return;
            _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
            _IdLoaidoituongKcb = objDoituongKCB.IdLoaidoituongKcb;
            _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
            PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
            PtramBhytGocCu = PtramBhytCu;
            AllowTextChanged = true;
            //Chuyển về trạng thái thêm mới
            m_enAction = action.Insert;
            SinhMaLanKham();
            PtramBhytCu = 0;
            PtramBhytGocCu = 0;
            if (m_enAction == action.Insert)
            {
                dtpInputDate.Value = globalVariables.SysDate;
                dtCreateDate.Value = globalVariables.SysDate;
            }
            SetActionStatus();
        }

        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            //Cập nhật lại mã lượt khám chưa dùng tới trong trường hợp nhấn New liên tục
            ResetLuotkham();
            ClearControl();
        }

        /// <summary>
        ///     hàm thực hiện viecj thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     hàm thực hiện việc lưu thông tin của đối tượng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmdSave.Enabled = false;
                if (m_enAction == action.Update)
                    if (!IsValidData()) return;
                PerformAction();
                cmdSave.Enabled = true;
            }
            catch(Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                DiachiBNCu = false;
                DiachiBHYTCu = false;
                cmdSave.Enabled = true;
            }
        }

        private bool isExceedData()
        {
            try
            {
                if (PropertyLib._ConfigProperties.HIS_AppMode != AppEnum.AppMode.License)
                {
                    var lst = new Select().From(KnDangkyXn.Schema).ExecuteAsCollection<KnDangkyXnCollection>();
                    return lst.Count >= 1500;
                }
                return false;
            }
            catch (Exception ex)
            {
                Utility.CatchException("isExceedData()-->", ex);
                return true;
            }
        }


        private bool IsValidData()
        {
            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
            if (m_enAction == action.Insert &&
                dtCreateDate.Value.ToString("dd/MM/yyyy") != globalVariables.SysDate.ToString("dd/MM/yyyy"))
            {
                if (
                    !Utility.AcceptQuestion("Ngày tiếp đón khác ngày hiện tại. Bạn có chắc chắn hay không?", "Cảnh báo",
                        true))
                {
                    dtCreateDate.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtTenKH.Text))
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập tên Khách hàng", true);
                txtTenKH.Focus();
                return false;
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BENHNHAN", "0", false) == "1")
            {
                if (Utility.DoTrim(txtDiachi.Text) == "")
                {
                    Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập địa chỉ Khách hàng", true);
                    txtDiachi.Focus();
                    return false;
                }
            }
            return true;
        }


        private void ModifyCommand()
        {
            cmdSave.Enabled = Utility.DoTrim(txtTenKH.Text).Length > 0;
        }


        private void PerformAction()
        {
            switch (m_enAction)
            {
                case action.Update:
                    if (!InValiExistsBN()) return;
                    UpdatePatient();
                    break;
                case action.Insert:
                    InsertPatient();
                    break;
                case action.Add:
                    ThemLanKham();
                    break;
            }

            ModifyCommand();
        }

        private bool InValiExistsBN()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaLankham.Text))
                {
                    Utility.ShowMsg("Mã đăng ký không bỏ trống", "Thông báo", MessageBoxIcon.Error);
                    txtMaLankham.Focus();
                    txtMaLankham.SelectAll();
                    return false;
               }
                SqlQuery sqlQuery = new Select().From(KnDangkyXn.Schema)
                    .Where(KnDangkyXn.Columns.MaDangky).IsEqualTo(Utility.sDbnull(txtMaLankham.Text));
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg("Mã đăng ký này không tồn tại trong CSDL,Mời bạn xem lại", "Thông báo",
                        MessageBoxIcon.Error);
                    txtMaLankham.Focus();
                    txtMaLankham.SelectAll();
                    return false;
                }
                //Kiểm tra xem có thay đổi phần trăm BHYT
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi kiểm tra hợp lệ dữ liệu trước khi cập nhật Khách hàng", ex);
                return false;
            }
        }

        private void ChangeObjectRegion()
        {
            if (objDoituongKCB == null) return;
            _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
            _IdLoaidoituongKcb = objDoituongKCB.IdLoaidoituongKcb;
            _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
            PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
            PtramBhytGocCu = PtramBhytCu;

            if (objDoituongKCB.IdLoaidoituongKcb == 0) //ĐỐi tượng BHYT
            {
            }
            else //Đối tượng khác BHYT
            {
                txtTenKH.Focus();
            }
        }

        /// <summary>
        ///     hàm thực hienej phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Dangky_Kiemnghiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (ActiveControl != null && ActiveControl.Name == txtTenKH.Name && Utility.DoTrim(txtTenKH.Text) != "")
                {
                    var Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM(Args);
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", Utility.DoTrim(txtTenKH.Text), "", "", "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == DialogResult.OK)
                    {
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                else if (ActiveControl != null && ActiveControl.Name == txtSoDT.Name &&
                         Utility.DoTrim(txtSoDT.Text) != "")
                {
                    var Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM(Args);
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", "", "", Utility.DoTrim(txtSoDT.Text), "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == DialogResult.OK)
                    {
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                return;
            }

            if (e.Control && e.KeyCode == Keys.D)
            {
                _MaDoituongKcb = "DV";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                return;
            }
            if (e.Control && e.KeyCode == Keys.B)
            {
                _MaDoituongKcb = "BHYT";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                return;
            }

            string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
            if (e.Control && e.KeyCode == Keys.K)
            {
                if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))
                {
                    m_enAction = action.Add;
                    SinhMaLanKham();
                    chkPhongchuyenmon.Focus();
                }
                else
                {
                    //nếu là ngày hiện tại thì đặt về trạng thái sửa
                    if (ngay_kham == globalVariables.SysDate.ToString("dd/MM/yyyy"))
                    {
                        Utility.ShowMsg(
                            "Khách hàng đang có lần đăng ký chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Khách hàng trước khi thêm lần đăng ký mới.Nhấn OK để hệ thống quay về trạng thái sửa thông tin BN");
                        m_enAction = action.Update;
                        AllowTextChanged = false;
                        LoadThongtinBenhnhan();
                        txtTenKH.Focus();
                    }
                    else //Không cho phép thêm lần đăng ký khác nếu chưa thanh toán lần đăng ký của ngày hôm trước
                    {
                        Utility.ShowMsg(
                            "Khách hàng đang có lần đăng ký chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Khách hàng trước khi thêm lần đăng ký mới. Nhấn OK để hệ thống chuyển về trạng thái thêm mới Khách hàng");
                        cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    }
                }
                return;
            }
            if (e.Control && e.KeyCode == Keys.F)
            {
                txtMaBN.SelectAll();
                txtMaBN.Focus();
            }

            if (e.KeyCode == Keys.F11) Utility.ShowMsg(ActiveControl.Name);
            if (e.KeyCode == Keys.Escape && ActiveControl != null && ActiveControl.GetType() != txtDiachi.GetType())
            {
                Close();
            }
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.N && e.Control) cmdThemMoiBN.PerformClick();
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }


        private void txtTEN_BN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cmdSave.Enabled = Utility.DoTrim(txtTenKH.Text).Length > 0;
            }
            catch (Exception exception)
            {
            }
        }

        private void ResetLuotkham()
        {
            new Update(KcbDmucLuotkham.Schema)
                .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(0)
                .Set(KcbDmucLuotkham.Columns.UsedBy).EqualTo(DBNull.Value)
                .Set(KcbDmucLuotkham.Columns.StartTime).EqualTo(DBNull.Value)
                .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(null)
                .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(m_strMaluotkham)
                .And(KcbDmucLuotkham.Columns.TrangThai).IsEqualTo(1)
                .And(KcbDmucLuotkham.Columns.UsedBy).IsEqualTo(globalVariables.UserName)
                .And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year).Execute();
        }

        private void frm_Dangky_Kiemnghiem_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Utility.FreeLockObject(m_strMaluotkham);
                //Trả lại mã lượt khám nếu chưa được dùng đến
                ResetLuotkham();
            }
            catch (Exception exception)
            {
            }
        }


        private void SetActionStatus()
        {
            lblStatus.Text = m_enAction == action.Insert
                ? "Khách hàng MỚI"
                : (m_enAction == action.Add ? "THÊM lần đăng ký" : "CẬP NHẬT");
        }

        private void CauHinhKCB()
        {
            if (PropertyLib._KCBProperties != null)
            {
                chkTudongthemmoi.Checked = PropertyLib._KCBProperties.Tudongthemmoi;
            }
        }

        #region "Su kien autocomplete của thành phố"

        private bool AllowTextChanged;
        private string _rowFilter = "1=1";

        #endregion

        #region "Su kien autocomplete của quận huyện"

        private string _rowFilterQuanHuyen = "1=1";

        #region Diachi

        private bool DiachiBHYTCu;
        private bool DiachiBNCu;

        private void setMsg(UIStatusBarPanel item, string msg, bool isError)
        {
            try
            {
                item.Text = msg;
                if (isError) item.FormatStyle.ForeColor = Color.Red;
                else item.FormatStyle.ForeColor = Color.DarkBlue;

                Application.DoEvents();
            }
            catch
            {
            }
        }

        #endregion

        #endregion

        #region "khởi tạo sự kiện để lưu lại thông tin của Khách hàng"

        private string mavuasinh = "";

        private void ThemMoiLanKhamVaoLuoi()
        {
            DataRow dr = m_dtPatient.NewRow();
            dr[KnDanhsachKhachhang.Columns.IdKhachhang] = Utility.sDbnull(txtMaBN.Text, "-1");
            dr[KnDanhsachKhachhang.Columns.TenKhachhang] = Utility.sDbnull(txtTenKH.Text, "");
            dr[KnDanhsachKhachhang.Columns.Email] = Utility.sDbnull(txtEmail.Text, "");
            dr[KnDanhsachKhachhang.Columns.DiaChi] = Utility.sDbnull(txtDiachi.Text, "");
            dr[KnDanhsachKhachhang.Columns.NguoiTao] = globalVariables.UserName;
            dr[KnDanhsachKhachhang.Columns.DienThoai] = Utility.sDbnull(txtSoDT.Text, "");
            dr[KnDangkyXn.Columns.MaDangky] = Utility.sDbnull(txtMaLankham.Text, "");
            dr["sNgay_tiepdon"] = dtCreateDate.Value;
            m_dtPatient.Rows.InsertAt(dr, 0);
        }

        private void UpdateBNVaoTrenLuoi()
        {
            EnumerableRowCollection<DataRow> query = from bn in m_dtPatient.AsEnumerable()
                where
                    Utility.sDbnull(bn[KnDangkyXn.Columns.MaDangky]) ==
                    txtMaLankham.Text
                select bn;
            if (query.Any())
            {
                DataRow dr = query.FirstOrDefault();
                dr[KnDanhsachKhachhang.Columns.IdKhachhang] = Utility.sDbnull(txtMaBN.Text, "-1");
                dr[KnDanhsachKhachhang.Columns.TenKhachhang] = Utility.sDbnull(txtTenKH.Text, "");
                dr[KnDanhsachKhachhang.Columns.Email] = Utility.sDbnull(txtEmail.Text, "");
                dr[KnDanhsachKhachhang.Columns.DiaChi] = Utility.sDbnull(txtDiachi.Text, "");
                dr[KnDanhsachKhachhang.Columns.NguoiTao] = globalVariables.UserName;
                dr[KnDanhsachKhachhang.Columns.DienThoai] = Utility.sDbnull(txtSoDT.Text, "");
                dr[KnDangkyXn.Columns.MaDangky] = Utility.sDbnull(txtMaLankham.Text, "");
                dr[DmucDoituongkcb.Columns.TenDoituongKcb] = _TenDoituongKcb;
                dr[KnDangkyXn.Columns.TrangThai] = 0;
                dr[KnDangkyXn.Columns.NgayDangky] = dtCreateDate.Value;
                dr["sNgay_tiepdon"] = dtCreateDate.Value; //globalVariables.SysDate;

                m_dtPatient.AcceptChanges();
            }
        }

        private void ThemLanKham()
        {
            KnDanhsachKhachhang objKhachhang = TaoBenhnhan();
            objDangkyXn = TaoLuotkham();

            long vIdKham = -1;
            string msg = "";
            errorProvider1.Clear();
            ActionResult actionResult = _KCB_DANGKY.ThemLanDangkyKiemnghiem(objKhachhang, objDangkyXn, ref msg);
            switch (actionResult)
            {
                case ActionResult.Success:

                    PtramBhytCu = 0;
                    PtramBhytGocCu = 0;
                    txtMaLankham.Text = Utility.sDbnull(objDangkyXn.MaDangky);
                    txtMaBN.Text = Utility.sDbnull(objDangkyXn.IdKhachhang);

                    m_blnHasJustInsert = true;
                    m_enAction = action.Update;
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thêm mới lần đăng ký Khách hàng thành công", false);
                    ThemMoiLanKhamVaoLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    Utility.GotoNewRowJanus(grdList, KnDangkyXn.Columns.MaDangky, txtMaLankham.Text);
                    if (_OnAssign != null) _OnAssign();
                    cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    m_blnCancel = false;
                    break;
                case ActionResult.Error:
                    setMsg(uiStatusBar1.Panels["MSG"], "Lỗi trong quá trình thêm lần đăng ký !", true);
                    cmdSave.Focus();
                    break;
            }
        }


        private void InsertPatient()
        {
            KnDanhsachKhachhang objKhachhang = TaoBenhnhan();
            objDangkyXn = TaoLuotkham();
            long vIdKham = -1;
            string msg = "";
            errorProvider1.Clear();
            ActionResult actionResult = _KCB_DANGKY.ThemmoiDangkyKiemnghiem(objKhachhang, objDangkyXn, ref msg);
            switch (actionResult)
            {
                case ActionResult.Success:

                    PtramBhytCu = 0;
                    PtramBhytGocCu = 0;
                    txtMaLankham.Text = Utility.sDbnull(objDangkyXn.MaDangky);
                    txtMaBN.Text = Utility.sDbnull(objDangkyXn.IdKhachhang);
                    mavuasinh = Utility.sDbnull(objDangkyXn.IdKhachhang);
                    m_enAction = action.Update;
                    m_blnHasJustInsert = true;
                    m_strMaluotkham = txtMaLankham.Text;
                    ThemMoiLanKhamVaoLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thêm mới Khách hàng thành công", false);
                    Utility.GotoNewRowJanus(grdList, KnDangkyXn.Columns.MaDangky, txtMaLankham.Text);
                    m_blnCancel = false;

                    //if (_OnAssign != null) _OnAssign();
                    if (PropertyLib._KCBProperties.Tudongthemmoi) cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    txtMaBN.Text = Utility.sDbnull(mavuasinh);

                    break;
                case ActionResult.Error:
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thực hiện thêm dữ liệu không thành công !", true);
                    cmdSave.Focus();
                    break;
            }
        }


        private void UpdatePatient()
        {
            KnDanhsachKhachhang objKhachhang = TaoBenhnhan();
            objDangkyXn = TaoLuotkham();

            string msg = "";
            errorProvider1.Clear();
            ActionResult actionResult = _KCB_DANGKY.DangkymauKiemnghiem(objKhachhang, objDangkyXn, ref msg);

            switch (actionResult)
            {
                case ActionResult.Success:

                    //gọi lại nếu thay đổi địa chỉ
                    m_blnHasJustInsert = false;
                    PtramBhytCu = 0;
                    PtramBhytGocCu = 0;
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn sửa thông tin Khách hàng thành công", false);

                    UpdateBNVaoTrenLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();


                    Utility.GotoNewRowJanus(grdList, KnDangkyXn.Columns.MaDangky, txtMaLankham.Text);
                    m_blnCancel = false;

                    break;
                case ActionResult.Error:
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thực hiện sửa thông tin không thành công !", true);
                    break;
                case ActionResult.Cancel:
                    break;
            }
        }


        /// <summary>
        ///     Insert dữ liệu khi thêm mới hoàn toàn
        /// </summary>
        /// hàm chen du lieu moi tin day, benhnhan kham benh moi tinh
        private KnDanhsachKhachhang TaoBenhnhan()
        {
            var objKhachhang = new KnDanhsachKhachhang();
            if (m_enAction == action.Add || m_enAction == action.Update)
            {
                objKhachhang = KnDanhsachKhachhang.FetchByID(Utility.Int64Dbnull(txtMaBN.Text, -1));
                if (objKhachhang == null) return null;
                objKhachhang.IsNew = false;
                objKhachhang.MarkOld();
            }
            objKhachhang.TenKhachhang = txtTenKH.Text;
            objKhachhang.DiaChi = txtDiachi.Text;
            objKhachhang.DienThoai = txtSoDT.Text;
            objKhachhang.Email = Utility.sDbnull(txtEmail.Text, "");
            objKhachhang.NguoiLienhe = Utility.sDbnull(txtNguoiLienhe.Text);
            objKhachhang.Fax = Utility.sDbnull(txtFax.Text);
            objKhachhang.NgayTao = globalVariables.SysDate;
            objKhachhang.NguoiTao = globalVariables.UserName;
            objKhachhang.MotaThem = txtMotathem.Text;
            if (m_enAction == action.Insert)
            {
                objKhachhang.NgayDangky = dtCreateDate.Value;
                objKhachhang.NguoiTao = globalVariables.UserName;
                objKhachhang.IpMaytao = globalVariables.gv_strIPAddress;
                objKhachhang.SolanDky = 1;
            }
            if (m_enAction == action.Update)
            {
                objKhachhang.NgaySua = globalVariables.SysDate;
                objKhachhang.NguoiSua = globalVariables.UserName;
                objKhachhang.IpMaysua = globalVariables.gv_strIPAddress;
                objKhachhang.SolanDky = Utility.Int16Dbnull(txtSolankham.Text, 1);
            }
            return objKhachhang;
        }

        /// <summary>
        ///     hàm thực hiện việc khwoir tạo thoog tin PatietnExam
        /// </summary>
        /// <returns></returns>
        private KnDangkyXn TaoLuotkham()
        {
            objDangkyXn = new KnDangkyXn();
            if (m_enAction == action.Insert || m_enAction == action.Add)
            {
                //Bỏ đi do đã sinh theo cơ chế bảng danh mục mã lượt khám. Nếu ko sẽ mất mã lượt khám hiện thời.
                // txtMaLankham.Text = THU_VIEN_CHUNG.KCB_SINH_MALANKHAM();
                objDangkyXn.IsNew = true;
            }
            else
            {
                objDangkyXn = KnDangkyXn.FetchByID(m_strMaluotkham);
                if (objDangkyXn == null) return null;
                objDangkyXn.MarkOld();
                objDangkyXn.IsNew = false;
            }
            objDangkyXn.NguoiTao = globalVariables.UserName;
            objDangkyXn.NgayTao = globalVariables.SysDate;
            objDangkyXn.TrakqTaiphong = Utility.Bool2byte(chkPhongchuyenmon.Checked);
            objDangkyXn.TrakqFax = Utility.Bool2byte(chkFax.Checked);
            objDangkyXn.TrakqGuithu = Utility.Bool2byte(chkMail.Checked);
            objDangkyXn.TrangThai = 1;
            objDangkyXn.UuTien = Utility.Bool2byte(chkuutien.Checked);
            objDangkyXn.TrakqEmail = Utility.Bool2byte(chkEmail.Checked);
            objDangkyXn.KqQcvn = Utility.Bool2byte(chkSosanh.Checked);
            objDangkyXn.YeucauKhac = Utility.DoTrim(txtyeucaukhac.Text);
            if (m_enAction == action.Update) txtMaLankham.Text = m_strMaluotkham;
            objDangkyXn.MaDangky = Utility.sDbnull(txtMaLankham.Text, "");
            objDangkyXn.IdKhachhang = Utility.Int64Dbnull(txtMaBN.Text, -1);
            if (m_enAction == action.Update)
            {
                objDangkyXn.NgayDangky = dtCreateDate.Value;
                objDangkyXn.NguoiSua = globalVariables.UserName;
                objDangkyXn.NgaySua = globalVariables.SysDate;
                objDangkyXn.IpMaysua = globalVariables.gv_strIPAddress;
            }
            if (m_enAction == action.Add || m_enAction == action.Insert)
            {
                objDangkyXn.NgayDangky = dtCreateDate.Value;
                objDangkyXn.NguoiTao = globalVariables.UserName;
                objDangkyXn.IpMaytao = globalVariables.gv_strIPAddress;
            }
            return objDangkyXn;
        }

        #endregion

        private void txtTenKH_Leave(object sender, EventArgs e)
        {
            txtTenKH.Text = txtTenKH.Text.ToUpper();
        }
    }
}