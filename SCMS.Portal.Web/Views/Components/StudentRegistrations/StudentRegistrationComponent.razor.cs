// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;
using SCMS.Portal.Web.Models.Views.Components.Colors;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Components.StudentRegistrations.Exceptions;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Models.Views.StudentViews.Exceptions;
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
            try
            {
                ApplyRegisteringStatus();
                await this.studentViewService.AddStudentViewAsync(this.StudentView);
                ApplyRegisteredStatus();
            }
            catch (StudentViewValidationException studentViewValidationException)
            {
                string validationMessage =
                    studentViewValidationException.InnerException.Message;

                ApplyRegistrationFailed(validationMessage);
            }
            catch (StudentViewDependencyValidationException
                studentViewDependencyValidationException)
            {
                string validationMessage =
                    studentViewDependencyValidationException.InnerException.Message;

                ApplyRegistrationFailed(validationMessage);
            }
            catch (StudentViewDependencyException
                studentViewDependencyException)
            {
                ApplyRegistrationFailed(
                    studentViewDependencyException.Message);
            }
            catch (StudentViewServiceException
                studentViewDependencyException)
            {
                ApplyRegistrationFailed(
                    studentViewDependencyException.Message);
            }
            catch(Exception exception)
            {
                ApplyRegistrationFailed(
                    exception.Message);
            }
        }

        private void ApplyRegisteringStatus()
        {
            this.StatusLabel.SetColor(Color.Black);
            this.StatusLabel.SetValue("Registering...");
            this.FirstNameTextBox.Disable();
            this.LastNameTextBox.Disable();
            this.DateOfBirthPicker.Disable();
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
            this.FirstNameTextBox.Enable();
            this.LastNameTextBox.Enable();
            this.DateOfBirthPicker.Enable();
            this.RegisterButton.Enable();
        }
    }
}
