// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Microsoft.Bot.Connector.Client.Models
{
    public partial class TokenExchangeResource
    {
        internal static TokenExchangeResource DeserializeTokenExchangeResource(JsonElement element)
        {
            Optional<string> id = default;
            Optional<string> uri = default;
            Optional<string> providerId = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("id"))
                {
                    id = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("uri"))
                {
                    uri = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("providerId"))
                {
                    providerId = property.Value.GetString();
                    continue;
                }
            }
            return new TokenExchangeResource(id.Value, uri.Value, providerId.Value);
        }
    }
}
