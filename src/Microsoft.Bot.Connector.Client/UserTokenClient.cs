// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Bot.Connector.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Bot.Connector.Client
{
    public class UserTokenClient
    {
        private readonly UserTokenRestClient _client;
        private readonly BotSignInRestClient _signInClient;
        private UserTokenClientOptions _options;
        private ClientDiagnostics _clientDiagnostics;

        public UserTokenClient(
            TokenCredential credential,
            string endpoint,
            string scope,
            UserTokenClientOptions options = null)
            : this(new BearerTokenAuthenticationPolicy(credential, scope), endpoint, options)
        {
        }

        public UserTokenClient(
            HttpPipelinePolicy pipelinePolicy,
            string endpoint,
            UserTokenClientOptions options = null)
        {
            _options = options ?? new UserTokenClientOptions();

            _clientDiagnostics = new ClientDiagnostics(_options);
            var pipeline = HttpPipelineBuilder.Build(_options, pipelinePolicy);
            
            _client = new UserTokenRestClient(_clientDiagnostics, pipeline, new Uri(endpoint));
            _signInClient = new BotSignInRestClient(_clientDiagnostics, pipeline, new Uri(endpoint));
        }

        public async Task<string> GetSignInUrlAsync(string state, string codeChallenge = null, string emulatorUrl = null, string finalRedirect = null, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(UserTokenClient)}.{nameof(GetSignInUrlAsync)}");
            scope.Start();
            try
            {
                _ = state ?? throw new ArgumentNullException(nameof(state));

                return await _signInClient.GetSignInUrlAsync(state, codeChallenge, emulatorUrl, finalRedirect, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public async Task<SignInResource> GetSignInResourceAsync(string appId, string connectionName, Activity activity, string finalRedirect, CancellationToken cancellationToken)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(UserTokenClient)}.{nameof(GetSignInResourceAsync)}");
            scope.Start();
            try
            {
                _ = appId ?? throw new ArgumentNullException(nameof(appId));
                _ = connectionName ?? throw new ArgumentNullException(nameof(connectionName));
                _ = activity ?? throw new ArgumentNullException(nameof(activity));

                var state = CreateTokenExchangeState(appId, connectionName, activity);
                var result = await _signInClient.GetSignInResourceAsync(state, null, null, finalRedirect, cancellationToken).ConfigureAwait(false);
                return result.Value;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public async Task<TokenResponse> GetUserTokenAsync(string userId, string connectionName, string channelId, string magicCode, CancellationToken cancellationToken)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(UserTokenClient)}.{nameof(GetUserTokenAsync)}");
            scope.Start();
            try
            {
                _ = userId ?? throw new ArgumentNullException(nameof(userId));
                _ = connectionName ?? throw new ArgumentNullException(nameof(connectionName));

                var result = await _client.GetTokenAsync(userId, connectionName, channelId, magicCode, cancellationToken).ConfigureAwait(false);

                if(result == null)
                {
                    return null;
                }

                return result;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public async Task SignOutUserAsync(string userId, string connectionName, string channelId, CancellationToken cancellationToken)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(UserTokenClient)}.{nameof(SignOutUserAsync)}");
            scope.Start();
            try
            {
                _ = userId ?? throw new ArgumentNullException(nameof(userId));
                _ = connectionName ?? throw new ArgumentNullException(nameof(connectionName));

                await _client.SignOutAsync(userId, connectionName, channelId, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public async Task<TokenStatus[]> GetTokenStatusAsync(string userId, string channelId, string includeFilter, CancellationToken cancellationToken)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(UserTokenClient)}.{nameof(GetTokenStatusAsync)}");
            scope.Start();
            try
            {
                _ = userId ?? throw new ArgumentNullException(nameof(userId));
                _ = channelId ?? throw new ArgumentNullException(nameof(channelId));

                var result = await _client.GetTokenStatusAsync(userId, channelId, includeFilter, cancellationToken).ConfigureAwait(false);
                return result.Value?.ToArray();
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public async Task<Dictionary<string, TokenResponse>> GetAadTokensAsync(string userId, string connectionName, string[] resourceUrls, string channelId, CancellationToken cancellationToken)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(UserTokenClient)}.{nameof(GetAadTokensAsync)}");
            scope.Start();
            try
            {
                _ = userId ?? throw new ArgumentNullException(nameof(userId));
                _ = connectionName ?? throw new ArgumentNullException(nameof(connectionName));

                return (Dictionary<string, TokenResponse>)await _client.GetAadTokensAsync(userId, connectionName, new AadResourceUrls() { ResourceUrls = resourceUrls?.ToList() }, channelId, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public async Task<TokenResponse> ExchangeTokenAsync(string userId, string connectionName, string channelId, TokenExchangeRequest exchangeRequest, CancellationToken cancellationToken)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(UserTokenClient)}.{nameof(ExchangeTokenAsync)}");
            scope.Start();
            try
            {
                _ = userId ?? throw new ArgumentNullException(nameof(userId));
                _ = connectionName ?? throw new ArgumentNullException(nameof(connectionName));

                var result = await _client.ExchangeAsyncAsync(userId, connectionName, channelId, exchangeRequest, cancellationToken).ConfigureAwait(false);

                if (result.Value is ErrorResponse errorResponse)
                {
                    throw new InvalidOperationException($"Unable to exchange token: ({errorResponse?.Error?.Code}) {errorResponse?.Error?.Message}");
                }
                else if (result.Value is TokenResponse tokenResponse)
                {
                    return tokenResponse;
                }
                else
                {
                    throw new InvalidOperationException($"ExchangeAsyncAsync returned improper result: {result.GetType()}");
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static string CreateTokenExchangeState(string appId, string connectionName, Activity activity)
        {
            _ = appId ?? throw new ArgumentNullException(nameof(appId));
            _ = connectionName ?? throw new ArgumentNullException(nameof(connectionName));
            _ = activity ?? throw new ArgumentNullException(nameof(activity));

            var tokenExchangeState = new TokenExchangeState
            {
                ConnectionName = connectionName,
                Conversation = new ConversationReference
                {
                    ActivityId = activity.Type != ActivityTypes.ConversationUpdate.ToString() || (!string.Equals(activity.ChannelId, "directline", StringComparison.OrdinalIgnoreCase) && !string.Equals(activity.ChannelId, "webchat", StringComparison.OrdinalIgnoreCase)) ? activity.Id : null,
                    Bot = activity.Recipient,       // Activity is from the user to the bot
                    ChannelId = activity.ChannelId,
                    Conversation = activity.Conversation,
                    Locale = activity.Locale,
                    ServiceUrl = activity.ServiceUrl,
                    User = activity.From,
                },
                RelatesTo = activity.RelatesTo,
                MsAppId = appId,
            };

            var bytes = JsonSerializer.SerializeToUtf8Bytes<TokenExchangeState>(tokenExchangeState, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return Convert.ToBase64String(bytes);
        }

        private class TokenExchangeState
        {
            public string ConnectionName { get; set; }
            public ConversationReference Conversation { get; set; }
            public ConversationReference RelatesTo { get; set; }
            public string BotUrl { get; set; }
            public string MsAppId { get; set; }
        }
    }
}
