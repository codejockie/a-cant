using FluentValidation;
using Hahn.ApplicatonProcess.May2020.Domain.Entities;
using Hahn.ApplicatonProcess.May2020.Web.Validators.Custom;

namespace Hahn.ApplicatonProcess.May2020.Web.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MinimumLength(5);
            RuleFor(a => a.FamilyName).NotEmpty().MinimumLength(5);
            RuleFor(a => a.Address).NotEmpty().MinimumLength(10);
            RuleFor(a => a.CountryOfOrigin).ValidCountry();
            RuleFor(a => a.EmailAddress).EmailAddress();
            RuleFor(a => a.Age).InclusiveBetween(20, 60);
            RuleFor(a => a.Hired).NotNull();
        }
    }
}
