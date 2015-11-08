using System;
using System.Net.Mail;
using System.Text;
using BecomeSolid.Day3.Model;

namespace BecomeSolid.Day3.Builder
{
    public class ContactInformationMailMessageBuilder : IMailMessageBuilder<ContactInformation>
    {
        private readonly MailMessage mailMessage;
        private readonly StringBuilder body = new StringBuilder();

        public ContactInformationMailMessageBuilder()
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

        public IMailMessageBuilder<ContactInformation> WithEntity(ContactInformation contactInformation)
        {
            ParseContactInformation(contactInformation);
            if (contactInformation is ContactInformationSubsidiary)
            {
                ContactInformationSubsidiary contact = contactInformation as ContactInformationSubsidiary;
                ParseContactInformationSubsidiary(contact);
            }
            else if (contactInformation is ContactInformationAuction)
            {
                ContactInformationAuction contact = contactInformation as ContactInformationAuction;
                ParseContactInformationAuction(contact);
            }
            return this;
        }

        private void ParseContactInformation(ContactInformation contact)
        {
            AddBodyLine("Nombre: {0}", contact.FirstName);
            AddBodyLine("Apellido: {0}", contact.LastName);
        }

        private void ParseContactInformationSubsidiary(ContactInformationSubsidiary contact)
        {
            AddBodyLine("Sucursal: {0}", contact.Subsidiary);
        }

        private void ParseContactInformationAuction(ContactInformationAuction contact)
        {
            AddBodyLine("Autor: {0}", contact.Author);
            AddBodyLine("Dimensiones: {0}", contact.Dimensions);
        }

        private void AddBodyLine(String line, params object[] args)
        {
            body.AppendLine(String.Format(line, args));
        }

    }
}
