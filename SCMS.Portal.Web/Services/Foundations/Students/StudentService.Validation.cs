// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Portal.Web.Models.Foundations.Students;
using SCMS.Portal.Web.Models.Foundations.Students.Exceptions;

namespace SCMS.Portal.Web.Services.Foundations.Students
{
    public partial class StudentService
    {
        private void ValidateStudentOnAdd(Student student)
        {
            ValidateInput(student);

            Validate(
               (Rule: IsInvalid(student.Id), Parameter: nameof(Student.Id)),
               (Rule: IsInvalid(text: student.FirstName), Parameter: nameof(Student.FirstName)),
               (Rule: IsInvalid(text: student.LastName), Parameter: nameof(Student.LastName)),
               (Rule: IsInvalid(date: student.DateOfBirth), Parameter: nameof(Student.DateOfBirth)),
               (Rule: IsInvalid(student.Status), Parameter: nameof(Student.Status)),
               (Rule: IsInvalid(student.CreatedDate), Parameter: nameof(Student.CreatedDate)),
               (Rule: IsInvalid(id: student.CreatedBy), Parameter: nameof(Student.CreatedBy)),

               (Rule: IsInvalid(
                   firstDate: student.UpdateDate,
                   secondDate: student.CreatedDate,
                   secondParameterName: nameof(Student.CreatedDate)),
                Parameter: nameof(Student.UpdateDate)),

               (Rule: IsInvalid(
                   firstId: student.UpdatedBy,
                   secondId: student.CreatedBy,
                   secondParameterName: nameof(Student.CreatedBy)),
                Parameter: nameof(Student.UpdatedBy))
           );
        }

        private void ValidateInput(Student student)
        {
            if (student == null)
            {
                throw new NullStudentException();
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

        private static dynamic IsInvalid(StudentStatus status) => new
        {
            Condition = status != StudentStatus.Active,
            Message = "Value is invalid."
        };

        private static dynamic IsInvalid(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondParameterName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not same as {secondParameterName}."
            };

        private static dynamic IsInvalid(
            Guid firstId,
            Guid secondId,
            string secondParameterName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not same as {secondParameterName}."
            };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentException = new InvalidStudentException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidStudentException.ThrowIfContainsErrors();
        }
    }
}