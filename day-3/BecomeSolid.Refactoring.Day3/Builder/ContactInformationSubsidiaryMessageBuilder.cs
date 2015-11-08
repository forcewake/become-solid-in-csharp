using System;
using System.Net.Mail;
using System.Text;
using BecomeSolid.Refactoring.Day3.Model;

namespace BecomeSolid.Refactoring.Day3.Builder
{
    class ContactInformationSubsidiaryMessageBuilder : IMailMessageBuilder<ContactInformationSubsidiary>
    {
        private MailMessage mailMessage;
        private StringBuilder body = new StringBuilder();

        public ContactInformationSubsidiaryMessageBuilder()
        {
            mailMessage = new MailMessage();
        }

        public IMailMessageBuilder<ContactInformationSubsidiary> WithTo(string to)
        {
            mailMessage.To.Add(to);
            return this;
        }

        public IMailMessageBuilder<ContactInformationSubsidiary> WithSubject(string subject)
        {
            mailMessage.Subject = subject;
            return this;
        }

        public IMailMessageBuilder<ContactInformationSubsidiary> WithFrom(string from)
        {
            mailMessage.From = new MailAddress(from);
            return this;
        }

        public MailMessage BuildMessage()
        {
            mailMessage.Body = body.ToString();
            return mailMessage;
        }

        public IMailMessageBuilder<ContactInformationSubsidiary> WithEntity(ContactInformationSubsidiary contact)
        {
            AddBodyLine("Nombre: {0}", contact.FirstName);
            AddBodyLine("Apellido: {0}", contact.LastName);
            AddBodyLine("Sucursal: {0}", contact.Subsidiary);
            return this;
        }

        private void AddBodyLine(String line, params object[] args)
        {
            body.AppendLine(String.Format(line, args));
        }
    }
}