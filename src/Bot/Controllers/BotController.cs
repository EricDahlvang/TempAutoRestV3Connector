// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector.Client.Authentication;
using Microsoft.Bot.Connector.Client.Models;
using System.Text.Json;

namespace NewConnectorBot.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class BotController : ControllerBase
    {
        private readonly ActivityProcessor _activityProcessor;
        private readonly string _password;
        private readonly string _appId;

        public BotController(IConfiguration configuration)
        {
            _appId = configuration["MicrosoftAppId"];
            _password = configuration["MicrosoftAppPassword"];
            _activityProcessor = new ActivityProcessor(configuration);
        }

        [HttpPost]
        [HttpGet]
        public async Task PostAsync()
        {
            Activity incoming = null;
            try
            {
                var authHeader = Request.Headers["Authorization"];
                var channelProvider = new DefaultChannelProvider();
                var credentialProvider = new DefaultCredentialProvider(_appId, _password);

                incoming = await JsonSerializer.DeserializeAsync<Activity>(Request.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                await _activityProcessor.ProcessAsync(incoming).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var outgoing = new Activity { Text = $"Exception: {ex.Message}", Type = "message", Conversation = incoming.Conversation, From = incoming.Recipient, Recipient = incoming.From, ReplyToId = incoming.Id };
                var result = await _activityProcessor.CreateConversationsClient(incoming.ServiceUrl).SendToConversationAsync(outgoing).ConfigureAwait(false);
            }
        }
    }
}