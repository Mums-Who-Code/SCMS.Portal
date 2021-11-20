// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;

namespace SCMS.Portal.Web.Services.Foundations.Users
{
    public interface IUserService
    {
        Guid GetCurrentlyLoggedInUser();
    }
}
