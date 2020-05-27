using System;
using Hahn.ApplicatonProcess.May2020.Data.Models.Base;

namespace Hahn.ApplicatonProcess.May2020.Data.Models
{
  public class ApplicantModel : BaseModel
  {
    /// <summary>
    /// Applicant's name
    /// </summary>
    /// <example>John Doe</example>
    public string Name { get; set; }
    /// <summary>
    /// Applicant's family name
    /// </summary>
    /// <example>Smith</example>
    public string FamilyName { get; set; }
    /// <summary>
    /// Applicant's address
    /// </summary>
    /// <example>Block 10A New State Avenue</example>
    public string Address { get; set; }
    /// <summary>
    /// Applicant's country of origin
    /// </summary>
    /// <example>Nigeria</example>
    public string CountryOfOrigin { get; set; }
    /// <summary>
    /// Applicant's email
    /// </summary>
    /// <example>johndoe@example.com</example>
    public string EmailAddress { get; set; }
    /// <summary>
    /// Applicant's age
    /// </summary>
    /// <example>25</example>
    public int Age { get; set; }
    /// <summary>
    /// Applicant's hired status
    /// </summary>
    /// <example>true</example>
    public bool Hired { get; set; }
  }
}
