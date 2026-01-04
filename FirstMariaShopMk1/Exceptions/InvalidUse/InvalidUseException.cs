using FirstMariaShopMk1.Exceptions.Domain;

namespace FirstMariaShopMk1.Exceptions.InvalidUse
{
    public class InvalidUseException : DomainException
    {
        public InvalidUseException(string message, string code) : base(message, code)
        {
            
        }
    }
}
