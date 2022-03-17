﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;

namespace Microsoft.Bot.Connector.Client.Models
{
    /// <summary>
    /// Ids of channels supported by the Bot Builder.
    /// </summary>
    public static class Channels
    {
        /// <summary>
        /// Alexa channel.
        /// </summary>
        public const string Alexa = "alexa";

        /// <summary>
        /// Console channel.
        /// </summary>
        public const string Console = "console";

        /// <summary>
        /// Direct Line channel.
        /// </summary>
        public const string Directline = "directline";

        /// <summary>
        /// Direct Line Speech channel.
        /// </summary>
        public const string DirectlineSpeech = "directlinespeech";

        /// <summary>
        /// Email channel.
        /// </summary>
        public const string Email = "email";

        /// <summary>
        /// Emulator channel.
        /// </summary>
        public const string Emulator = "emulator";

        /// <summary>
        /// Facebook channel.
        /// </summary>
        public const string Facebook = "facebook";

        /// <summary>
        /// Group Me channel.
        /// </summary>
        public const string Groupme = "groupme";

        /// <summary>
        /// Kik channel.
        /// </summary>
        public const string Kik = "kik";

        /// <summary>
        /// Line channel.
        /// </summary>
        public const string Line = "line";

        /// <summary>
        /// MS Teams channel.
        /// </summary>
        public const string Msteams = "msteams";

        /// <summary>
        /// Slack channel.
        /// </summary>
        public const string Slack = "slack";

        /// <summary>
        /// SMS (Twilio) channel.
        /// </summary>
        public const string Sms = "sms";

        /// <summary>
        /// Telegram channel.
        /// </summary>
        public const string Telegram = "telegram";

        /// <summary>
        /// WebChat channel.
        /// </summary>
        public const string Webchat = "webchat";

        /// <summary>
        /// Test channel.
        /// </summary>
        public const string Test = "test";

        /// <summary>
        /// Twilio channel.
        /// </summary>
        public const string Twilio = "twilio-sms";

        /// <summary>
        /// Telephony channel.
        /// </summary>
        public const string Telephony = "telephony";

        /// <summary>
        /// Omni channel.
        /// </summary>
        public const string Omni = "omnichannel";
    }
}
