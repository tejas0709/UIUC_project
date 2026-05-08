using System.Globalization;
using UiucTakeHome.Lib.Services;

var showHelp = args.Any(arg => string.Equals(arg, "--help", StringComparison.OrdinalIgnoreCase) || string.Equals(arg, "-h", StringComparison.OrdinalIgnoreCase));
if (showHelp)
{
	PrintHelp();
	return 0;
}

var folder = ResolveFolder(args);
var verbose = args.Any(arg => string.Equals(arg, "--verbose", StringComparison.OrdinalIgnoreCase) || string.Equals(arg, "-v", StringComparison.OrdinalIgnoreCase));

if (!Directory.Exists(folder))
{
	Console.WriteLine($"Error: Folder not found: {folder}");
	return 1;
}

Console.WriteLine($"Reading spreadsheets from: {Path.GetFullPath(folder)}\n");

var parser = new SubawardParser();
var allEntries = new List<UiucTakeHome.Lib.Models.SubawardEntry>();

foreach (var file in FileEnumerator.GetExcelFiles(folder))
{
	Console.WriteLine($"{Path.GetFileName(file)}");
	var entries = parser.ParseFile(file).ToList();

	if (!entries.Any())
	{
		Console.WriteLine("  No subawards found.\n");
		continue;
	}

	foreach (var e in entries.OrderBy(e => e.Subrecipient, StringComparer.OrdinalIgnoreCase))
	{
		Console.WriteLine($"  Subaward: {e.Subrecipient}");
		if (verbose)
			Console.WriteLine($"    Amount: {e.Amount.ToString("C", CultureInfo.CurrentCulture)}");
	}

	allEntries.AddRange(entries);
	Console.WriteLine();
}

if (!allEntries.Any())
{
	Console.WriteLine("No subawards found across any spreadsheets.");
	return 0;
}

Console.WriteLine("Distinct subrecipients and total subaward amounts:");
var totals = allEntries
	.GroupBy(e => e.Subrecipient, StringComparer.OrdinalIgnoreCase)
	.Select(g => new { Name = g.Key, Total = g.Sum(x => x.Amount) })
	.OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
	.ToList();

foreach (var item in totals)
	Console.WriteLine($"  {item.Name,-24} {item.Total.ToString("C", CultureInfo.CurrentCulture)}");

return 0;

static string ResolveFolder(string[] args)
{
	var folderIndex = Array.FindIndex(args, arg => string.Equals(arg, "--folder", StringComparison.OrdinalIgnoreCase) || string.Equals(arg, "-f", StringComparison.OrdinalIgnoreCase));
	return folderIndex >= 0 && folderIndex + 1 < args.Length ? args[folderIndex + 1] : "samples";
}

static void PrintHelp()
{
	Console.WriteLine("Usage:");
	Console.WriteLine("  dotnet run --project src/UiucTakeHome.Console -- --folder ./samples [--verbose]");
	Console.WriteLine();
	Console.WriteLine("Options:");
	Console.WriteLine("  -f, --folder   Folder containing .xlsx files (default: samples)");
	Console.WriteLine("  -v, --verbose  Show amounts for each file listing");
	Console.WriteLine("  -h, --help     Show this help message");
}
