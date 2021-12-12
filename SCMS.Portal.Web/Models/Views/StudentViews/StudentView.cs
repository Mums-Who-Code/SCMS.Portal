// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Portal.Web.Models.Foundations.Students;

namespace SCMS.Portal.Web.Models.Views.StudentViews
{
    public class StudentView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public StudentGenderView Gender { get; set; }
        public Guid SchoolId { get; set; }
        public string FideId { get; set; }
        public string Notes { get; set; }
    }
}
