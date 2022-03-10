// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Microsoft.Bot.Connector.Client.Models
{
    public partial class SignInResource
    {
        internal static SignInResource DeserializeSignInResource(JsonElement element)
        {
            Optional<string> signInLink = default;
            Optional<TokenExchangeResource> tokenExchangeResource = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("signInLink"))
                {
                    signInLink = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("tokenExchangeResource"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    tokenExchangeResource = TokenExchangeResource.DeserializeTokenExchangeResource(property.Value);
                    continue;
                }
            }
            return new SignInResource(signInLink.Value, tokenExchangeResource.Value);
        }
    }
}
