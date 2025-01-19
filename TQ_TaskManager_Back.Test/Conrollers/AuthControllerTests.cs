using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TQ_TaskManager_Back.Controllers;
using TQ_TaskManager_Back.Dtos;
using TQ_TaskManager_Back.Models;
using TQ_TaskManager_Back.Services;

namespace TQ_TaskManager_Back.Test.Conrollers;

public class AuthControllerTests
{

    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockAuthService = new Mock<IAuthService>();
        _controller = new AuthController(_mockUserService.Object, _mockAuthService.Object);
    }

    [Fact]
    public async Task Login_WhenUserIsAuthenticated_ReturnsOkResultWithToken()
    {
        // Arrange
        var loginRequest = new LoginRequestDto
        {
            Email = "test@example.com",
            Password = "password123"
        };

        var usuario = new Usuario
        {
            Id = 1,
            Correo = "test@example.com",
            Rolid = 1
        };

        // Configura el mock de IUserService para devolver `true` cuando se intente autenticar.
        _mockUserService.Setup(x => x.AuthenticateAsync(loginRequest)).ReturnsAsync(true);
        _mockUserService.Setup(x => x.GetUsuarioByEmail(loginRequest.Email)).ReturnsAsync(usuario);

        // Configura el mock de IAuthService para generar un token.
        var token = "mocked-jwt-token";
        _mockAuthService.Setup(x => x.GenerateJwtToken(usuario.Correo, usuario.Id, usuario.Rolid)).Returns(token);

        // Act
        var result = await _controller.Login(loginRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        
        _mockUserService.Verify(x => x.AuthenticateAsync(loginRequest), Times.Once);
        _mockUserService.Verify(x => x.GetUsuarioByEmail(loginRequest.Email), Times.Once);
        _mockAuthService.Verify(x => x.GenerateJwtToken(usuario.Correo, usuario.Id, usuario.Rolid), Times.Once);
    }

}
