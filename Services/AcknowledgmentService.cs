using HL7APIProject.Interfaces;

namespace HL7APIProject.Services
{
    public class AcknowledgmentService : IAcknowledgmentService
    {
        private readonly Dictionary<string, bool> _acknowledgments = new();

        public bool WaitForAcknowledgment(string uniqueId)
        {
            return _acknowledgments.ContainsKey(uniqueId) && _acknowledgments[uniqueId];
        }

        public void ReceiveAcknowledgment(string uniqueId)
        {
            _acknowledgments[uniqueId] = true;
        }
    }
}
