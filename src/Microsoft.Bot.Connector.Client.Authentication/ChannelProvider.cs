// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading.Tasks;

namespace Microsoft.Bot.Connector.Client.Authentication
{
    /// <summary>
    /// This abstract class allows Bots to provide their own
    /// implementation for the configuration parameters to connect to a Bot.
    /// </summary>
    public abstract class ChannelProvider
    {
        /// <summary>
        /// Gets the channel service property for this channel provider.
        /// </summary>
        /// <returns>The channel service property for the channel provider.</returns>
        public abstract Task<string> GetChannelServiceAsync();

        /// <summary>
        /// Gets a value of whether this provider represents a channel on Government Azure.
        /// </summary>
        /// <returns>True if this channel provider represents a channel on Government Azure.</returns>
        public abstract bool IsGovernment();

        /// <summary>
        /// Gets a value of whether this provider represents a channel on Public Azure.
        /// </summary>
        /// <returns>True if this channel provider represents a channel on Public Azure.</returns>
        public abstract bool IsPublicAzure();
    }
}
