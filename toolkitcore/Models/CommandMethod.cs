using TwitchLib.Client.Interfaces;

namespace ToolkitCore.Models
{
    public abstract class CommandMethod
    {
        public ToolkitChatCommand command;

        public CommandMethod(ToolkitChatCommand command)
        {
            this.command = command;
        }

        public virtual bool CanExecute(ITwitchCommand twitchCommand)
        {
            // If command not enabled
            if (!command.enabled) 
                return false;

            // If there is no message to act on
            if (twitchCommand.ChatMessage == null)
                return false;

            // If command requires broadcaster status and message not from broadcaster
            if (command.requiresBroadcaster && !twitchCommand.ChatMessage.IsBroadcaster) 
                return false;

            // If command requires moderator status and message not from broadcaster or moderator
            if (command.requiresMod && !(twitchCommand.ChatMessage.IsBroadcaster || twitchCommand.ChatMessage.IsModerator)) 
                return false;

            return true;
        }

        public abstract void Execute(ITwitchCommand twitchCommand);
    }
}
