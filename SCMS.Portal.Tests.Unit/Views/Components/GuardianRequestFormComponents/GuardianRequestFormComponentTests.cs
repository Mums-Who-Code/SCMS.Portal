// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Views.Components.GuardianRequestForms;
using Syncfusion.Blazor;
using Tynamix.ObjectFiller;

namespace SCMS.Portal.Tests.Unit.Views.Components.GuardianRequestForms
{
    public partial class GuardianRequestFormComponentTests : TestContext
    {
        private readonly Mock<IGuardianRequestViewService> guardianRequestViewServiceMock;
        private IRenderedComponent<GuardianRequestFormComponent> renderedGuardianRequestFormComponent;

        public GuardianRequestFormComponentTests()
        {
            this.guardianRequestViewServiceMock = new Mock<IGuardianRequestViewService>();
            this.Services.AddScoped(service => this.guardianRequestViewServiceMock.Object);
            this.Services.AddSyncfusionBlazor();
            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }

        private static GuardianRequestView CreateRandomGuardianRequestView() =>
            CreateGuardianRequestViewFiller().Create();

        private static Filler<GuardianRequestView> CreateGuardianRequestViewFiller() =>
            new Filler<GuardianRequestView>();
    }
}
