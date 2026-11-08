using System.Text;
using WorldConquest.Entities;

namespace WorldConquest;

public class ModLocalizationGenerator(Mod mod)
{
    public Mod Mod { get; } = mod;

    public async Task GenerateAsync()
    {
        await GenerateAndWriteWargoalAsync();
        await GenerateAndWriteCasusBelliAsync();
        await GenerateAndWriteGenericActionClaimAsync();
        await GenerateAndWriteLawAndPolicyAsync();
    }

    private async Task GenerateAndWriteWargoalAsync()
    {
        foreach (string supportedLocalization in Mod.Config.SupportedLocalizations)
        {
            string outputPath = Path.Combine(
                Mod.Config.ModDirectory,
                Mod.Config.LocalizationPath,
                supportedLocalization,
                "wc_wargoal_l_" + supportedLocalization + ".yml"
            );

            using FileStream fileStream = new(outputPath, FileMode.Create, FileAccess.ReadWrite);
            using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

            await streamWriter.WriteLineAsync("l_" + supportedLocalization + ":");

            string str = Wargoal.GetLocalizationString(Mod.MapData);

            await streamWriter.WriteLineAsync(str);
        }
    }

    private async Task GenerateAndWriteCasusBelliAsync()
    {
        foreach (string supportedLocalization in Mod.Config.SupportedLocalizations)
        {
            string outputPath = Path.Combine(
                Mod.Config.ModDirectory,
                Mod.Config.LocalizationPath,
                supportedLocalization,
                "wc_casus_belli_l_" + supportedLocalization + ".yml"
            );

            using FileStream fileStream = new(outputPath, FileMode.Create, FileAccess.ReadWrite);
            using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

            await streamWriter.WriteLineAsync("l_" + supportedLocalization + ":");

            string str = CasusBelli.GetLocalizationString(Mod.MapData);

            await streamWriter.WriteLineAsync(str);
        }
    }

    private async Task GenerateAndWriteGenericActionClaimAsync()
    {
        foreach (string supportedLocalization in Mod.Config.SupportedLocalizations)
        {
            string outputPath = Path.Combine(
                Mod.Config.ModDirectory,
                Mod.Config.LocalizationPath,
                supportedLocalization,
                "wc_generic_action_claim_l_" + supportedLocalization + ".yml"
            );

            using FileStream fileStream = new(outputPath, FileMode.Create, FileAccess.ReadWrite);
            using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

            await streamWriter.WriteLineAsync("l_" + supportedLocalization + ":");

            string str = GenericActionClaim.GetLocalizationString(Mod.MapData);

            await streamWriter.WriteLineAsync(str);
        }
    }

    private async Task GenerateAndWriteLawAndPolicyAsync()
    {
        foreach (string supportedLocalization in Mod.Config.SupportedLocalizations)
        {
            string outputPath = Path.Combine(
                Mod.Config.ModDirectory,
                Mod.Config.LocalizationPath,
                supportedLocalization,
                "wc_law_l_" + supportedLocalization + ".yml"
            );

            using FileStream fileStream = new(outputPath, FileMode.Create, FileAccess.ReadWrite);
            using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

            await streamWriter.WriteLineAsync("l_" + supportedLocalization + ":");

            string strLaw = Law.GetLawLocalizationString(Mod.MapData);
            string strPolicy = Law.GetPolicyLocalizationString(Mod.MapData);

            await streamWriter.WriteLineAsync(strLaw);
            await streamWriter.WriteLineAsync(strPolicy);
        }
    }
}
