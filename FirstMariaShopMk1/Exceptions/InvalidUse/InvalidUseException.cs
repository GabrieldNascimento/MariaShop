using MariaShop.Api.Exceptions.Domain;

namespace MariaShop.Api.Exceptions.InvalidUse
{
    public class InvalidUseException : DomainException
    {
        public InvalidUseException(string message, string code) : base(message, code) {

        }
    }
}
