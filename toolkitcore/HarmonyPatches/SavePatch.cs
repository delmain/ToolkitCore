﻿using HarmonyLib;
using Verse;

namespace ToolkitCore.HarmonyPatches
{
    [StaticConstructorOnStartup]
    static class SavePatch
    {
        static SavePatch()
        {
            new Harmony("com.rimworld.mod.hodlhodl.toolkit.core")
                .Patch(
                    original: AccessTools.Method(
                            type: typeof(GameDataSaveLoader),
                            name: "SaveGame"),
                    postfix: new HarmonyMethod(typeof(SavePatch), nameof(SaveGame_PostFix))
                );
        }

        static void SaveGame_PostFix()
        {
            Database.DatabaseController.SaveToolkit();
            ToolkitData.globalDatabase.Write();
        }
    }
}
