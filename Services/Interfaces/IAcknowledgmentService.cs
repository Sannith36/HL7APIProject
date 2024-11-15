namespace HL7APIProject.Services.Interfaces
{
    public interface IAcknowledgmentService
    {
        bool WaitForAcknowledgment(string uniqueId);
        void ReceiveAcknowledgment(string uniqueId);
    }
}
