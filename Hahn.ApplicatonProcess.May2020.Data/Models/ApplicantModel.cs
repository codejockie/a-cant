using System;
using Hahn.ApplicatonProcess.May2020.Data.Models.Base;

namespace Hahn.ApplicatonProcess.May2020.Data.Models
{
    public class ApplicantModel : BaseModel
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }
    }
}
