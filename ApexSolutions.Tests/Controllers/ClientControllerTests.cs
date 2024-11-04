using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ApexSolutions.Controllers;
using ApexSolutions.DTOs;
using ApexSolutions.Models;
using ApexSolutions.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApexSolutions.Interfaces;

public class ClientControllerTests
{
    private readonly Mock<IClientRepository> _clientRepositoryMock; // Use the specific repository interface
    private readonly ClientService _clientService;
    private readonly ClientController _clientController;

    public ClientControllerTests()
    {
        _clientRepositoryMock = new Mock<IClientRepository>(); // Mock the specific client repository
        _clientService = new ClientService(_clientRepositoryMock.Object); // Inject the mock repository into the service
        _clientController = new ClientController(_clientService);
    }

    [Fact]
    public async Task GetClientById_ShouldReturnOkResult_WithClient_WhenClientExists()
    {
        // Arrange
        var clientId = 1;
        var client = new Client { ClientID = clientId, Name = "Client 1", Email = "client1@example.com" };

        // Mock the repository to return a Client object
        _clientRepositoryMock.Setup(repo => repo.GetByIdAsync(clientId))
            .ReturnsAsync(client); // Return the Client domain model

        // Act
        var result = await _clientController.GetClientById(clientId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnClientDto = Assert.IsType<ClientDTO>(okResult.Value); // Expecting ClientDTO

        // Assuming you have a mapping method to convert Client to ClientDTO
        var expectedClientDto = new ClientDTO { ClientID = clientId, Name = "Client 1", Email = "client1@example.com" };
        Assert.Equal(expectedClientDto.ClientID, returnClientDto.ClientID);
    }

    [Fact]
    public async Task GetClientById_ShouldReturnNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        var clientId = 999; // Non-existent client ID

        _clientRepositoryMock.Setup(repo => repo.GetByIdAsync(clientId))
            .ReturnsAsync((Client)null); // Simulate client not found

        // Act
        var result = await _clientController.GetClientById(clientId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}