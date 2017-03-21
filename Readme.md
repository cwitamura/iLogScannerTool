## iLogScannerToolの使用方法

実行日の1日前のログをiLogScannerToolを利用して出力することができます。
正常終了した場合は、メールを通知することができます。

- 初期設定
App.Configに以下を設定します。

	- コマンドパラメータ
|Key|Value|説明|
|:--|:--|:--|
|Mode|gui / cui|下記URL参照|
|LogType|apache / iis / iis_w3c / ssh / vsftpd / wu-ftpd|下記URL参照|
|AccessLog|アクセスログファイル名※カンマ区切りで複数指定可|下記URL参照|
|OutDir|レポートの出力先ディレクトリを指定|下記URL参照|
|ReportType|html / text / xml / all|下記URL参照|
|Level|standard / detail|下記URL参照|
https://www.ipa.go.jp/security/vuln/iLogScanner/app/offline.html

	- 正常メールパラメータ
|Key|Value|説明|
|:--|:--|:--|
|From|from@gmail.com|送信元アドレス|
|To|to@gmail.com|送信先アドレス|
|Subject|ログを出力しました。|題名|
|body|ログを確認して下さい。|本文|
|SMTPHost|192.168.1.1|ホスト名|
|ErrorTo|errorto@gmail.com|送信元アドレス（エラー時）|
|Errorfrom|errorto@gmail.com|送信先アドレス（エラー時）|

