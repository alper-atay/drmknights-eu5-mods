using System.Text;
using Scriban;
using Scriban.Runtime;

namespace WorldConquest;

public class WorldConquestObtainmentTrigger
{
    private readonly string _templateSource;
    private readonly Template _template;
    private readonly TemplateContext _templateContext = new();

    public WorldConquestObtainmentTrigger()
    {
        _templateSource = File.ReadAllText(WorldConquestPredefined.ObtainmentTriggerTemplatePath);
        _template = Template.Parse(_templateSource);
    }

    public string GetString(WorldConquestMapData data)
    {
        string owningListString = GetOwningListString(data);

        ScriptObject scriptObject = new()
        {
            { "KeyName", data.KeyName },
            { "OwningList", owningListString },
        };

        _templateContext.PushGlobal(scriptObject);

        string result = _template.Render(_templateContext);

        _templateContext.PopGlobal();

        return result;
    }

    private static string GetOwningListString(WorldConquestMapData data)
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (string area in data.AreaNames)
        {
            stringBuilder.AppendLine($"own_entire_area = area:{area}");
        }

        return stringBuilder.ToString();
    }
}
