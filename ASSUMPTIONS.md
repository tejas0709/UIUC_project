- Assumed each subaward row contains the label "Subaward:" with the recipient name in the next populated cell on the same row.
- Amounts are taken from the first numeric cell in the same row as the subaward label.
- If multiple amounts appear in a row, the first numeric-looking value is used.
- The sample spreadsheets are expected to live in `samples/`.

Questions:
- Should negative amounts ever appear? Currently parsed as-is.
- Are there variants of the label text beyond "Subaward:" that should be supported?
- Should there be a minimum threshold for inclusion (e.g., ignore zero amounts)?
