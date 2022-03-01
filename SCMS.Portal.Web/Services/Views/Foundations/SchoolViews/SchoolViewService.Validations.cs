// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews;
using SCMS.Portal.Web.Models.Views.Foundations.SchoolViews.Exceptions;

namespace SCMS.Portal.Web.Services.Views.Foundations.SchoolViews
{
    public partial class SchoolViewService
    {
        private void ValidateGuardianRequestViewOnAdd(SchoolView schoolView)
        {
            ValidateInput(schoolView);

            Validate(
                (Rule: IsInvalid(id: schoolView.Id), Parameter: nameof(SchoolView.Id)),
                (Rule: IsInvalid(text: schoolView.Name), Parameter: nameof(SchoolView.Name))
                );
        }

        private void ValidateRoute(string route)
        {
            if (IsInvalidRoute(route))
            {
                throw new InvalidSchoolViewException(
                    parameterName: "Route",
                    parameterValue: route);
            }
        }

        private bool IsInvalidRoute(string route) =>
            string.IsNullOrWhiteSpace(route);

        private void ValidateInput(SchoolView schoolView)
        {
            if (schoolView == null)
            {
                throw new NullSchoolViewException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required."
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required."
        };

        
        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidSchoolViewException = new InvalidSchoolViewException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidSchoolViewException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidSchoolViewException.ThrowIfContainsErrors();
        }
    }
}
