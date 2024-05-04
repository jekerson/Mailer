namespace Domain.Abstraction
{
    public sealed record Error
    {
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            Type = errorType;
        }

        public string Code { get; }

        public string Description { get; }

        public ErrorType Type { get; }

        public static Error Failure(string code, string description) =>
            new Error(code, description, ErrorType.Failure);

        public static Error Validation(string code, string description) =>
            new Error(code, description, ErrorType.Validation);

        public static Error NotFound(string code, string description) =>
            new Error(code, description, ErrorType.NotFound);

        public static Error Conflict(string code, string description) =>
            new Error(code, description, ErrorType.Conflict);

    }

    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3
    }

    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
    }

    public class Result<T> : Result
    {
        protected internal Result(bool isSuccess, Error error, T? value = default)
            : base(isSuccess, error)
        {
            Value = value;
        }

        public T? Value { get; }
        public static Result<T> Success(T value) => new(true, Error.None, value);
        public new static Result<T> Failure(Error error) => new(false, error);
    }

    public class AuthenticationResult : Result<(string AccessToken, string RefreshToken)>
    {
        protected AuthenticationResult(bool isSuccess, Error error, (string Token, string RefreshToken) value)
            : base(isSuccess, error, value)
        {
        }

        public string Token => Value.AccessToken;
        public string RefreshToken => Value.RefreshToken;

        public static AuthenticationResult Success(string token, string refreshToken)
            => new(true, Error.None, (token, refreshToken));

        public new static AuthenticationResult Failure(Error error) => new(false, error, default);
    }
}
