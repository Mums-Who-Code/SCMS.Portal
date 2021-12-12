// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews;
using SCMS.Portal.Web.Models.Views.Foundations.StudentViews.Exceptions;

namespace SCMS.Portal.Web.Services.Views.Foundations.StudentViews
{
    public partial class StudentViewService
    {
        private void ValidateStudentViewOnAdd(StudentView studentView)
        {
            ValidateInput(studentView);

            Validate(
               (Rule: IsInvalid(text: studentView.FirstName), Parameter: nameof(StudentView.FirstName)),
               (Rule: IsInvalid(text: studentView.LastName), Parameter: nameof(StudentView.LastName)),
               (Rule: IsInvalid(date: studentView.DateOfBirth), Parameter: nameof(StudentView.DateOfBirth)),
               (Rule: IsInvalid(id: studentView.SchoolId), Parameter: nameof(StudentView.SchoolId))
            );
        }

        private void ValidateInput(StudentView studentView)
        {
            if (studentView == null)
            {
                throw new NullStudentViewException();
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required."
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required."
        };

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required."
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentViewException = new InvalidStudentViewException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentViewException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidStudentViewException.ThrowIfContainsErrors();
        }
    }
}
