// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.Schools.Exceptions;
using SCMS.Portal.Web.Services.Foundations.Schools;
using SCMS.Portal.Web.Services.Views.Foundations.SchoolViews;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Foundations.SchoolViews
{
    public partial class SchoolViewServiceTests
    {
        private readonly Mock<ISchoolService> schoolServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ISchoolViewService schoolViewService;

        public SchoolViewServiceTests()
        {
            this.schoolServiceMock = new Mock<ISchoolService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.schoolViewService = new SchoolViewService(
                schoolService: this.schoolServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData DependencyExceptions()
        {
            var innerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new SchoolDependencyException(innerException),
                new SchoolServiceException(innerException)
            };
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static List<dynamic> CreatedRandomSchoolViewCollections()
        {
            int randomCount = GetRandomNumber();

            return Enumerable.Range(0, randomCount).Select(item =>
            {
                return new
                {
                    Id = Guid.NewGuid(),
                    Name = GetRandomString(),
                    CreatedDate = GetRandomDate(),
                    UpdatedDate = GetRandomDate(),
                    CreatedBy = Guid.NewGuid(),
                    UpdatedBy = Guid.NewGuid()
                };
            }).ToList<dynamic>();
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 0, max: 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();
    }
}