using System.Net;
using System.Net.Http;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Web.Validators;
using Moq;
using Xunit;

namespace Hahn.ApplicatonProcess.Application.Tests
{
  public class ApplicantValidatorTest
  {
    private Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler;
    private HttpClient _httpClient;

    public ApplicantValidatorTest()
    {
      _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
      _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);
    }

    [Fact]
    public void WhenNameIsEmpty_ShouldHaveError()
    {
      _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(@"{
          'name': 'Germany'
        }")
      });

      var model = new ApplicantModel
      {
        ID = 1,
        Age = 24,
        Hired = true,
        Name = "",
        FamilyName = "Smith",
        CountryOfOrigin = "Germany",
        EmailAddress = "johndoe@example.com",
        Address = "Block 10A New State Avenue",
      };
      var sut = new ApplicantValidator(_httpClient);
      var isValid = sut.Validate(model).IsValid;

      Assert.False(isValid);
    }

    [Fact]
    public void WhenNameIsLessFive_ShouldHaveError()
    {
      _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(@"{
          'name': 'Germany'
        }")
      });

      var model = new ApplicantModel
      {
        ID = 1,
        Age = 24,
        Hired = true,
        Name = "John",
        FamilyName = "Smith",
        CountryOfOrigin = "Germany",
        EmailAddress = "johndoe@example.com",
        Address = "Block 10A New State Avenue",
      };
      var sut = new ApplicantValidator(_httpClient);
      var isValid = sut.Validate(model).IsValid;

      Assert.False(isValid);
    }

    [Fact]
    public void WhenFamilyNameIsEmpty_ShouldHaveError()
    {
      _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(@"{
          'name': 'Germany'
        }")
      });

      var model = new ApplicantModel
      {
        ID = 1,
        Age = 24,
        Hired = true,
        Name = "John Doe",
        FamilyName = "",
        CountryOfOrigin = "Germany",
        EmailAddress = "johndoe@example.com",
        Address = "Block 10A New State Avenue",
      };
      var sut = new ApplicantValidator(_httpClient);
      var isValid = sut.Validate(model).IsValid;

      Assert.False(isValid);
    }

    [Fact]
    public void WhenAgeIsLessThan20_ShouldHaveError()
    {
      _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(@"{
          'name': 'Germany'
        }")
      });

      var model = new ApplicantModel
      {
        ID = 1,
        Age = 19,
        Hired = true,
        Name = "John Doe",
        FamilyName = "Smith",
        CountryOfOrigin = "Germany",
        EmailAddress = "johndoe@example.com",
        Address = "Block 10A New State Avenue",
      };
      var sut = new ApplicantValidator(_httpClient);
      var isValid = sut.Validate(model).IsValid;

      Assert.False(isValid);
    }

    [Fact]
    public void WhenCountryOfOriginIsInvalid_ShouldHaveError()
    {
      _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.NotFound,
        Content = new StringContent(@"{
          'status': 404,
          'message': 'Not Found'
        }")
      });

      var model = new ApplicantModel
      {
        ID = 1,
        Age = 24,
        Hired = true,
        Name = "John Doe",
        FamilyName = "Smith",
        CountryOfOrigin = "Micro",
        EmailAddress = "johndoe@example.com",
        Address = "Block 10A New State Avenue",
      };
      var sut = new ApplicantValidator(_httpClient);
      var isValid = sut.Validate(model).IsValid;

      Assert.False(isValid);
    }

    [Fact]
    public void WhenModelIsValid_ShouldNotHaveError()
    {
      _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(@"{
          'name': 'Nigeria',
          'message': 'Not Found'
        }")
      });

      var model = new ApplicantModel
      {
        ID = 1,
        Age = 24,
        Hired = true,
        Name = "James",
        FamilyName = "Smith",
        CountryOfOrigin = "Nigeria",
        EmailAddress = "johndoe@example.com",
        Address = "Block 10A New State Avenue",
      };
      var sut = new ApplicantValidator(_httpClient);
      var isValid = sut.Validate(model).IsValid;

      Assert.True(isValid);
    }
  }
}