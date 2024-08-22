using WebApi.Attributes;

namespace WebApi.Contracts.Notification;

public class SendMessageDto
{
     [FormField(Name = "mobile_phone")]
     public string PhoneNumber { get; set; } = default!;

     [FormField(Name = "message")]
     public string Message { get; set; } = default!;

     [FormField(Name = "from")]
     public string From { get; set; } = default!;

     [FormField(Name = "callback_url")]
     public string? CallbackUrl { get; set; }
}