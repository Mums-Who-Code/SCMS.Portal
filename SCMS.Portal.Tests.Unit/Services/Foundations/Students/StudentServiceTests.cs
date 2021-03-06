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
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Services.Foundations.Students;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IStudentService studentService;

        public StudentServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.studentService = new StudentService(
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

        private static Student CreateRandomStudent() =>
            CreateStudentFiller(dateTime: GetRandomDateTime()).Create();

        private static Dictionary<string, List<string>> CreateRandomDictionary() =>
            new Filler<Dictionary<string, List<string>>>().Create();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetNegativeRandomNumber() => -1 * GetRandomNumber();
        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }
        private static Filler<Student> CreateStudentFiller(DateTimeOffset dateTime)
        {
            var filler = new Filler<Student>();
            Guid userId = Guid.NewGuid();

            filler.Setup()
                .OnProperty(student => student.Status).Use(StudentStatus.Active)
                .OnType<DateTimeOffset>().Use(dateTime)
                .OnType<Guid>().Use(userId);

            return filler;
        }
    }
}
