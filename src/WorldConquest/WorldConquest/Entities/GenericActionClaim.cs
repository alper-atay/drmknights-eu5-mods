using Scriban;
using Scriban.Runtime;

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
}
