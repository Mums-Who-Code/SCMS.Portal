// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.DateTimes;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Services.Foundations.GuardianRequests;
using SCMS.Portal.Web.Services.Foundations.Users;

namespace SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews
{
    public class GuardianRequestViewService : IGuardianRequestViewService
    {
        private readonly IGuardianRequestService guardianRequestService;
        private readonly IUserService userService;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuardianRequestViewService(
            IGuardianRequestService guardianRequestService,
            IUserService userService,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.guardianRequestService = guardianRequestService;
            this.userService = userService;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<GuardianRequestView> AddGuardianRequestViewAsync(GuardianRequestView guardianRequestView) =>
            throw new System.NotImplementedException();
    }
}
