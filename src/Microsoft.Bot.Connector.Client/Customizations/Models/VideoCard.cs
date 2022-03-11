// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Azure.Core;

namespace Microsoft.Bot.Connector.Client.Models
{
    /// <summary>
    /// VideoCard ContentType value.
    /// </summary>
    [CodeGenModel(Formats = new[] { "json" }, Usage = new[] { "input", "output", "model" })]
    public partial class VideoCard
    {
        /// <summary>
        /// The content type value of a <see cref="VideoCard"/>.
        /// </summary>
        public const string ContentType = "application/vnd.microsoft.card.video";
    }
}
