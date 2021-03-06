// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace Microsoft.Bot.Connector.Client.Models
{
    /// <summary> Action types valid for ContactRelationUpdate activities. </summary>
    internal readonly partial struct ContactRelationUpdateActionTypes : IEquatable<ContactRelationUpdateActionTypes>
    {
        private readonly string _value;

        /// <summary> Determines if two <see cref="ContactRelationUpdateActionTypes"/> values are the same. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public ContactRelationUpdateActionTypes(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string AddValue = "add";
        private const string RemoveValue = "remove";

        /// <summary> add. </summary>
        public static ContactRelationUpdateActionTypes Add { get; } = new ContactRelationUpdateActionTypes(AddValue);
        /// <summary> remove. </summary>
        public static ContactRelationUpdateActionTypes Remove { get; } = new ContactRelationUpdateActionTypes(RemoveValue);
        /// <summary> Determines if two <see cref="ContactRelationUpdateActionTypes"/> values are the same. </summary>
        public static bool operator ==(ContactRelationUpdateActionTypes left, ContactRelationUpdateActionTypes right) => left.Equals(right);
        /// <summary> Determines if two <see cref="ContactRelationUpdateActionTypes"/> values are not the same. </summary>
        public static bool operator !=(ContactRelationUpdateActionTypes left, ContactRelationUpdateActionTypes right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="ContactRelationUpdateActionTypes"/>. </summary>
        public static implicit operator ContactRelationUpdateActionTypes(string value) => new ContactRelationUpdateActionTypes(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is ContactRelationUpdateActionTypes other && Equals(other);
        /// <inheritdoc />
        public bool Equals(ContactRelationUpdateActionTypes other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
