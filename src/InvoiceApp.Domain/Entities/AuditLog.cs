using InvoiceApp.Domain.Common;
using InvoiceApp.Domain.Enums;

namespace InvoiceApp.Domain.Entities;

public class AuditLog : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public ActionType Action { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string IPAddress { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
