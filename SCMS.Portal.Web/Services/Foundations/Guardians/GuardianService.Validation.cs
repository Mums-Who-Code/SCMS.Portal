// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Portal.Web.Models.Foundations.Guardians;
using SCMS.Portal.Web.Models.Foundations.Guardians.Exceptions;

namespace SCMS.Portal.Web.Services.Foundations.Guardians
{
    public partial class GuardianService
    {
        private void ValidateGuardianOnAdd(Guardian guardian)
        {
            ValidateInput(guardian);

            Validate(
                (Rule: IsInvalid(id: guardian.Id), Parameter: nameof(Guardian.Id)),
                (Rule: IsInvalid(title: guardian.Title), Parameter: nameof(Guardian.Title)),
                (Rule: IsInvalid(text: guardian.FirstName), Parameter: nameof(Guardian.FirstName)),
                (Rule: IsInvalid(text: guardian.LastName), Parameter: nameof(Guardian.LastName)),
                (Rule: IsInvalid(text: guardian.EmailId), Parameter: nameof(Guardian.EmailId)),
                (Rule: IsInvalid(text: guardian.CountryCode), Parameter: nameof(Guardian.CountryCode)),
                (Rule: IsInvalid(text: guardian.ContactNumber), Parameter: nameof(Guardian.ContactNumber)),
                (Rule: IsInvalid(text: guardian.Occupation), Parameter: nameof(Guardian.Occupation)),
                (Rule: IsInvalid(id: guardian.StudentId), Parameter: nameof(Guardian.StudentId)),
                (Rule: IsInvalid(date: guardian.CreatedDate), Parameter: nameof(Guardian.CreatedDate)),
                (Rule: IsInvalid(id: guardian.CreatedBy), Parameter: nameof(Guardian.CreatedBy))
            );
        }

        private void ValidateInput(Guardian guardian)
        {
            if (guardian == null)
            {
                throw new NullGuardianException();
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

        private static dynamic IsInvalid(Title title) => new
        {
            Condition = title == Title.None,
            Message = "Value is invalid."
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidGuardianException = new InvalidGuardianException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidGuardianException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidGuardianException.ThrowIfContainsErrors();
        }
    }
}
