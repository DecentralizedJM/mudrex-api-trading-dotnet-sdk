namespace Mudrex.TradingSDK.Exceptions;

/// <summary>
/// Base exception for Mudrex API errors
/// </summary>
public class MudrexException : Exception
{
    public int Code { get; }
    public int HttpStatus { get; }

    public MudrexException(string message, int code = -1, int httpStatus = -1)
        : base(message)
    {
        Code = code;
        HttpStatus = httpStatus;
    }

    public MudrexException(string message, Exception innerException)
        : base(message, innerException)
    {
        Code = -1;
        HttpStatus = -1;
    }
}

/// <summary>
/// Authentication error (401)
/// </summary>
public class AuthenticationException : MudrexException
{
    public AuthenticationException(string message, int code = -1)
        : base(message, code, 401)
    {
    }
}

/// <summary>
/// Rate limit error (429)
/// </summary>
public class RateLimitException : MudrexException
{
    public RateLimitException(string message, int code = -1)
        : base(message, code, 429)
    {
    }
}

/// <summary>
/// Validation error (400)
/// </summary>
public class ValidationException : MudrexException
{
    public ValidationException(string message, int code = -1)
        : base(message, code, 400)
    {
    }
}

/// <summary>
/// Not found error (404)
/// </summary>
public class NotFoundException : MudrexException
{
    public NotFoundException(string message, int code = -1)
        : base(message, code, 404)
    {
    }
}

/// <summary>
/// Conflict error (409)
/// </summary>
public class ConflictException : MudrexException
{
    public ConflictException(string message, int code = -1)
        : base(message, code, 409)
    {
    }
}

/// <summary>
/// Server error (5xx)
/// </summary>
public class ServerException : MudrexException
{
    public ServerException(string message, int code = -1, int httpStatus = 500)
        : base(message, code, httpStatus)
    {
    }
}

/// <summary>
/// Insufficient balance error
/// </summary>
public class InsufficientBalanceException : MudrexException
{
    public InsufficientBalanceException(string message, int code = -1)
        : base(message, code, 400)
    {
    }
}
