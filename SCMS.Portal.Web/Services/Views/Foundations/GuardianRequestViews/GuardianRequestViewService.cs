// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Portal.Web.Brokers.DateTimes;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Brokers.Navigations;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Services.Foundations.GuardianRequests;
using SCMS.Portal.Web.Services.Foundations.Users;

namespace SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewService : IGuardianRequestViewService
    {
        private readonly IGuardianRequestService guardianRequestService;
        private readonly IUserService userService;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly INavigationBroker navigationBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuardianRequestViewService(
            IGuardianRequestService guardianRequestService,
            IUserService userService,
            IDateTimeBroker dateTimeBroker,
            INavigationBroker navigationBroker,
            ILoggingBroker loggingBroker)
        {
            this.guardianRequestService = guardianRequestService;
            this.userService = userService;
            this.dateTimeBroker = dateTimeBroker;
            this.navigationBroker = navigationBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<GuardianRequestView> AddGuardianRequestViewAsync(GuardianRequestView guardianRequestView) =>
        TryCatch(async () =>
        {
            ValidateGuardianRequestViewOnAdd(guardianRequestView);
            GuardianRequest guardianRequest = MapToGuardianRequest(guardianRequestView);
            await this.guardianRequestService.AddGuardianRequestAsync(guardianRequest);

            return guardianRequestView;
        });

        public void NavigateTo(string route) =>
            this.navigationBroker.NavigateTo(route);
        private GuardianRequest MapToGuardianRequest(GuardianRequestView guardianRequestView)
        {
            DateTimeOffset currentDateTime = this.dateTimeBroker.GetCurrentDateTime();
            Guid currentlyLoggedInUserId = this.userService.GetCurrentlyLoggedInUser();

            return new GuardianRequest
            {
                Id = Guid.NewGuid(),
                Title = (GuardianRequestTitle)guardianRequestView.Title,
                FirstName = guardianRequestView.FirstName,
                LastName = guardianRequestView.LastName,
                EmailId = guardianRequestView.EmailId,
                CountryCode = guardianRequestView.CountryCode,
                ContactNumber = guardianRequestView.ContactNumber,
                Occupation = guardianRequestView.Occupation,
                ContactLevel = (GuardianRequestContactLevel)guardianRequestView.ContactLevel,
                Relationship = (GuardianRequestRelationship)guardianRequestView.Relationship,
                StudentId = guardianRequestView.StudentId,
                CreatedDate = currentDateTime,
                UpdatedDate = currentDateTime,
                CreatedBy = currentlyLoggedInUserId,
                UpdatedBy = currentlyLoggedInUserId
            };
        }
    }
}
