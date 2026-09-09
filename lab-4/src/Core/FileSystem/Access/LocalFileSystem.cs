namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;

public class LocalFileSystem : IFileSystem
{
    public bool FileExists(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        return File.Exists(path);
    }

    public bool DirectoryExists(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        return Directory.Exists(path);
    }

    public string ReadFile(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        return File.ReadAllText(path);
    }

    public void CopyFile(string source, string destination)
    {
        ArgumentNullException.ThrowIfNull(source);

        ArgumentNullException.ThrowIfNull(destination);

        if (!FileExists(source))
        {
            throw new FileNotFoundException("File not found", source);
        }

        if (!DirectoryExists(destination))
        {
            throw new DirectoryNotFoundException("Directory not found");
        }

        string fileName = Path.GetFileName(source);
        string directoryName = Path.Combine(destination, fileName);

        if (FileExists(directoryName)) throw new IOException("File already exists");

        File.Copy(source, directoryName, overwrite: false);
    }

    public void DeleteFile(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        if (!FileExists(path))
        {
            throw new FileNotFoundException("File not found", path);
        }

        File.Delete(path);
    }

    public void MoveFile(string source, string destination)
    {
        ArgumentNullException.ThrowIfNull(source);

        ArgumentNullException.ThrowIfNull(destination);

        if (!FileExists(source))
        {
            throw new FileNotFoundException("File not found", source);
        }

        if (!DirectoryExists(destination))
        {
            throw new DirectoryNotFoundException("Directory not found");
        }

        string fileName = Path.GetFileName(source);
        string directoryName = Path.Combine(destination, fileName);

        if (FileExists(directoryName)) throw new IOException("File already exists");

        File.Move(source, directoryName);
    }

    public void RenameFile(string path, string newName)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(newName);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException("File not found", path);
        }

        string? directoryName = Path.GetDirectoryName(path);
        if (!Directory.Exists(directoryName))
        {
            throw new DirectoryNotFoundException("Directory not found");
        }

        string newPath = Path.Combine(directoryName, newName);

        if (FileExists(newPath)) throw new IOException("File already exists");

        File.Move(path, newPath);
    }

    public IEnumerable<string> GetDirectories(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        return Directory.GetDirectories(path);
    }

    public IEnumerable<string> GetFiles(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        return Directory.GetFiles(path);
    }
}