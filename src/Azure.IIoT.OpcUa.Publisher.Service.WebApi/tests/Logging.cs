﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Azure.IIoT.OpcUa.Publisher.Service.WebApi.Tests
{
    using Neovolve.Logging.Xunit;
    using Microsoft.Extensions.Logging;

    internal static class Logging
    {
        /// <summary>
        /// Default level
        /// </summary>
        public static LogLevel Level => LogLevel.Warning;

        /// <summary>
        /// Configuration
        /// </summary>
        public static LoggingConfig Config => new()
        {
            LogLevel = Level,
            IgnoreTestBoundaryException = true // Due to current way logging is implemented in ua stack
        };
    }
}
