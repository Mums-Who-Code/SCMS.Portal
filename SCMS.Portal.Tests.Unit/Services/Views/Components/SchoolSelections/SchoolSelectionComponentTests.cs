// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SCMS.Portal.Web.Services.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Views.Components.SchoolSelections;
using Syncfusion.Blazor;

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
    }
}
