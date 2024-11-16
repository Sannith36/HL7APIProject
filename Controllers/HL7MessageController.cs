using HL7APIProject.Interfaces;
using HL7APIProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace HL7APIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HL7MessageController : ControllerBase
    {
        private readonly IHL7MessageService _hl7MessageService;
        private readonly IHttpAcknowledgmentSender _acknowledgmentSender;

        public HL7MessageController(IHL7MessageService hl7MessageService, IHttpAcknowledgmentSender acknowledgmentSender)
        {
            _hl7MessageService = hl7MessageService;
            _acknowledgmentSender = acknowledgmentSender; // Ensuring it's stored if needed
        }

        [HttpPost]
        public IActionResult PostHL7Message([FromBody] HL7MessageRequest request)
        {
            if (string.IsNullOrEmpty(request.HL7Message))
                return BadRequest("HL7 message cannot be null or empty");

            var result = _hl7MessageService.ProcessHL7Message(request.HL7Message);

            if (result == null)
                return StatusCode(500, "An error occurred while processing the HL7 message");

            return Ok(result);
        }
    }
}
