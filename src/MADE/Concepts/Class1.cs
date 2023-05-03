using System.Net;
using System.Security.Cryptography;

namespace Concepts;

public class FileAction
{
    public FileAction(string name, string destinationPath, string sourcePath)
    {
        Name = name;
        DestinationPath = destinationPath;
        SourcePath = sourcePath;
    }

    public string Name { get; set; }
    public string DestinationPath { get; set; }
    public string SourcePath { get; set; }
}

public class Class1
{
    async Task<List<FileAction>> Work()
    {
        string sourcePath = "source";
        string destinationPath = "destination";

        Dictionary<byte[], string> sourceFiles = await GetFiles(sourcePath);
        Dictionary<byte[], string> destinationFiles = await GetFiles(destinationPath);

        List<FileAction> actions = GenerateActions(sourceFiles, destinationFiles, destinationPath);

        return actions;
    }

    private async Task<Dictionary<byte[], string>> GetFiles(string sourcePath)
    {
        Dictionary<byte[], string> files = new Dictionary<byte[], string>();
        var sourceFilePaths = Directory.GetFiles(sourcePath);
        foreach (var f in sourceFilePaths)
        {
            var content = await File.ReadAllBytesAsync(f);
            MD5 md5 = MD5.Create();
            var hash = md5.ComputeHash(content);
            
            files.Add(hash, Path.Combine(sourcePath, f));
        }

        return files;
    }

    private List<FileAction> GenerateActions(Dictionary<byte[], string> sourceFiles, Dictionary<byte[], string> destinationFiles, string destinationPath)
    {
        List<FileAction> actions = new();
        foreach (var sf in sourceFiles)
        {
            if (!destinationFiles.ContainsKey(sf.Key))
            {
                actions.Add(new FileAction("COPY", Path.Combine(destinationPath, Path.GetFileName(sf.Value)), sf.Value));
                continue;
            }

            if (destinationFiles[sf.Key] != sf.Value)
            {
                actions.Add(new FileAction("MOVE", Path.Combine(destinationPath, Path.GetFileName(sf.Value)), destinationFiles[sf.Key]));
            }
        }

        foreach (var df in destinationFiles)
        {
            actions.Add(new FileAction("REMOVE", df.Value, destinationPath));
        }

        return actions;
    }
}