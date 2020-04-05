using System;
using System.Linq;
using ToolkitCore.Utilities;
using TwitchLib.Client.Models;

namespace ToolkitCore.Models
{
    public abstract class MessageDetails
    {
        private Lazy<Viewer> viewer = null;
        public Viewer Viewer => viewer?.Value;

        public abstract string Message { get; }

        public MessageDetails(string username)
        {
            viewer = new Lazy<Viewer>(() => Viewers.FindByUsername(username) ?? Viewers.CreateAndAddViewer(username));
        }

        public abstract void UpdateViewer();

        protected void UpdateViewer(TwitchLibMessage message)
        {
            Viewer.DisplayName = message.DisplayName ?? Viewer.DisplayName;
            Viewer.ColorHex = message.ColorHex ?? Viewer.ColorHex ?? RandomUtilities.Color();
            // TODO Figure out how to handle if these change (aka how can they can "change" since we use username as the sync)
            // Viewer.UserId = message.UserId;
            // Viewer.Username = message.Username;
        }

        public abstract void Reply(string message);
    }

    public class WhisperDetails : MessageDetails
    {
        private readonly WhisperMessage message;

        public WhisperDetails(WhisperMessage message) : base(message.Username)
        {
            this.message = message;
            UpdateViewer();
        }

        public override string Message => message?.Message;

        public override void Reply(string message)
        {
            TwitchWrapper.SendWhisper(Viewer.Username, message);
        }

        public override void UpdateViewer()
        {
            if (message == null)
                return;

            UpdateViewer(message);
        }
    }

    public class ChatDetails : MessageDetails
    {
        private readonly ChatMessage message;

        public ChatDetails(ChatMessage message) : base(message.Username)
        {
            this.message = message;
            UpdateViewer();
        }

        public override string Message => message?.Message;

        public override void Reply(string message)
        {
            TwitchWrapper.SendChatMessage($"@{Viewer.DisplayName}, {message}");
        }

        public override void UpdateViewer()
        {
            if (message == null)
                return;

            UpdateViewer(message);

            Viewer.IsBot = message.IsMe;
            Viewer.IsBroadcaster = message.Badges.Any(b => b.Key == TwitchBadges.Broadcaster);
            Viewer.IsModerator = message.Badges.Any(b => TwitchBadges.ModOptions.Contains(b.Key));
            Viewer.IsSubscriber = message.Badges.Any(b => TwitchBadges.SubOptions.Contains(b.Key));
            Viewer.IsVIP = message.Badges.Any(b => b.Key == TwitchBadges.VIP);
        }
    }
}
