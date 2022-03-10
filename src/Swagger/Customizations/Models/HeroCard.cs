// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;

namespace Microsoft.Bot.Connector.Client.Models
{
    /// <summary>
    /// HeroCard ContentType value.
    /// </summary>
    [CodeGenModel(Formats = new[] { "json" }, Usage = new[] { "input", "output", "model" })]
    public partial class HeroCard
    {
        /// <summary>
        /// The content type value of a <see cref="HeroCard"/>.
        /// </summary>
        public const string ContentType = "application/vnd.microsoft.card.hero";
    }
}
