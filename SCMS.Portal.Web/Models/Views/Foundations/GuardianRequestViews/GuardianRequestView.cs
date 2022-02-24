// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;

namespace SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews
{
    public class GuardianRequestView
    {
        public Guid Id { get; set; }
        public GuardianRequestViewTitle Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string ContactNumber { get; set; }
        public string Occupation { get; set; }
        public GuardianRequestViewContactLevel ContactLevel { get; set; }
        public GuardianRequestViewRelationship Relationship { get; set; }
        public Guid StudentId { get; set; }
    }
}
