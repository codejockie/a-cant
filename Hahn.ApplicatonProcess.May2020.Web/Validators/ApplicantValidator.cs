using System.Net.Http;
using FluentValidation;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Domain.Entities;
using Hahn.ApplicatonProcess.May2020.Web.Validators.Custom;

namespace Hahn.ApplicatonProcess.May2020.Web.Validators
{
    public class ApplicantValidator : AbstractValidator<ApplicantModel>
    {
        private readonly HttpClient _httpClient;

        public ApplicantValidator(HttpClient httpClient)
        {
            _httpClient = httpClient;

            RuleFor(a => a.Name).NotEmpty().MinimumLength(5);
            RuleFor(a => a.FamilyName).NotEmpty().MinimumLength(5);
            RuleFor(a => a.Address).NotEmpty().MinimumLength(10);
            RuleFor(a => a.CountryOfOrigin).MustAsync(async (countryName, cancellation) =>
            {
                var response = await _httpClient.GetAsync($"https://restcountries.eu/rest/v2/name/{countryName}?fullText=true");
                return response.IsSuccessStatusCode;
            }).WithMessage("Country of Origin must be a valid country.");
            RuleFor(a => a.EmailAddress).NotEmpty().EmailAddress();
            RuleFor(a => a.Age).InclusiveBetween(20, 60);
            RuleFor(a => a.Hired).NotNull();
        }
    }
}
