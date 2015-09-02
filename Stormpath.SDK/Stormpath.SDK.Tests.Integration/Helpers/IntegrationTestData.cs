﻿// <copyright file="IntegrationTestData.cs" company="Stormpath, Inc.">
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
using Stormpath.SDK.Account;
using Stormpath.SDK.Application;
using Stormpath.SDK.Client;

namespace Stormpath.SDK.Tests.Integration.Helpers
{
    public class IntegrationTestData
    {
        public IntegrationTestData()
        {
            this.Nonce = RandomString.Create().Substring(0, 6);
        }

        public string Nonce { get; private set; }

        public IApplication GetTestApplication(IClient client)
        {
            return client.Instantiate<IApplication>()
                        .SetName($".NET IT {this.Nonce} - {DateTimeOffset.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture)}")
                        .SetDescription("The Battle of Endor")
                        .SetStatus(ApplicationStatus.Enabled);
        }

        public List<IAccount> GetTestAccounts(IClient client)
        {
            return new List<IAccount>()
            {
                {
                    client.Instantiate<IAccount>()
                        .SetGivenName("Luke")
                        .SetSurname("Skywalker")
                        .SetEmail("lskywalker@tattooine.rim")
                        .SetPassword("whataPieceofjunk$1138")
                        .SetUsername($"sonofthesuns-{this.Nonce}")
                },
                {
                    client.Instantiate<IAccount>()
                        .SetGivenName("Han")
                        .SetSurname("Solo")
                        .SetEmail("han.solo@corellia.core")
                        .SetPassword(new RandomPassword(12))
                        .SetUsername($"cptsolo-{this.Nonce}")
                },
                {
                    client.Instantiate<IAccount>()
                        .SetGivenName("Leia")
                        .SetSurname("Organa")
                        .SetEmail("leia.organa@alderaan.core")
                        .SetPassword(new RandomPassword(12))
                        .SetUsername($"princessleia-{this.Nonce}")
                },
                {
                    client.Instantiate<IAccount>()
                        .SetGivenName("Chewbacca")
                        .SetSurname("the Wookie")
                        .SetEmail("chewie@kashyyyk.rim")
                        .SetPassword(new RandomPassword(12))
                        .SetUsername($"rrwwwggg-{this.Nonce}")
                },
                {
                    client.Instantiate<IAccount>()
                        .SetGivenName("Lando")
                        .SetSurname("Calrissian")
                        .SetEmail("lcalrissian@socorro.rim")
                        .SetPassword(new RandomPassword(12))
                        .SetUsername($"lottanerve-{this.Nonce}")
                },
                {
                    client.Instantiate<IAccount>()
                        .SetGivenName("Darth")
                        .SetSurname("Vader")
                        .SetEmail("vader@galacticempire.co")
                        .SetPassword(new RandomPassword(12))
                        .SetUsername($"lordvader-{this.Nonce}")
                },
                {
                    client.Instantiate<IAccount>()
                        .SetGivenName("Emperor")
                        .SetSurname("Palpatine")
                        .SetEmail("emperor@galacticempire.co")
                        .SetPassword(new RandomPassword(12))
                        .SetUsername($"rulethegalaxy-{this.Nonce}")
                },
            };
        }
    }
}
