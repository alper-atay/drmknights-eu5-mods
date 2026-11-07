namespace WorldConquest;

public static class MapDataRules
{
    public static readonly IEnumerable<string> UnacceptedTokens;

    public static readonly IEnumerable<string> AcceptedContinents;

    static MapDataRules()
    {
        UnacceptedTokens = ["sea_zones", "sea_area", "channel_area", "coast_area", "coastline"];

        AcceptedContinents = ["europe", "asia", "africa", "america", "oceania"];
    }

    public static bool IsAcceptedContinent(string continent)
    {
        return AcceptedContinents.Contains(continent);
    }

    public static bool HasUnacceptedToken(string token)
    {
        foreach (string unacceptedToken in UnacceptedTokens)
        {
            if (token.Contains(unacceptedToken))
            {
                return true;
            }
        }

        return false;
    }
}
