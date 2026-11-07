using Scriban;
using Scriban.Runtime;

namespace WorldConquest;

public class WorldConquestGenericActionClaim
{
    private readonly string _templateSource;
    private readonly Template _template;
    private readonly TemplateContext _templateContext = new();

    public WorldConquestGenericActionClaim()
    {
        _templateSource = File.ReadAllText(WorldConquestPredefined.GenericActionClaimTemplatePath);
        _template = Template.Parse(_templateSource);
    }

    public string GetString(WorldConquestData data)
    {
        ScriptObject scriptObject = new()
        {
            { "KeyName", data.KeyName },
            { "SituationName", WorldConquestPredefined.SituationName },
            { "RegionName", data.RegionName },
        };

        _templateContext.PushGlobal(scriptObject);

        string result = _template.Render(_templateContext);

        _templateContext.PopGlobal();

        return result;
    }
}
