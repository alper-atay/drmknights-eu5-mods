using Microsoft.Extensions.Configuration;
using Pdoxcl2Sharp;
using System.Text;

namespace WorldConquest;

public class WorldConquest
{
    private readonly List<WorldConquestData> _data = [];
    private readonly IConfigurationRoot _configuration;

    private readonly string _modPath;

    private readonly Map _map;

    public WorldConquest()
    {
        using FileStream fileStream = new("MapData.txt", FileMode.Open);
        MapDataReader mapData = ParadoxParser.Parse(fileStream, new MapDataReader());
        _map = new(mapData);

        _configuration = new ConfigurationBuilder()
            .AddJsonFile("AppSettings.json", optional: false)
            .Build();

        _modPath = _configuration.GetSection("ModPath").Value!;
    }

    public async Task Generate()
    {
        IEnumerable<string> continents = _map.GetContinents();

        foreach (string continent in continents)
        {
            IEnumerable<string> subcontinents = _map.GetSubcontinentsByContinent(continent);

            foreach (string subcontinent in subcontinents)
            {
                IEnumerable<string> regions = _map.GetRegionsBySubcontinent(subcontinent);

                int eventIndex = WorldConquestPredefined.EventIndexOrigin;

                foreach (string region in regions)
                {
                    IEnumerable<string> areas = _map.GetAreasByRegion(region);

                    string pathName = string.Join(
                        WorldConquestPredefined.Separator,
                        WorldConquestPredefined.Prefix,
                        continent,
                        subcontinent,
                        region
                    );

                    WorldConquestData data = new()
                    {
                        ContinentName = continent,
                        SubcontinentName = subcontinent,
                        RegionName = region,
                        AreaNames = areas,

                        KeyName = pathName,
                        EventIndex = eventIndex,
                    };

                    _data.Add(data);

                    eventIndex++;
                }
            }
        }

        await GenerateAndWriteAll();
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
                _modPath,
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

        foreach (WorldConquestData data in _data)
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
                _modPath,
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

        foreach (WorldConquestData data in _data)
        {
            string wargoalString = casusBelli.GetString(data);

            await streamWriter.WriteLineAsync(wargoalString);
        }
    }

    private async Task GenerateAndWriteObtainmentTrigger()
    {
        WorldConquestObtainmentTrigger obtainmentTrigger = new();

        using FileStream fileStream = new(
            Path.Combine(
                _modPath,
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

        foreach (WorldConquestData data in _data)
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
                _modPath,
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

        foreach (WorldConquestData data in _data)
        {
            string genericActionClaimString = genericActionClaim.GetString(data);

            await streamWriter.WriteLineAsync(genericActionClaimString);
        }
    }
}
