namespace WorldConquest;

public partial class RawMapDataParser
{
    private string? _currentContinentName;
    private string? _currentSubcontinentName;
    private string? _currentRegionName;
    private string? _currentAreaName;

    public async Task<IReadOnlyList<RawMapDataItem>> ParseAsync(string rawMapDataPath)
    {
        List<RawMapDataItem> rawMapDataList = [];

        using StreamReader streamReader = File.OpenText(rawMapDataPath);

        string? currentLine;

        while ((currentLine = await streamReader.ReadLineAsync()) is not null)
        {
            if (string.IsNullOrWhiteSpace(currentLine))
            {
                continue;
            }

            if (currentLine.Length < 2)
            {
                continue;
            }

            int currentIndent = currentLine.Count(x => x == '\t');

            string? token = ParseLine(currentLine);

            if (string.IsNullOrWhiteSpace(token))
            {
                continue;
            }

            if (MapDataRules.HasUnacceptedToken(token))
            {
                continue;
            }

            switch (currentIndent)
            {
                case 0:
                    if (MapDataRules.IsAcceptedContinent(token))
                    {
                        _currentContinentName = token;
                    }
                    else
                    {
                        continue;
                    }
                    break;
                case 1:
                    _currentSubcontinentName = token;
                    break;
                case 2:
                    _currentRegionName = token;
                    break;
                case 3:
                    _currentAreaName = token;
                    break;
            }

            if (string.IsNullOrWhiteSpace(_currentContinentName))
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(_currentSubcontinentName))
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(_currentRegionName))
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(_currentAreaName))
            {
                continue;
            }

            if (currentIndent == 3)
            {
                rawMapDataList.Add(
                    new RawMapDataItem()
                    {
                        ContinentName = _currentContinentName,
                        SubcontinentName = _currentSubcontinentName,
                        RegionName = _currentRegionName,
                        AreaName = _currentAreaName,
                    }
                );
            }
        }

        return rawMapDataList;
    }

    public static string? ParseLine(string line)
    {
        if (line.Contains('#'))
        {
            int commentIndex = line.IndexOf('#');

            if (commentIndex == 0)
            {
                return null;
            }

            line = line[..commentIndex];
        }

        return new string([.. line.Where(x => char.IsLetter(x) || x == '_')]);
    }
}
