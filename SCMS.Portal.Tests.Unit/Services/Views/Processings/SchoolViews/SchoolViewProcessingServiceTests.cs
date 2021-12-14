// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions;
using SCMS.Portal.Web.Services.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Services.Views.Processings.SchoolViews;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Processings.SchoolViews
{
    public partial class SchoolViewProcessingServiceTests
    {
        private readonly ISchoolViewProcessingService schoolViewProcessingService;
        private readonly Mock<ISchoolViewService> schoolViewServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;

        public SchoolViewProcessingServiceTests()
        {
            this.schoolViewServiceMock = new Mock<ISchoolViewService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.schoolViewProcessingService = new SchoolViewProcessingService(
                schoolViewService: this.schoolViewServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData DependencyExceptions()
        {
            string exceptionMessage = GetRandomString();
            var innerException = new Xeption(exceptionMessage);

            return new TheoryData<Xeption>
            {
                new SchoolViewDependencyException(innerException),
                new SchoolViewServiceException(innerException)
            };
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static List<SchoolView> CreateRandomSchoolViews()
        {
            return CreateSchoolViewFiller()
                .Create(count: GetRandomNumber())
                    .ToList();
        }

        private static Filler<SchoolView> CreateSchoolViewFiller() =>
            new Filler<SchoolView>();
    }
}
