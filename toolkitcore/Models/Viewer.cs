using System;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Enums;
using Verse;

namespace ToolkitCore.Models
{
    public static class Viewers
    {
        // TODO Figure out a thread-safe implementation for this
        public static List<Viewer> All
        {
            get
            {
                return ToolkitData.globalDatabase.viewers;
            }
        }

        public static Viewer FindByUsername(string username)
        {
            return All.FirstOrDefault(v => string.Equals(v?.Username, username, StringComparison.InvariantCultureIgnoreCase));
        }

        public static Viewer CreateAndAddViewer(string username)
        {
            var viewer = new Viewer(Username: username);
            All.Add(viewer);
            return viewer;
        }
    }

    public class Viewer : IExposable
    {
        public string Username;

        public string DisplayName;

        public string UserId;

        public string ColorHex;

        public bool IsBroadcaster;

        public bool IsBot;

        public bool IsModerator;

        public bool IsSubscriber;

        public bool IsVIP;

        public UserType UserType;

        public Viewer()
        {

        }

        public Viewer(string username)
        {
            Username = username;
        }

        public Viewer(string Username = null, string DisplayName = null, string UserId = null, bool IsBroadcaster = false, bool IsBot = false, bool IsModerator = false, bool IsSubscriber = false, bool IsVIP = false)
        {
            this.Username = Username;
            this.DisplayName = DisplayName;
            this.UserId = UserId;
            this.IsBroadcaster = IsBroadcaster;
            this.IsBot = IsBot;
            this.IsModerator = IsModerator;
            this.IsSubscriber = IsSubscriber;
            this.IsVIP = IsVIP;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref Username, "Username");
            Scribe_Values.Look(ref DisplayName, "DisplayName");
            Scribe_Values.Look(ref UserId, "UserId");
            Scribe_Values.Look(ref IsBroadcaster, "IsBroadcaster");
            Scribe_Values.Look(ref IsBot, "IsBot");
            Scribe_Values.Look(ref IsModerator, "IsModerator");
            Scribe_Values.Look(ref IsSubscriber, "IsSubscriber");
            Scribe_Values.Look(ref IsVIP, "IsVIP");
            Scribe_Values.Look(ref ColorHex, "ColorHex");
        }

        public override string ToString()
        {
            return $"<{UserId}> {DisplayName} ({Username})";
        }
    }
}
