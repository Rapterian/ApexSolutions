using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ApexSolutions.DTOs;
using ApexSolutions.Interfaces;
using ApexSolutions.Models;
using ApexSolutions.Services;

namespace ApexSolutions.Tests.Services
{
    public class TechnicianServiceTests
    {
        private readonly Mock<ITechnicianRepository> _mockRepository;
        private readonly TechnicianService _technicianService;

        public TechnicianServiceTests()
        {
            _mockRepository = new Mock<ITechnicianRepository>();
            _technicianService = new TechnicianService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateTechnician_ShouldReturnTechnicianDTO_WhenValidInput()
        {
            // Arrange
            var technicianDto = new TechnicianDTO
            {
                Name = "Jane Doe",
                Skills = "Electrical, Plumbing",
                AvailabilityStatus = true,
                ContactNumber = "1234567890"
            };

            var technician = new Technician
            {
                TechnicianID = 1,
                Name = technicianDto.Name,
                Skills = technicianDto.Skills,
                AvailabilityStatus = technicianDto.AvailabilityStatus,
                ContactNumber = technicianDto.ContactNumber
            };

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Technician>()))
                .ReturnsAsync(technician);

            // Act
            var result = await _technicianService.CreateTechnicianAsync(technicianDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(technicianDto.Name, result.Name);
            Assert.Equal(technicianDto.Skills, result.Skills);
            Assert.Equal(technicianDto.AvailabilityStatus, result.AvailabilityStatus);
            Assert.Equal(technicianDto.ContactNumber, result.ContactNumber);
            Assert.True(result.TechnicianID > 0);
        }

        [Fact]
        public async Task GetTechnicianById_ShouldReturnTechnicianDTO_WhenTechnicianExists()
        {
            // Arrange
            var technician = new Technician
            {
                TechnicianID = 1,
                Name = "Jane Doe",
                Skills = "Electrical, Plumbing",
                AvailabilityStatus = true,
                ContactNumber = "1234567890"
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(technician);

            // Act
            var result = await _technicianService.GetTechnicianByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(technician.TechnicianID, result.TechnicianID);
            Assert.Equal(technician.Name, result.Name);
            Assert.Equal(technician.Skills, result.Skills);
        }

        [Fact]
        public async Task DeleteTechnician_ShouldReturnFalse_WhenTechnicianDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync((Technician)null);

            // Act
            var result = await _technicianService.DeleteTechnicianAsync(1);

            // Assert
            Assert.False(result);
        }
    }
}