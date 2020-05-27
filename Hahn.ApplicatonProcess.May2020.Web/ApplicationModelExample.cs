using Hahn.ApplicatonProcess.May2020.Data.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicatonProcess.May2020.Web
{
  public class ApplicantModelExample : IExamplesProvider<ApplicantModel>
  {
    public ApplicantModel GetExamples() => new ApplicantModel
    {
      Name = "John Doe",
      FamilyName = "Smith",
      Address = "Block 10A New State Avenue",
      CountryOfOrigin = "Nigeria",
      EmailAddress = "johndoe@example.com",
      Age = 25,
      Hired = true
    };
  }
}
