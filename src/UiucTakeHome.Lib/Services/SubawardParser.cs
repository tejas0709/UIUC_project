using System.Globalization;
using ClosedXML.Excel;
using UiucTakeHome.Lib.Models;

namespace UiucTakeHome.Lib.Services;

public class SubawardParser
{
    public IEnumerable<SubawardEntry> ParseFile(string path)
    {
        using var wb = new XLWorkbook(path);
        foreach (var ws in wb.Worksheets)
        {
            foreach (var row in ws.RowsUsed())
            {
                var cells = row.CellsUsed().ToList();
                if (!cells.Any())
                    continue;

                var labelIndex = cells.FindIndex(c => c.GetString().Contains("Subaward:", StringComparison.OrdinalIgnoreCase));
                if (labelIndex < 0)
                    continue;

                var labelCell = cells[labelIndex];
                var remainder = labelCell.GetString();
                remainder = remainder[(remainder.IndexOf("Subaward:", StringComparison.OrdinalIgnoreCase) + "Subaward:".Length)..].Trim();

                var name = remainder;
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = cells.Skip(labelIndex + 1)
                        .Select(c => c.GetString().Trim())
                        .FirstOrDefault(text => !string.IsNullOrWhiteSpace(text) && !text.Equals("Subaward:", StringComparison.OrdinalIgnoreCase)) ?? string.Empty;
                }

                if (string.IsNullOrWhiteSpace(name))
                    continue;

                var amount = ExtractAmount(cells);
                yield return new SubawardEntry(name, amount, Path.GetFileName(path));
            }
        }
    }

    private static decimal ExtractAmount(IReadOnlyList<IXLCell> cells)
    {
        foreach (var cell in cells)
        {
            var text = cell.GetString().Trim();
            if (decimal.TryParse(text, NumberStyles.Currency | NumberStyles.Number, CultureInfo.InvariantCulture, out var amount) ||
                decimal.TryParse(text, NumberStyles.Currency | NumberStyles.Number, CultureInfo.CurrentCulture, out amount))
            {
                return amount;
            }
        }

        return 0m;
    }
}
