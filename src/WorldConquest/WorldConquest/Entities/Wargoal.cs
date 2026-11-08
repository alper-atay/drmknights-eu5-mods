using System.Text;
using Scriban;
using Scriban.Runtime;

namespace WorldConquest.Entities;

public class Wargoal : BaseModeEntityGenerator
{
    private readonly string _templateSource;
    private readonly Template _template;
    private readonly TemplateContext _templateContext = new();

    public Wargoal(Mod mod)
        : base(mod)
    {
        _templateSource = File.ReadAllText(Mod.Config.WargoalTemplatePath);
        _template = Template.Parse(_templateSource);
    }

    public string GetString(MapDataItem data)
    {
        ScriptObject scriptObject = new()
        {
            { "WargoalName", data.KeyName },
            { "RegionName", data.RegionName },
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
            string localizationKey = "war_goal_" + item.KeyName;
            string localizationValue =
                $"[ShowRegionNameWithNoTooltip('{item.RegionName}')] [region|e]";

            stringBuilder.AppendLine($" {localizationKey}: \"{localizationValue}\"");

            string localizationDescKey = "war_goal_" + item.KeyName + "_desc";
            string localizationDescValue = string.Join(
                ", ",
                item.AreaNames.Select(x => $"[ShowAreaName('{x}')]")
            );
            stringBuilder.AppendLine($" {localizationDescKey}: \"{localizationDescValue}\"");
        }

        return stringBuilder.ToString();
    }
}
