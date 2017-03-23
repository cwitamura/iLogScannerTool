## iLogScannerToolの使用方法

実行日の1日前のログをiLogScannerToolを利用して出力することができます。
正常終了した場合は、メールを通知することができます。

- 初期設定

App.Configに以下を設定します。

|Key|Value|説明|
|:--|:--|:--|
|Mode|cui|下記URL参照|
|LogType|apache|下記URL参照|
|AccessLog|アクセスログファイル名※カンマ区切りで複数指定可|下記URL参照|
|OutDir|レポートの出力先ディレクトリを指定|下記URL参照|
|ReportType|html|下記URL参照|
|Level|standard|下記URL参照|

メール系の設定

|Key|Value|説明|
|:--|:--|:--|
|From|from@gmail.com|送信元アドレス|
|To|to@gmail.com|送信先アドレス|
|Subject|yy/mm/ddのログを出力しました。|題名|
|body|ログを確認して下さい。|本文|
|SMTPHost|192.168.1.1|ホスト名|
|ErrorTo|errorto@gmail.com|送信元アドレス（エラー時）|
|Errorfrom|errorfrom@gmail.com|送信先アドレス（エラー時）|

