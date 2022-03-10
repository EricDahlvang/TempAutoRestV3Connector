// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Identity;
using Microsoft.Bot.Connector.Client;
using Microsoft.Bot.Connector.Client.Models;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace NewConnectorBot
{
    /// <summary>
    /// Crude, hard coded example of 'login', 'logout' bot.
    /// </summary>
    public class ActivityProcessor
    {
        string _appId;
        string _password;
        string _tenant;
        string _msTenantId;
        string _connectionName;
        private static ConcurrentDictionary<string, byte> _usersLoggingIn = new ConcurrentDictionary<string, byte>();

        public ActivityProcessor(IConfiguration configuration)
        {
            _appId = configuration["MicrosoftAppId"];
            _password = configuration["MicrosoftAppPassword"];
            _tenant = configuration["Tenant"];
            _msTenantId = configuration["MsTenant"];
            _connectionName = configuration["UserAuthConnectionName"];
        }

        public async Task ProcessAsync(Activity incoming)
        {
            var outgoing = new Activity { Type = "message", Conversation = incoming.Conversation, From = incoming.Recipient, Recipient = incoming.From, ReplyToId = incoming.Id };
            var connectorClient = CreateConversationsClient(incoming.ServiceUrl);
            switch (incoming.Text)
            {
                case "login":
                    var token = await CreateUserTokenClient(incoming.ServiceUrl).GetUserTokenAsync(incoming.From.Id, _connectionName, incoming.ChannelId, null, CancellationToken.None).ConfigureAwait(false);
                    if (token != null)
                    {
                        outgoing.Text = $"You are already logged in.  Here is your token: {token.Token}";
                        var loggedInResponse = await connectorClient.ReplyToActivityAsync(outgoing);
                        return;
                    }

                    var state = GetSignInUrlState(incoming);
                    var signInUrl = await CreateUserTokenClient(incoming.ServiceUrl).GetSignInUrlAsync(state).ConfigureAwait(false);
                    var buttons = new List<CardAction>() { new CardAction { Title = "Login", Type = ActionTypes.Signin, Value = signInUrl } };
                    var oauthCard = new OAuthCard() { ConnectionName = _connectionName, Text = "Login to continue", Buttons = buttons };
                    var cardAttachment = new Attachment { ContentType = OAuthCard.ContentType, Content = oauthCard };
                    outgoing.Attachments.Add(cardAttachment);
                    var loginResponse = await connectorClient.ReplyToActivityAsync(outgoing);

                    _usersLoggingIn.TryAdd(incoming.From.Id, 1);
                    return;
                case "logout":
                    await CreateUserTokenClient(incoming.ServiceUrl).SignOutUserAsync(incoming.From.Id, _connectionName, incoming.ChannelId, CancellationToken.None).ConfigureAwait(false);
                    outgoing.Text = "You have been signed out.";
                    var logoutResponse = await connectorClient.ReplyToActivityAsync(outgoing);
                    _usersLoggingIn.Remove(incoming.From.Id, out byte _);
                    return;
            }

            if (incoming.Type == ActivityTypes.ConversationUpdate.ToString())
            {
                outgoing.Text = "hi";
                var response = await connectorClient.ReplyToActivityAsync(outgoing);
            }
            else
            {
                if (_usersLoggingIn.ContainsKey(incoming.From.Id))
                {
                    var magicCodeRegex = new Regex(@"(\d{6})");
                    var matched = magicCodeRegex.Match(incoming.Text);
                    if (matched.Success)
                    {
                        var token = await CreateUserTokenClient(incoming.ServiceUrl).GetUserTokenAsync(incoming.From.Id, _connectionName, incoming.ChannelId, incoming.Text, CancellationToken.None).ConfigureAwait(false);
                        if (token != null)
                        {
                            outgoing.Text = $"Successful sign in!  Here is your token: {token.Token}";
                            _usersLoggingIn.Remove(incoming.From.Id, out byte _);
                        }
                        else
                        {
                            outgoing.Text = $"Unable to retrieve your token. Cannot login.";
                        }
                    }
                    else
                    {
                        outgoing.Text = $"You are currently logging in. Please complete the login process by providing a valid magic code, or send 'logout'.";
                    }
                }
                else
                {
                    outgoing.Text = $"you said {incoming.Text}";
                }
                
                if (!string.IsNullOrEmpty(incoming.ReplyToId))
                {
                    var response = await connectorClient.ReplyToActivityAsync(outgoing);
                }
                else
                {
                    var response = await connectorClient.SendToConversationAsync(outgoing);
                }
            }
        }

        private string GetSignInUrlState(Activity activity)
        {
            var tokenExchangeState = new TokenExchangeState
            {
                ConnectionName = _connectionName,
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
                MsAppId = _appId,
            };

            var bytes = JsonSerializer.SerializeToUtf8Bytes<TokenExchangeState>(tokenExchangeState, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return Convert.ToBase64String(bytes);
        }

        public ConversationsClient CreateConversationsClient(string serviceUrl)
        {
            //var options = new ClientSecretCredentialOptions { AuthorityHost = new Uri("https://login.microsoftonline.com") };
            var credential = new ClientSecretCredential(_tenant, _appId, _password);
            return new ConversationsClient(credential, serviceUrl, "https://api.botframework.com/.default", new ConversationsClientOptions());
        }

        private UserTokenClient CreateUserTokenClient(string serviceUrl)
        {
            var options = new ClientSecretCredentialOptions { AuthorityHost = new Uri("https://login.microsoftonline.com/botframework.com") };
            var credential = new ClientSecretCredential(_tenant, _appId, _password, options);
            // "api://" + appId + "/.default"
            return new UserTokenClient(credential, "https://api.botframework.com", "https://api.botframework.com/.default", new UserTokenClientOptions());
        }

        public class TokenExchangeState
        {
            public string ConnectionName { get; set; }
            public ConversationReference Conversation { get; set; }
            public ConversationReference RelatesTo { get; set; }
            public string BotUrl { get; set; }
            public string MsAppId { get; set; }
        }
    }
}
