namespace WorldConquest;

public class Map
{
    private readonly IReadOnlyList<MapData> _mapData;

    public readonly IEnumerable<string> Continents;
    public readonly IEnumerable<string> Subcontinents;
    public readonly IEnumerable<string> Regions;
    public readonly IEnumerable<string> Areas;

    public Map(IReadOnlyList<MapData> mapData)
    {
        _mapData = mapData;

        Continents = mapData.Select(x => x.ContinentName).Distinct();
        Subcontinents = mapData.Select(x => x.SubcontinentName).Distinct();
        Regions = mapData.Select(x => x.RegionName).Distinct();
        Areas = mapData.Select(x => x.AreaName).Distinct();
    }

    public IEnumerable<string> GetContinents()
    {
        return Continents;
    }

    public IEnumerable<string> GetSubcontinentsByContinent(string continentName)
    {
        return _mapData
            .Where(x => x.ContinentName == continentName)
            .Select(x => x.SubcontinentName)
            .Distinct();
    }

    public IEnumerable<string> GetRegionsBySubcontinent(string subcontinentName)
    {
        return _mapData
            .Where(x => x.SubcontinentName == subcontinentName)
            .Select(x => x.RegionName)
            .Distinct();
    }

    public IEnumerable<string> GetAreasByRegion(string regionName)
    {
        return _mapData.Where(x => x.RegionName == regionName).Select(x => x.AreaName).Distinct();
    }
}
