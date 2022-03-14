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
        private ClientDiagnostics _clientDiagnostics;

        public ConversationsClient(
            TokenCredential credential,
            string endpoint,
            string scope,
            ConversationsClientOptions options = null)
        {
            var policy = new BearerTokenAuthenticationPolicy(credential, scope);
            _clientDiagnostics = new ClientDiagnostics(options);

            var pipeline = HttpPipelineBuilder.Build(options, policy);

            _client = new ConversationsRestClient(_clientDiagnostics, pipeline, new Uri(endpoint));
        }

        /// <summary>
        /// SendToConversation.
        /// </summary>
        /// <remarks>
        /// This method allows you to send an activity to the end of a conversation.
        ///
        /// This is slightly different from ReplyToActivity().
        /// * SendToConversation(activity) - will append the activity to the end
        /// of the conversation according to the timestamp or semantics of the channel.
        /// * ReplyToActivity(activity) - adds the activity as a reply
        /// to another activity, if the channel supports it. If the channel does not
        /// support nested replies, ReplyToActivity falls back to SendToConversation.
        ///
        /// Use ReplyToActivity when replying to a specific activity in the
        /// conversation.
        ///
        /// Use SendToConversation in all other cases.
        /// </remarks>
        /// <param name='activity'>
        /// Activity to send.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <returns>The <see cref="ResourceResponse"/>.</returns>
        public async Task<ResourceResponse> SendToConversationAsync(Activity activity, CancellationToken cancellationToken = default(CancellationToken))
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(ConversationsClient)}.{nameof(SendToConversationAsync)}");
            scope.Start();
            try
            {
                _ = activity ?? throw new ArgumentNullException(nameof(activity));

                var response = await _client.SendToConversationAsync(activity.Conversation.Id, activity, cancellationToken).ConfigureAwait(false);
                return response.Value;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Replyto an activity in an existing conversation.
        /// </summary>
        /// <param name='activity'>
        /// Activity to send.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <returns>The <see cref="ResourceResponse"/>.</returns>
        public async Task<ResourceResponse> ReplyToActivityAsync(Activity activity, CancellationToken cancellationToken = default(CancellationToken))
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(ConversationsClient)}.{nameof(ReplyToActivityAsync)}");
            scope.Start();
            try
            {
                _ = activity ?? throw new ArgumentNullException(nameof(activity));
                if (activity.ReplyToId == null)
                {
                    throw new InvalidOperationException("ReplyToId is required.");
                }

                var response = await _client.ReplyToActivityAsync(activity.Conversation.Id, activity.ReplyToId, activity, cancellationToken).ConfigureAwait(false);
                return response.Value;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// List the Conversations in which this bot has participated.
        /// Call this method method with a continuation token.
        /// The return value is a ConversationsResult, which contains an array of ConversationMembers and a skip token.  If the skip token is not empty, then
        /// there are further values to be returned. Call this method again with the returned token to get more values.
        /// Each ConversationMembers object contains the ID of the conversation and an array of ChannelAccounts that describe the members of the conversation.
        /// </summary>
        /// <param name="continuationToken"> Kkip or continuation token. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns>The <see cref="ConversationsResult"/>.</returns>
        public async Task<ConversationsResult> GetConversationsAsync(string continuationToken = null, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(ConversationsClient)}.{nameof(ReplyToActivityAsync)}");
            scope.Start();
            try
            {
                var response = await _client.GetConversationsAsync(continuationToken, cancellationToken).ConfigureAwait(false);
                return response.Value;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Create a new Conversation.
        /// Call this method with:
        /// 
        /// * Bot being the bot creating the conversation
        /// 
        /// * IsGroup set to true if this is not a direct message (default is false)
        /// 
        /// * Array containing the members to include in the conversation
        /// 
        /// The return value is a ResourceResponse which contains a conversation id which is suitable for use
        /// in the message payload and REST API uris.
        /// 
        /// Most channels only support the semantics of bots initiating a direct message conversation.  An example of how to do that would be:
        /// 
        /// ```
        /// var resource = await conversations.CreateConversationAsync(new ConversationParameters(){ Bot = bot, members = new ChannelAccount[] { new ChannelAccount(&quot;user1&quot;) } );
        /// 
        /// await connect.Conversations.SendToConversationAsync(resource.Id, new Activity() ... ) ;
        /// ```.
        /// </summary>
        /// <param name="parameters"> Parameters to create the conversation from. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="parameters"/> is null. </exception>
        /// <returns>The <see cref="ConversationResourceResponse"/>.</returns>
        public async Task<ConversationResourceResponse> CreateConversationAsync(ConversationParameters parameters, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(ConversationsClient)}.{nameof(CreateConversationAsync)}");
            scope.Start();
            try
            {
                _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
                var response = await _client.CreateConversationAsync(parameters, cancellationToken).ConfigureAwait(false);
                return response.Value;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// This method allows you to upload the historic activities to the conversation.
        /// 
        /// Sender must ensure that the historic activities have unique ids and appropriate timestamps.
        /// The ids are used by the client to deal with duplicate activities and the timestamps are used
        /// by the client to render the activities in the right order.
        /// </summary>
        /// <param name="conversationId"> Conversation ID. </param>
        /// <param name="history"> Historic activities. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="conversationId"/> or <paramref name="history"/> is null. </exception>
        /// <returns>The <see cref="ResourceResponse"/>.</returns>
        public async Task<ResourceResponse> SendConversationHistoryAsync(string conversationId, Transcript history, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(ConversationsClient)}.{nameof(SendConversationHistoryAsync)}");
            scope.Start();
            try
            {
                _ = conversationId ?? throw new ArgumentNullException(nameof(conversationId));
                _ = history ?? throw new ArgumentNullException(nameof(history));

                var response = await _client.SendConversationHistoryAsync(conversationId, history, cancellationToken).ConfigureAwait(false);
                return response.Value;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Edit an existing activity.
        /// 
        /// Some channels allow you to edit an existing activity to reflect the new state of a bot conversation.
        /// 
        /// For example, you can remove buttons after someone has clicked &quot;Approve&quot; button.
        /// </summary>
        /// <param name="conversationId"> Conversation ID. </param>
        /// <param name="activityId"> activityId to update. </param>
        /// <param name="activity"> replacement Activity. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="conversationId"/>, <paramref name="activityId"/>, or <paramref name="activity"/> is null. </exception>
        /// <returns>The <see cref="ResourceResponse"/>.</returns>
        public async Task<ResourceResponse> UpdateActivityAsync(string conversationId, string activityId, Activity activity, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(ConversationsClient)}.{nameof(UpdateActivityAsync)}");
            scope.Start();
            try
            {
                _ = conversationId ?? throw new ArgumentNullException(nameof(conversationId));
                _ = activityId ?? throw new ArgumentNullException(nameof(activityId));
                _ = activity ?? throw new ArgumentNullException(nameof(activity));

                var response = await _client.UpdateActivityAsync(conversationId, activityId, activity, cancellationToken).ConfigureAwait(false);
                return response.Value;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Delete an existing activity.
        /// 
        /// Some channels allow you to delete an existing activity, and if successful this method will remove the specified activity.
        /// </summary>
        /// <param name="conversationId"> Conversation ID. </param>
        /// <param name="activityId"> activityId to delete. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="conversationId"/> or <paramref name="activityId"/> is null. </exception>
        public async Task DeleteActivityAsync(string conversationId, string activityId, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(ConversationsClient)}.{nameof(DeleteActivityAsync)}");
            scope.Start();
            try
            {
                _ = conversationId ?? throw new ArgumentNullException(nameof(conversationId));
                _ = activityId ?? throw new ArgumentNullException(nameof(activityId));
                
                var response = await _client.DeleteActivityAsync(conversationId, activityId, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Get a single member of a conversation.
        /// 
        /// This REST API takes a ConversationId and MemberId and returns a single ChannelAccount object, if that member is found in this conversation.
        /// </summary>
        /// <param name="conversationId"> Conversation ID. </param>
        /// <param name="memberId"> The String to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="conversationId"/> or <paramref name="memberId"/> is null. </exception>
        /// <returns>The <see cref="ChannelAccount"/> for the member.</returns>
        public async Task<ChannelAccount> GetConversationMemberAsync(string conversationId, string memberId, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(ConversationsClient)}.{nameof(GetConversationMemberAsync)}");
            scope.Start();
            try
            {
                _ = conversationId ?? throw new ArgumentNullException(nameof(conversationId));
                _ = memberId ?? throw new ArgumentNullException(nameof(memberId));

                var response = await _client.GetConversationMemberAsync(conversationId, memberId, cancellationToken).ConfigureAwait(false);
                return response.Value;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Deletes a member from a conversation.
        /// 
        /// This REST API takes a ConversationId and a memberId (of type string) and removes that member from the conversation. 
        /// If that member was the last member of the conversation, the conversation will also be deleted.
        /// </summary>
        /// <param name="conversationId"> Conversation ID. </param>
        /// <param name="memberId"> ID of the member to delete from this conversation. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="conversationId"/> or <paramref name="memberId"/> is null. </exception>
        public async Task DeleteConversationMemberAsync(string conversationId, string memberId, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(ConversationsClient)}.{nameof(DeleteConversationMemberAsync)}");
            scope.Start();
            try
            {
                _ = conversationId ?? throw new ArgumentNullException(nameof(conversationId));
                _ = memberId ?? throw new ArgumentNullException(nameof(memberId));

                var response = await _client.DeleteConversationMemberAsync(conversationId, memberId, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Enumerate the members of a conversation one page at a time.
        /// 
        /// This REST API takes a ConversationId. Optionally a pageSize and/or continuationToken can be provided.
        /// It returns a PagedMembersResult, which contains an array of ChannelAccounts representing the members
        /// of the conversation and a continuation token that can be used to get more values.
        /// 
        /// One page of ChannelAccounts records are returned with each call. The number of records in a page may
        /// vary between channels and calls. The pageSize parameter can be used as a suggestion. If there are no
        /// additional results the response will not contain a continuation token. If there are no members in the
        /// conversation the Members will be empty or not present in the response.
        /// 
        /// A response to a request that has a continuation token from a prior request may rarely return members
        /// from a previous request.
        /// </summary>
        /// <param name="conversationId"> Conversation ID. </param>
        /// <param name="pageSize"> Suggested page size. </param>
        /// <param name="continuationToken"> Continuation Token. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="conversationId"/> is null. </exception>
        /// <returns>The <see cref="PagedMembersResult"/> for the members.</returns>
        public async Task<PagedMembersResult> GetConversationPagedMembersAsync(string conversationId, int? pageSize = null, string continuationToken = null, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(ConversationsClient)}.{nameof(GetConversationPagedMembersAsync)}");
            scope.Start();
            try
            {
                _ = conversationId ?? throw new ArgumentNullException(nameof(conversationId));

                var response = await _client.GetConversationPagedMembersAsync(conversationId, pageSize, continuationToken, cancellationToken).ConfigureAwait(false);
                return response.Value;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Upload an attachment directly into a channel&apos;s blob storage.
        /// 
        /// This is useful because it allows you to store data in a compliant store when dealing with enterprises.
        /// </summary>
        /// <param name="conversationId"> Conversation ID. </param>
        /// <param name="attachmentUpload"> Attachment data. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="conversationId"/> or <paramref name="attachmentUpload"/> is null. </exception>
        /// <returns>The response is a <see cref="ResourceResponse"/> which contains an AttachmentId which is suitable for using with the attachments API.</returns>
        public async Task<ResourceResponse> UploadAttachmentAsync(string conversationId, AttachmentData attachmentUpload, CancellationToken cancellationToken = default)
        {
            using var scope = _clientDiagnostics.CreateScope($"{nameof(ConversationsClient)}.{nameof(UploadAttachmentAsync)}");
            scope.Start();
            try
            {
                _ = conversationId ?? throw new ArgumentNullException(nameof(conversationId));

                var response = await _client.UploadAttachmentAsync(conversationId, attachmentUpload, cancellationToken).ConfigureAwait(false);
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
