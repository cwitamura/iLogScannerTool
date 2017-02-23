using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;

namespace iLogScannerTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Process pro = new Process();
                        
            // コマンド名
            pro.StartInfo.FileName = ConfigurationManager.AppSettings["CommondPath"];
            var arguments = string.Format(@"mode={0} logtype={1} accesslog={2}\u_ex{3}.log outdir={4} reporttype={5} level={6}"
                , ConfigurationManager.AppSettings["Mode"]
                , ConfigurationManager.AppSettings["LogType"]
                , ConfigurationManager.AppSettings["AccessLog"]
                , DateTime.Now.AddDays(-1).ToString("yyMMdd") // 一日前のログを取得
                , ConfigurationManager.AppSettings["OutDir"]
                , ConfigurationManager.AppSettings["ReportType"]
                , ConfigurationManager.AppSettings["Level"]);

            // 引数
            pro.StartInfo.Arguments = arguments;
            // DOSプロンプトの黒い画面を非表示
            pro.StartInfo.CreateNoWindow = true;
            // プロセスを新しいウィンドウで起動するか否か
            pro.StartInfo.UseShellExecute = false;
            // 標準出力をリダイレクトして取得したい
            pro.StartInfo.RedirectStandardOutput = true;
            // 実行
            pro.Start();

            // 出力を取得する
            var output = pro.StandardOutput.ReadToEnd();
            
        }
    }
}
