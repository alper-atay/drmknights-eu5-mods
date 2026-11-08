using System.Text;
using WorldConquest.Entities;

namespace WorldConquest;

public class ModLocalizationGenerator(Mod mod)
{
    public Mod Mod { get; } = mod;

    public async Task GenerateAndWriteWargoalAsync()
    {
        foreach (string supportedLocalization in Mod.Config.SupportedLocalizations)
        {
            string outputPath = Path.Combine(Mod.Config.ModDirectory, Mod.Config.LocalizationPath, supportedLocalization, "wc_wargoal_l_" + supportedLocalization + ".yml");

            using FileStream fileStream = new(outputPath, FileMode.Create, FileAccess.ReadWrite);
            using StreamWriter streamWriter = new(fileStream, new UTF8Encoding(true));

            await streamWriter.WriteLineAsync("l_" + supportedLocalization + ":");

            string str = Wargoal.GetLocalizationString(Mod.MapData);

            await streamWriter.WriteLineAsync(str);
        }
    }
}
