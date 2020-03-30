﻿using System;
using System.Collections.Generic;
using System.Linq;
using ToolkitCore.Controllers;
using ToolkitCore.Models;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using Verse;

namespace ToolkitCore
{
    public static class TwitchWrapper
    {
        public static TwitchClient Client { get; private set; }

        public static void StartAsync()
        {
            TwitchWrapper.Initialize(new ConnectionCredentials(ToolkitCoreSettings.bot_username, ToolkitCoreSettings.oauth_token));
        }

        public static void Initialize(ConnectionCredentials credentials)
        {
            ResetClient();

            if (ToolkitCoreSettings.connectOnGameStartup)
            {
                InitializeClient(credentials);
            }
        }

        private static void ResetClient()
        {
            if (Client != null)
            {
                Client.Disconnect();
            }

            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };

            WebSocketClient customClient = new WebSocketClient(clientOptions);

            Client = new TwitchClient(customClient);
        }

        private static void InitializeClient(ConnectionCredentials credentials)
        {
            // Initialize the client with the credentials instance, and setting a default channel to connect to.
            Client.Initialize(credentials, ToolkitCoreSettings.channel_username);

            // Bind callbacks to events
            Client.OnConnected += OnConnected;
            Client.OnJoinedChannel += OnJoinedChannel;
            Client.OnMessageReceived += OnMessageReceived;
            Client.OnWhisperReceived += OnWhisperReceived;
            Client.OnChatCommandReceived += OnChatCommandReceived;

            Client.Connect();
        }

        private static void OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            
        }

        private static void OnConnected(object sender, OnConnectedArgs e)
        {
        }

        private static void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Client.SendMessage(e.Channel, "Toolkit Core has Connected to Chat");
        }

        private static void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e?.ChatMessage != null)
                Log.Message($"{e.ChatMessage.DisplayName}: {e.ChatMessage.Message}");
            else
                Log.Message("Empty ChatMessage Received");

            List<TwitchInterfaceBase> receivers = Current.Game.components.OfType<TwitchInterfaceBase>().ToList();
            
            foreach (TwitchInterfaceBase receiver in receivers)
            {
                receiver.ParseCommand(e.ChatMessage);
            }
        }

        private static void OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            if (e?.Command?.ChatMessage != null)
                Log.Message($"{e.Command.ChatMessage.DisplayName}: {e.Command.ChatMessage.Message}");
            else
                Log.Message("Empty Command Received");

            ToolkitChatCommand chatCommand = ChatCommandController.GetChatCommand(e.Command.CommandText);
            if (chatCommand != null)
            {
                chatCommand.TryExecute(e.Command as ITwitchCommand);
            }
        }

        public static void SendChatMessage(string message)
        {
            Client.SendMessage(Client.GetJoinedChannel(ToolkitCoreSettings.channel_username), message);
        }
    }
}

