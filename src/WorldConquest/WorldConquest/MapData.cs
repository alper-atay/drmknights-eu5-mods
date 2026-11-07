namespace WorldConquest;

public readonly record struct MapData
{
    public required string ContinentName { get; init; }

    public required string SubcontinentName { get; init; }

    public required string RegionName { get; init; }

    public required string AreaName { get; init; }
}
