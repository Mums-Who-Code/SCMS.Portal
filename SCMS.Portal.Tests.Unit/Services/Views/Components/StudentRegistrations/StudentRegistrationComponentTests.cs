// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews.Exceptions;
using SCMS.Portal.Web.Services.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Services.Views.Foundations.StudentViews;
using SCMS.Portal.Web.Views.Components.StudentRegistrations;
using Syncfusion.Blazor;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Services.Views.Components.StudentRegistrations
{
    public partial class StudentRegistrationComponentTests : TestContext
    {
        private readonly Mock<IStudentViewService> studentViewServiceMock;
        private readonly Mock<ISchoolViewService> schoolViewServiceMock;
        private IRenderedComponent<StudentRegistrationComponent> renderedStudentRegistrationComponent;

        public StudentRegistrationComponentTests()
        {
            this.studentViewServiceMock = new Mock<IStudentViewService>();
            this.schoolViewServiceMock = new Mock<ISchoolViewService>();
            this.Services.AddScoped(service => this.studentViewServiceMock.Object);
            this.Services.AddScoped(service => this.schoolViewServiceMock.Object);
            this.Services.AddSyncfusionBlazor();
            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        public static TheoryData StudentViewValidationExceptions()
        {
            string randomMessage = GetRandomString();
            string validationMesage = randomMessage;
            var innerValidationException = new Xeption(validationMesage);

            return new TheoryData<Xeption>
            {
                new StudentViewValidationException(innerValidationException),
                new StudentViewDependencyValidationException(innerValidationException)
            };
        }

        public static TheoryData StudentViewDependencyExceptions()
        {
            var innerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new StudentViewDependencyException(innerException),
                new StudentViewServiceException(innerException)
            };
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static StudentView CreateRandomStudentView() =>
            CreateStudentFiller().Create();

        private static List<SchoolView> CreateRandomSchoolViews()
        {
            return CreateRandomSchoolViewFiller()
                .Create(count: GetRandomNumber()).ToList();
        }

        private static Filler<SchoolView> CreateRandomSchoolViewFiller() =>
            new Filler<SchoolView>();

        private static Filler<StudentView> CreateStudentFiller()
        {
            var filler = new Filler<StudentView>();
            DateTimeOffset date = GetRandomDateTime();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
