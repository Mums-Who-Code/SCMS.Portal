// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Views.Components.GuardianRequestForms;
using Syncfusion.Blazor;

namespace SCMS.Portal.Tests.Unit.Views.Components.GuardianRequestForms
{
    public partial class GuardianRequestFormComponentTests : TestContext
    {
        private readonly Mock<IGuardianRequestViewService> guardianRequestViewServiceMock;
        private readonly IRenderedComponent<GuardianRequestFormComponent> guardianRequestFormComponent;

        public GuardianRequestFormComponentTests()
        {
            this.guardianRequestViewServiceMock = new Mock<IGuardianRequestViewService>();
            this.Services.AddScoped(service => this.guardianRequestViewServiceMock);
            this.Services.AddSyncfusionBlazor();
            this.Services.AddOptions();
            this.JSInterop.Mode = JSRuntimeMode.Loose;
        }
    }
}
