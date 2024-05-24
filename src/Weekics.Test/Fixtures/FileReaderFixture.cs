namespace Weekics.Test.Fixtures;

public static class FileReaderFixture
{
    internal static string CreateTempFile(string content)
    {
        var path = Path.GetTempFileName();
        File.WriteAllText(path, content);
        return path;
    }

    internal static void CleanupTempFile(string path)
    {
        File.Delete(path);
    }
}

