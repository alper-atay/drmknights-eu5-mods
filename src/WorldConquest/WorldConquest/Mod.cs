namespace WorldConquest;

public class Mod
{
    public ModConfig Config { get; }

    public ModMapData MapData { get; }

    public ModEntityGenerator EntityGenerator { get; }

    public ModLocalizationGenerator LocalizationGenerator { get; }

    public Mod()
    {
        Config = new ModConfig();

        MapData = new ModMapData(this);
        EntityGenerator = new ModEntityGenerator(this);
        LocalizationGenerator = new ModLocalizationGenerator(this);
    }
}
