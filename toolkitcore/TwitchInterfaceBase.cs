using TwitchLib.Client.Models;
using Verse;

namespace ToolkitCore
{
    public abstract class TwitchInterfaceBase : GameComponent
    {
        public abstract void ParseCommand(ChatMessage msg);
    }
}
