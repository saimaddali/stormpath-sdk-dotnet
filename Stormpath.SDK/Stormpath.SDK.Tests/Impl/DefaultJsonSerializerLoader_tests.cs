﻿// <copyright file="DefaultJsonSerializerLoader_tests.cs" company="Stormpath, Inc.">
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

using Shouldly;
using Stormpath.SDK.Impl.Serialization;
using Stormpath.SDK.Serialization;
using Xunit;

namespace Stormpath.SDK.Tests.Impl
{
    public class DefaultJsonSerializerLoader_tests
    {
        [Fact]
        public void Default_library_is_loaded()
        {
            IJsonSerializerLoader loader = new DefaultJsonSerializerLoader();

            // This test project has a reference to Stormpath.SDK.JsonNetSerializer, so the file lookup will succeed
            IJsonSerializer serializer = null;
            bool loadResult = loader.TryLoad(out serializer);

            loadResult.ShouldBe(true);
            serializer.ShouldNotBe(null);
        }
    }
}