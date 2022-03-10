// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Bot.Connector.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Bot.Connector.Client
{
    public class ConversationsClient
    {
        private readonly ConversationsRestClient _client;

        public ConversationsClient(
            TokenCredential credential,
            string endpoint,
            string scope,
            ConversationsClientOptions options = null)
        {
            var policy = new BearerTokenAuthenticationPolicy(credential, scope);
            var diagnostics = new ClientDiagnostics(options);
            var pipeline = HttpPipelineBuilder.Build(options, policy);

            _client = new ConversationsRestClient(diagnostics, pipeline, new Uri(endpoint));
        }

        public async Task<ResourceResponse> SendToConversationAsync(Activity activity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.SendToConversationAsync(activity.Conversation.Id, activity, cancellationToken).ConfigureAwait(false);
            return response.Value;
        }

        public async Task<ResourceResponse> ReplyToActivityAsync(Activity activity, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (activity.ReplyToId == null)
            {
                throw new InvalidOperationException("ReplyToId is required.");
            }

            var response = await _client.ReplyToActivityAsync(activity.Conversation.Id, activity.ReplyToId, activity, cancellationToken).ConfigureAwait(false);
            return response.Value;
        }
    }
}
