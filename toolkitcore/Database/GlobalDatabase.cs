using System;
using System.Collections.Generic;
using ToolkitCore.Models;
using Verse;

namespace ToolkitCore.Database
{
    public class GlobalDatabase : ModSettings
    {
        public List<Viewer> viewers = new List<Viewer>();

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref viewers, "viewers", LookMode.Deep, Array.Empty<object>());
        }
    }
}
