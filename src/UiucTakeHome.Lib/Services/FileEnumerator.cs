using System.IO;

namespace UiucTakeHome.Lib.Services;

public static class FileEnumerator
{
    public static IEnumerable<string> GetExcelFiles(string folder)
    {
        if (string.IsNullOrWhiteSpace(folder)) folder = Directory.GetCurrentDirectory();
        if (!Directory.Exists(folder)) yield break;
        foreach (var f in Directory.EnumerateFiles(folder, "*.xlsx", SearchOption.TopDirectoryOnly))
            yield return f;
    }
}
