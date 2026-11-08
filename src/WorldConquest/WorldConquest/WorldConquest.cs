using Pdoxcl2Sharp;
using System.Text;

namespace WorldConquest;

public class WorldConquest
{
    public WorldConquestConfig Config { get; }

    public WorldConquestMap Map { get; private set; }

    private readonly List<WorldConquestMapData> _mapData = [];

    public WorldConquest()
    {
        Config = new WorldConquestConfig();
    }

    public async Task GenerateMap()
    {

    }

    public async Task Generate()
    {
        IEnumerable<string> continents = Map.GetContinents();

        foreach (string continent in continents)
        {
            IEnumerable<string> subcontinents = Map.GetSubcontinentsByContinent(continent);

            foreach (string subcontinent in subcontinents)
            {
                IEnumerable<string> regions = Map.GetRegionsBySubcontinent(subcontinent);

                int eventIndex = WorldConquestPredefined.EventIndexOrigin;

                foreach (string region in regions)
                {
                    IEnumerable<string> areas = Map.GetAreasByRegion(region);

                    string pathName = string.Join(
                        WorldConquestPredefined.Separator,
                        WorldConquestPredefined.Prefix,
                        continent,
                        subcontinent,
                        region
                    );

                    WorldConquestMapData mapData = new()
                    {
                        ContinentName = continent,
                        SubcontinentName = subcontinent,
                        RegionName = region,
                        AreaNames = areas,

                        KeyName = pathName,
                        EventIndex = eventIndex,
                    };

                    _mapData.Add(mapData);

                    eventIndex++;
                }
            }
        }

        await GenerateAndWriteAll();
    }

    public async Task GenerateMapData()
    {
        using FileStream fileStream = new("MapData.txt", FileMode.Open);
        RawMapDataParser mapData = ParadoxParser.Parse(fileStream, new RawMapDataParser());
    }

    public async Task GenerateAndWriteAll()
    {
        await GenerateAndWriteWargoal();
        await GenerateAndWriteCasusBelli();
        await GenerateAndWriteObtainmentTrigger();

        await GenerateAndWriteGenericActionClaim();
    }

    private async Task GenerateAndWriteWargoal()
    {
        WorldConquestWargoal wargoal = new();

        using FileStream fileStream = new(
            Path.Combine(
                Config.ModDirectory,
                WorldConquestPredefined.WargoalPath,
                string.Join(
                    WorldConquestPredefined.Separator,
                    WorldConquestPredefined.Prefix,
                    "wargoal.txt"
                )
            ),
            FileMode.Create,
            FileAccess.ReadWrite
        );

        using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

        foreach (WorldConquestMapData data in _mapData)
        {
            string wargoalString = wargoal.GetString(data);

            await streamWriter.WriteLineAsync(wargoalString);
        }
    }

    private async Task GenerateAndWriteCasusBelli()
    {
        WorldConquestCasusBelli casusBelli = new();

        using FileStream fileStream = new(
            Path.Combine(
                Config.ModDirectory,
                WorldConquestPredefined.CasusBelliPath,
                string.Join(
                    WorldConquestPredefined.Separator,
                    WorldConquestPredefined.Prefix,
                    "casus_belli.txt"
                )
            ),
            FileMode.Create,
            FileAccess.ReadWrite
        );

        using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

        foreach (WorldConquestMapData data in _mapData)
        {
            string wargoalString = casusBelli.GetString(data);

            await streamWriter.WriteLineAsync(wargoalString);
        }

        await streamWriter.DisposeAsync();
        await fileStream.DisposeAsync();
    }

    private async Task GenerateAndWriteObtainmentTrigger()
    {
        WorldConquestObtainmentTrigger obtainmentTrigger = new();

        using FileStream fileStream = new(
            Path.Combine(
                Config.ModDirectory,
                WorldConquestPredefined.ObtainmentTriggerPath,
                string.Join(
                    WorldConquestPredefined.Separator,
                    WorldConquestPredefined.Prefix,
                    "obtainment_trigger.txt"
                )
            ),
            FileMode.Create,
            FileAccess.ReadWrite
        );

        using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

        foreach (WorldConquestMapData data in _mapData)
        {
            string wargoalString = obtainmentTrigger.GetString(data);

            await streamWriter.WriteLineAsync(wargoalString);
        }
    }

    private async Task GenerateAndWriteGenericActionClaim()
    {
        WorldConquestGenericActionClaim genericActionClaim = new();

        using FileStream fileStream = new(
            Path.Combine(
                Config.ModDirectory,
                WorldConquestPredefined.GenericActionClaimPath,
                string.Join(
                    WorldConquestPredefined.Separator,
                    WorldConquestPredefined.Prefix,
                    "generic_action_claim.txt"
                )
            ),
            FileMode.Create,
            FileAccess.ReadWrite
        );

        using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

        foreach (WorldConquestMapData data in _mapData)
        {
            string genericActionClaimString = genericActionClaim.GetString(data);

            await streamWriter.WriteLineAsync(genericActionClaimString);
        }
    }
}
