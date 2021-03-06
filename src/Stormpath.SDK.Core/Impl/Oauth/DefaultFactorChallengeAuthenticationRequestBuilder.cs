﻿// <copyright file="DefaultFactorChallengeAuthenticationRequestBuilder.cs" company="Stormpath, Inc.">
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

using Stormpath.SDK.Account;
using Stormpath.SDK.Oauth;

namespace Stormpath.SDK.Impl.Oauth
{
    internal sealed class DefaultFactorChallengeAuthenticationRequestBuilder : IFactorChallengeAuthenticationRequestBuilder
    {
        private string _challengeHref;
        private string _code;

        public IFactorChallengeAuthenticationRequestBuilder SetChallenge(IChallenge challenge)
        {
            _challengeHref = challenge?.Href;
            return this;
        }

        public IFactorChallengeAuthenticationRequestBuilder SetChallenge(string href)
        {
            _challengeHref = href;
            return this;
        }

        public IFactorChallengeAuthenticationRequestBuilder SetCode(string code)
        {
            _code = code;
            return this;
        }

        public IFactorChallengeAuthenticationRequest Build()
            => new DefaultFactorChallengeAuthenticationRequest(_challengeHref, _code);
    }
}
