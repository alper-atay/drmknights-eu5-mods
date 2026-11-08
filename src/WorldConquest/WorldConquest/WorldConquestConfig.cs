using Microsoft.Extensions.Configuration;

namespace WorldConquest;

public sealed class WorldConquestConfig
{
    public WorldConquestConfig()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        configuration.Bind(this);
    }

    public string ModDirectory { get; init; }

    public string Prefix { get; init; }

    public string Separator { get; init; }

    public string SituationName { get; init; }

    public IEnumerable<string> SupportedLocalizations { get; init; }

    public string WargoalPath { get; init; }

    public string CasusBelliPath { get; init; }

    public string ObtainmentTriggerPath { get; init; }

    public string GenericActionClaimPath { get; init; }

    public string LocalizationPath { get; init; }
}
