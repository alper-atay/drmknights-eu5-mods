using Scriban;
using Scriban.Runtime;
using System.Text;

namespace WorldConquest.Entities;

public class GenericActionClaim : BaseModeEntityGenerator
{
    private readonly string _templateSource;
    private readonly Template _template;
    private readonly TemplateContext _templateContext = new();

    public GenericActionClaim(Mod mod)
        : base(mod)
    {
        _templateSource = File.ReadAllText(Mod.Config.GenericActionClaimTemplatePath);
        _template = Template.Parse(_templateSource);
    }

    public string GetString(MapDataItem data)
    {
        ScriptObject scriptObject = new()
        {
            { "KeyName", data.KeyName },
            { "SituationName", Mod.Config.SituationName },
            { "RegionName", data.RegionName },
        };

        _templateContext.PushGlobal(scriptObject);

        string result = _template.Render(_templateContext);

        _templateContext.PopGlobal();

        return result;
    }

    public static string GetLocalizationString(IReadOnlyList<MapDataItem> mapDataItems)
    {
        StringBuilder stringBuilder = new();

        foreach (MapDataItem item in mapDataItems)
        {
            string localizationKey = item.KeyName + "_claim";
            string localizationValue =
                $"[ShowRegionNameWithNoTooltip('{item.RegionName}')] [region|e]";

            stringBuilder.AppendLine($" {localizationKey}: \"{localizationValue}\"");

            string localizationDescKey = item.KeyName + "_claim_desc";
            string localizationDescValue = string.Join(
                ", ",
                item.AreaNames.Select(x => $"[ShowAreaName('{x}')]")
            );
            stringBuilder.AppendLine($" {localizationDescKey}: \"{localizationDescValue}\"");
        }

        return stringBuilder.ToString();
    }
}
