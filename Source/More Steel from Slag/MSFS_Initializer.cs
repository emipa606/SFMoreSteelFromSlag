using RimWorld;
using Verse;

namespace MSFS_Code;

[StaticConstructorOnStartup]
internal static class MSFS_Initializer
{
    static MSFS_Initializer()
    {
        LongEventHandler.QueueLongEvent(Setup, "LibraryStartup", false, null);
    }

    public static void Setup()
    {
        var thingDef = DefDatabase<ThingDef>.GetNamedSilentFail("Steel");
        var recipeDef = DefDatabase<RecipeDef>.GetNamedSilentFail("ExtractMetalFromSlag");
        if (recipeDef == null || thingDef == null)
        {
            Log.Message("[SFMoreSteelFromSlag]: Failed to find Steel or ExtractMetalFromSlag in database.");
            return;
        }

        recipeDef.workAmount = Controller.Settings.workAmount;
        recipeDef.products.Clear();
        recipeDef.products.Add(new ThingDefCountClass(thingDef, Controller.Settings.steelAmount));
        if (Controller.Settings.component)
        {
            recipeDef.products.Add(new ThingDefCountClass(ThingDefOf.ComponentIndustrial, 1));
        }

        Log.Message("[SFMoreSteelFromSlag]: Updated the ExtractMetalFromSlag recipe");
    }
}