// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector.Client.Models;
using System.Text.Json;

namespace NewConnectorBot.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class BotController : ControllerBase
    {
        private readonly ActivityProcessor _activityProcessor;

        public BotController(IConfiguration configuration)
        {
            _activityProcessor = new ActivityProcessor(configuration);
        }

        [HttpPost]
        [HttpGet]
        public async Task PostAsync()
        {
            Activity incoming=null;
            try
            {
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

/*
 
//Azure.Core.TokenRequestContext _tokenRequestContext = new(new[] { "https://login.microsoftonline.com/botframework.com/oauth2/v2.0/token" });

Azure.Core.TokenRequestContext _tokenRequestContext = new(new[] { "https://api.botframework.com/.default" });
var token = credential.GetToken(_tokenRequestContext);

*/
