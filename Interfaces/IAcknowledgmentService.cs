namespace HL7APIProject.Interfaces
{
    public interface IAcknowledgmentService
    {
        bool WaitForAcknowledgment(string uniqueId);
        void ReceiveAcknowledgment(string uniqueId);
    }
}
