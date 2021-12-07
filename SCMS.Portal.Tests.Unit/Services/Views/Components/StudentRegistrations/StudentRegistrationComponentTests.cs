// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Models.Views.StudentViews.Exceptions;
using SCMS.Portal.Web.Services.Views.StudentViews;
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
        private IRenderedComponent<StudentRegistrationComponent> renderedStudentRegistrationComponent;

        public StudentRegistrationComponentTests()
        {
            this.studentViewServiceMock = new Mock<IStudentViewService>();
            this.Services.AddScoped(service => this.studentViewServiceMock.Object);
            this.Services.AddSyncfusionBlazor();
            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        public static TheoryData StudentViewValidationExceptions()
        {
            string randomMessage = GetRandomString();
            string validationMesage = randomMessage;
            var innerValidationException = new Xeption(validationMesage);

            return new TheoryData<Exception>
            {
                new StudentViewValidationException(innerValidationException),
                new StudentViewDependencyValidationException(innerValidationException)
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static StudentView CreateRandomStudentView() =>
            CreateStudentFiller().Create();

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
