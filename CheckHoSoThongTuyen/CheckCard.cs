using System;
using System.Collections.Generic;
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
    public class CheckCard
    {
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
        public void CheckTheThongTuyen(string maThe, string hoTen, string ngaySinh, short gioiTinh,
                                        string maCskcb, string ngayBd, string ngayKt, string userName, string passWord, ref string messge, ref string maloi)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://egw.baohiemxahoi.gov.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                 string username = userName;
                string password = CreateMd5(passWord);
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
                                maThe = maThe,
                                hoTen = hoTen,
                                ngaySinh = ngaySinh,
                                gioiTinh =  gioiTinh,
                                maCSKCB = maCskcb,
                                ngayBD = ngayBd,
                                ngayKT = ngayKt,
                            }).Result;
                        if (response2.IsSuccessStatusCode)
                        {
                            string result = response2.Content.ReadAsStringAsync().Result;
                            try
                            {
                                var kqua = (KQNhanLichSuKCB)JsonConvert.DeserializeObject<KQNhanLichSuKCB>(result);
                                maloi = kqua.maKetQua;
                                switch (kqua.maKetQua)
                                {
                                    case "00":
                                       messge = @"Thông tin thẻ chính xác!";
                                        break;
                                    case "01":
                                        messge = @"Thẻ hết giá trị sử dụng!";
                                        break;
                                    case "02":
                                        messge = @"Khám chữa bệnh khi chưa đến hạn!";
                                        break;
                                    case "03":
                                        messge = @"Hết hạn thẻ khi chưa ra viện!";
                                        break;
                                    case "04":
                                        messge = @"Thẻ có giá trị khi đang nằm viện!";
                                        break;
                                    case "05":
                                        messge = @"Mã thẻ không có trong dữ liệu thẻ!";
                                        break;
                                    case "06":
                                        messge = @"Thẻ sai họ tên!";
                                        break;
                                    case "07":
                                        messge = @"Thẻ sai ngày sinh!";
                                        break;
                                    case "08":
                                        messge = @"Thẻ sai giới tính";
                                        break;
                                    case "09":
                                        messge = @"Thẻ sai nơi đăng ký KCB ban đầu!";
                                        break;
                                    case "401":
                                        messge = @"Lỗi không xác thực!";
                                        break;
                                    case "500":
                                        messge = @"An unexpectec error occurred";
                                        break;
                                    default:
                                        messge = @"Mã thẻ không có trong dữ liệu thẻ!";
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(@"Lỗi không xác thực" + ex.Message, @"Thông Báo");
                                //  throw;
                            }
                            finally
                            {
                            }
                        }
                    }
                }
            }
        }
    }
}
