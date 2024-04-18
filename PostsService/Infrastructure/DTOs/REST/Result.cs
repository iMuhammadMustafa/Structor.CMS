namespace PostsService.Infrastructure.DTOs.REST;


//TODO: Implement Results Pattern
public class Result
{
    public bool IsSuccess { get; set; }
    public bool IsError => !IsSuccess;
    public string? Error { get; set; }


    protected Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true, null);
    public static Result Failure(string message) => new Result(false, message);
}
