using ToolkitCore.Controllers;
using TwitchLib.Client.Models;
using Verse;

namespace ToolkitCore.Utilities
{
    public class ViewerInterface : TwitchInterfaceBase
    {
        public ViewerInterface(Game game)
        { }

        public override void ParseCommand(ChatMessage msg)
        {
            ViewerController.GetViewer(msg.Username).UpdateViewerFromMessage(msg);
        }
    }
}
