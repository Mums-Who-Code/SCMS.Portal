// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using Moq;
using RESTFulSense.Exceptions;
using SCMS.Portal.Web.Brokers.Apis;
using SCMS.Portal.Web.Brokers.DateTimes;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using SCMS.Portal.Web.Services.Foundations.GuardianRequests;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.GuardianRequests
{
    public partial class GuardianRequestServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IGuardianRequestService guardianRequestService;

        public GuardianRequestServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.guardianRequestService = new GuardianRequestService(
                apiBroker: apiBrokerMock.Object,
                dateTimeBroker: dateTimeBrokerMock.Object,
                loggingBroker: loggingBrokerMock.Object);
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

        public static TheoryData CriticalDependencyExceptions()
        {
            string someMessage = GetRandomMessage();

            var httpResponseMessage =
                new HttpResponseMessage();

            var httpRequestException =
                new HttpRequestException();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException(
                    httpResponseMessage,
                    someMessage);

            var unauthorisedHttpResponseException =
                new HttpResponseUnauthorizedException(
                    httpResponseMessage,
                    someMessage);

            return new TheoryData<Exception>
            {
                httpRequestException,
                httpResponseUrlNotFoundException,
                unauthorisedHttpResponseException
            };
        }

        public static TheoryData DependencyValidationExceptions()
        {
            string exceptionMessage = GetRandomMessage();
            var responseMessage = new HttpResponseMessage();


            var httpResponseConflictException =
                new HttpResponseConflictException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            var httpResponseFailedDependencyException =
                new HttpResponseFailedDependencyException(
                    responseMessage: responseMessage,
                    message: exceptionMessage);

            return new TheoryData<Exception>
            {
                httpResponseConflictException,
                httpResponseFailedDependencyException
            };
        }

        private static string GetRandomMessage() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static GuardianRequest CreateRandomGuardianRequest() =>
            CreateGuardianRequestFiller().Create();

        private static Dictionary<string, List<string>> CreateRandomDictionary() =>
            new Filler<Dictionary<string, List<string>>>().Create();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private static Filler<GuardianRequest> CreateGuardianRequestFiller()
        {
            var filler = new Filler<GuardianRequest>();
            Guid id = Guid.NewGuid();

            filler.Setup()
                .OnProperty(guardian => guardian.Title).Use(GuardianRequestTitle.Dr)
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow)
                .OnType<Guid>().Use(id);

            return filler;
        }
    }
}
