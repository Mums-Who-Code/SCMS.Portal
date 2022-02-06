// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Views.Bases.Buttons;
using SCMS.Portal.Web.Views.Bases.Dropdowns.Selects;
using SCMS.Portal.Web.Views.Bases.Labels;
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
        public DropdownSelectBase<GuardianRequestViewTitle> TitleDropdown { get; set; }
        public TextBoxBase FirstNameTextBox { get; set; }
        public TextBoxBase LastNameTextBox { get; set; }
        public TextBoxBase EmailTextBox { get; set; }
        public TextBoxBase CountryCodeTextBox { get; set; }
        public TextBoxBase ContactNumberTextBox { get; set; }
        public TextBoxBase OccupationTextBox { get; set; }
        public DropdownSelectBase<GuardianRequestViewContactLevel> ContactLevelDropdown { get; set; }
        public DropdownSelectBase<GuardianRequestViewRelationship> RelationshipDropdown { get; set; }
        public ButtonBase RegisterButton { get; set; }
        public LabelBase StatusLabel { get; set; }
    }
}
