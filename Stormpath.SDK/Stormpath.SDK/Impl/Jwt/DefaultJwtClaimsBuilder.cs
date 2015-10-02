﻿// <copyright file="DefaultJwtClaimsBuilder.cs" company="Stormpath, Inc.">
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

using System;
using System.Collections.Generic;
using Stormpath.SDK.Jwt;

namespace Stormpath.SDK.Impl.Jwt
{
    internal sealed class DefaultJwtClaimsBuilder : IJwtClaimsBuilder
    {
        private readonly IDictionary<string, object> claimsInProgress;

        public DefaultJwtClaimsBuilder()
        {
            this.claimsInProgress = new Dictionary<string, object>();
        }

        private void SetOrRemove<T>(string claimName, T value)
        {
            if (string.IsNullOrEmpty(claimName))
                throw new ArgumentNullException(nameof(claimName));

            var type = typeof(T);

            if (value == null)
            {
                this.claimsInProgress.Remove(claimName);
                return;
            }

            this.claimsInProgress[claimName] = value;
        }

        IJwtClaimsBuilder IJwtClaimsBuilder.SetAudience(string aud)
        {
            this.SetOrRemove(DefaultJwtClaims.Audience, aud);
            return this;
        }

        IJwtClaimsBuilder IJwtClaimsBuilder.SetExpiration(DateTimeOffset? exp)
        {
            this.SetOrRemove(DefaultJwtClaims.Expiration, exp);
            return this;
        }

        IJwtClaimsBuilder IJwtClaimsBuilder.SetId(string jti)
        {
            this.SetOrRemove(DefaultJwtClaims.Id, jti);
            return this;
        }

        IJwtClaimsBuilder IJwtClaimsBuilder.SetIssuedAt(DateTimeOffset? iat)
        {
            this.SetOrRemove(DefaultJwtClaims.IssuedAt, iat);
            return this;
        }

        IJwtClaimsBuilder IJwtClaimsBuilder.SetIssuer(string iss)
        {
            this.SetOrRemove(DefaultJwtClaims.Issuer, iss);
            return this;
        }

        IJwtClaimsBuilder IJwtClaimsBuilder.SetNotBeforeDate(DateTimeOffset? nbf)
        {
            this.SetOrRemove(DefaultJwtClaims.NotBefore, nbf);
            return this;
        }

        IJwtClaimsBuilder IJwtClaimsBuilder.SetSubject(string sub)
        {
            this.SetOrRemove(DefaultJwtClaims.Subject, sub);
            return this;
        }

        IJwtClaims IJwtClaimsBuilder.Build() => new DefaultJwtClaims(this.claimsInProgress);
    }
}
