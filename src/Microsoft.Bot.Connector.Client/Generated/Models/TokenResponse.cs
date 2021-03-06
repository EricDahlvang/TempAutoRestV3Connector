// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Microsoft.Bot.Connector.Client.Models
{
    /// <summary> A response that includes a user token. </summary>
    public partial class TokenResponse
    {
        /// <summary> Initializes a new instance of TokenResponse. </summary>
        public TokenResponse()
        {
        }

        /// <summary> Initializes a new instance of TokenResponse. </summary>
        /// <param name="channelId"> The channel id. </param>
        /// <param name="connectionName"> The connection name. </param>
        /// <param name="token"> The token. </param>
        /// <param name="expiration"> The expiration.. </param>
        public TokenResponse(string channelId, string connectionName, string token, string expiration)
        {
            ChannelId = channelId;
            ConnectionName = connectionName;
            Token = token;
            Expiration = expiration;
        }

        /// <summary> The channelId of the TokenResponse. </summary>
        public string ChannelId { get; }
        /// <summary> The connection name. </summary>
        public string ConnectionName { get; }
        /// <summary> The user token. </summary>
        public string Token { get; }
        /// <summary> Expiration for the token, in ISO 8601 format (e.g. &quot;2007-04-05T14:30Z&quot;). </summary>
        public string Expiration { get; }
    }
}
