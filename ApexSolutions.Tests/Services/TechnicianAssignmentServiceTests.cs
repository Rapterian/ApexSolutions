using Xunit;
using Moq;
using ApexSolutions.Services;
using ApexSolutions.Models;
using ApexSolutions.Interfaces;
using System.Threading.Tasks;

namespace ApexSolutions.Tests.Services
{
    public class TechnicianAssignmentServiceTests
    {
        private readonly TechnicianAssignmentService _service;
        private readonly Mock<ITechnicianRepository> _technicianRepoMock;
        private readonly Mock<IServiceRequestRepository> _serviceRequestRepoMock;
        private readonly Mock<SmsService> _smsServiceMock;

        public TechnicianAssignmentServiceTests()
        {
            _technicianRepoMock = new Mock<ITechnicianRepository>();
            _serviceRequestRepoMock = new Mock<IServiceRequestRepository>();
            _smsServiceMock = new Mock<SmsService>();
            _service = new TechnicianAssignmentService(
                _technicianRepoMock.Object,
                _serviceRequestRepoMock.Object,
                _smsServiceMock.Object);
        }

        [Fact]
        public async Task AssignTechnicianAsync_ValidRequest_AssignsTechnician()
        {
            // Arrange
            var serviceRequestId = 1;
            var technicianId = 1;
            var serviceRequest = new ServiceRequest { Id = serviceRequestId, Status = "Pending" };
            var technician = new Technician { Id = technicianId, IsAvailable = true };

            _serviceRequestRepoMock.Setup(repo => repo.GetByIdAsync(serviceRequestId))
                .ReturnsAsync(serviceRequest);
            _technicianRepoMock.Setup(repo => repo.GetByIdAsync(technicianId))
                .ReturnsAsync(technician);

            // Act
            var result = await _service.AssignTechnicianAsync(serviceRequestId, technicianId);

            // Assert
            Assert.True(result);
            Assert.Equal(technicianId, serviceRequest.TechnicianID);
            Assert.Equal("Assigned", serviceRequest.Status);
            _serviceRequestRepoMock.Verify(repo => repo.UpdateAsync(serviceRequest), Times.Once);
            _smsServiceMock.Verify(sms => sms.SendSmsAsync(It.IsAny<string>(), technician.ContactNumber, "", It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task AssignTechnicianAsync_InvalidServiceRequest_ReturnsFalse()
        {
            // Arrange
            var serviceRequestId = 1;
            var technicianId = 1;

            _serviceRequestRepoMock.Setup(repo => repo.GetByIdAsync(serviceRequestId))
                .ReturnsAsync((ServiceRequest)null); // No service request found

            // Act
            var result = await _service.AssignTechnicianAsync(serviceRequestId, technicianId);

            // Assert
            Assert.False(result);
        }

        // Additional test cases for other scenarios...
    }
}