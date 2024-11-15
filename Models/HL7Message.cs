
using System;

namespace HL7APIProject.Models
{
    public class HL7Message
    {
        public int Id { get; set; }
        public string UniqueId { get; set; } = string.Empty; // Set default value
        public string JsonData { get; set; } = string.Empty; // Set default value
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
