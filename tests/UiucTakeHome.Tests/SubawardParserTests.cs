using System;
using System.IO;
using System.Linq;
using UiucTakeHome.Lib.Services;
using Xunit;

namespace UiucTakeHome.Tests;

public class SubawardParserTests
{
    [Fact]
    public void Example1_HasFourSubrecipients()
    {
        var repoRoot = FindRepoRoot();
        var path = Path.Combine(repoRoot, "samples", "SubawardBudgetExample1.xlsx");

        var parser = new SubawardParser();
        var names = parser.ParseFile(path)
            .Select(e => e.Subrecipient)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        Assert.Contains("Indiana", names, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("Mayo", names, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("Purdue", names, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("Florida", names, StringComparer.OrdinalIgnoreCase);
    }

    private static string FindRepoRoot()
    {
        var current = new DirectoryInfo(AppContext.BaseDirectory);
        while (current is not null && !File.Exists(Path.Combine(current.FullName, "README.md")))
        {
            current = current.Parent;
        }

        if (current is null)
            throw new InvalidOperationException("Could not locate repository root.");

        return current.FullName;
    }
}
