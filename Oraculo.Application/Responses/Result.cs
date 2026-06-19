namespace Oraculo.Application.Responses;

public class Result<T>
{
    private Result(CodeResponseType code, T? value, string errorMessage)
    {
        Code = code;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public CodeResponseType Code { get; }
    public T? Value { get; }
    public string ErrorMessage { get; }

    public static Result<T> Success(CodeResponseType code, T value)
    {
        return new Result<T>(code, value, string.Empty);
    }

    public static Result<T> Failure(CodeResponseType code, string errorMessage)
    {
        return new Result<T>(code, default, errorMessage);
    }
}