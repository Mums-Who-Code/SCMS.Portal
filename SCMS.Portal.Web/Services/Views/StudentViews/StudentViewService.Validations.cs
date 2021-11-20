// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Views.StudentViews;
using SCMS.Portal.Web.Models.Views.StudentViews.Exceptions;

namespace SCMS.Portal.Web.Services.Views.StudentViews
{
    public partial class StudentViewService
    {
        private void ValidateStudentViewOnAdd(StudentView studentView)
        {
            ValidateInput(studentView);
        }

        private void ValidateInput(StudentView studentView)
        {
            if (studentView == null)
            {
                throw new NullStudentViewException();
            }
        }
    }
}
