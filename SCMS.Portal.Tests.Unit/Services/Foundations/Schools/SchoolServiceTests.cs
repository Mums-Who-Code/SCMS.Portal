// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using Moq;
using SCMS.Portal.Web.Brokers.Apis;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.Schools;
using SCMS.Portal.Web.Services.Foundations.Schools;
using Tynamix.ObjectFiller;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Schools
{
    public partial class SchoolServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ISchoolService schoolService;

        public SchoolServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.schoolService = new SchoolService(
                apiBroker: this.apiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static IQueryable<School> CreateRandomSchools()
        {
            return CreateRandomSchoolFiller()
                .Create(count: GetRandomNumber()).AsQueryable();
        }

        private static Filler<School> CreateRandomSchoolFiller()
        {
            var filler = new Filler<School>();
            Guid randomGuid = Guid.NewGuid();
            DateTimeOffset dates = GetRandomDateTime();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(valueToUse: dates);

            return filler;
        }
    }
}
