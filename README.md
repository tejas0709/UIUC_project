# Subaward Aggregator

A .NET console app for extracting and aggregating subaward data from Excel spreadsheets.

## Usage

```bash
dotnet run --project src/UiucTakeHome.Console -- --folder ./samples
```

Add `--verbose` to show amounts under each file listing.

Example:

```text
SubawardBudgetExample1.xlsx
	Subaward: Indiana
	Subaward: Mayo

Distinct subrecipients and total subaward amounts:
	Indiana                 $25,000.00
```

## Testing

```bash
dotnet test
```

## Notes

- This project uses ClosedXML for Excel parsing.
- If your environment surfaces NuGet security warnings, update dependencies as needed.

## Assumptions & Questions

See `ASSUMPTIONS.md`.
