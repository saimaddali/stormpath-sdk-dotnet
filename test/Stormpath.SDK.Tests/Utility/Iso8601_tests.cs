﻿// <copyright file="Iso8601_tests.cs" company="Stormpath, Inc.">
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
using Shouldly;
using Stormpath.SDK.Impl.Utility;
using Xunit;

namespace Stormpath.SDK.Tests.Utility
{
    public class Iso8601_tests
    {
        [Fact]
        public void Formatting_to_UTC_from_local_date()
        {
            var pacificTimeZoneName = "Pacific Standard Time";

            // TODO fix this for Mono?
            //if (PlatformServices.Default.Runtime.RuntimeType == "Mono")
            //{
            //    pacificTimeZoneName = "US/Pacific";
            //}

            // Midnight Pacific Time, Jan 1, 2015 = 2015-01-01 08:00 UTC
            var midnightPacificTimeJan1 = new DateTimeOffset(2015, 01, 01, 00, 00, 00, 00, TimeSpan.FromHours(-8));
            var eightHoursLaterUtc8601 = Iso8601.Format(midnightPacificTimeJan1);
            eightHoursLaterUtc8601.ShouldBe("2015-01-01T08:00:00.000Z");

            // 4PM Pacific Time, Dec 31 2015 = 2015-01-01 00:00 UTC
            var fourInTheLocalAfternoonDec31 = new DateTime(2014, 12, 31, 16, 00, 00);
            var midnightUtcIso8601 = Iso8601.Format(new DateTimeOffset(fourInTheLocalAfternoonDec31, TimeZoneInfo.FindSystemTimeZoneById(pacificTimeZoneName).BaseUtcOffset));
            midnightUtcIso8601.ShouldBe("2015-01-01T00:00:00.000Z");
        }

        [Fact]
        public void Formatting_to_UTC_from_UTC_date()
        {
            var alreadyInUtc = new DateTimeOffset(2016, 02, 01, 12, 00, 00, TimeSpan.Zero);
            var directlyInto8601 = Iso8601.Format(alreadyInUtc);
            directlyInto8601.ShouldBe("2016-02-01T12:00:00.000Z");

            var dateTimeInUtc = new DateTime(2016, 02, 02, 16, 30, 00, DateTimeKind.Utc);
            var alsoConvertsFine = Iso8601.Format(new DateTimeOffset(dateTimeInUtc, TimeSpan.Zero));
            alsoConvertsFine.ShouldBe("2016-02-02T16:30:00.000Z");
        }

        [Fact]
        public void Formatting_without_converting_to_UTC()
        {
            // Midnight Pacific Time, Jan 1, 2015
            var midnightPacificTimeJan1 = new DateTimeOffset(2015, 01, 01, 00, 00, 00, 00, TimeSpan.FromHours(-8));
            var eightHoursLaterUtc8601 = Iso8601.Format(midnightPacificTimeJan1);
            eightHoursLaterUtc8601.ShouldBe("2015-01-01T08:00:00.000Z");

            Iso8601.Format(midnightPacificTimeJan1, convertToUtc: false).ShouldBe("2015-01-01T00:00:00.000-08:00");
        }
    }
}
