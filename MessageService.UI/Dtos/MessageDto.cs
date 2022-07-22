using System.Collections.Generic;

namespace MessageService.UI.Dtos
{
    public class MessageDto
    {
        public List<InMessageDto> InMessageDtos { get; set; } = new List<InMessageDto>();
        public OutMessageDto OutMessageDto { get; set; } = new OutMessageDto();
    }
}
