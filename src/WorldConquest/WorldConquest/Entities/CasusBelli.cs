using System.Text;
using Scriban;
using Scriban.Runtime;

namespace WorldConquest.Entities;

public class CasusBelli : BaseModeEntityGenerator
{
    private readonly string _templateSource;
    private readonly Template _template;
    private readonly TemplateContext _templateContext = new();

    public CasusBelli(Mod mod)
        : base(mod)
    {
        _templateSource = File.ReadAllText(Mod.Config.CasusBelliTemplatePath);
        _template = Template.Parse(_templateSource);
    }

    public string GetString(MapDataItem data)
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

    public static string GetLocalizationString(IReadOnlyList<MapDataItem> data)
    {
        StringBuilder stringBuilder = new();

        foreach (MapDataItem item in data)
        {
            string localizationKey = item.KeyName + "_casus_belli";
            string localizationValue =
                $"[ShowRegionNameWithNoTooltip('{item.RegionName}')] [region|e]";

            stringBuilder.AppendLine($" {localizationKey}: \"{localizationValue}\"");
            stringBuilder.AppendLine($" {localizationKey}_PROV: \"{localizationValue}\"");

            string localizationDescKey = item.KeyName + "_casus_belli_desc";
            string localizationDescValue = string.Join(
                ", ",
                item.AreaNames.Select(x => $"[ShowAreaName('{x}')]")
            );
            stringBuilder.AppendLine($" {localizationDescKey}: \"{localizationDescValue}\"");
        }

        return stringBuilder.ToString();
    }
}
