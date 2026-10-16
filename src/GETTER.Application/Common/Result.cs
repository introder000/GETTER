using GETTER.Domain.Common;

namespace GETTER.Application.Common;

public class Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }
    public bool IsFail => !IsSuccess;

    protected Result(bool isSuccess, Error? error)
    {
        if (isSuccess == (error is not null))
            throw new InvalidOperationException("Error nie moze wystepowac przy stanie success");

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
    {
        return new Result(true, null);
    }
    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }
}


public class Result<T> : Result
{
    private readonly T? _value;

    public T Value
    {
        get
        {
            if (IsFail)
                throw new InvalidOperationException("Cannot read Value of a failed result.");

            return _value!;
        }
    }
    private Result(bool isSuccess, T? value, Error? error) : base(isSuccess, error)
    {
        _value = value;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, null);
    }

    public static new Result<T> Failure(Error error)
    {
        return new Result<T>(false, default, error);
    }
}
