namespace FirstMariaShopMk1.Exceptions.Domain
{
    public class DomainException : Exception
    {
        public string Code { get; }
        public DomainException(string message, string code) : base(message) {
            Code = code;
        }
    }
}
