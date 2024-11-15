

namespace HL7APIProject.Interfaces
{
    public interface IHttpAcknowledgmentSender
    {
        bool SendAcknowledgment(string acknowledgmentUrl, string uniqueId);
    }

}
