using System.Text;
using Scriban;
using Scriban.Runtime;

namespace WorldConquest;

public class WorldConquestCasusBelli
{
    private readonly string _templateSource;
    private readonly Template _template;
    private readonly TemplateContext _templateContext = new();

    public WorldConquestCasusBelli()
    {
        _templateSource = File.ReadAllText(WorldConquestPredefined.CasusBelliTemplatePath);
        _template = Template.Parse(_templateSource);
    }

    public string GetString(WorldConquestMapData data)
    {
        ScriptObject scriptObject = new()
        {
            { "CasusBelliName", data.KeyName },
            { "WargoalName", data.KeyName },
        };

        _templateContext.PushGlobal(scriptObject);

        string result = _template.Render(_templateContext);

        _templateContext.PopGlobal();

        return result;
    }

    public static string GetLocalizationString(IReadOnlyList<WorldConquestMapData> data)
    {
        StringBuilder stringBuilder = new();

        foreach (WorldConquestMapData item in data)
        {
            string localizationKey = item.KeyName + "casus_belli";
            string localizationValue =
                $"[ShowRegionNameWithNoTooltip('{item.RegionName}')] [region|e]";

            stringBuilder.AppendLine($" {localizationKey}:{localizationValue}");

            //wc_europe_eastern_europe_russian_region_casus_belli_desc

            string localizationDescKey = item.KeyName + "casus_belli_desc";
            string localizationDescValue =
                string.Join(", ", item.AreaNames.Select(x => $"[ShowAreaName('{x}')]")) + ".";
            stringBuilder.AppendLine($" {localizationDescKey}:{localizationDescValue}");
        }

        return stringBuilder.ToString();
    }
}
