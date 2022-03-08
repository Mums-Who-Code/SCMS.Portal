// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions
{
    public partial class SchoolViewValidationException : Xeption
    {
        public SchoolViewValidationException(Xeption innerException)
            : base(message: "School view validation error occured, try again.", innerException)
        { }
    }
}
