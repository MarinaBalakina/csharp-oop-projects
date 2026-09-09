namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.State;

public class FileSystemStatus
{
    public bool IsConnected { get; private set; }

    public string? RootPath { get; private set; }

    public string LocalPath { get; private set; }

    public FileSystemMode Mode { get; private set; }

    public FileSystemStatus()
    {
        RootPath = null;
        LocalPath = ".";
        Mode = FileSystemMode.Local;
        IsConnected = false;
    }

    public void Connect(string rootPath, FileSystemMode mode)
    {
        ArgumentNullException.ThrowIfNull(rootPath);

        RootPath = rootPath;
        LocalPath = ".";
        Mode = mode;
        IsConnected = true;
    }

    public void Disconnect()
    {
        IsConnected = false;
        LocalPath = ".";
        RootPath = null;
    }

    public void SetLocalPath(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        LocalPath = path;
    }
}