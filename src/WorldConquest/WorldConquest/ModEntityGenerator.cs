using System.Text;
using WorldConquest.Entities;

namespace WorldConquest;

public class ModEntityGenerator(Mod mod)
{
    public Mod Mod { get; } = mod;

    public async Task Generate()
    {
        await GenerateAndWriteAll();
    }

    private async Task GenerateAndWriteAll()
    {
        await GenerateAndWriteWargoal();
        await GenerateAndWriteCasusBelli();
        await GenerateAndWriteObtainmentTrigger();
        await GenerateAndWriteGenericActionClaim();
    }

    private async Task GenerateAndWriteWargoal()
    {
        Wargoal wargoal = new(Mod);

        using FileStream fileStream = new(
            Path.Combine(
                Mod.Config.ModDirectory,
                Mod.Config.WargoalPath,
                string.Join(Mod.Config.Separator, Mod.Config.Prefix, Mod.Config.WargoalFileName)
            ),
            FileMode.Create,
            FileAccess.ReadWrite
        );

        using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

        foreach (MapDataItem mapDataItem in Mod.MapData!)
        {
            string wargoalString = wargoal.GetString(mapDataItem);

            await streamWriter.WriteLineAsync(wargoalString);
        }
    }

    private async Task GenerateAndWriteCasusBelli()
    {
        CasusBelli casusBelli = new(Mod);

        using FileStream fileStream = new(
            Path.Combine(
                Mod.Config.ModDirectory,
                Mod.Config.CasusBelliPath,
                string.Join(Mod.Config.Separator, Mod.Config.Prefix, Mod.Config.CasusBelliFileName)
            ),
            FileMode.Create,
            FileAccess.ReadWrite
        );

        using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

        foreach (MapDataItem data in Mod.MapData)
        {
            string wargoalString = casusBelli.GetString(data);

            await streamWriter.WriteLineAsync(wargoalString);
        }

        await streamWriter.DisposeAsync();
        await fileStream.DisposeAsync();
    }

    private async Task GenerateAndWriteObtainmentTrigger()
    {
        ObtainmentTrigger obtainmentTrigger = new(Mod);

        using FileStream fileStream = new(
            Path.Combine(
                Mod.Config.ModDirectory,
                Mod.Config.ObtainmentTriggerPath,
                string.Join(
                    Mod.Config.Separator,
                    Mod.Config.Prefix,
                    Mod.Config.ObtainmentTriggerFileName
                )
            ),
            FileMode.Create,
            FileAccess.ReadWrite
        );

        using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

        foreach (MapDataItem data in Mod.MapData)
        {
            string wargoalString = obtainmentTrigger.GetString(data);

            await streamWriter.WriteLineAsync(wargoalString);
        }
    }

    private async Task GenerateAndWriteGenericActionClaim()
    {
        GenericActionClaim genericActionClaim = new(Mod);

        using FileStream fileStream = new(
            Path.Combine(
                Mod.Config.ModDirectory,
                Mod.Config.GenericActionClaimPath,
                string.Join(
                    Mod.Config.Separator,
                    Mod.Config.Prefix,
                    Mod.Config.GenericActionClaimFileName
                )
            ),
            FileMode.Create,
            FileAccess.ReadWrite
        );

        using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

        foreach (MapDataItem data in Mod.MapData)
        {
            string genericActionClaimString = genericActionClaim.GetString(data);

            await streamWriter.WriteLineAsync(genericActionClaimString);
        }
    }
}
