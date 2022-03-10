// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;

namespace Microsoft.Bot.Connector.Client.Models
{
    /// <summary> Conversation and its members. </summary>
    public partial class ConversationMembers
    {
        /// <summary> Initializes a new instance of ConversationMembers. </summary>
        internal ConversationMembers()
        {
            Members = new ChangeTrackingList<ChannelAccount>();
        }

        /// <summary> Initializes a new instance of ConversationMembers. </summary>
        /// <param name="id"> Conversation ID. </param>
        /// <param name="members"> List of members in this conversation. </param>
        internal ConversationMembers(string id, IReadOnlyList<ChannelAccount> members)
        {
            Id = id;
            Members = members;
        }

        /// <summary> Conversation ID. </summary>
        public string Id { get; }
        /// <summary> List of members in this conversation. </summary>
        public IReadOnlyList<ChannelAccount> Members { get; }
    }
}