namespace HR.LeaveManagement.Application.Models.Email
{
    public class EmailSettings
    {
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public string EmailPassword { get; set; }
        public string SmtpServerAddress { get; set; }
        public int ServerPort { get; set; }
    }
}