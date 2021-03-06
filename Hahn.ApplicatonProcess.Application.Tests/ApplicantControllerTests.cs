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
    private ApplicantModel _applicant;
    public ApplicantControllerTests() {
      _applicant =  new ApplicantModel {
        ID = 1,
        Age = 24,
        Hired = true,
        Name = "John Doe",
        FamilyName = "Smith",
        CountryOfOrigin = "Germany",
        EmailAddress = "johndoe@example.com",
        Address = "Block 10A New State Avenue",
      };
    }
    
    T GetViewModel<T>(IActionResult result) where T : class
    {
      return (result as ObjectResult).Value as T;
    }

    [Fact]
    public async Task Get_Returns_NotFound_When_An_Applicant_Is_Not_Found()
    {
      // Arrange - create the mock service
      Mock<IApplicantService> mock = new Mock<IApplicantService>();
      mock.Setup(m => m.GetApplicantById(1)).ReturnsAsync(() => null);

      // Arrange - create a controller
      ApplicantController target = new ApplicantController(mock.Object);

      // Action
      var result = await target.Get(1);

      // Assert
      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Get_Returns_An_Applicant()
    {
      // Arrange - create the mock service
      Mock<IApplicantService> mock = new Mock<IApplicantService>();
      mock.Setup(m => m.GetApplicantById(1)).ReturnsAsync(_applicant);

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
    public async Task Post_Creates_An_Applicant()
    {
      var newApplicant = new ApplicantModel {
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
      mock.Setup(m => m.Create(newApplicant)).ReturnsAsync(_applicant);

      // Arrange - create a controller
      ApplicantController target = new ApplicantController(mock.Object);

      // Action
      ApplicantModel result = GetViewModel<ApplicantModel>(await target.Post(newApplicant));

      // Assert
      Assert.Equal(1, result.ID);
      Assert.Equal("John Doe", result.Name);
      Assert.Equal("Smith", result.FamilyName);
      Assert.Equal("Block 10A New State Avenue", result.Address);
    }

    [Fact]
    public async Task Delete_Returns_NotFound_When_An_Applicant_Is_Not_Found()
    {
      // Arrange - create the mock service
      Mock<IApplicantService> mock = new Mock<IApplicantService>();
      mock.Setup(m => m.GetApplicantById(_applicant.ID)).ReturnsAsync(() => null);
      mock.Setup(m => m.Delete(_applicant)).Returns(Task.CompletedTask);

      // Arrange - create a controller
      ApplicantController target = new ApplicantController(mock.Object);

      // Action
      var result = await target.Delete(_applicant.ID);

      // Assert
      Assert.IsType<NotFoundResult>(result);
      // Assert - ensure that the service delete method was
      // not called
      mock.Verify(m => m.Delete(_applicant), Times.Never());
    }

    [Fact]
    public async Task Delete_Removes_An_Applicant()
    {
      // Arrange - create the mock service
      Mock<IApplicantService> mock = new Mock<IApplicantService>();
      mock.Setup(m => m.GetApplicantById(_applicant.ID)).ReturnsAsync(_applicant);
      mock.Setup(m => m.Delete(_applicant)).Returns(Task.CompletedTask);

      // Arrange - create a controller
      ApplicantController target = new ApplicantController(mock.Object);

      // Action
      ApplicantModel result = GetViewModel<ApplicantModel>(await target.Delete(_applicant.ID));

      // Assert
      Assert.Equal(24, result.Age);
      // Assert - ensure that the service delete method was
      // called with the correct Applicant
      mock.Verify(m => m.Delete(_applicant));
    }
    
    [Fact]
    public async Task Put_Returns_BadRequest_For_Non_Matching_ID()
    {
      // Arrange - create the mock service
      Mock<IApplicantService> mock = new Mock<IApplicantService>();
      mock.Setup(m => m.Update(_applicant)).Returns(Task.CompletedTask);

      // Arrange - create a controller
      ApplicantController target = new ApplicantController(mock.Object);

      // Action
      var result = await target.Put(2, _applicant);

      // Assert
      Assert.IsType<BadRequestResult>(result);
      mock.Verify(m => m.Update(_applicant), Times.Never());
    }
    
    [Fact]
    public async Task Put_Returns_NotFound_When_An_Applicant_Is_Not_Found()
    {
      // Arrange - create the mock service
      Mock<IApplicantService> mock = new Mock<IApplicantService>();
      mock.Setup(m => m.GetApplicantById(_applicant.ID)).ReturnsAsync(() => null);
      mock.Setup(m => m.Update(_applicant)).Returns(Task.CompletedTask);

      // Arrange - create a controller
      ApplicantController target = new ApplicantController(mock.Object);

      // Action
      var result = await target.Put(1, _applicant);

      // Assert
      Assert.IsType<NotFoundResult>(result);
      mock.Verify(m => m.Update(_applicant), Times.Never());
    }
    
    [Fact]
    public async Task Put_Returns_NoContent_On_Successful_Update()
    {
      var applicant = new ApplicantModel {
        ID = 1,
        Age = 25,
        Hired = true,
        Name = "James",
        FamilyName = "Smith",
        CountryOfOrigin = "Nigeria",
        EmailAddress = "jamessmith@example.com",
        Address = "Block 10B New State Avenue",
      };
      // Arrange - create the mock service
      Mock<IApplicantService> mock = new Mock<IApplicantService>();
      mock.Setup(m => m.GetApplicantById(_applicant.ID)).ReturnsAsync(_applicant);
      mock.Setup(m => m.Update(applicant)).Returns(Task.CompletedTask);

      // Arrange - create a controller
      ApplicantController target = new ApplicantController(mock.Object);

      // Action
      var result = await target.Put(1, _applicant);

      // Assert
      Assert.IsType<NoContentResult>(result);
      mock.Verify(m => m.Update(_applicant), Times.Once());
    }
  }
}
