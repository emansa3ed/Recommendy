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
using Shared.DTO.Notification;
using Shared.RequestFeatures;
using System.ComponentModel.DataAnnotations;

namespace RecommendyUnitTests.FeedBack
{
    public class FeedBackDeletionTests
	{
        [Fact]
        public async Task DeleteFeedBack_ReturnNoContent()
        {
            //Arrange
            var mockServiceManager = new Mock<IServiceManager>();
            mockServiceManager.Setup(x => x.FeedbackService.DeleteFeedbackAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FeedbackDelationDto>())).Returns(Task.CompletedTask);
            var controller = new FeedBackController(mockServiceManager.Object);
            var dto = new FeedbackDelationDto() { Type = FeedbackType.Scholarship,Id=1  };

            //Act
            controller.ModelState.Clear(); // Clear any previous state
            var validationContext = new ValidationContext(dto, null, null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
            }
            var res = await controller.DeleteFeedBack(It.IsAny<string>(), It.IsAny<int>(), dto);

            //Assert
            var NoContent = Assert.IsType<NoContentResult>(res);
            Assert.Equal(204, NoContent.StatusCode);
        }

        [Fact]
        public async Task DeleteFeedBack_InavlidData_ReturnBadRequest()
        {
            //Arrange
            var mockServiceManager = new Mock<IServiceManager>();
            mockServiceManager.Setup(x => x.FeedbackService.DeleteFeedbackAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FeedbackDelationDto>())).Returns(Task.CompletedTask);
            var controller = new FeedBackController(mockServiceManager.Object);
			var dto = new FeedbackDelationDto() { Type = FeedbackType.Scholarship };

            //Act
            controller.ModelState.Clear(); // Clear any previous state
            var validationContext = new ValidationContext(dto, null, null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
            }


            var res = await controller.DeleteFeedBack("1", 1, dto);

            //Assert
            var BadRequestResult = Assert.IsType<BadRequestObjectResult>(res);
            Assert.Equal(400, BadRequestResult.StatusCode);
        }

        [Fact]
        public async Task DeleteFeedBack_InavlidPostId_ReturnNotFound()
        {
            //Arrange
            var mockServiceManager = new Mock<IServiceManager>();
            mockServiceManager.Setup(x => x.FeedbackService.DeleteFeedbackAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FeedbackDelationDto>())).Throws(new ScholarshipNotFoundException(1));
            var controller = new FeedBackController(mockServiceManager.Object);
			var dto = new FeedbackDelationDto() { Type = FeedbackType.Scholarship, Id = 1 };

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
                var res = await controller.DeleteFeedBack("1", 1, dto);
            }
            catch (Exception e)
            {
                Assert.IsType<ScholarshipNotFoundException>(e);
                Assert.Equal($"Scholarship with id: {1} doesn't exist in the database.", e.Message);
            }
        }

        [Fact]
        public async Task DeleteFeedBack_InavlidCompanyID_ReturnNotFound()
        {
            //Arrange
            var mockServiceManager = new Mock<IServiceManager>();
            mockServiceManager.Setup(x => x.FeedbackService.DeleteFeedbackAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FeedbackDelationDto>())).Throws(new StudentNotFoundException("ID"));
            var controller = new FeedBackController(mockServiceManager.Object);
            var dto = new FeedbackDelationDto() { Type = FeedbackType.Scholarship,Id=1 };

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
                var res = await controller.DeleteFeedBack("1", 1, dto);
            }
            catch (Exception e)
            {
                Assert.IsType<StudentNotFoundException>(e);
                Assert.Equal($"The student with id: ID doesn't exist in the database.", e.Message);
            }
        }

		[Fact]
		public async Task DeleteFeedBack_InavlidFeedBackID_ReturnNotFound()
		{
			//Arrange
			var mockServiceManager = new Mock<IServiceManager>();
			mockServiceManager.Setup(x => x.FeedbackService.DeleteFeedbackAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<FeedbackDelationDto>())).Throws(new FeedbackNotFoundException(1));
			var controller = new FeedBackController(mockServiceManager.Object);
			var dto = new FeedbackDelationDto() { Type = FeedbackType.Scholarship, Id = 1 };

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
				var res = await controller.DeleteFeedBack("1", 1, dto);
			}
			catch (Exception e)
			{
				Assert.IsType<FeedbackNotFoundException>(e);
				Assert.Equal($"The FeedBack with id: {1} doesn't exist in the database.", e.Message);
			}
		}

	}
}