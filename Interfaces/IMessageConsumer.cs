namespace HL7APIProject.Interfaces
{
    public interface IMessageConsumer
    {
        void ConsumeMessage();
        void AcknowledgeMessage(string acknowledgment);
    }
}
