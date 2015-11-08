using System.Net.Mail;

namespace BecomeSolid.Day3.Builder
{
    public interface IMailMessageBuilder<T>
    {
        IMailMessageBuilder<T> WithTo(string to);
        IMailMessageBuilder<T> WithSubject(string subject);
        IMailMessageBuilder<T> WithFrom(string from);
        MailMessage BuildMessage();
        IMailMessageBuilder<T> WithEntity(T entity);
    }
}
