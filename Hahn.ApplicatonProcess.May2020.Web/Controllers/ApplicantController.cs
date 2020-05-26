using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicatonProcess.May2020.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService ?? throw new ArgumentNullException(nameof(applicantService));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var applicant = await _applicantService.GetApplicantById(id);

            if (applicant == null)
            {
                return NotFound();
            }

            return Ok(applicant);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApplicantModel applicantModel)
        {
            var applicant = await _applicantService.Create(applicantModel);
            return CreatedAtAction(nameof(Get), new { id = applicant.ID }, applicant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] ApplicantModel applicantModel)
        {
            if (id != applicantModel.ID)
            {
                return BadRequest();
            }

            var applicant = await _applicantService.GetApplicantById(id);
            if (applicant == null)
            {
                return NotFound();
            }

            await _applicantService.Update(applicantModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var applicant = await _applicantService.GetApplicantById(id);
            if (applicant == null)
            {
                return NotFound();
            }

            await _applicantService.Delete(applicant);
            return Ok(applicant);
        }
    }
}
