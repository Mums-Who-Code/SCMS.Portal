// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;
using SCMS.Portal.Web.Models.Views.Components.Colors;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions;
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
        public LabelBase TitleLabel { get; set; }
        public DropdownSelectBase<GuardianRequestViewTitle> TitleDropdown { get; set; }
        public TextBoxBase FirstNameTextBox { get; set; }
        public TextBoxBase LastNameTextBox { get; set; }
        public TextBoxBase EmailTextBox { get; set; }
        public TextBoxBase CountryCodeTextBox { get; set; }
        public TextBoxBase ContactNumberTextBox { get; set; }
        public TextBoxBase OccupationTextBox { get; set; }
        public LabelBase ContactLevelLabel { get; set; }
        public DropdownSelectBase<GuardianRequestViewContactLevel> ContactLevelDropdown { get; set; }
        public LabelBase RelationshipLabel { get; set; }
        public DropdownSelectBase<GuardianRequestViewRelationship> RelationshipDropdown { get; set; }
        public ButtonBase RegisterButton { get; set; }
        public LabelBase StatusLabel { get; set; }

        protected override void OnInitialized()
        {
            this.GuardianRequestView = new GuardianRequestView();
            this.State = ComponentState.Content;
        }

        public async void RegisterGuardianRequestAsync()
        {
            try
            {
                ApplyRegisteringStatus();
                this.GuardianRequestView.StudentId = this.StudentId;
                await this.guardianRequestViewService.AddGuardianRequestViewAsync(this.GuardianRequestView);
                ApplyRegisteredStatus();
            }
            catch (GuardianRequestViewValidationException guardianRequestViewValidationException)
            {
                string validationMessage =
                    guardianRequestViewValidationException.InnerException.Message;

                ApplyRegistrationFailed(validationMessage);
            }
            catch (GuardianRequestViewDependencyValidationException guardianRequestViewDependencyValidationException)
            {
                string validationMessage =
                    guardianRequestViewDependencyValidationException.InnerException.Message;

                ApplyRegistrationFailed(validationMessage);
            }
        }

        private void ApplyRegisteringStatus()
        {
            this.StatusLabel.SetColor(Color.Black);
            this.StatusLabel.SetValue("Registering...");
            this.TitleDropdown.Disable();
            this.FirstNameTextBox.Disable();
            this.LastNameTextBox.Disable();
            this.EmailTextBox.Disable();
            this.CountryCodeTextBox.Disable();
            this.ContactNumberTextBox.Disable();
            this.OccupationTextBox.Disable();
            this.ContactLevelDropdown.Disable();
            this.RelationshipDropdown.Disable();
            this.RegisterButton.Disable();
        }

        private void ApplyRegisteredStatus()
        {
            this.StatusLabel.SetColor(Color.Green);
            this.StatusLabel.SetValue("Registration completed.");
        }

        private void ApplyRegistrationFailed(string validationMessage)
        {
            this.StatusLabel.SetValue(validationMessage);
            this.StatusLabel.SetColor(Color.Red);
            this.TitleDropdown.Enable();
            this.FirstNameTextBox.Enable();
            this.LastNameTextBox.Enable();
            this.EmailTextBox.Enable();
            this.CountryCodeTextBox.Enable();
            this.ContactNumberTextBox.Enable();
            this.OccupationTextBox.Enable();
            this.ContactLevelDropdown.Enable();
            this.RelationshipDropdown.Enable();
            this.RegisterButton.Enable();
        }
    }
}
