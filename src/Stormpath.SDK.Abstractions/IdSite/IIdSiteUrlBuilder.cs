﻿// <copyright file="IIdSiteUrlBuilder.cs" company="Stormpath, Inc.">
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

namespace Stormpath.SDK.IdSite
{
    /// <summary>
    /// Used to construct a URL you can use to redirect your application users to a hosted login/registration/forgot-password site -
    /// what Stormpath calls an 'Identity Site' (or 'ID Site' for short) - for performing common user identity functionality.
    /// When the user is done (logging in, registering, etc), they will be redirected back to a <c>callbackUri</c> of your choice.
    /// </summary>
    public interface IIdSiteUrlBuilder
    {
        /// <summary>
        /// The built ID Site URL (when invoked) will logout the current user.
        /// </summary>
        /// <remarks>There is no way to undo this operation on the builder. To undo, you must create a new <see cref="IIdSiteUrlBuilder"/>.</remarks>
        /// <returns>This instance for method chaining.</returns>
        IIdSiteUrlBuilder ForLogout();

        /// <summary>
        /// Sets the location where the user will be sent when returning from the ID Site. This property is mandatory and must be set.
        /// Use <see cref="Application.IApplication.NewIdSiteAsyncCallbackHandler(Http.IHttpRequest)"/> to process requests to your <paramref name="callbackUri"/>.
        /// </summary>
        /// <remarks>For security reasons, this location must be registered in your ID Site configuration in the Stormpath Admin Console.</remarks>
        /// <param name="callbackUri">The final destination the browser will be redirected to.</param>
        /// <returns>This instance for method chaining.</returns>
        IIdSiteUrlBuilder SetCallbackUri(string callbackUri);

        /// <summary>
        /// Sets the location where the user will be sent when returning from the ID Site. This property is mandatory and must be set.
        /// Use <see cref="Application.IApplication.NewIdSiteAsyncCallbackHandler(Http.IHttpRequest)"/> to process requests to your <paramref name="callbackUri"/>.
        /// </summary>
        /// <remarks>For security reasons, this location must be registered in your ID Site configuration in the Stormpath Admin Console.</remarks>
        /// <param name="callbackUri">The final destination the browser will be redirected to.</param>
        /// <returns>This instance for method chaining.</returns>
        IIdSiteUrlBuilder SetCallbackUri(Uri callbackUri);

        /// <summary>
        /// Sets the initial path in the ID Site where the user should be sent. If unspecified, this defaults to <c>/</c>,
        /// implying that the ID Site's landing/home page is the desired location.
        /// </summary>
        /// <param name="path">The initial path in the ID Site where the user should be sent.</param>
        /// <returns>This instance for method chaining.</returns>
        IIdSiteUrlBuilder SetPath(string path);

        /// <summary>
        /// Ensures the ID Site is customized for the <c>Organization</c> with the specified <paramref name="organizationNameKey"/>
        /// This is useful for multi-tenant or white label scenarios where you know the user belongs to a specific <see cref="Organization.IOrganization">Organization</see>.
        /// </summary>
        /// <param name="organizationNameKey">The unique identifier of the <see cref="Organization.IOrganization">Organization</see> to use when customizing ID Site.</param>
        /// <returns>This instance for method chaining.</returns>
        IIdSiteUrlBuilder SetOrganizationNameKey(string organizationNameKey);

        /// <summary>
        /// Ensures that the user will visit ID Site using a subdomain equal to the
        /// <see cref="SetOrganizationNameKey(string)"/> instead of the standard base domain.
        /// </summary>
        /// <param name="useSubdomain">
        /// <see langword="true"/> to ensure that the user will visit ID Site using a subdomain equal to the
        /// <see cref="SetOrganizationNameKey(string)"/>, <see langword="false"/> to ensure that the standard ID Site domain.
        /// </param>
        /// <returns>This instance for method chaining.</returns>
        IIdSiteUrlBuilder SetUseSubdomain(bool useSubdomain);

        /// <summary>
        /// Ensures that the ID Site will show the <see cref="SetOrganizationNameKey(string)"/> field to
        /// the end-user in the ID Site user interface.
        /// </summary>
        /// <param name="showOrganizationField">
        /// <see langword="true"/> the ID Site will show the
        /// <see cref="SetOrganizationNameKey(string)"/> field to end-user in
        /// the ID Site user interface, <see langword="false"/> otherwise.
        /// </param>
        /// <returns>This instance for method chaining.</returns>
        IIdSiteUrlBuilder SetShowOrganizationField(bool showOrganizationField);

        /// <summary>
        /// Sets application-specific state data that should be retained and made available to your <c>callbackUri</c>
        /// when the user returns from the ID Site.
        /// </summary>
        /// <param name="state">Application-specific state data that should be retained and made available to your <c>callbackUri</c> when the user returns from the ID Site.</param>
        /// <returns>This instance for method chaining.</returns>
        IIdSiteUrlBuilder SetState(string state);

        /// <summary>
        /// Sets the <c>sptoken</c> claim, used when initiating an ID Site-based password reset.
        /// </summary>
        /// <param name="sptoken">The <c>sptoken</c> parameter provided by Stormpath.</param>
        /// <returns>This instance for method chaining.</returns>
        IIdSiteUrlBuilder SetSpToken(string sptoken);

        /// <summary>
        /// Sets an ID Site claim.
        /// </summary>
        /// <param name="key">The claim name.</param>
        /// <param name="value">The claim value.</param>
        /// <returns>This instance for method chaining.</returns>
        IIdSiteUrlBuilder SetClaim(string key, object value);

        /// <summary>
        /// Builds the fully-qualified URL representing the initial location in the ID Site where the end-user should be redirected.
        /// </summary>
        /// <returns>The fully-qualified URL representing the initial location in the ID Site where the end-user should be redirected.</returns>
        string Build();
    }
}
