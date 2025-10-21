using System;
using System.Collections.Generic;

namespace SistemaChamados.Api.Models
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Open";
        public string Priority { get; set; } = "Medium";
        public Guid CreatedByUserId { get; set; }
        public Guid? AssignedToUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public virtual List<TicketComment> Comments { get; set; } = new();
    }
}
