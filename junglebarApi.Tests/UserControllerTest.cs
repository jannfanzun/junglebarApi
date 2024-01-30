namespace junglebarApi.Tests;

using System.Collections.Generic;
using junglebarApi.Controllers;
using junglebarApi.Models;
using junglebarApi.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class UserControllerTests
{
    private Mock<UserService> _mockUserService;
    private UserController _userController;

    public UserControllerTests()
    {
        _mockUserService = new Mock<UserService>();
        _userController = new UserController(_mockUserService.Object);
    }

    [Fact]
    public void GetAllUsers_ShouldReturnOkResultWithUsers()
    {
        // Arrange
        var users = new List<User> { new User { Id = 1, Name = "User1" }, new User { Id = 2, Name = "User2" } };
        _mockUserService.Setup(service => service.GetAllUsers()).Returns(users);

        // Act
        var result = _userController.GetAllUsers() as ActionResult<IEnumerable<User>>;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(users, okResult.Value);
    }

    [Fact]
    public void GetUserById_ExistingId_ShouldReturnOkResultWithUser()
    {
        // Arrange
        var userId = 1;
        var user = new User { Id = userId, Name = "User1" };
        _mockUserService.Setup(service => service.GetUserById(userId)).Returns(user);

        // Act
        var result = _userController.GetUserById(userId) as ActionResult<User>;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(user, okResult.Value);
    }

    [Fact]
    public void GetUserById_NonExistingId_ShouldReturnNotFoundResult()
    {
        // Arrange
        var userId = 1;
        _mockUserService.Setup(service => service.GetUserById(userId)).Returns((User)null);

        // Act
        var result = _userController.GetUserById(userId) as ActionResult<User>;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result.Result);
        var notFoundResult = result.Result as NotFoundResult;
        Assert.NotNull(notFoundResult);
        Assert.Equal(404, notFoundResult.StatusCode);
    }
}