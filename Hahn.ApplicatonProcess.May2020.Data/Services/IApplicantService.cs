using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Data.Models;

namespace Hahn.ApplicatonProcess.May2020.Data.Services
{
    public interface IApplicantService
    {
        Task<ApplicantModel> Create(ApplicantModel applicantModel);
        Task Delete(ApplicantModel applicantModel);
        Task<ApplicantModel> GetApplicantById(long applicantId);
        Task Update(ApplicantModel applicantModel);
    }
}
