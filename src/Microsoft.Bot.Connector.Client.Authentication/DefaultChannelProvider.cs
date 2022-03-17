// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;

namespace Microsoft.Bot.Connector.Client.Authentication
{
    public class DefaultChannelProvider : ChannelProvider
    {
        private readonly string _channelService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultChannelProvider"/> class.
        /// Creates a ChannelProvider with no ChannelService which will use Public Azure.
        /// </summary>
        public DefaultChannelProvider()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultChannelProvider"/> class.
        /// </summary>
        /// <param name="channelService">The ChannelService to use. Null or empty strings represent Public Azure, the string 'https://botframework.us' represents US Government Azure, and other values are for private channels.</param>
        public DefaultChannelProvider(string channelService)
        {
            this._channelService = channelService;
        }

        public override Task<string> GetChannelServiceAsync()
        {
            return Task.FromResult(this._channelService);
        }

        public override bool IsGovernment()
        {
            return string.Equals(GovernmentAuthenticationConstants.ChannelService, _channelService, StringComparison.OrdinalIgnoreCase);
        }

        public override bool IsPublicAzure()
        {
            return string.IsNullOrEmpty(_channelService);
        }
    }
}
