using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Validators;

namespace Hahn.ApplicatonProcess.May2020.Web.Validators.Custom
{
    public class CountryValidator : PropertyValidator
    {
        private readonly HttpClient _httpClient;

        public CountryValidator(HttpClient httpClient) : base("{PropertyName} must be a valid country.")
        {
            _httpClient = httpClient;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var countryName = (string)context.PropertyValue;
            var response = _httpClient.GetAsync($"https://restcountries.eu/rest/v2/name/{countryName}?fullText=true").Result;
            return response.IsSuccessStatusCode;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            var countryName = (string)context.PropertyValue;
            var response = await _httpClient.GetAsync($"https://restcountries.eu/rest/v2/name/{countryName}?fullText=true");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
