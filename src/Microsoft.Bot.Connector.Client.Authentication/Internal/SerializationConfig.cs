﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.Bot.Connector.Client.Authentication
{
    /// <summary>
    /// This class defines the configuration settings used when serializing and deserializing data
    /// contained in objects defined by the Bot Framework Protocol schema.
    /// </summary>
    internal static class SerializationConfig
    {
        /// <summary>
        /// The default serialization settings for use in most cases.
        /// </summary>
        public static readonly JsonSerializerOptions DefaultSerializeOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            Converters = { new UtcDateTimeConverter() }
        };

        /// <summary>
        /// The default deserialization settings for use in most cases.
        /// </summary>
        public static readonly JsonSerializerOptions DefaultDeserializeOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            Converters = { new UtcDateTimeConverter() }
        };

        private class UtcDateTimeConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return reader.GetDateTime().ToUniversalTime();
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToUniversalTime());
            }
        }
    }
}
