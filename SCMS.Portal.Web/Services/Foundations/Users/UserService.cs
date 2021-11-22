// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;

namespace SCMS.Portal.Web.Services.Foundations.Users
{
    public class UserService : IUserService
    {
        public Guid GetCurrentlyLoggedInUser() => Guid.Parse("DDBDA33E-4DF4-44CA-945D-62FEC7F73973");
    }
}
