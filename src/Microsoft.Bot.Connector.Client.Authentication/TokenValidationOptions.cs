// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Connector.Client.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.Bot.Connector.Client.Authentication
{
    /// <summary>
    /// Options for jwt token validation.
    /// </summary>
    public class TokenValidationOptions
    {
        public ValueTask<Activity?> ActivityTask { get; set; }
        public string AuthHeader { get; set; } 
        public CredentialProvider Credentials { get; set; }
        public ChannelProvider ChannelProvider { get; set; }
        public HttpClient HttpClient { get; set; }
    }
}
