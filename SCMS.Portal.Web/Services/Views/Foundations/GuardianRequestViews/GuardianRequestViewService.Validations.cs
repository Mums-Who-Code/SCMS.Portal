// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews;
using SCMS.Portal.Web.Models.Views.Foundations.GuardianRequestViews.Exceptions;

namespace SCMS.Portal.Web.Services.Views.Foundations.GuardianRequestViews
{
    public partial class GuardianRequestViewService
    {
        private void ValidateGuardianRequestViewOnAdd(GuardianRequestView guardianRequestView)
        {
            ValidateInput(guardianRequestView);

            Validate(
                (Rule: IsInvalid(title: guardianRequestView.Title), Parameter: nameof(GuardianRequestView.Title)),
                (Rule: IsInvalid(text: guardianRequestView.FirstName), Parameter: nameof(GuardianRequestView.FirstName)),
                (Rule: IsInvalid(text: guardianRequestView.LastName), Parameter: nameof(GuardianRequestView.LastName)),
                (Rule: IsInvalid(text: guardianRequestView.Email), Parameter: nameof(GuardianRequestView.Email)),
                (Rule: IsInvalid(text: guardianRequestView.CountryCode), Parameter: nameof(GuardianRequestView.CountryCode)),
                (Rule: IsInvalid(text: guardianRequestView.ContactNumber), Parameter: nameof(GuardianRequestView.ContactNumber)),
                (Rule: IsInvalid(text: guardianRequestView.Occupation), Parameter: nameof(GuardianRequestView.Occupation)),
                (Rule: IsInvalid(id: guardianRequestView.StudentId), Parameter: nameof(GuardianRequestView.StudentId))
            );
        }

        private void ValidateRoute(string route)
        {
            if (IsInvalidRoute(route))
            {
                throw new InvalidGuardianRequestViewException(
                    parameterName: "Route",
                    parameterValue: route);
            }
        }

        private bool IsInvalidRoute(string route) =>
            string.IsNullOrWhiteSpace(route);

        private void ValidateInput(GuardianRequestView guardianRequestView)
        {
            if (guardianRequestView == null)
            {
                throw new NullGuardianRequestViewException();
            }
        }

        private void ValidateInput(GuardianRequest guardianRequest)
        {
            if (guardianRequest == null)
            {
                throw new NullGuardianRequestException();
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

        private static dynamic IsInvalid(GuardianRequestViewTitle title) => new
        {
            Condition = title == GuardianRequestViewTitle.None,
            Message = "Value is invalid."
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidGuardianRequestViewException = new InvalidGuardianRequestViewException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidGuardianRequestViewException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidGuardianRequestViewException.ThrowIfContainsErrors();
        }

    }
}
