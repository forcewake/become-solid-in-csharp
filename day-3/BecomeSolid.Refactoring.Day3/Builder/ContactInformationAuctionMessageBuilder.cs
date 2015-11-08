using System;
using System.Net.Mail;
using System.Text;
using BecomeSolid.Refactoring.Day3.Model;

namespace BecomeSolid.Refactoring.Day3.Builder
{
    class ContactInformationAuctionMessageBuilder : IMailMessageBuilder<ContactInformationAuction>
    {
        private MailMessage mailMessage;
        private StringBuilder body = new StringBuilder();

        public ContactInformationAuctionMessageBuilder()
        {
            mailMessage = new MailMessage();
        }

        public IMailMessageBuilder<ContactInformationAuction> WithTo(string to)
        {
            mailMessage.To.Add(to);
            return this;
        }

        public IMailMessageBuilder<ContactInformationAuction> WithSubject(string subject)
        {
            mailMessage.Subject = subject;
            return this;
        }

        public IMailMessageBuilder<ContactInformationAuction> WithFrom(string from)
        {
            mailMessage.From = new MailAddress(from);
            return this;
        }

        public MailMessage BuildMessage()
        {
            mailMessage.Body = body.ToString();
            return mailMessage;
        }

        public IMailMessageBuilder<ContactInformationAuction> WithEntity(ContactInformationAuction contact)
        {
            AddBodyLine("Nombre: {0}", contact.FirstName);
            AddBodyLine("Apellido: {0}", contact.LastName);
            AddBodyLine("Autor: {0}", contact.Author);
            AddBodyLine("Dimensiones: {0}", contact.Dimensions);
            return this;
        }

        private void AddBodyLine(String line, params object[] args)
        {
            body.AppendLine(String.Format(line, args));
        }
    }
}