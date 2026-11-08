using System.Collections;

namespace WorldConquest;

public partial class RawMapDataParser : IReadOnlyList<RawMapData>
{
    private readonly List<RawMapData> _rawMapDataList = [];

    private string? _currentContinentName;
    private string? _currentSubcontinentName;
    private string? _currentRegionName;
    private string? _currentAreaName;

    public RawMapData this[int index] => ((IReadOnlyList<RawMapData>)_rawMapDataList)[index];

    public int Count => ((IReadOnlyCollection<RawMapData>)_rawMapDataList).Count;

    public IEnumerator<RawMapData> GetEnumerator()
    {
        return ((IEnumerable<RawMapData>)_rawMapDataList).GetEnumerator();
    }


    public async Task ParseAsync(string rawMapDataPath)
    {
        using StreamReader streamReader = File.OpenText(rawMapDataPath);

        string? line;
        while ((line = await streamReader.ReadLineAsync()) is not null)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.Length < 2)
            {
                continue;
            }

            int currentIndent = line.Count(x => x == '\t');

            string? token = ParseLine(line);

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
                        return;
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
                _rawMapDataList.Add(new RawMapData()
                {
                    ContinentName = _currentContinentName,
                    SubcontinentName = _currentSubcontinentName,
                    RegionName = _currentRegionName,
                    AreaName = _currentAreaName
                });
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_rawMapDataList).GetEnumerator();
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
