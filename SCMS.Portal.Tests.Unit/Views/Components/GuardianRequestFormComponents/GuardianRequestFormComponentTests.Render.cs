// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Views.Components.GuardianRequestForms;
using Syncfusion.Blazor;
using Xunit;

namespace SCMS.Portal.Tests.Unit.Views.Components.GuardianRequestForms
{
    public partial class GuardianRequestFormComponentTests
    {
        [Fact]
        public void ShouldInitializeComponent()
        {
            // given
            ComponentState expectedComponentState = ComponentState.Loading;

            // when
            var initialGuardianRequestFormComponent = new GuardianRequestFormComponent();

            // then
            initialGuardianRequestFormComponent.State.Should().Be(expectedComponentState);
            initialGuardianRequestFormComponent.GuardianRequestView.Should().BeNull();
            initialGuardianRequestFormComponent.Title.Should().BeNull();
            initialGuardianRequestFormComponent.FirstName.Should().BeNull();
            initialGuardianRequestFormComponent.LastName.Should().BeNull();
            initialGuardianRequestFormComponent.EmailId.Should().BeNull();
            initialGuardianRequestFormComponent.CountryCode.Should().BeNull();
            initialGuardianRequestFormComponent.ContactNumber.Should().BeNull();
            initialGuardianRequestFormComponent.Occupation.Should().BeNull();
            initialGuardianRequestFormComponent.ContactLevel.Should().BeNull();
            initialGuardianRequestFormComponent.Relationship.Should().BeNull();
            initialGuardianRequestFormComponent.StudentId.Should().BeEmpty();
        }
    }
}
