namespace GETTER.Domain.Common;

public class DomainException(Error error) : Exception(error.Message)
{
    public Error Error { get; } = error;

    //public DomainException(Error error) : base(error.Message)
    //{
    //    Error = error;

    //}
}
