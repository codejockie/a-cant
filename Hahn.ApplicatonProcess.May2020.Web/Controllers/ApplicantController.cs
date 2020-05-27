using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicatonProcess.May2020.Web.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Produces("application/json")]
  public class ApplicantController : ControllerBase
  {
    private readonly IApplicantService _applicantService;

    public ApplicantController(IApplicantService applicantService)
    {
      _applicantService = applicantService ?? throw new ArgumentNullException(nameof(applicantService));
    }

    /// <summary>
    /// Gets a specific applicant.
    /// </summary>
    /// <param name="id" example="1">The applicant ID</param>
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

    /// <summary>
    /// Creates an applicant.
    /// </summary>
    /// <param name="applicantModel"></param>
    /// <returns>A newly created applicant</returns>
    /// <response code="201">Returns the newly created applicant</response>
    /// <response code="400">Applicant has missing/invalid values</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ApplicantModel applicantModel)
    {
      var applicant = await _applicantService.Create(applicantModel);
      return CreatedAtAction(nameof(Get), new { id = applicant.ID }, applicant);
    }

    /// <summary>
    /// Updates an applicant.
    /// </summary>
    /// <param name="id" example="1">The applicant ID</param>
    /// <param name="applicantModel">The applicant model</param>
    /// <response code="204">Returns no content</response>
    /// <response code="400">Applicant has missing/invalid values</response>
    /// <response code="404">Applicant not found</response>
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

    /// <summary>
    /// Deletes a specific applicant.
    /// </summary>
    /// <param name="id" example="1">The applicant ID</param>
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
