// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;

namespace Microsoft.Bot.Connector.Client.Models
{
    /// <summary>
    /// ThumbnailCard ContentType value.
    /// </summary>
    [CodeGenModel(Formats = new[] { "json" }, Usage = new[] { "input", "output", "model" })]
    public partial class ThumbnailCard
    {
        /// <summary>
        /// The content type value of a <see cref="ThumbnailCard"/>.
        /// </summary>
        public const string ContentType = "application/vnd.microsoft.card.thumbnail";
    }
}
