// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;
using SCMS.Portal.Web.Models.Views.Components.Containers;
using SCMS.Portal.Web.Models.Views.Components.StudentRegistrations.Exceptions;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Services.Views.StudentViews;
using SCMS.Portal.Web.Views.Bases.TextBoxes;

namespace SCMS.Portal.Web.Views.Components.StudentRegistrations
{
    public partial class StudentRegistrationComponent
    {
        [Inject]
        public IStudentViewService studentViewService { get; set; }

        public ComponentState State { get; set; }
        public StudentRegistrationComponentException Exception { get; set; }
        public StudentView StudentView { get; set; }
        public TextBoxBase FirstNameTextBox { get; set; }
        public TextBoxBase LastNameTextBox { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
    }
}
