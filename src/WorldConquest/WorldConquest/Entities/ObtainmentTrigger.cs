using System.Text;
using Scriban;
using Scriban.Runtime;

namespace WorldConquest.Entities;

public class ObtainmentTrigger : BaseModeEntityGenerator
{
    private readonly string _templateSource;
    private readonly Template _template;
    private readonly TemplateContext _templateContext = new();

    public ObtainmentTrigger(Mod mod)
        : base(mod)
    {
        _templateSource = File.ReadAllText(Mod.Config.ObtainmentTriggerTemplatePath);
        _template = Template.Parse(_templateSource);
    }

    public string GetString(MapDataItem data)
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

    private static string GetOwningListString(MapDataItem data)
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (string area in data.AreaNames)
        {
            stringBuilder.AppendLine($"own_entire_area = area:{area}");
        }

        return stringBuilder.ToString();
    }
}
