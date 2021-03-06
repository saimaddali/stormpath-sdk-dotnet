﻿// <copyright file="ApplicationCreationOptionsBuilder.cs" company="Stormpath, Inc.">
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

using Stormpath.SDK.Impl.Application;
using Stormpath.SDK.Impl.Resource;
using Stormpath.SDK.Resource;

namespace Stormpath.SDK.Application
{
    /// <summary>
    /// A builder to construct <see cref="IApplicationCreationOptions"/> instances.
    /// </summary>
    public sealed class ApplicationCreationOptionsBuilder
    {
        /// <summary>
        /// Gets or sets a value indicating whether to create a new <see cref="Directory.IDirectory">Directory</see> along with the new Application.
        /// </summary>
        /// <value>
        /// <para>
        /// If <see langword="true"/>, a new Directory will be created. The new Directory will automatically be assigned as the Application's
        /// default Account and Group store.
        /// If the <see cref="DirectoryName"/> property is not null, the new Directory will be created with that name.
        /// Otherwise, the Directory will be automatically named based on heuristics to ensure a guaranteed unique name based on the Application.
        /// </para>
        /// <para>
        /// If <see langword="false"/>, no Directory will be created.
        /// </para>
        /// </value>
        public bool CreateDirectory { get; set; } = false;

        /// <summary>
        /// Gets the response options to apply to the request.
        /// </summary>
        /// <value>The response options to apply to the request.</value>
        /// <example>
        /// <code source="CreationOptionsBuilderExamples.cs" region="RequestCustomData" lang="C#" title="Request and cache Custom Data" />
        /// </example>
        public IRetrievalOptions<IApplication> ResponseOptions { get; } = new DefaultRetrievalOptions<IApplication>();

        /// <summary>
        /// Gets or sets the name to use when creating a new <see cref="Directory.IDirectory">Directory</see> for this Application.
        /// </summary>
        /// <value>
        /// Default value: <see langword="null"/>.
        /// <para>The name to assign to the new Directory. This only has an effect if <see cref="CreateDirectory"/> is <see langword="true"/>.</para>
        /// <para>If you want to have a default name assigned automatically, set this to <see cref="string.Empty"/> or <see langword="null"/>.</para>
        /// </value>
        public string DirectoryName { get; set; } = null;

        /// <summary>
        /// Creates a new <see cref="IApplicationCreationOptions"/> instance based on the current builder state.
        /// </summary>
        /// <returns>A new <see cref="IApplicationCreationOptions"/> instance.</returns>
        public IApplicationCreationOptions Build()
        {
            return new DefaultApplicationCreationOptions(this.CreateDirectory, this.DirectoryName, this.ResponseOptions);
        }
    }
}
