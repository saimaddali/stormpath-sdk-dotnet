﻿// <copyright file="Serializers.cs" company="Stormpath, Inc.">
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

using Stormpath.SDK.Impl.Serialization;

namespace Stormpath.SDK.Serialization
{
    /// <summary>
    /// Static entry point for creating <see cref="IJsonSerializer">serializer</see> instances.
    /// </summary>
    public static class Serializers
    {
        /// <summary>
        /// Gets a new <see cref="ISerializerFactory">factory</see> instance.
        /// </summary>
        /// <returns>A new <see cref="ISerializerFactory">factory</see> instance.</returns>
        public static ISerializerFactory Create()
            => new DefaultSerializerFactory();
    }
}
