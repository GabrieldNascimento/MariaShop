using FirstMariaShopMk1.Exceptions.Domain;

namespace FirstMariaShopMk1.Exceptions.InvalidData
{
    public class InvalidDataException : DomainException
    {
        public InvalidDataException(string message, string code) : base(message, code)
        {
            
        }
    }
}
