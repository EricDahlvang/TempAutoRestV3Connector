// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Bot.Connector.Client.Models;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Bot.Connector.Client
{
    public class AttachmentsClient
    {
        private readonly AttachmentsRestClient _client;
        private AttachmentsClientOptions _options;
        private ClientDiagnostics _clientDiagnostics;

        public AttachmentsClient(
            TokenCredential credential,
            string endpoint,
            string scope,
            AttachmentsClientOptions options = null)
            : this(new BearerTokenAuthenticationPolicy(credential, scope), endpoint, options)
        {
        }

        public AttachmentsClient(
            HttpPipelinePolicy pipelinePolicy,
            string endpoint,
            AttachmentsClientOptions options = null)
        {
            _options = options ?? new AttachmentsClientOptions();

            _clientDiagnostics = new ClientDiagnostics(_options);
            var pipeline = HttpPipelineBuilder.Build(_options, pipelinePolicy);

            _client = new AttachmentsRestClient(_clientDiagnostics, pipeline, new Uri(endpoint));
        }

        /// <summary> Get the named view as binary content. </summary>
        /// <param name="attachmentId"> attachment id. </param>
        /// <param name="viewId"> View id from attachmentInfo. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="attachmentId"/> or <paramref name="viewId"/> is null. </exception>
        /// <returns>The <see cref="Stream"/> for the <see cref="Attachment"/> file.</returns>
        public async Task<Stream> GetAttachmentAsync(string attachmentId, string viewId, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(AttachmentsClient)}.{nameof(GetAttachmentAsync)}");
            scope.Start();
            try
            {
                _ = attachmentId ?? throw new ArgumentNullException(nameof(attachmentId));
                _ = viewId ?? throw new ArgumentNullException(nameof(viewId));

                var response = await _client.GetAttachmentAsync(attachmentId, viewId, cancellationToken).ConfigureAwait(false);
                return response.Value;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary> Get AttachmentInfo structure describing the attachment views. </summary>
        /// <param name="attachmentId"> attachment id. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="attachmentId"/> is null. </exception>
        /// <returns>The <see cref="AttachmentInfo"/>.</returns>
        public async Task<AttachmentInfo> GetAttachmentInfoAsync(string attachmentId, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(AttachmentsClient)}.{nameof(GetAttachmentInfoAsync)}");
            scope.Start();
            try
            {
                _ = attachmentId ?? throw new ArgumentNullException(nameof(attachmentId));
                
                var response = await _client.GetAttachmentInfoAsync(attachmentId, cancellationToken).ConfigureAwait(false);
                return response.Value;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
    }
}
