﻿// <copyright file="DefaultApplication.Login.cs" company="Stormpath, Inc.">
// Copyright (c) 2016 Stormpath, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using Stormpath.SDK.Application;
using Stormpath.SDK.Auth;
using Stormpath.SDK.Impl.Auth;
using Stormpath.SDK.Impl.Resource;
using Stormpath.SDK.Resource;

namespace Stormpath.SDK.Impl.Application
{
    internal sealed partial class DefaultApplication
    {
        Task<IAuthenticationResult> IApplication.AuthenticateAccountAsync(IAuthenticationRequest request, CancellationToken cancellationToken)
        {
            var dispatcher = new AuthenticationRequestDispatcher();

            return dispatcher.AuthenticateAsync(this.GetInternalAsyncDataStore(), this, request, null, cancellationToken);
        }

        Task<IAuthenticationResult> IApplication.AuthenticateAccountAsync(IAuthenticationRequest request, Action<IRetrievalOptions<IAuthenticationResult>> responseOptions, CancellationToken cancellationToken)
        {
            var options = new DefaultRetrievalOptions<IAuthenticationResult>();
            responseOptions(options);

            var dispatcher = new AuthenticationRequestDispatcher();

            return dispatcher.AuthenticateAsync(this.GetInternalAsyncDataStore(), this, request, options, cancellationToken);
        }

        Task<IAuthenticationResult> IApplication.AuthenticateAccountAsync(string username, string password, CancellationToken cancellationToken)
        {
            var request = new UsernamePasswordRequest(username, password, null, null) as IAuthenticationRequest;

            return this.AsInterface.AuthenticateAccountAsync(request, cancellationToken);
        }

        async Task<bool> IApplication.TryAuthenticateAccountAsync(string username, string password, CancellationToken cancellationToken)
        {
            try
            {
                var loginResult = await this.AsInterface.AuthenticateAccountAsync(username, password, cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
