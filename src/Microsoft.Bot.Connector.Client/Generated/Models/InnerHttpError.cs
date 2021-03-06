// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Microsoft.Bot.Connector.Client.Models
{
    /// <summary> Object representing inner http error. </summary>
    internal partial class InnerHttpError
    {
        /// <summary> Initializes a new instance of InnerHttpError. </summary>
        internal InnerHttpError()
        {
        }

        /// <summary> Initializes a new instance of InnerHttpError. </summary>
        /// <param name="statusCode"> HttpStatusCode from failed request. </param>
        /// <param name="body"> Body from failed request. </param>
        internal InnerHttpError(int? statusCode, object body)
        {
            StatusCode = statusCode;
            Body = body;
        }

        /// <summary> HttpStatusCode from failed request. </summary>
        public int? StatusCode { get; }
        /// <summary> Body from failed request. </summary>
        public object Body { get; }
    }
}
