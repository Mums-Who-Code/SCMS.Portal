// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.Apis;
using SCMS.Portal.Web.Brokers.DateTimes;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;

namespace SCMS.Portal.Web.Services.Foundations.GuardianRequests
{
    public partial class GuardianRequestService : IGuardianRequestService
    {
        private readonly IApiBroker apiBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuardianRequestService(
            IApiBroker apiBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<GuardianRequest> AddGuardianRequestAsync(GuardianRequest guardianRequest) =>
        TryCatch(async () =>
        {
            ValidateGuardianRequestOnAdd(guardianRequest);

            return await this.apiBroker.PostGuardianRequestAsync(guardianRequest);
        });

    }
}
