using Microsoft.Extensions.Configuration;

namespace WorldConquest;

public sealed class ModConfig
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public ModConfig()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        configuration.Bind(this);
    }

    public string RawMapDataPath { get; init; }

    public string ModDirectory { get; init; }

    public string Prefix { get; init; }

    public string Separator { get; init; }

    public int EventIndexOrigin { get; init; }

    public string SituationName { get; init; }

    public IEnumerable<string> SupportedLocalizations { get; init; }

    public string WargoalPath { get; init; }

    public string WargoalTemplatePath { get; init; }

    public string WargoalFileName { get; init; }

    public string CasusBelliPath { get; init; }

    public string CasusBelliTemplatePath { get; init; }

    public string CasusBelliFileName { get; init; }

    public string ObtainmentTriggerPath { get; init; }

    public string ObtainmentTriggerTemplatePath { get; init; }

    public string ObtainmentTriggerFileName { get; init; }

    public string GenericActionClaimPath { get; init; }

    public string GenericActionClaimTemplatePath { get; init; }

    public string GenericActionClaimFileName { get; init; }

    public string LocalizationPath { get; init; }
}
