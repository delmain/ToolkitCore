using System;
using System.Linq;
using ToolkitCore.Models;
using Verse;

namespace ToolkitCore.Controllers
{
    public static class ChatCommandController
    {
        public static ToolkitChatCommand GetChatCommand(string commandText)
        {
            return DefDatabase<ToolkitChatCommand>.AllDefs.ToList().Find(cc => string.Equals(cc.commandText, commandText, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
