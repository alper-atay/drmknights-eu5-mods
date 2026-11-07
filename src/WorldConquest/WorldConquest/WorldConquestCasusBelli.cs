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

    public string GetString(WorldConquestData data)
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
}
