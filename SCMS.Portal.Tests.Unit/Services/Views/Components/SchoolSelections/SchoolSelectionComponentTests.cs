// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Services.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Views.Components.SchoolSelections;
using Syncfusion.Blazor;
using Tynamix.ObjectFiller;

namespace SCMS.Portal.Tests.Unit.Services.Views.Components.SchoolSelections
{
    public partial class SchoolSelectionComponentTests : TestContext
    {
        private IRenderedComponent<SchoolSelectionComponent> renderedSchoolSelectionComponent;
        private readonly Mock<ISchoolViewService> schoolViewServiceMock;

        public SchoolSelectionComponentTests()
        {
            this.schoolViewServiceMock = new Mock<ISchoolViewService>();
            this.Services.AddScoped(service => this.schoolViewServiceMock.Object);
            this.Services.AddSyncfusionBlazor();
            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static SchoolView CreateRandomSchoolView() =>
            CreateRandomSchoolViewFiller().Create();

        private static List<SchoolView> CreateRandomSchoolViewsWith(SchoolView schoolView)
        {
            List<SchoolView> schoolViews = CreateRandomSchoolViewFiller()
                .Create(count: GetRandomNumber()).ToList();

            schoolViews.Add(schoolView);

            return schoolViews;
        }

        private static List<SchoolView> CreateRandomSchoolViews()
        {
            return CreateRandomSchoolViewFiller()
                .Create(count: GetRandomNumber()).ToList();
        }

        private static Filler<SchoolView> CreateRandomSchoolViewFiller() =>
            new Filler<SchoolView>();

    }
}
