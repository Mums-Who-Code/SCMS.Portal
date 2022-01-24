// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using SCMS.Portal.Web.Brokers.DateTimes;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Services.Foundations.GuardianRequests;
using SCMS.Portal.Web.Services.Foundations.Users;
using SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews;
using Tynamix.ObjectFiller;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewServiceTests
    {
        private readonly Mock<IGuardianRequestService> guardianRequestServiceMock;
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IGuardianRequestViewService guardianRequestViewService;
        private readonly ICompareLogic compareLogic;

        public GuardianRequestViewServiceTests()
        {
            this.guardianRequestServiceMock = new Mock<IGuardianRequestService>();
            this.userServiceMock = new Mock<IUserService>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            var compareConfig = new ComparisonConfig();
            compareConfig.IgnoreProperty<GuardianRequest>(guardianRequest => guardianRequest.Id);
            this.compareLogic = new CompareLogic(compareConfig);

            this.guardianRequestViewService = new GuardianRequestViewService(
                guardianRequestService: this.guardianRequestServiceMock.Object,
                userService: this.userServiceMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static string GetRandomEmail() =>
            new EmailAddresses().GetValue().ToString();

        private static string GetValidContactNumber() =>
            new LongRange(min: 1000000000, max: 9999999999).GetValue().ToString();

        private static GuardianRequestViewTitle GetRandomTitleThatIsNot(GuardianRequestViewTitle title)
        {
            GuardianRequestViewTitle randomTitle = GetRandomTitle();

            while (randomTitle == title)
            {
                randomTitle = GetRandomTitle();
            }

            return randomTitle;
        }

        private static GuardianRequestViewTitle GetRandomTitle()
        {
            IEnumerable<GuardianRequestViewTitle> guardianRequestViewTitles =
                Enum.GetValues(typeof(GuardianRequestViewTitle)).Cast<GuardianRequestViewTitle>();

            int randomIndex = new IntRange(min: 0, max: guardianRequestViewTitles.Count()).GetValue();

            return guardianRequestViewTitles.ElementAt(randomIndex);
        }

        private static GuardianRequestViewContactLevel GetRandomContactLevel()
        {
            IEnumerable<GuardianRequestViewContactLevel> guardianRequestViewContactLevels =
                Enum.GetValues(typeof(GuardianRequestViewContactLevel)).Cast<GuardianRequestViewContactLevel>();

            int randomIndex = new IntRange(min: 0, max: guardianRequestViewContactLevels.Count()).GetValue();

            return guardianRequestViewContactLevels.ElementAt(randomIndex);
        }

        private static GuardianRequestViewRelationship GetRandomRelationship()
        {
            IEnumerable<GuardianRequestViewRelationship> guardianRequestViewRelationships =
                Enum.GetValues(typeof(GuardianRequestViewRelationship)).Cast<GuardianRequestViewRelationship>();

            int randomIndex = new IntRange(min: 0, max: guardianRequestViewRelationships.Count()).GetValue();

            return guardianRequestViewRelationships.ElementAt(randomIndex);
        }

        private Expression<Func<GuardianRequest, bool>> SameGuardianRequestAs(GuardianRequest expectedGuardianRequest)
        {
            return actualGuardianRequest => this.compareLogic.Compare(actualGuardianRequest, expectedGuardianRequest).AreEqual;
        }

        private static dynamic CreateRandomGuardianRequestViewProperties(
            Guid userId, DateTimeOffset dateTime) => new
            {
                Id = Guid.NewGuid(),
                Title = GetRandomTitleThatIsNot(GuardianRequestViewTitle.None),
                FirstName = GetRandomString(),
                LastName = GetRandomString(),
                EmailId = GetRandomEmail(),
                CountryCode = GetRandomString(),
                ContactNumber = GetValidContactNumber(),
                Occupation = GetRandomString(),
                ContactLevel = GetRandomContactLevel(),
                Relationship = GetRandomRelationship(),
                StudentId = Guid.NewGuid(),
                CreatedDate = dateTime,
                UpdatedDate = dateTime,
                CreatedBy = userId,
                UpdatedBy = userId
            };
    }
}
