namespace ShopDH.Models;

public class ResponseResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; }

    public ResponseResult()
    {

    }

    public ResponseResult(bool success, string message, object? data)
    {
        Success = success;
        Message = message;
        Data = data;
    }
}