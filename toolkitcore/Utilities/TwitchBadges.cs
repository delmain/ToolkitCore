using System;
using System.Collections.Generic;

namespace ToolkitCore.Utilities
{
    /// <summary>
    /// A list of 'badge' options as sent by the Twitch IRC API.
    /// This is not an exhaustive list, but rather a list of the ones that this application cares about.
    /// </summary>
    public static class TwitchBadges
    {
        public const string Admin = "admin";
        //public const string Bits = "bits"; TODO Add bits handling
        public const string Broadcaster = "broadcaster";
        public const string Founder = "founder";
        public const string GlobalMod = "global_mod";
        public const string Moderator = "moderator";
        public const string Subscriber = "subscriber";
        public const string Staff = "staff";
        public const string VIP = "vip";

        private static readonly Lazy<string[]> modOptions = new Lazy<string[]>(() => new[] { Moderator, Staff, Admin, GlobalMod });
        public static string[] ModOptions => modOptions.Value;

        private static readonly Lazy<string[]> subOptions = new Lazy<string[]>(() => new[] { Subscriber, Founder });
        public static string[] SubOptions => subOptions.Value;
    }
}
