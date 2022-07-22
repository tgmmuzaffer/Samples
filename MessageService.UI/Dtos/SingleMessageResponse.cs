namespace MessageService.UI.Dtos
{
    public class SingleMessageResponse
    {
        public SingleDataDto Data { get; set; } = new SingleDataDto();
        public int TotalCount { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        public int StatusCode { get; set; }
        public string Messages { get; set; }
        public string ModelStateError { get; set; }
    }
}
