using Pdoxcl2Sharp;

namespace WorldConquest;

public class WorldConquestChallengeTriggerWriter : IParadoxWrite
{
    private const string _TriggerPrefix = "wc";
    private const string _TriggerSuffix = "challenge_trigger";

    private readonly WorldConquestMap _map;

    public WorldConquestChallengeTriggerWriter(WorldConquestMap map)
    {
        _map = map;
    }

    public void Write(ParadoxStreamWriter writer)
    {
        IEnumerable<string> continents = _map.GetContinents();

        foreach (string continent in continents)
        {
            IEnumerable<string> subcontinents = _map.GetSubcontinentsByContinent(continent);

            foreach (string subcontinent in subcontinents)
            {
                IEnumerable<string> regions = _map.GetRegionsBySubcontinent(subcontinent);

                foreach (string region in regions)
                {
                    string pathName = string.Join("_", continent, subcontinent, region);
                    string policyName = string.Join("_", _TriggerPrefix, pathName, "policy");
                    string triggerName = string.Join("_", _TriggerPrefix, pathName, _TriggerSuffix);

                    writer.WriteComment(pathName);
                    writer.WriteLine(triggerName + " = {");

                    writer.WriteLine("trigger_if = {");
                    writer.WriteLine("limit = {");
                    writer.WriteLine($"has_policy = {policyName}");
                    writer.WriteLine("}");
                    writer.WriteLine($"has_policy = {policyName}");
                    writer.WriteLine("}");

                    writer.WriteLine("trigger_else = {");

                    IEnumerable<string> areas = _map.GetAreasByRegion(region);

                    List<string> ownEntireAreaLines = [];

                    foreach (string area in areas)
                    {
                        writer.WriteLine(GetOwnEntireAreaLine(area));
                    }

                    writer.WriteLine("}");

                    writer.WriteLine("}");
                }
            }
        }
    }

    private static string GetOwnEntireAreaLine(string areaName)
    {
        return string.Format("own_entire_area = area:{0}", areaName);
    }
}
