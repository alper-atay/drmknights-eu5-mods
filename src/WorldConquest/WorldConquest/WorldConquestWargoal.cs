using Scriban;
using Scriban.Runtime;

namespace WorldConquest;

public class WorldConquestWargoal
{
    private readonly string _templateSource;
    private readonly Template _template;
    private readonly TemplateContext _templateContext = new();

    public WorldConquestWargoal()
    {
        _templateSource = File.ReadAllText(WorldConquestPredefined.WargoalTemplatePath);
        _template = Template.Parse(_templateSource);
    }

    public string GetString(WorldConquestMapData data)
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
}
