using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;


//１行ずつ処理する
public class LineProcesser
{
    public string GetTargetFolder(string targetFoderConfigPath)
    {
        // 設定したターゲットフォルダーを読み込む
        StreamReader sr = new StreamReader(targetFoderConfigPath);
        string targetFolder = sr.ReadLine();
        return targetFolder;
    }
    public string GetTargetFilePaths(string targetFileConfigPath, string targetFolder)
    {
        string line;
        string targetFilePaths = "";

        FilePathLocator filePathLocator = new FilePathLocator();

        // 読み込むテキストファイル
        StreamReader sr = new StreamReader(targetFileConfigPath);

        // 最初の行を読み込む
        line = sr.ReadLine();

        // ファイルの終わりまで読み込む
        while (line != null)
        {
            targetFilePaths += filePathLocator.FindFilePaths(targetFolder, line);
            //次の行
            line = sr.ReadLine();
        }
        sr.Close();

        return targetFilePaths;
    }
}


//ファイルがどこにあるのかを探し、そのパスを返す
public class FilePathLocator
{
    public string FindFilePaths(string targetFolderConfigPath, string targetFileName)
    {
        string result = "";

        // フォルダ内の全てのファイルパスを取得
        string[] filePaths = Directory.GetFiles(targetFolderConfigPath, targetFileName, SearchOption.AllDirectories);

        // ファイルが見つからなかった場合はパスを表示し、見つからなかった場合はメッセージを表示
        if (filePaths.Length > 0)
        {
            Console.WriteLine($"以下のパスに{targetFileName}が見つかりました");
            foreach (string filePath in filePaths)
            {
                Console.WriteLine($"{filePath}");
                result += $"{targetFileName},{filePath}\n";
            }
        }
        else
        {
            Console.WriteLine($"{targetFileName}は見つかりませんでした");
            result = $"{targetFileName},指定したファイルは見つかりませんでした";
        }

        return result;
    }
}

public class TextWrite
{
    public void OutPutText(string outPutFile, string outTxt)
    {
        using (StreamWriter sw = new StreamWriter(outPutFile, false, Encoding.GetEncoding("shift_jis")))
        {
            sw.WriteLine(outTxt);
        }
    }
}


class Program
{
    static void Main()
    {
        // コンフィグファイルの場所とアウトプットファイルの場所を必要に応じて編集してください
        string currentDirectory = Directory.GetCurrentDirectory();
        string configDirectory = Path.Combine(currentDirectory, "config");
        string outputDirectory = Path.Combine(currentDirectory, "OutPut");

        string targetFileConfigPath = Path.Combine(configDirectory, "targetFileNames.txt");
        string targetFolderConfigPath = Path.Combine(configDirectory, "targetFolder.txt");
        string outPutFile = Path.Combine(outputDirectory, "outPut.csv");

        LineProcesser lineProcesser= new LineProcesser();
        string targetFolder = lineProcesser.GetTargetFolder(targetFolderConfigPath);
        string outTxt = lineProcesser.GetTargetFilePaths(targetFileConfigPath, targetFolder);
        outTxt = $"ファイル名,フルパス\n{outTxt}";
        TextWrite textWrite = new TextWrite();
        textWrite.OutPutText(outPutFile, outTxt);

        Console.WriteLine("完了しました。何かキーを教えてください...");
        Console.ReadKey(); // ユーザーが何かキーを押すまで待機
    }
}