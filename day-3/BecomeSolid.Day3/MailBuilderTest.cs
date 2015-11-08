using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BecomeSolid.Day3.Builder;
using BecomeSolid.Day3.Model;
using Xunit;

namespace BecomeSolid.Day3
{
    public class MailBuilderTest
    {
        [Fact]
        public void TestContactInformation()
        {
            // arrange
            ContactInformation contactInformation = new ContactInformation()
            {
                FirstName = "Homero",
                LastName = "Simpson"
            };

            IMailMessageBuilder<ContactInformation> mailBuilder = new ContactInformationMessageBuilder();

            // act
            MailMessage message = mailBuilder
                                    .WithFrom("cliente@gmail.com")
                                    .WithTo("empresa@gmail.com")
                                    .WithSubject("Hola")
                                    .WithEntity(contactInformation).BuildMessage();

            // assert
            Assert.Equal("cliente@gmail.com", message.From.Address);
            Assert.Contains(message.To, address => address.Address == "empresa@gmail.com");
            Assert.Equal("Hola", message.Subject);
            Assert.Contains("Nombre: Homero", message.Body);
            Assert.Contains("Apellido: Simpson", message.Body);
        }

        [Fact]
        public void TestContactInformationSubsidiary()
        {
            // arrange
            ContactInformationSubsidiary contactInformation = new ContactInformationSubsidiary()
            {
                FirstName = "Homero",
                LastName = "Simpson",
                Subsidiary = "Retiro"
            };

            IMailMessageBuilder<ContactInformationSubsidiary> mailBuilder = new ContactInformationSubsidiaryMessageBuilder();

            // act
            MailMessage message = mailBuilder
                                    .WithFrom("cliente@gmail.com")
                                    .WithTo("empresa@gmail.com")
                                    .WithSubject("Hola")
                                    .WithEntity(contactInformation).BuildMessage();

            // assert
            Assert.Equal("cliente@gmail.com", message.From.Address);
            Assert.Contains(message.To, x => x.Address == "empresa@gmail.com");
            Assert.Equal("Hola", message.Subject);
            Assert.Contains("Nombre: Homero", message.Body);
            Assert.Contains("Apellido: Simpson", message.Body);
            Assert.Contains("Sucursal: Retiro", message.Body);
        }

        [Fact]
        public void TestContactInformationAuction()
        {
            // arrange
            ContactInformationAuction contactInformation = new ContactInformationAuction()
            {
                FirstName = "Homero",
                LastName = "Simpson",
                Author = "Picasso",
                Dimensions = "3x3"
            };

            IMailMessageBuilder<ContactInformationAuction> mailBuilder = new ContactInformationAuctionMessageBuilder();

            // act
            MailMessage message = mailBuilder
                                    .WithFrom("cliente@gmail.com")
                                    .WithTo("empresa@gmail.com")
                                    .WithSubject("Hola")
                                    .WithEntity(contactInformation).BuildMessage();

            // assert
            Assert.Equal("cliente@gmail.com", message.From.Address);
            Assert.Contains(message.To, x => x.Address == "empresa@gmail.com");
            Assert.Equal("Hola", message.Subject);
            Assert.Contains("Nombre: Homero", message.Body);
            Assert.Contains("Apellido: Simpson", message.Body);
            Assert.Contains("Autor: Picasso", message.Body);
            Assert.Contains("Dimensiones: 3x3", message.Body);
        }

    }
}
