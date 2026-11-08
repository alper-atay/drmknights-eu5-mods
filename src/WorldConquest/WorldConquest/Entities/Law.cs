using Scriban;
using Scriban.Runtime;
using System.Text;

namespace WorldConquest.Entities;

public class Law : BaseModeEntityGenerator
{
    public Law(Mod mod) : base(mod)
    {
        _templateSource = File.ReadAllText(Mod.Config.LawTemplatePath);
        _template = Template.Parse(_templateSource);
    }

    private readonly string _templateSource;
    private readonly Template _template;
    private readonly TemplateContext _templateContext = new();

    public string GetString(MapDataItem mapDataItem)
    {
        ScriptObject scriptObject = new()
        {
            { "KeyName", mapDataItem.KeyName }
        };

        _templateContext.PushGlobal(scriptObject);

        string result = _template.Render(_templateContext);

        _templateContext.PopGlobal();

        return result;
    }

    public static string GetLawLocalizationString(IReadOnlyList<MapDataItem> mapDataItems)
    {
        StringBuilder stringBuilder = new();

        foreach (MapDataItem item in mapDataItems)
        {
            string localizationKey = item.KeyName + "_law";
            string localizationValue =
                $"[ShowRegionNameWithNoTooltip('{item.RegionName}')] [region|e] [law|e]";

            stringBuilder.AppendLine($" {localizationKey}: \"{localizationValue}\"");

            string localizationDescKey = item.KeyName + "_law_desc";
            string localizationDescValue = string.Join(
                ", ",
                item.AreaNames.Select(x => $"[ShowAreaName('{x}')]")
            );
            stringBuilder.AppendLine($" {localizationDescKey}: \"{localizationDescValue}\"");
        }

        return stringBuilder.ToString();
    }

    public static string GetPolicyLocalizationString(IReadOnlyList<MapDataItem> mapDataItems)
    {
        StringBuilder stringBuilder = new();

        foreach (MapDataItem item in mapDataItems)
        {
            string localizationKey = item.KeyName + "_policy";
            string localizationValue =
                $"[ShowRegionNameWithNoTooltip('{item.RegionName}')] [region|e] [policy|e]";

            stringBuilder.AppendLine($" {localizationKey}: \"{localizationValue}\"");

            string localizationDescKey = item.KeyName + "_policy_desc";
            string localizationDescValue = string.Join(
                ", ",
                item.AreaNames.Select(x => $"[ShowAreaName('{x}')]")
            );
            stringBuilder.AppendLine($" {localizationDescKey}: \"{localizationDescValue}\"");
        }

        return stringBuilder.ToString();
    }
}
