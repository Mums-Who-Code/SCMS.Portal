// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SCMS.Portal.Web.Models.Views.Components.Colors;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Components.StudentRegistrations.Exceptions;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Services.Views.StudentViews;
using SCMS.Portal.Web.Views.Bases.Buttons;
using SCMS.Portal.Web.Views.Bases.DatePickers;
using SCMS.Portal.Web.Views.Bases.Labels;
using SCMS.Portal.Web.Views.Bases.TextBoxes;

namespace SCMS.Portal.Web.Views.Components.StudentRegistrations
{
    public partial class StudentRegistrationComponent : ComponentBase
    {
        [Inject]
        public IStudentViewService studentViewService { get; set; }

        public ComponentState State { get; set; }
        public StudentRegistrationComponentException Exception { get; set; }
        public StudentView StudentView { get; set; }
        public TextBoxBase FirstNameTextBox { get; set; }
        public TextBoxBase LastNameTextBox { get; set; }
        public DatePickerBase DateOfBirthPicker { get; set; }
        public ButtonBase RegisterButton { get; set; }
        public LabelBase StatusLabel { get; set; }

        protected override void OnInitialized()
        {
            this.StudentView = new StudentView();
            this.State = ComponentState.Content;
        }

        public async void RegisterStudentAsync()
        {
            ApplyRegisteringStatus();
            await this.studentViewService.AddStudentViewAsync(this.StudentView);
        }

        private void ApplyRegisteringStatus()
        {
            this.StatusLabel.SetValue("Registering...");
            this.StatusLabel.SetColor(Color.Black);
            this.FirstNameTextBox.Disable();
            this.LastNameTextBox.Disable();
            this.DateOfBirthPicker.Disable();
            this.RegisterButton.Disable();
        }
    }
}
