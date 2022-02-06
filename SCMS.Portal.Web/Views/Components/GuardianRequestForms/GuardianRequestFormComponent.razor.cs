// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Views.Bases.Dropdowns.Selects;
using SCMS.Portal.Web.Views.Bases.TextBoxes;

namespace SCMS.Portal.Web.Views.Components.GuardianRequestForms
{
    public partial class GuardianRequestFormComponent : ComponentBase
    {
        [Inject]
        public IGuardianRequestViewService guardianRequestViewService { get; set; }

        [Parameter]
        public Guid StudentId { get; set; }

        public ComponentState State { get; set; }
        public GuardianRequestView GuardianRequestView { get; set; }
        public DropdownSelectBase<GuardianRequestViewTitle> Title { get; set; }
        public TextBoxBase FirstName { get; set; }
        public TextBoxBase LastName { get; set; }
        public TextBoxBase EmailId { get; set; }
        public TextBoxBase CountryCode { get; set; }
        public TextBoxBase ContactNumber { get; set; }
        public TextBoxBase Occupation { get; set; }
        public DropdownSelectBase<GuardianRequestViewContactLevel> ContactLevel { get; set; }
        public DropdownSelectBase<GuardianRequestViewRelationship> Relationship { get; set; }
    }
}
