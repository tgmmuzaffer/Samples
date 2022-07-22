﻿using System;

namespace MessageService.UI.Dtos
{
    public class SingleDataDto
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public int RelatedId { get; set; }
        public bool IsDeleted { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public string IpAddr { get; set; }
        public int ReplayCount { get; set; }
        public string MessageContent { get; set; }
    }
}
