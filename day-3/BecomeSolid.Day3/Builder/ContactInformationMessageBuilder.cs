using System;
using System.Net.Mail;
using System.Text;
using BecomeSolid.Day3.Model;

namespace BecomeSolid.Day3.Builder
{
    public class ContactInformationMessageBuilder : IMailMessageBuilder<ContactInformation>
    {
        private MailMessage mailMessage;
        private StringBuilder body = new StringBuilder();

        public ContactInformationMessageBuilder()
        {
            mailMessage = new MailMessage();
        }

        public IMailMessageBuilder<ContactInformation> WithTo(string to)
        {
            mailMessage.To.Add(to);
            return this;
        }

        public IMailMessageBuilder<ContactInformation> WithSubject(string subject)
        {
            mailMessage.Subject = subject;
            return this;
        }

        public IMailMessageBuilder<ContactInformation> WithFrom(string from)
        {
            mailMessage.From = new MailAddress(from);
            return this;
        }

        public MailMessage BuildMessage()
        {
            mailMessage.Body = body.ToString();
            return mailMessage;
        }

        public IMailMessageBuilder<ContactInformation> WithEntity(ContactInformation contact)
        {
            AddBodyLine("Nombre: {0}", contact.FirstName);
            AddBodyLine("Apellido: {0}", contact.LastName);
            return this;
        }

        private void AddBodyLine(String line, params object[] args)
        {
            body.AppendLine(String.Format(line, args));
        }

    }
}