namespace WorldConquest;

public readonly struct MapDataItem
{
    public required int EventIndex { get; init; }

    public required string KeyName { get; init; }

    public required string ContinentName { get; init; }

    public required string SubcontinentName { get; init; }

    public required string RegionName { get; init; }

    public required IEnumerable<string> AreaNames { get; init; }
}
