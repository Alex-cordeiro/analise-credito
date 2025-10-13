namespace AnaliseCredito.Application.Response;

public class ResponseResult
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = null!;
    public List<string> Errors { get; private set; } = null!;

    public bool IsValid()
    {
        return Success;
    }
    public ResponseResult Add(bool operationResult)
    {
        return new ResponseResult
        {
            Success = operationResult,
            Message = string.Empty,
            Errors = new List<string> { }
        };
    }

    public static ResponseResult RequestError(string message)
    {
        return new ResponseResult
        {
            Success = false,
            Message = message,
            Errors = new List<string> { message }
        };
    }

    public static ResponseResult<T> RequestError<T>(string message, List<string> errors)
    {
        return new ResponseResult<T>
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }
    public static ResponseResult Fail(string message) => new ResponseResult { Success = false, Message = message };
    public static ResponseResult<T> Fail<T>(string message, List<string> errors) => new ResponseResult<T> { Success = false, Message = message, Errors = errors };
    public static ResponseResult<T> Fail<T>(string message) => new ResponseResult<T> { Success = false, Message = message};
    public static ResponseResult Ok(string message) => new ResponseResult { Success = true, Message = message };
    public static ResponseResult<T> Ok<T>(T data) => new ResponseResult<T> { Success = true, Data = data };
    public static ResponseResult<T> Ok<T>(string message) => new ResponseResult<T> { Success = true, Message = message};
}

public class ResponseResult<T> : ResponseResult
{
    public T? Data { get; set; }
}