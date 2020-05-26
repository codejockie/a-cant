using System;
using Hahn.ApplicatonProcess.May2020.Data.Context;
using Hahn.ApplicatonProcess.May2020.Data.Repository.Base;
using Hahn.ApplicatonProcess.May2020.Domain.Entities;
using Hahn.ApplicatonProcess.May2020.Domain.Repositories;

namespace Hahn.ApplicatonProcess.May2020.Data.Repository
{
    public class ApplicantRepository : Repository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
