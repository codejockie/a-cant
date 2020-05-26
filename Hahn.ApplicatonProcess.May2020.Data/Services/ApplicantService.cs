using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Data.Mapper;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Domain.Entities;
using Hahn.ApplicatonProcess.May2020.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicatonProcess.May2020.Data.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly ILogger<ApplicantService> _logger;

        public ApplicantService(IApplicantRepository applicantRepository, ILogger<ApplicantService> logger)
        {
            _applicantRepository = applicantRepository ?? throw new ArgumentNullException(nameof(applicantRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApplicantModel> Create(ApplicantModel applicantModel)
        {
            var mappedEntity = ObjectMapper.Mapper.Map<Applicant>(applicantModel);
            if (mappedEntity == null)
                throw new Exception($"Entity could not be mapped.");

            var newEntity = await _applicantRepository.AddAsync(mappedEntity);
            _logger.LogInformation($"Entity successfully added - ApplicantService");

            var newMappedEntity = ObjectMapper.Mapper.Map<ApplicantModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Delete(ApplicantModel applicantModel)
        {
            ValidateApplicantIfNotExist(applicantModel);
            var deletedApplicant = await _applicantRepository.GetByIdAsync(applicantModel.ID);
            if (deletedApplicant == null)
                throw new Exception($"Entity could not be loaded.");

            await _applicantRepository.DeleteAsync(deletedApplicant);
            _logger.LogInformation($"Entity successfully deleted - ApplicantService");
        }

        public async Task<ApplicantModel> GetApplicantById(long productId)
        {
            var product = await _applicantRepository.GetByIdAsync(productId);
            var mapped = ObjectMapper.Mapper.Map<ApplicantModel>(product);
            return mapped;
        }

        public async Task Update(ApplicantModel applicantModel)
        {
            ValidateApplicantIfNotExist(applicantModel);

            var editApplicant = await _applicantRepository.GetByIdAsync(applicantModel.ID);
            if (editApplicant == null)
                throw new Exception($"Entity could not be loaded.");

            ObjectMapper.Mapper.Map(applicantModel, editApplicant);

            await _applicantRepository.UpdateAsync(editApplicant);
            _logger.LogInformation($"Entity successfully updated - ApplicantService");
        }

        private async Task ValidateApplicantIfExist(ApplicantModel applicantModel)
        {
            var existingEntity = await _applicantRepository.GetByIdAsync(applicantModel.ID);
            if (existingEntity != null)
                throw new Exception($"{applicantModel} with this id already exists");
        }

        private void ValidateApplicantIfNotExist(ApplicantModel applicantModel)
        {
            var existingEntity = _applicantRepository.GetByIdAsync(applicantModel.ID);
            if (existingEntity == null)
                throw new Exception($"{applicantModel} with this id is not exists");
        }
    }
}
