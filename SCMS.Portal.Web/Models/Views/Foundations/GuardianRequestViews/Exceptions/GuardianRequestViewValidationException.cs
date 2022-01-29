// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions
{
    public class GuardianRequestViewValidationException : Xeption
    {
        public GuardianRequestViewValidationException(Xeption innerException) :
            base("Guardian request view validation error occured, try again.", innerException)
        { }
    }
}
