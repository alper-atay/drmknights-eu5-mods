using Scriban;
using Scriban.Runtime;

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
}
