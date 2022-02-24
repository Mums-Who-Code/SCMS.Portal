// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.SchoolViews
{
    public class SchoolViewValidationException : Xeption
    {
        public SchoolViewValidationException(Exception innerException) 
           : base("School view validation error occured, try again.", innerException)
        { }
    }
}
