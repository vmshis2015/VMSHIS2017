using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Test;

namespace CheckHoSoThongTuyen
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private static string CreateMd5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                var sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        private void cmdCheck_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://egw.baohiemxahoi.gov.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                const string username = "01010_BV";
                string password = CreateMd5("tckt1234");
                // HTTP POST
                var input = new ApiToken { username = username, password = password };
                var values = new Dictionary<string, string>
                {
                   { "username", username },
                   { "password", password }
                };
                var content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = client.PostAsync("api/token/take", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    KQPhienLamViec plv = response.Content.ReadAsAsync<KQPhienLamViec>().Result;
                    var key = plv.APIKey;
                    using (var clientPush = new HttpClient())
                    {
                        clientPush.BaseAddress = new Uri("http://egw.baohiemxahoi.gov.vn/");
                        clientPush.DefaultRequestHeaders.Accept.Clear();
                        clientPush.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        //HTTP POST 
                        string data2 = string.Format("token={0}&id_token={1}&username={2}&password={3}", key.access_token, key.id_token, username, password);
                        HttpResponseMessage response2 = clientPush.PostAsJsonAsync("api/egw/nhanLichSuKCB?" + data2,
                            new ApiTheBHYT
                            {
                                maThe = txtMaThe.Text.Trim(),
                                hoTen = txtTenBenhNhan.Text.Trim(),
                                ngaySinh = dtpNgaySinh.Value.ToString("dd/MM/yyyy"),
                                gioiTinh = (short) (cboGioiTinh.SelectedIndex == 1?2:1),
                                maCSKCB = txtMaKCBBD.Text,
                                ngayBD = dtpNgayBD.Value.ToString("dd/MM/yyyy"),
                                ngayKT = dtpNgayKT.Value.ToString("dd/MM/yyyy"),
                            }).Result;
                        if (response2.IsSuccessStatusCode)
                        {
                            string result = response2.Content.ReadAsStringAsync().Result;
                            try
                            {
                                var kqua = (KQNhanLichSuKCB) JsonConvert.DeserializeObject<KQNhanLichSuKCB>(result);

                                switch (kqua.maKetQua)
                                {
                                    case "00":
                                        lblMess.Text = @"Thông tin thẻ chính xác!";
                                        lblMess.ForeColor = Color.DarkBlue;
                                       
                                        break;
                                    case "01":
                                        lblMess.Text = @"Thẻ hết giá trị sử dụng!";
                                        lblMess.ForeColor = Color.Red;
                                        break;
                                    case "02":
                                        lblMess.Text = @"Khám chữa bệnh khi chưa đến hạn!";
                                        lblMess.ForeColor = Color.Red;
                                        break;
                                    case "03":
                                        lblMess.Text = @"Hết hạn thẻ khi chưa ra viện!";
                                        lblMess.ForeColor = Color.Red;
                                        break;
                                    case "04":
                                        lblMess.Text = @"Thẻ có giá trị khi đang nằm viện!";
                                        lblMess.ForeColor = Color.DarkBlue;
                                        break;
                                    case "05":
                                        lblMess.Text = @"Mã thẻ không có trong dữ liệu thẻ!";
                                        lblMess.ForeColor = Color.Red;
                                        break;
                                    case "06":
                                        lblMess.Text = @"Thẻ sai họ tên!";
                                        lblMess.ForeColor = Color.Red;
                                        break;
                                    case "07":
                                        lblMess.Text = @"Thẻ sai ngày sinh!";
                                        lblMess.ForeColor = Color.Red;
                                        break;
                                    case "08":
                                        lblMess.Text = @"Thẻ sai giới tính";
                                        lblMess.ForeColor = Color.Red;
                                        break;
                                    case "09":
                                        lblMess.Text = @"Thẻ sai nơi đăng ký KCB ban đầu!";
                                        lblMess.ForeColor = Color.Red;
                                        break;
                                    case "401":
                                        lblMess.Text = @"Lỗi không xác thực!";
                                        lblMess.ForeColor = Color.Red;
                                        break;
                                    case "500":
                                        lblMess.Text = @"An unexpectec error occurred";
                                        lblMess.ForeColor = Color.Red;
                                        break;
                                    default:
                                        lblMess.Text = @"Mã thẻ không có trong dữ liệu thẻ!";
                                        lblMess.ForeColor = Color.Red;
                                        break;

                                }
                                grdLichSuKhamBenh.DataSource = kqua.dsLichSuKCB;
                                //if (kqua.maKetQua == "00")
                                //{
                                //    
                                //    lblMess.Text = "Thông tin thẻ chính xác!";
                                //    //var lst = kqua.dsLichSuKCB;
                                //    //foreach (var it in lst)
                                //    //    TestNhanHoSoKcbChiTiet(it.maHoSo);
                                //}
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(@"Lỗi không xác thực" + ex.Message, @"Thông Báo");
                                //  throw;
                            }
                            finally
                            {
                                txtQrCode.Clear();
                            }
                        }
                    }
                }
            }
        }
        public static string ConvertHexStrToUnicode(string hexString)
        {
            int length = hexString.Length;
            byte[] bytes = new byte[length / 2];

            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return Encoding.UTF8.GetString(bytes);
        }
        private void txtQrCode_TextChanged(object sender, EventArgs e)
        {
            if (txtQrCode.Text.EndsWith("$"))
            {
                string[] qrcode = txtQrCode.Text.Split('|');
                var list = qrcode.ToList();
                dtpNgayBD.IsNullDate = false;
                dtpNgayKT.IsNullDate = false;
                dtpNgaySinh.IsNullDate = false;
                txtTenBenhNhan.Text = ConvertHexStrToUnicode(list[1]);
                txtMaKCBBD.Text = list[5].Substring(0, 2) + list[5].Substring(5, 3);
                txtMaThe.Text = list[0].ToUpper();
                dtpNgaySinh.Value = list[2].Length <= 4 ? DateTime.ParseExact(list[2], "yyyy", null) : Convert.ToDateTime(list[2]);
                cboGioiTinh.SelectedIndex = Convert.ToInt16(list[3]) == 2 ? 1 : 0;
                dtpNgayBD.Value = Convert.ToDateTime(list[6]);
                
                dtpNgayKT.Value = Convert.ToDateTime(list[7]);
                cmdCheck.PerformClick();
               
            }
        }

        private bool Isvalid()
        {
            if (string.IsNullOrEmpty(txtMaThe.Text))
            {
                txtMaThe.Focus();
             //   MessageBox.Show("Mã thẻ không được để trống!", "Thông Báo", MessageBoxIcon.Warning);
            }
            return true;
        }
        private void ClearControl()
        {
            txtQrCode.Clear();
            txtMaThe.Clear();
            txtTenBenhNhan.Clear();
            dtpNgayBD.IsNullDate = true;
            dtpNgayKT.IsNullDate = true;
            dtpNgaySinh.IsNullDate = true;
            txtQrCode.Focus();
            lblMess.Text = "";
            lblMess.ForeColor = Color.DarkBlue;
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            ClearControl();
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.N) ClearControl();
            if(e.Control && e.KeyCode == Keys.F) cmdCheck.PerformClick();
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
            }
        }
    }
}
