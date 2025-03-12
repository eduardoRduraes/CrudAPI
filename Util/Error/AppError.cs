namespace CrudAPI.Util;

public class AppError
{
    public int StatusCode { get;  }
    public string Message { get;  }

    public AppError(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}