using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Presentation.Controllers;
using Service.Contracts;
using Shared.DTO.Feedback;
using Shared.RequestFeatures;
using System.ComponentModel.DataAnnotations;

namespace RecommendyUnitTests.FeedBack
{
    public class FeedBackCreationTests
    {
        [Fact]
        public async Task CreateFeedBack_ReturnOK()
        {
            //Arrange
            var mockServiceManager = new Mock<IServiceManager>();
            mockServiceManager.Setup(x => x.FeedbackService.CreateFeedbackAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<FeedbackCreationDto>())).Returns(Task.CompletedTask);
            var controller = new FeedBackController(mockServiceManager.Object);
            var dto = new FeedbackCreationDto() { Type = FeedbackType.Scholarship, Content = "Good", Rating = 5, StudentId = "a" };

            //Act
            controller.ModelState.Clear(); // Clear any previous state
            var validationContext = new ValidationContext(dto, null, null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
            }
            var res = await controller.CreateFeedBack(It.IsAny<string>(), It.IsAny<int>(), dto);

            //Assert
            var okResult = Assert.IsType<OkResult>(res);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task CreateFeedBack_InavlidData_ReturnBadRequest()
        {
            //Arrange
            var mockServiceManager = new Mock<IServiceManager>();
            mockServiceManager.Setup(x => x.FeedbackService.CreateFeedbackAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<FeedbackCreationDto>())).Returns(Task.CompletedTask);
            var controller = new FeedBackController(mockServiceManager.Object);
            var dto = new FeedbackCreationDto
            {
                Type = FeedbackType.Scholarship,
                Content = null,
                Rating = 6
            };

            //Act
            controller.ModelState.Clear(); // Clear any previous state
            var validationContext = new ValidationContext(dto, null, null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
            }


            var res = await controller.CreateFeedBack("1", 1, dto);

            //Assert
            var BadRequestResult = Assert.IsType<BadRequestObjectResult>(res);
            Assert.Equal(400, BadRequestResult.StatusCode);
        }

        [Fact]
        public async Task CreateFeedBack_InavlidPostId_ReturnNotFound()
        {
            //Arrange
            var mockServiceManager = new Mock<IServiceManager>();
            mockServiceManager.Setup(x => x.FeedbackService.CreateFeedbackAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<FeedbackCreationDto>())).Throws(new ScholarshipNotFoundException(1));
            var controller = new FeedBackController(mockServiceManager.Object);
            var dto = new FeedbackCreationDto() { Type = FeedbackType.Scholarship, Content = "Good", Rating = 5, StudentId = "a" };

            //Act
            controller.ModelState.Clear(); // Clear any previous state
            var validationContext = new ValidationContext(dto, null, null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
            }


            // Assert
            try
            {
                var res = await controller.CreateFeedBack("1", 1, dto);
                var notFoundResult = Assert.IsType<NotFoundObjectResult>(res);
            }
            catch (Exception e)
            {
                Assert.IsType<ScholarshipNotFoundException>(e);
                Assert.Equal($"Scholarship with id: {1} doesn't exist in the database.", e.Message);
            }
        }

        [Fact]
        public async Task CreateFeedBack_InavlidCompanyID_ReturnNotFound()
        {
            //Arrange
            var mockServiceManager = new Mock<IServiceManager>();
            mockServiceManager.Setup(x => x.FeedbackService.CreateFeedbackAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<FeedbackCreationDto>())).Throws(new StudentNotFoundException("ID"));
            var controller = new FeedBackController(mockServiceManager.Object);
            var dto = new FeedbackCreationDto() { Type = FeedbackType.Scholarship, Content = "Good", Rating = 5,StudentId="a" };

            //Act
            controller.ModelState.Clear(); // Clear any previous state
            var validationContext = new ValidationContext(dto, null, null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
            }


            // Assert
            try
            {
                var res = await controller.CreateFeedBack("1", 1,dto);
                var notFoundResult = Assert.IsType<NotFoundObjectResult>(res);
            }
            catch (Exception e)
            {
                Assert.IsType<StudentNotFoundException>(e);
                Assert.Equal($"The student with id: ID doesn't exist in the database.", e.Message);
            }
        }

    }
}