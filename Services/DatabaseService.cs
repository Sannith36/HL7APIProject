using HL7APIProject.Interfaces;
using HL7APIProject.Models;
using Microsoft.Data.SqlClient;

namespace HL7APIProject.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly YourDbContext _context;

        public DatabaseService(YourDbContext context)
        {
            _context = context;
        }

        public string InsertMessage(string jsonMessage)
        {
            var message = new HL7Message
            {
                UniqueId = Guid.NewGuid().ToString(),
                JsonData = jsonMessage,
                CreatedAt = DateTime.UtcNow
            };
            _context.HL7Messages.Add(message);
            _context.SaveChanges();
            return message.UniqueId;
        }

        public string GetMessageById(string uniqueId)
        {
            var message = _context.HL7Messages.FirstOrDefault(m => m.UniqueId == uniqueId);
            return message?.JsonData;
        }

        public int InsertMessage(HL7Message message)
        {
            throw new NotImplementedException();
        }

        public HL7Message GetMessageById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
