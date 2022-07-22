using System;

namespace MessageService.UI.Dtos
{
    public class OutMessageDto
    {
        public DateTime DateAdded { get; set; }
        public int RelatedId { get; set; }
        public int? FromId { get; set; }
        public int? ToId { get; set; }
        public string IpAddr { get; set; }
        public string MessageContent { get; set; }
        public int ReplyCount { get; set; }
        public bool IsDeleted { get; set; }
    }
}
