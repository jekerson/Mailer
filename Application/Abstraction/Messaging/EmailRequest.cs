namespace Application.Abstraction.Messaging
{
    public class EmailRequest
    {
        public required string Name { get; set; }
        public required string Subject { get; set; }
        public required string To { get; set; }
        public required string Text { get; set; }
    }
}
