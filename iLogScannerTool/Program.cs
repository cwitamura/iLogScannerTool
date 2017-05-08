using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace iLogScannerTool
{
    class Program
    {
        private　static void Main(string[] args)
        {
            var connection = new SqlConnection();
            var command = new SqlCommand();
            var dt = new DataTable();

            try
            {
                // 接続します。
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                connection.Open();
                
                // 取得したデータの行数を取得します。
                var rowCount = 0;
                try
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM MSTPMarkIisLog WHERE Active = 1";

                        adapter.SelectCommand = command;
                        adapter.Fill(dt);

                        rowCount = dt.Rows.Count;
                    }
                }
                finally
                {
                    // 接続を解除します。
                    connection.Close();
                }
                
                // プロセスの開始
                var process = new Process();
                
                // コマンド名の取得
                process.StartInfo.FileName = ConfigurationManager.AppSettings["CommondPath"];

                connection.Open();
                // アクティブなもののみ抽出します。
                command.CommandText = "SELECT * FROM MSTPMarkIisLog WHERE Active = 1";
                command.Connection = connection;

                // SQLを実行します。
                var readers = command.ExecuteReader();

                // 取得したデータ分繰り返します。
                for (int i = 0; i < rowCount; i++)
                {
                    readers.Read();

                    var siteId = readers.GetValue(1);
                    var serverName = readers.GetValue(2);
                    var logDir = readers.GetValue(3);

                    // ファイルの置き場所を作成
                    var outPutDir = Path.Combine(ConfigurationManager.AppSettings["OutDir"], DateTime.Now.ToString("yyyyMMdd"), String.Format("{0}_{1}", siteId, serverName));
                    Directory.CreateDirectory(outPutDir);

                    // 一日前のログを選択する
                    var accessLog = String.Format(@"{0}u_ex{1}.log", logDir, DateTime.Now.AddDays(-1).ToString("yyMMdd"));

                    // iLogScannerの引数を設定する
                    process.StartInfo.Arguments = String.Format(@"mode={0} logtype={1} accesslog={2} outdir={3} reporttype={4} level={5}"
                        , ConfigurationManager.AppSettings["Mode"]
                        , ConfigurationManager.AppSettings["LogType"]
                        , accessLog
                        , outPutDir
                        , ConfigurationManager.AppSettings["ReportType"]
                        , ConfigurationManager.AppSettings["Level"]);

                    // iLogScannerを実行
                    process.Start();                  
                }

                // メールを送信します。
                SendMail(ConfigurationManager.AppSettings["From"]
                    , ConfigurationManager.AppSettings["To"]
                    , String.Format(ConfigurationManager.AppSettings["Subject"], DateTime.Now.ToString("yyyy/MM/dd"))
                    , ConfigurationManager.AppSettings["body"]);
            }
            catch (Exception ex)
            {
                SendMail(ConfigurationManager.AppSettings["ErrorFrom"]
                    , ConfigurationManager.AppSettings["ErrorTo"]
                    , "Logエラー"
                    , ex.StackTrace
                    );
            }
            finally
            {
                // 接続を解除します。
                connection.Close();
            }
        }

        /// <summary>
        /// メールを送信する
        /// </summary>
        private static void SendMail(string from, string to, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["SMTPHost"]);
            smtpClient.Send(from, to, subject, body);
            smtpClient.Dispose();
        }
    }
}
