using System;
using System.IO;
using System.Text;


//１行ずつ処理する
public class LineProcesser
{
    public static string GetTargetFolder(string targetFoderConfigPath)
    {
        // 設定したターゲットフォルダーを読み込む
        StreamReader sr = new StreamReader(targetFoderConfigPath);
        string targetFolder = sr.ReadLine();
        return targetFolder;
    }
    public static string GetTargetFilePaths(string targetFileConfigPath, string targetFolder)
    {
        StringBuilder sb = new StringBuilder();

        // 読み込むテキストファイル
        string[] readText = File.ReadAllLines(targetFileConfigPath);

        
        // ファイルの終わりまで読み込む
        foreach(string s in readText)
        {
            sb.Append(FilePathLocator.FindFilePaths(targetFolder, s) + "\n");
        }


        return sb.ToString();
    }
}


//ファイルがどこにあるのかを探し、そのパスを返す
public class FilePathLocator
{
    public static string FindFilePaths(string targetFolderConfigPath, string targetFileName)
    {
        StringBuilder sb = new StringBuilder();

        // フォルダ内の全てのファイルパスを取得
        string[] filePaths = Directory.GetFiles(targetFolderConfigPath, targetFileName, SearchOption.AllDirectories);

        // ファイルが見つからなかった場合はパスを表示し、見つからなかった場合はメッセージを表示
        if (filePaths.Length > 0)
        {
            Console.WriteLine($"以下のパスに{targetFileName}が見つかりました");
            foreach (string filePath in filePaths)
            {
                Console.WriteLine($"{filePath}");
                sb.Append($"{targetFileName},{filePath}\n");
            }
        }
        else
        {
            Console.WriteLine($"{targetFileName}は見つかりませんでした");
            sb.Append($"{targetFileName},指定したファイルは見つかりませんでした");
        }

        return sb.ToString();
    }
}

public class TextWrite
{
    public static void OutPutText(string outPutFile, string outTxt)
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

        string targetFolder = LineProcesser.GetTargetFolder(targetFolderConfigPath);
        string outTxt = LineProcesser.GetTargetFilePaths(targetFileConfigPath, targetFolder);
        outTxt = $"ファイル名,フルパス\n{outTxt}";
        TextWrite.OutPutText(outPutFile, outTxt);

        Console.WriteLine("完了しました。何かキーを教えてください...");
        Console.ReadKey(); // ユーザーが何かキーを押すまで待機
    }
}