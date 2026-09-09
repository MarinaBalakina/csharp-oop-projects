namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Access;

public interface IFileSystem
{
    bool FileExists(string path);

    bool DirectoryExists(string path);

    string ReadFile(string path);

    void CopyFile(string source, string destination);

    void DeleteFile(string path);

    void MoveFile(string source, string destination);

    void RenameFile(string path, string newName);

    IEnumerable<string> GetDirectories(string path);

    IEnumerable<string> GetFiles(string path);
}
