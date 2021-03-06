// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.Extensions.Logging;

namespace SCMS.Portal.Web.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger logger;
        public LoggingBroker(ILogger logger) => this.logger = logger;

        public void LogCritical(Exception exception) =>
            this.logger.LogCritical(exception, exception.Message);

        public void LogError(Exception exception) =>
            this.logger.LogError(exception, exception.Message);

        public void LogDebug(string message) => this.logger.LogDebug(message);
        public void LogInformation(string message) => this.logger.LogInformation(message);
        public void LogTrace(string message) => this.logger.LogTrace(message);
        public void LogWarning(string message) => this.logger.LogWarning(message);
    }
}
