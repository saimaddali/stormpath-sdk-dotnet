﻿// <copyright file="IApplication.cs" company="Stormpath, Inc.">
//      Copyright (c) 2015 Stormpath, Inc.
// </copyright>
// <remarks>
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </remarks>

using System.Threading;
using System.Threading.Tasks;
using Stormpath.SDK.Account;
using Stormpath.SDK.AccountStore;
using Stormpath.SDK.Auth;
using Stormpath.SDK.Linq;
using Stormpath.SDK.Resource;

namespace Stormpath.SDK.Application
{
    /// <summary>
    /// Represents a Stormpath registered application.
    /// </summary>
    public interface IApplication : IResource, ISaveable<IApplication>, IDeletable, IAuditable, IAccountCreation
    {
        /// <summary>
        /// Gets the application's name.
        /// </summary>
        /// <value>The application's name. An application's name must be unique across all other applications in the owning <see cref="Tenant.ITenant"/>.</value>
        string Name { get; }

        /// <summary>
        /// Gets the application description.
        /// </summary>
        /// <value>The application's description text.</value>
        string Description { get; }

        /// <summary>
        /// Gets the application's status.
        /// </summary>
        /// <value>
        /// Application users may login to an <see cref="ApplicationStatus.Enabled"/> application.
        /// They may not login to a <see cref="ApplicationStatus.Disabled"/> application.
        /// </value>
        ApplicationStatus Status { get; }

        /// <summary>
        /// Sets the application description.
        /// </summary>
        /// <param name="description">The application's description text.</param>
        /// <returns>This instance for method chaining.</returns>
        IApplication SetDescription(string description);

        /// <summary>
        /// Sets the application's name.
        /// </summary>
        /// <param name="name">The application's name. Application names must be unique within a <see cref="Tenant.ITenant"/>.</param>
        /// <returns>This instance for method chaining.</returns>
        IApplication SetName(string name);

        /// <summary>
        /// Sets the application's status.
        /// </summary>
        /// <param name="status">The application's status.
        /// Application users may login to an <see cref="ApplicationStatus.Enabled"/> application.
        /// They may not login to a <see cref="ApplicationStatus.Disabled"/> application.
        /// </param>
        /// <returns>This instance for method chaining.</returns>
        IApplication SetStatus(ApplicationStatus status);

        /// <summary>
        /// Authenticates an account's submitted principals and credentials (e.g. username and password).
        /// The account must be in one of the Application's assigned account stores.
        /// If not in an assigned account store, the authentication attempt will fail.
        /// </summary>
        /// <param name="request">Any supported <see cref="IAuthenticationRequest"/> object (e.g. <see cref="UsernamePasswordRequest"/>).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A Task whose result is the result of the authentication.
        /// The authenticated account can be obtained from <see cref="IAuthenticationResult.GetAccountAsync(CancellationToken)"/>.
        /// </returns>
        /// <exception cref="Error.ResourceException">if the authentication attempt fails.</exception>
        /// <example>
        ///     var loginRequest = new UsernamePasswordRequest("jsmith", "Password123#");
        ///     var result = await app.AuthenticateAccountAsync(loginRequest);
        /// </example>
        Task<IAuthenticationResult> AuthenticateAccountAsync(IAuthenticationRequest request, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Authenticates an account's submitted principals and credentials (e.g. username and password).
        /// The account must be in one of the Application's assigned account stores.
        /// If not in an assigned account store, the authentication attempt will fail.
        /// </summary>
        /// <param name="username">The account's username.</param>
        /// <param name="password">The account's raw (plaintext) password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A Task whose result is the result of the authentication.
        /// The authenticated account can be obtained from <see cref="IAuthenticationResult.GetAccountAsync(CancellationToken)"/>.
        /// </returns>
        /// <exception cref="Error.ResourceException">if the authentication attempt fails.</exception>
        /// <example>
        ///     var result = await app.AuthenticateAccountAsync("jsmith", "Password123#");
        /// </example>
        Task<IAuthenticationResult> AuthenticateAccountAsync(string username, string password, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Attempts to authenticate an account with the specified username and password.
        /// <para>If you need to obtain the authenticated account details, use <see cref="AuthenticateAccountAsync(string, string, CancellationToken)"/> instead.</para>
        /// </summary>
        /// <param name="username">The account's username.</param>
        /// <param name="password">The account's raw (plaintext) password</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is <c>true</c> if the authentication attempt succeeded; <c>false</c> otherwise.</returns>
        /// <example>
        ///     if (await app.TryAuthenticateAccountAsync("jsmith", "Password123#"))
        ///     {
        ///         // Login successful
        ///     }
        /// </example>
        Task<bool> TryAuthenticateAccountAsync(string username, string password, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a queryable list of all accounts in this application.
        /// </summary>
        /// <returns>An <see cref="IAsyncQueryable{IAccount}"/> that may be asynchronously consumed to list or search accounts.</returns>
        /// <example>
        ///     var allAccounts = await app.GetAccounts().ToListAsync();
        /// </example>
        IAsyncQueryable<IAccount> GetAccounts();

        /// <summary>
        /// Gets a queryable list of all account store mappings accessible to the application.
        /// </summary>
        /// <returns>An <see cref="IAsyncQueryable{IAccountStoreMapping}"/> that may be asynchronously consumed to list or search account store mappings.</returns>
        IAsyncQueryable<IAccountStoreMapping> GetAccountStoreMappings();

        /// <summary>
        /// Gets the <see cref="IAccountStore"/> (either a Group or <see cref="Directory.IDirectory"/>)
        /// used to persist new accounts created by the application.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is the default <see cref="IAccountStore"/>,
        /// or <c>null</c> if no default <see cref="IAccountStore"/> has been designated.</returns>
        Task<IAccountStore> GetDefaultAccountStoreAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Verifies the password reset token (received in the user's email) and immediately
        /// changes the password in the same request, if the token is valid.
        /// <para>Once the token has been successfully used, it is immediately invalidated and can't be used again.
        /// If you need to change the password again, you will previously need to execute
        /// <see cref="SendPasswordResetEmailAsync(string, CancellationToken)"/> again in order to obtain a new password reset token.</para>
        /// </summary>
        /// <param name="token">The verification token, usually obtained as a request parameter by your application.</param>
        /// <param name="newPassword">The new password that will be set to the <see cref="IAccount"/> if the token is successfully validated.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is the account matching the specified token.</returns>
        /// <exception cref="Error.ResourceException">if the token is not valid</exception>
        Task<IAccount> ResetPasswordAsync(string token, string newPassword, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Sends a password reset email for the specified account email address.
        /// The email will contain a password reset link that the user can click or copy into their browser address bar.
        /// </summary>
        /// <param name="email">An email address of an <see cref="IAccount"/> that may login to the application.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is the created <see cref="IPasswordResetToken"/>.
        /// You can obtain the associated account via <see cref="IPasswordResetToken.GetAccountAsync(CancellationToken)"/>.</returns>
        /// <exception cref="Error.ResourceException">if there is no account that matches the specified email address</exception>
        Task<IPasswordResetToken> SendPasswordResetEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Verifies a password reset token.
        /// </summary>
        /// <param name="token">The verification token, usually obtained as a request paramter by your application.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is the <see cref="IAccount"/> matching the specified token.</returns>
        /// <exception cref="Error.ResourceException">if the token is not valid.</exception>
        Task<IAccount> VerifyPasswordResetTokenAsync(string token, CancellationToken cancellationToken = default(CancellationToken));
    }
}
