using System.Collections;

namespace WorldConquest;

public class ModMapData(Mod mod) : IReadOnlyList<MapDataItem>
{
    private IReadOnlyList<RawMapDataItem>? _rawMapDataItems;
    private readonly List<MapDataItem> _mapDataItems = [];

    public int Count => ((IReadOnlyCollection<MapDataItem>)_mapDataItems).Count;

    public Mod Mod { get; } = mod;

    public MapDataItem this[int index] => ((IReadOnlyList<MapDataItem>)_mapDataItems)[index];

    public async Task GenerateAsync()
    {
        RawMapDataParser rawMapDataParser = new();

        _rawMapDataItems = await rawMapDataParser.ParseAsync(
            Mod.Config.RawMapDataPath
        );

        IEnumerable<string> continents = _rawMapDataItems.Select(x => x.ContinentName).Distinct();

        foreach (string continent in continents)
        {
            IEnumerable<string> subcontinents = _rawMapDataItems
                .Where(x => x.ContinentName == continent)
                .Select(x => x.SubcontinentName)
                .Distinct(); ;

            foreach (string subcontinent in subcontinents)
            {
                IEnumerable<string> regions = _rawMapDataItems
                    .Where(x => x.SubcontinentName == subcontinent)
                    .Select(x => x.RegionName)
                    .Distinct();

                int eventIndex = Mod.Config.EventIndexOrigin;

                foreach (string region in regions)
                {
                    IEnumerable<string> areas = _rawMapDataItems
                        .Where(x => x.RegionName == region)
                        .Select(x => x.AreaName)
                        .Distinct();

                    string pathName = string.Join(
                        Mod.Config.Separator,
                        Mod.Config.Prefix,
                        continent,
                        subcontinent,
                        region
                    );

                    MapDataItem mapData = new()
                    {
                        ContinentName = continent,
                        SubcontinentName = subcontinent,
                        RegionName = region,
                        AreaNames = areas,

                        KeyName = pathName,
                        EventIndex = eventIndex,
                    };

                    _mapDataItems.Add(mapData);

                    eventIndex++;
                }
            }
        }
    }

    public IEnumerator<MapDataItem> GetEnumerator()
    {
        return ((IEnumerable<MapDataItem>)_mapDataItems).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_mapDataItems).GetEnumerator();
    }
}
