namespace ApexSolutions.Interfaces
{
    public interface ISmsService
    {
        Task SendSmsAsync(string apiToken, string recipient1, string recipient2, string message);
    }
}
