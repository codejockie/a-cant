using System;
using FluentValidation;

namespace Hahn.ApplicatonProcess.May2020.Web.Validators.Custom
{
    public static class CustomValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidCountry<T>(
        this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CountryValidator(new System.Net.Http.HttpClient()));
        }
    }
}
