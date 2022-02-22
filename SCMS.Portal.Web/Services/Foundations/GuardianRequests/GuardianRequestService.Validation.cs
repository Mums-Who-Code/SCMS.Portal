// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests;
using SCMS.Portal.Web.Models.Foundations.GuardianRequests.Exceptions;

namespace SCMS.Portal.Web.Services.Foundations.GuardianRequests
{
    public partial class GuardianRequestService
    {
        private void ValidateGuardianRequestOnAdd(GuardianRequest guardianRequest)
        {
            ValidateInput(guardianRequest);

            Validate(
                (Rule: IsInvalid(id: guardianRequest.Id), Parameter: nameof(GuardianRequest.Id)),
                (Rule: IsInvalid(title: guardianRequest.Title), Parameter: nameof(GuardianRequest.Title)),
                (Rule: IsInvalid(value: guardianRequest.ContactLevel), Parameter: nameof(GuardianRequest.ContactLevel)),
                (Rule: IsInvalid(value: guardianRequest.Relationship), Parameter: nameof(GuardianRequest.Relationship)),
                (Rule: IsInvalid(text: guardianRequest.FirstName), Parameter: nameof(GuardianRequest.FirstName)),
                (Rule: IsInvalid(text: guardianRequest.LastName), Parameter: nameof(GuardianRequest.LastName)),
                (Rule: IsInvalid(text: guardianRequest.Email), Parameter: nameof(GuardianRequest.Email)),
                (Rule: IsInvalid(text: guardianRequest.CountryCode), Parameter: nameof(GuardianRequest.CountryCode)),
                (Rule: IsInvalid(text: guardianRequest.ContactNumber), Parameter: nameof(GuardianRequest.ContactNumber)),
                (Rule: IsInvalid(text: guardianRequest.Occupation), Parameter: nameof(GuardianRequest.Occupation)),
                (Rule: IsInvalid(id: guardianRequest.StudentId), Parameter: nameof(GuardianRequest.StudentId)),
                (Rule: IsInvalid(date: guardianRequest.CreatedDate), Parameter: nameof(GuardianRequest.CreatedDate)),
                (Rule: IsInvalid(id: guardianRequest.CreatedBy), Parameter: nameof(GuardianRequest.CreatedBy))
            );
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

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required."
        };

        private static dynamic IsInvalid(GuardianRequestTitle title) => new
        {
            Condition = title == GuardianRequestTitle.None,
            Message = "Value is invalid."
        };

        private static dynamic IsInvalid<T>(T value) => new
        {
            Condition = Enum.IsDefined(typeof(T), value) is false,
            Message = "Value is invalid."
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidGuardianRequestException = new InvalidGuardianRequestException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidGuardianRequestException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidGuardianRequestException.ThrowIfContainsErrors();
        }
    }
}
