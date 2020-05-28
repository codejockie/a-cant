using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Data.Services;
using Hahn.ApplicatonProcess.May2020.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Hahn.ApplicatonProcess.Application.Tests
{
  public class ApplicantControllerTests
  {
    T GetViewModel<T>(IActionResult result) where T : class
    {
      return (result as ObjectResult).Value as T;
    }

    [Fact]
    public async Task Get_Returns_An_Applicant()
    {
      // Arrange - create the mock service
      Mock<IApplicantService> mock = new Mock<IApplicantService>();
      mock.Setup(m => m.GetApplicantById(1)).Returns(Task.FromResult(new ApplicantModel
      {
        ID = 1,
        Age = 24,
        Hired = true,
        Name = "John Doe",
        FamilyName = "Smith",
        CountryOfOrigin = "Germany",
        EmailAddress = "johndoe@example.com",
        Address = "Block 10A New State Avenue",
      }));

      // Arrange - create a controller
      ApplicantController target = new ApplicantController(mock.Object);

      // Action
      ApplicantModel result = GetViewModel<ApplicantModel>(await target.Get(1));

      // Assert
      Assert.Equal("John Doe", result.Name);
      Assert.Equal("Smith", result.FamilyName);
      Assert.Equal("Block 10A New State Avenue", result.Address);
    }

    [Fact]
    public async Task Delete_Removes_An_Applicant()
    {
      // Arrange - create an applicant
      var applicant = new ApplicantModel {
        ID = 1,
        Age = 24,
        Hired = true,
        Name = "John Doe",
        FamilyName = "Smith",
        CountryOfOrigin = "Germany",
        EmailAddress = "johndoe@example.com",
        Address = "Block 10A New State Avenue",
      };

      // Arrange - create the mock service
      Mock<IApplicantService> mock = new Mock<IApplicantService>();
      mock.Setup(m => m.GetApplicantById(applicant.ID)).ReturnsAsync(applicant);
      mock.Setup(m => m.Delete(applicant)).Returns(Task.CompletedTask);

      // Arrange - create a controller
      ApplicantController target = new ApplicantController(mock.Object);

      // Action
      ApplicantModel result = GetViewModel<ApplicantModel>(await target.Delete(applicant.ID));

      // Assert - ensure that the service delete method was
      // called with the correct Applicant
      mock.Verify(m => m.Delete(applicant));
      // Assert
      Assert.Equal(24, result.Age);
    }

    [Fact]
    public async Task Delete_Returns_NotFound_When_Applicant_Not_Found()
    {
      // Arrange - create an applicant
      var applicant = new ApplicantModel {
        ID = 1,
        Age = 24,
        Hired = true,
        Name = "John Doe",
        FamilyName = "Smith",
        CountryOfOrigin = "Germany",
        EmailAddress = "johndoe@example.com",
        Address = "Block 10A New State Avenue",
      };

      // Arrange - create the mock service
      Mock<IApplicantService> mock = new Mock<IApplicantService>();
      mock.Setup(m => m.GetApplicantById(applicant.ID)).ReturnsAsync(() => null);
      mock.Setup(m => m.Delete(applicant)).Returns(Task.CompletedTask);

      // Arrange - create a controller
      ApplicantController target = new ApplicantController(mock.Object);

      // Action
      var result = await target.Delete(applicant.ID);

      // Assert - ensure that the service delete method was
      // not called
      mock.Verify(m => m.Delete(applicant), Times.Never());
      // Assert
      Assert.IsType<NotFoundResult>(result);
    }
  }
}
