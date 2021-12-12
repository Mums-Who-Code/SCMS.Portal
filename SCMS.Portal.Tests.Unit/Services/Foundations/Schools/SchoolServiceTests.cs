// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using Moq;
using RESTFulSense.Exceptions;
using SCMS.Portal.Web.Brokers.Apis;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.Schools;
using SCMS.Portal.Web.Services.Foundations.Schools;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

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

        public static TheoryData CriticalDependencyExceptions()
        {
            string someMessage = GetRandomMessage();

            var httpResponseMessage =
                new HttpResponseMessage();

            var httpRequestException =
                new HttpRequestException();

            var httpReponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException(
                    httpResponseMessage,
                    someMessage);

            var unauthorizedHttpResponseException =
                new HttpResponseUnauthorizedException(
                    httpResponseMessage,
                    someMessage);

            return new TheoryData<Exception>
            {
                httpRequestException,
                httpReponseUrlNotFoundException,
                unauthorizedHttpResponseException
            };
        }

        public static TheoryData ApiDependencyExceptions()
        {
            var responseMessage = new HttpResponseMessage();
            string exceptionMessage = GetRandomMessage();

            var httpResponseException =
                new HttpResponseException(
                    httpResponseMessage: responseMessage,
                    message: exceptionMessage);

            var httpResponseInternalServerErrorException =
                new HttpResponseInternalServerErrorException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            return new TheoryData<Exception>
            {
                httpResponseException,
                httpResponseInternalServerErrorException
            };
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static string GetRandomMessage() =>
            new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static List<School> CreateRandomSchools()
        {
            return CreateRandomSchoolFiller()
                .Create(count: GetRandomNumber()).ToList();
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
