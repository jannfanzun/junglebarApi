using junglebarApi.Controllers;
using junglebarApi.Models;
using junglebarApi.Service;
using junglebarApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

public class UserControllerTests
{
    [Fact]
    public void GetAllUsers_ReturnsOkResult()
    {
        // Arrange
        var userServiceMock = new Mock<UserService>();
        userServiceMock.Setup(service => service.GetAllUsers()).Returns(new List<User>());

        var controller = new UserController(userServiceMock.Object);

        // Act
        var result = controller.GetAllUsers();

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetUserById_ValidId_ReturnsOkResult()
    {
        // Arrange
        var userId = 1;
        var user = new User { Id = userId, Name = "TestUser", Email = "test@example.com", Password = "password" };

        var userServiceMock = new Mock<UserService>();
        userServiceMock.Setup(service => service.GetUserById(userId)).Returns(user);

        var controller = new UserController(userServiceMock.Object);

        // Act
        var result = controller.GetUserById(userId);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetUserById_InvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var userId = 999; // Assume this ID does not exist
        var userServiceMock = new Mock<UserService>();
        userServiceMock.Setup(service => service.GetUserById(userId)).Returns((User)null);

        var controller = new UserController(userServiceMock.Object);

        // Act
        var result = controller.GetUserById(userId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}