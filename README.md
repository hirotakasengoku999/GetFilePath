# ファイルパス取得プログラム
このプログラムは、指定されたフォルダ内のファイルパスを取得し、CSVファイルに出力するためのコンソールアプリケーションです。
C＃で作成されています。

# 機能
- 指定したフォルダ内の指定したファイル名を持つファイルのパスを取得します。
- 取得したファイルパスをCSV形式で指定された出力ファイルに保存します。

# 使用方法
1. configフォルダに以下の設定ファイルを配置します：

    - targetFileNames.txt: 取得したいファイル名のリストを1行ずつ記述します。
    - targetFolder.txt: ファイルを検索するフォルダのパスを1行で指定します。
2. コンソールでプログラムを実行します。

3. 実行後、プログラムが指定された出力ファイルにファイルパスを保存します。

4. プログラムが完了したことを示すメッセージが表示され、何かキーを押すまでコンソールが開いたままになります。

# 注意事項
- 設定ファイルのパスや出力ファイルのパスはプログラム内で指定されています。必要に応じて変更してください。
- 出力ファイルの文字エンコーディングはShift_JISに設定されています。必要に応じて変更してください。
