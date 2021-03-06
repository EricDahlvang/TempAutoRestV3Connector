// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;

namespace Microsoft.Bot.Connector.Client.Models
{
    /// <summary> An animation card (Ex: gif or short video clip). </summary>
    public partial class AnimationCard
    {
        /// <summary> Initializes a new instance of AnimationCard. </summary>
        public AnimationCard()
        {
            Media = new ChangeTrackingList<MediaUrl>();
            Buttons = new ChangeTrackingList<CardAction>();
        }

        /// <summary> Title of this card. </summary>
        public string Title { get; }
        /// <summary> Subtitle of this card. </summary>
        public string Subtitle { get; }
        /// <summary> Text of this card. </summary>
        public string Text { get; }
        /// <summary> Thumbnail placeholder. </summary>
        public ThumbnailUrl Image { get; }
        /// <summary> Media URLs for this card. When this field contains more than one URL, each URL is an alternative format of the same content. </summary>
        public IReadOnlyList<MediaUrl> Media { get; }
        /// <summary> Actions on this card. </summary>
        public IReadOnlyList<CardAction> Buttons { get; }
        /// <summary> This content may be shared with others (default:true). </summary>
        public bool? Shareable { get; }
        /// <summary> Should the client loop playback at end of content (default:true). </summary>
        public bool? Autoloop { get; }
        /// <summary> Should the client automatically start playback of media in this card (default:true). </summary>
        public bool? Autostart { get; }
        /// <summary> Aspect ratio of thumbnail/media placeholder. Allowed values are &quot;16:9&quot; and &quot;4:3&quot;. </summary>
        public string Aspect { get; }
        /// <summary> Describes the length of the media content without requiring a receiver to open the content. Formatted as an ISO 8601 Duration field. </summary>
        public string Duration { get; }
        /// <summary> Supplementary parameter for this card. </summary>
        public object Value { get; }
    }
}
