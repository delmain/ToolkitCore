using ToolkitCore.Models;
using Verse;

namespace ToolkitCore.Utilities
{
    public class ViewerInterface : TwitchInterfaceBase
    {
        public ViewerInterface(Game game)
        { }

        public override void ParseCommand(MessageDetails msg)
        {
            // Do nothing because Viewer updating is moved to message instantiation
            // Leave this here just to not break saves that have it already loaded.
        }
    }
}
