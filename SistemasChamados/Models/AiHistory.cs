using System;

namespace SistemaChamados.Api.Models
{
    public class AiHistory
    {
        public Guid AiHistoryId { get; set; }
        public Guid? TicketId { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string ModelName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}