namespace Exercice02.utils
{
    public class Email
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public Email(string to, string subject, string message)
        {
            To = to;
            Subject = subject;
            Message = message;
        }
    }
}
