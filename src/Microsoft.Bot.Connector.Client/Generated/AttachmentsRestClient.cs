// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Bot.Connector.Client.Models;

namespace Microsoft.Bot.Connector.Client
{
    internal partial class AttachmentsRestClient
    {
        private Uri endpoint;
        private ClientDiagnostics _clientDiagnostics;
        private HttpPipeline _pipeline;

        /// <summary> Initializes a new instance of AttachmentsRestClient. </summary>
        /// <param name="clientDiagnostics"> The handler for diagnostic messaging in the client. </param>
        /// <param name="pipeline"> The HTTP pipeline for sending and receiving REST requests and responses. </param>
        /// <param name="endpoint"> server parameter. </param>
        public AttachmentsRestClient(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, Uri endpoint = null)
        {
            endpoint ??= new Uri("https://api.botframework.com");

            this.endpoint = endpoint;
            _clientDiagnostics = clientDiagnostics;
            _pipeline = pipeline;
        }

        internal HttpMessage CreateGetAttachmentInfoRequest(string attachmentId)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.Reset(endpoint);
            uri.AppendPath("/v3/attachments/", false);
            uri.AppendPath(attachmentId, true);
            request.Uri = uri;
            request.Headers.Add("Accept", "application/json, text/json");
            return message;
        }

        /// <summary> Get AttachmentInfo structure describing the attachment views. </summary>
        /// <param name="attachmentId"> attachment id. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="attachmentId"/> is null. </exception>
        public async Task<Response<AttachmentInfo>> GetAttachmentInfoAsync(string attachmentId, CancellationToken cancellationToken = default)
        {
            if (attachmentId == null)
            {
                throw new ArgumentNullException(nameof(attachmentId));
            }

            using var message = CreateGetAttachmentInfoRequest(attachmentId);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        AttachmentInfo value = default;
                        using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                        value = AttachmentInfo.DeserializeAttachmentInfo(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw await _clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Get AttachmentInfo structure describing the attachment views. </summary>
        /// <param name="attachmentId"> attachment id. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="attachmentId"/> is null. </exception>
        public Response<AttachmentInfo> GetAttachmentInfo(string attachmentId, CancellationToken cancellationToken = default)
        {
            if (attachmentId == null)
            {
                throw new ArgumentNullException(nameof(attachmentId));
            }

            using var message = CreateGetAttachmentInfoRequest(attachmentId);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        AttachmentInfo value = default;
                        using var document = JsonDocument.Parse(message.Response.ContentStream);
                        value = AttachmentInfo.DeserializeAttachmentInfo(document.RootElement);
                        return Response.FromValue(value, message.Response);
                    }
                default:
                    throw _clientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }

        internal HttpMessage CreateGetAttachmentRequest(string attachmentId, string viewId)
        {
            var message = _pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            var uri = new RawRequestUriBuilder();
            uri.Reset(endpoint);
            uri.AppendPath("/v3/attachments/", false);
            uri.AppendPath(attachmentId, true);
            uri.AppendPath("/views/", false);
            uri.AppendPath(viewId, true);
            request.Uri = uri;
            request.Headers.Add("Accept", "application/json, text/json");
            return message;
        }

        /// <summary> Get the named view as binary content. </summary>
        /// <param name="attachmentId"> attachment id. </param>
        /// <param name="viewId"> View id from attachmentInfo. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="attachmentId"/> or <paramref name="viewId"/> is null. </exception>
        public async Task<Response<Stream>> GetAttachmentAsync(string attachmentId, string viewId, CancellationToken cancellationToken = default)
        {
            if (attachmentId == null)
            {
                throw new ArgumentNullException(nameof(attachmentId));
            }
            if (viewId == null)
            {
                throw new ArgumentNullException(nameof(viewId));
            }

            using var message = CreateGetAttachmentRequest(attachmentId, viewId);
            await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        var value = message.ExtractResponseContent();
                        return Response.FromValue(value, message.Response);
                    }
                case 301:
                case 302:
                    return Response.FromValue<Stream>(null, message.Response);
                default:
                    throw await _clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
            }
        }

        /// <summary> Get the named view as binary content. </summary>
        /// <param name="attachmentId"> attachment id. </param>
        /// <param name="viewId"> View id from attachmentInfo. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="attachmentId"/> or <paramref name="viewId"/> is null. </exception>
        public Response<Stream> GetAttachment(string attachmentId, string viewId, CancellationToken cancellationToken = default)
        {
            if (attachmentId == null)
            {
                throw new ArgumentNullException(nameof(attachmentId));
            }
            if (viewId == null)
            {
                throw new ArgumentNullException(nameof(viewId));
            }

            using var message = CreateGetAttachmentRequest(attachmentId, viewId);
            _pipeline.Send(message, cancellationToken);
            switch (message.Response.Status)
            {
                case 200:
                    {
                        var value = message.ExtractResponseContent();
                        return Response.FromValue(value, message.Response);
                    }
                case 301:
                case 302:
                    return Response.FromValue<Stream>(null, message.Response);
                default:
                    throw _clientDiagnostics.CreateRequestFailedException(message.Response);
            }
        }
    }
}
