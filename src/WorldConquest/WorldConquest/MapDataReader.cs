using System.Collections;
using Pdoxcl2Sharp;

namespace WorldConquest;

public partial class MapDataReader : IParadoxRead, IReadOnlyList<MapData>
{
    private readonly List<MapData> _data = [];

    private string? _currentContinentName;
    private string? _currentSubcontinentName;
    private string? _currentRegionName;
    private string? _currentAreaName;

    public MapData this[int index] => ((IReadOnlyList<MapData>)_data)[index];

    public int Count => ((IReadOnlyCollection<MapData>)_data).Count;

    public IEnumerator<MapData> GetEnumerator()
    {
        return ((IEnumerable<MapData>)_data).GetEnumerator();
    }

    public void TokenCallback(ParadoxParser parser, string token)
    {
        token = token.Trim();

        if (string.IsNullOrWhiteSpace(token))
        {
            return;
        }

        if (MapDataRules.HasUnacceptedToken(token))
        {
            return;
        }

        switch (parser.CurrentIndent)
        {
            case 0:
                {
                    if (MapDataRules.IsAcceptedContinent(token))
                    {
                        _currentContinentName = token;
                    }
                    else
                    {
                        return;
                    }
                }
                break;
            case 1:
                {
                    _currentSubcontinentName = token;
                }
                break;
            case 2:
                {
                    _currentRegionName = token;
                }
                break;
            case 3:
                {
                    _currentAreaName = token;
                }
                break;
        }

        if (string.IsNullOrWhiteSpace(_currentContinentName))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(_currentSubcontinentName))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(_currentRegionName))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(_currentAreaName))
        {
            return;
        }

        if (parser.CurrentIndent == 3)
        {
            _data.Add(
                new MapData()
                {
                    ContinentName = _currentContinentName,
                    SubcontinentName = _currentSubcontinentName,
                    RegionName = _currentRegionName,
                    AreaName = _currentAreaName,
                }
            );
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_data).GetEnumerator();
    }
}
