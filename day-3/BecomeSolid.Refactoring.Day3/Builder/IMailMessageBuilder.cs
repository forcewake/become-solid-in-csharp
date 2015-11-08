using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Refactoring.Day3.Builder
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
