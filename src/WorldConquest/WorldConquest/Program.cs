using WorldConquest;


Mod mod = new Mod();

await mod.MapData.GenerateAsync();
await mod.LocalizationGenerator.GenerateAndWriteWargoalAsync();

//WorldConquestGenericActionClaim world = new WorldConquestGenericActionClaim();

//WorldConquestGenericActionClaimData data = new WorldConquestGenericActionClaimData()
//{
//    PathName = "wc_deneme",
//    CasusBelliName = "wc_cb_deneme",
//    RegionName = "deneme_region",
//    SituationName = "deneme_situation"
//};

//string? re = world.GetTxt(data);

//Console.WriteLine(re);

//Console.ReadLine();
