// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Foundations.Schools;

namespace SCMS.Portal.Web.Services.Foundations.Schools
{
    public interface ISchoolService
    {
        ValueTask<List<School>> RetrieveAllSchoolsAsync();
    }
}
