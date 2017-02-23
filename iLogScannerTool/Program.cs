using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;
using System.Net.Mail;

namespace iLogScannerTool
{
    class Program
    {
        private　static void Main(string[] args)
        {
            try
            {
            
                Process pro = new Process();

                // コマンド名
                pro.StartInfo.FileName = ConfigurationManager.AppSettings["CommondPath"];
                // 引数
                var arguments = string.Format(@"mode={0} logtype={1} accesslog={2}\u_ex{3}.log outdir={4} reporttype={5} level={6}"
                    , ConfigurationManager.AppSettings["Mode"]
                    , ConfigurationManager.AppSettings["LogType"]
                    , ConfigurationManager.AppSettings["AccessLog"]
                    , DateTime.Now.AddDays(-1).ToString("yyMMdd") // 一日前のログを取得
                    , ConfigurationManager.AppSettings["OutDir"]
                    , ConfigurationManager.AppSettings["ReportType"]
                    , ConfigurationManager.AppSettings["Level"]);
                pro.StartInfo.Arguments = arguments;            
                // 実行
                pro.Start();
                // メール送信
                SendMail(ConfigurationManager.AppSettings["From"]
                    , ConfigurationManager.AppSettings["To"]
                    , ConfigurationManager.AppSettings["Subject"]
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
