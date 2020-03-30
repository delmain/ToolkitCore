﻿using ToolkitCore.Database;
using UnityEngine;
using Verse;

namespace ToolkitCore
{
    public class ToolkitCore : Mod
    {
        public static ToolkitCoreSettings settings;

        public ToolkitCore(ModContentPack content) : base(content)
        {
            settings = GetSettings<ToolkitCoreSettings>();
        }

        public override string SettingsCategory() => "ToolkitCore";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoWindowContents(inRect);
        }
    }

    public class ToolkitData : Mod
    {
        public static GlobalDatabase globalDatabase;

        public ToolkitData(ModContentPack content) : base(content)
        {
            globalDatabase = GetSettings<GlobalDatabase>();
        }
    }

    [StaticConstructorOnStartup]
    public static class Startup
    {
        static Startup()
        {
            TwitchWrapper.StartAsync();
        }
    }
}
