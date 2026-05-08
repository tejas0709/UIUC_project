# Subaward Aggregator

A .NET console app for extracting and aggregating subaward data from Excel spreadsheets.

## Quick Start (non-technical)

1. Install the .NET 10 SDK from Microsoft.
2. Open a terminal (PowerShell on Windows).
3. In the project folder, run the command below to process the sample spreadsheets.
4. To process your own files, place .xlsx files in a folder and use the --folder option.

## Usage

```bash
dotnet run --project src/UiucTakeHome.Console -- --folder ./samples
```

Add `--verbose` to show amounts under each file listing:

```bash
dotnet run --project src/UiucTakeHome.Console -- --folder ./samples --verbose
```

Windows example with a full path:

```bash
dotnet run --project src/UiucTakeHome.Console -- --folder "C:\path\to\spreadsheets"
```

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

You should see a "Test Run Successful" message when all tests pass.

## Requirements

- .NET 10 SDK

## Notes

- This project uses ClosedXML for Excel parsing.
- If your environment surfaces NuGet security warnings, update dependencies as needed.

## Additional Questions

1. Describe a time when you worked with non-technical stakeholders to define requirements for a data-driven application. How did you ensure you fully understood their needs, and how did you handle misunderstandings or changing requirements?

For a sponsored project, I worked directly with the sponsor to define tournament flows, match visibility, and leaderboard expectations. I started by asking them to narrate how they wanted a user to enter a tournament and compare bots, then turned that into a short written scope plus a simple flow map. I confirmed details by reviewing example brackets and leaderboard views, and I restated each requirement in plain language so we both agreed on the expected behavior. When expectations shifted (for example, they wanted a more prominent leaderboard and clearer match states), I documented the change, adjusted the UI flow, and prioritized the update so it did not disrupt the core tournament logic.

2. When supporting an existing application, how do you communicate technical issues or limitations to users who may not have a technical background? Can you give a specific example?

I avoid jargon, explain the impact first, and then describe the constraint in plain terms with a clear next step. For example, in a reporting tool I supported, a user reported missing records after submitting a request. The root cause was a nightly data sync with the upstream system. I explained that new requests appear the next morning because the system only refreshes once overnight, gave them the exact cutoff time, and offered a workaround for urgent cases. That reduced confusion and set expectations while we planned a longer-term fix.

## Assumptions & Questions

See `ASSUMPTIONS.md`.
