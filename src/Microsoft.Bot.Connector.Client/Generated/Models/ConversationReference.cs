// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Microsoft.Bot.Connector.Client.Models
{
    /// <summary> An object relating to a particular point in a conversation. </summary>
    public partial class ConversationReference
    {
        /// <summary> Initializes a new instance of ConversationReference. </summary>
        public ConversationReference()
        {
        }

        /// <summary> (Optional) ID of the activity to refer to. </summary>
        public string ActivityId { get; set; }
        /// <summary> (Optional) User participating in this conversation. </summary>
        public ChannelAccount User { get; set; }
        /// <summary> (Optional) Bot participating in this conversation. </summary>
        public ChannelAccount Bot { get; set; }
        /// <summary> Reference to the conversation. </summary>
        public ConversationAccount Conversation { get; set; }
        /// <summary> ID of the channel in which the referenced conversation exists. </summary>
        public string ChannelId { get; set; }
        /// <summary> (Optional) Service endpoint where operations concerning the referenced conversation may be performed. </summary>
        public string ServiceUrl { get; set; }
        /// <summary> (Optional) A BCP-47 locale name for the referenced conversation. </summary>
        public string Locale { get; set; }
    }
}