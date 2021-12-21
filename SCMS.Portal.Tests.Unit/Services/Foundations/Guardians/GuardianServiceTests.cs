// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Net.NetworkInformation;
using Moq;
using SCMS.Portal.Web.Brokers.Apis;
using SCMS.Portal.Web.Brokers.DateTimes;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.Guardians;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Services.Foundations.Guardians;
using Tynamix.ObjectFiller;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Guardians
{
    public partial class GuardianServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IGuardianService guardianService;

        public GuardianServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.guardianService = new GuardianService(
                apiBroker: apiBrokerMock.Object,
                dateTimeBroker: dateTimeBrokerMock.Object,
                loggingBroker: loggingBrokerMock.Object);
        }

        private static Guardian CreateRandomGuardian() =>
            CreateGuardianFiller().Create();

        private static Filler<Guardian> CreateGuardianFiller()
        {
            var filler = new Filler<Guardian>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }
    }
}
