using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Test;

namespace CheckHoSoThongTuyen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static string CreateMd5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        private static void TestNhanHoSoKcbChiTiet(long id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://egw.baohiemxahoi.gov.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string username = "01010_BV";
                string password = CreateMd5("tckt1234");
                // HTTP POST
                ApiToken input = new ApiToken { username = username, password = password };
                var values = new Dictionary<string, string>
                {
                   { "username", username },
                   { "password", password }
                };
                var content = new FormUrlEncodedContent(values);
                //var data = string.Format("username={0}&password={1}", username, password);
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
                        string data2 = string.Format("token={0}&id_token={1}&username={2}&password={3}&maHoSo={4}", key.access_token, key.id_token, username, password, id);
                        HttpResponseMessage response2 = clientPush.PostAsJsonAsync("api/egw/nhanHoSoKCBChiTiet?" + data2, new { }).Result;
                        if (response2.IsSuccessStatusCode)
                        {
                            string result = response2.Content.ReadAsStringAsync().Result;
                            try
                            {
                                var kqua = (KQNhanHoSoKCBChiTiet)JsonConvert.DeserializeObject<KQNhanHoSoKCBChiTiet>(result);
                                if (kqua.maKetQua == "200")
                                {
                                    var kq = kqua.hoSoKCB;
                                }
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://egw.baohiemxahoi.gov.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string username = "01010_BV";
                string password = CreateMd5("tckt1234");
                // HTTP POST
                ApiToken input = new ApiToken { username = username, password = password };
                var values = new Dictionary<string, string>
                {
                   { "username", username },
                   { "password", password }
                };
                var content = new FormUrlEncodedContent(values);
                //var data = string.Format("username={0}&password={1}", username, password);
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
                                maThe = "HT2010600108610",
                                hoTen = "VŨ THỊ THE",
                                ngaySinh = "01/01/1946",
                                gioiTinh = 2,
                                maCSKCB = "01010",
                                ngayBD = "01/01/2016",
                                ngayKT = "31/12/2019"
                            }).Result;
                        if (response2.IsSuccessStatusCode)
                        {
                            string result = response2.Content.ReadAsStringAsync().Result;
                            try
                            {
                                var kqua = (KQNhanLichSuKCB)JsonConvert.DeserializeObject<KQNhanLichSuKCB>(result);
                                if (kqua.maKetQua == "00")
                                {
                                    dataGridView1.DataSource = kqua.dsLichSuKCB;
                                    //var lst = kqua.dsLichSuKCB;
                                    //foreach (var it in lst)
                                    //    TestNhanHoSoKcbChiTiet(it.maHoSo);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.ReadLine();
                                //  throw;
                            }
                        }
                    }
                }
            }
        }
    }
}
