// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading.Tasks;

namespace Microsoft.Bot.Connector.Client.Authentication
{
    public class DefaultCredentialProvider : CredentialProvider
    {
        private readonly string _appId;
        private readonly string _password;

        /// <summary>
        /// Constructs a new <see cref="DefaultCredentialProvider"/> using appid and password.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="password"></param>
        public DefaultCredentialProvider(string appId, string password)
        {
            _appId = appId;
            _password = password;
        }

        /// <inheritdoc/>
        public override Task<string> GetAppPasswordAsync(string appId)
        {
            return Task.FromResult((appId == _appId) ? _password : null);
        }

        /// <inheritdoc/>
        public override Task<bool> IsAuthenticationDisabledAsync()
        {
            return Task.FromResult(string.IsNullOrEmpty(_appId));
        }

        /// <inheritdoc/>
        public override Task<bool> IsValidAppIdAsync(string appId)
        {
            return Task.FromResult(appId == _appId);
        }
    }
}
