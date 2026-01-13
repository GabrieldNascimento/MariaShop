using MariaShop.Api.Exceptions.Domain;

namespace MariaShop.Api.Exceptions.InvalidData
{
    public class InvalidDataException : DomainException
    {
        public InvalidDataException(string message, string code) : base(message, code) {

        }
    }
}
