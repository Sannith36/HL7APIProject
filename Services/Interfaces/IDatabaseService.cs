namespace HL7APIProject.Services.Interfaces
{
    public interface IDatabaseService
    {
        string InsertMessage(string jsonMessage);
        string GetMessageById(string uniqueId);
    }
}
