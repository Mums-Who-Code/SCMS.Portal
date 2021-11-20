// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Portal.Web.Models.Views.StudentViews;

namespace SCMS.Portal.Web.Services.Views.StudentViews
{
    public interface IStudentViewService
    {
        ValueTask<StudentView> AddStudentViewAsync(StudentView studentView);
    }
}
