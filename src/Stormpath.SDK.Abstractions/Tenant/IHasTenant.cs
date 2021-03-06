﻿// <copyright file="IHasTenant.cs" company="Stormpath, Inc.">
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

using System.Threading;
using System.Threading.Tasks;

namespace Stormpath.SDK.Tenant
{
    /// <summary>
    /// Represents <see cref="Resource.IResource">Resources</see> that have a link property that points
    /// to the owning <see cref="ITenant">Tenant</see>.
    /// </summary>
    public interface IHasTenant
    {
        /// <summary>
        /// Gets the Stormpath <see cref="ITenant">Tenant</see> that owns this resource.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The tenant.</returns>
        Task<ITenant> GetTenantAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
