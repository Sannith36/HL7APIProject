using HL7APIProject.Models;

namespace HL7APIProject.Interfaces
{
    public interface IDatabaseService
    {
        int InsertMessage(HL7Message message);
        HL7Message GetMessageById(int id);
        string InsertMessage(string jsonMessage);
    }

}
