using System;

namespace SistemaChamados.Api.Models
{
    public class TicketComment
    {
        public Guid TicketCommentId { get; set; }
        public Guid TicketId { get; set; }
        public string Text { get; set; }
        public Guid AuthorUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual Ticket Ticket { get; set; }
    }
}