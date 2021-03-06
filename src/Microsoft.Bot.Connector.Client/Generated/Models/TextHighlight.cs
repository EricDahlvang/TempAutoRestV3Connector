// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Microsoft.Bot.Connector.Client.Models
{
    /// <summary> Refers to a substring of content within another field. </summary>
    public partial class TextHighlight
    {
        /// <summary> Initializes a new instance of TextHighlight. </summary>
        public TextHighlight()
        {
        }

        /// <summary> Defines the snippet of text to highlight. </summary>
        public string Text { get; set; }
        /// <summary> Occurrence of the text field within the referenced text, if multiple exist. </summary>
        public int? Occurrence { get; set; }
    }
}
