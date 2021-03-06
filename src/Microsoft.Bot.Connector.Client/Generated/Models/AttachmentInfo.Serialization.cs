// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace Microsoft.Bot.Connector.Client.Models
{
    public partial class AttachmentInfo
    {
        internal static AttachmentInfo DeserializeAttachmentInfo(JsonElement element)
        {
            Optional<string> name = default;
            Optional<string> type = default;
            Optional<IReadOnlyList<AttachmentView>> views = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("name"))
                {
                    name = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("type"))
                {
                    type = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("views"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    List<AttachmentView> array = new List<AttachmentView>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(AttachmentView.DeserializeAttachmentView(item));
                    }
                    views = array;
                    continue;
                }
            }
            return new AttachmentInfo(name.Value, type.Value, Optional.ToList(views));
        }
    }
}
