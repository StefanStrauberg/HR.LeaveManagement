using System.Threading.Tasks;
using HR.LeaveManagement.Application.Models.Email;

namespace HR.LeaveManagement.Application.Contracts.Email
{
    public interface IEmailSender
    {
        Task SendEmail(EmailMessage email);
    }
}