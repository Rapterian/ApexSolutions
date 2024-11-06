using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApexSolutions.Models;
using ApexSolutions.Data;
using ApexSolutions.Repositories;
using ApexSolutions.Services;
using System.Data.SqlClient;
using System.Data;

namespace ApexSolutions
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path to the current directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Load appsettings.json
                .Build();

            // Retrieve the connection string
            string connectionString = configuration.GetConnectionString("ApexSolutionsDB");

            // Create an instance of DatabaseContext
            var dbContext = new DatabaseContext(connectionString);

            // Create a client
            var client = new Client("John D", "joh@example.com", "0123456789", "123 Main St");
            client.ClientID=await dbContext.InsertClientAsync(client);
            Console.WriteLine("Client added successfully.");
            

            // Create a service request
            var serviceRequest = new ServiceRequest
            {
                ClientID = client.ClientID,
                Description = "Air conditioner malfunction",
                ServiceType = "HVAC",
                RequestDate = DateTime.UtcNow,
                Status = "Open",
                Priority = "Medium",
                Location = "123 Main St"
            };
            serviceRequest.ServiceRequestID= await dbContext.InsertServiceRequestAsync(serviceRequest);
            Console.WriteLine("Service request added successfully.");

            // Create a technician
            var technician = new Technician(501, "Jane Smith", "HVAC Specialist", "012 345 6789");
            technician.TechnicianID= await dbContext.InsertTechnicianAsync(technician);
            Console.WriteLine("Technician added successfully.");

            // Assign the technician to the service request
            serviceRequest.AssignTechnician(technician.TechnicianID);
            serviceRequest.ServiceRequestID = await dbContext.InsertServiceRequestAsync(serviceRequest); // Update the service request with the assigned technician
            Console.WriteLine("Technician assigned to service request.");

            // Capture client feedback
            var feedback = new Feedback( client.ClientID, serviceRequest.ServiceRequestID, 5, "Great service!");
            feedback.FeedbackID= await dbContext.InsertFeedbackAsync(feedback); 
            Console.WriteLine("Feedback recorded successfully.");

            Console.WriteLine("All records added successfully!");
        }

    }
}