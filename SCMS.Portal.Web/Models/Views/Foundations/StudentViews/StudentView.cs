// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;

namespace SCMS.Portal.Web.Models.Views.Foundations.StudentViews
{
    public class StudentView
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public StudentGenderView Gender { get; set; }
        public Guid SchoolId { get; set; }
        public string FideId { get; set; }
        public string Notes { get; set; }
    }
}
