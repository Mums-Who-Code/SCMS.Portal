// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.Apis;
using SCMS.Portal.Web.Brokers.DateTimes;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.Guardians;

namespace SCMS.Portal.Web.Services.Foundations.Guardians
{
    public class GuardianService : IGuardianService
    {
        private readonly IApiBroker apiBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuardianService(
            IApiBroker apiBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Guardian> AddGuardianAsync(Guardian guardian) =>
            await this.apiBroker.PostGuardianAsync(guardian);
        
    }
}
