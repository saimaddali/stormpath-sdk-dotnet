﻿// <copyright file="AbstractResource.cs" company="Stormpath, Inc.">
// Copyright (c) 2015 Stormpath, Inc.
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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Stormpath.SDK.Impl.DataStore;
using Stormpath.SDK.Resource;
using Stormpath.SDK.Tenant;

namespace Stormpath.SDK.Impl.Resource
{
    internal abstract class AbstractResource : IResource, ILinkable
    {
        public static readonly string HrefPropertyName = "href";

        private ResourceData resourceData;

        public AbstractResource(ResourceData data)
        {
            this.resourceData = data;
        }

        void ILinkable.Link(ResourceData data)
        {
            Interlocked.Exchange(ref this.resourceData, data);
        }

        public bool IsLinkedTo(AbstractResource other)
            => ReferenceEquals(this.resourceData, other.resourceData);

        protected IResource AsInterface => this;

        public ResourceData GetResourceData() => this.resourceData;

        string IResource.Href
        {
            get
            {
                var href = this.GetProperty<string>(HrefPropertyName);

                bool isEmptyOrDefault =
                    href == null ||
                    href.StartsWith("autogen", System.StringComparison.InvariantCultureIgnoreCase);

                return isEmptyOrDefault
                    ? null
                    : href;
            }
        }

        protected IInternalDataStore GetInternalDataStore()
            => this.GetResourceData()?.InternalDataStore;

        protected IInternalAsyncDataStore GetInternalAsyncDataStore()
            => this.GetResourceData()?.InternalAsyncDataStore;

        protected IInternalSyncDataStore GetInternalSyncDataStore()
            => this.GetResourceData()?.InternalSyncDataStore;

        protected Task<ITenant> GetTenantAsync(string tenantHref, CancellationToken cancellationToken)
            => this.GetInternalAsyncDataStore().GetResourceAsync<ITenant>(tenantHref, new IdentityMapOptions() { StoreWithInfiniteExpiration = true }, cancellationToken);

        protected ITenant GetTenant(string tenantHref)
            => this.GetInternalSyncDataStore().GetResource<ITenant>(tenantHref, new IdentityMapOptions() { StoreWithInfiniteExpiration = true });

        internal bool IsDirty => this.GetResourceData()?.IsDirty ?? true;

        public IReadOnlyList<string> GetPropertyNames()
            => this.GetResourceData()?.GetPropertyNames();

        public IReadOnlyList<string> GetUpdatedPropertyNames()
            => this.GetResourceData()?.GetUpdatedPropertyNames();

        public object GetProperty(string name)
            => this.GetResourceData()?.GetProperty(name);

        public T GetProperty<T>(string name)
            => (T)(this.GetProperty(name) ?? default(T));

        public IEmbeddedProperty GetLinkProperty(string name)
            => this.GetProperty<IEmbeddedProperty>(name) ?? new LinkProperty(null);

        public bool ContainsProperty(string name)
            => this.GetResourceData()?.ContainsProperty(name) ?? false;

        public void SetProperty(string name, object value)
            => this.GetResourceData()?.SetProperty(name, value);

        public void SetProperty<T>(string name, T value)
            => this.SetProperty(name, (object)value);

        public void SetLinkProperty(string name, string href)
            => this.SetProperty(name, new LinkProperty(href));
    }
}
