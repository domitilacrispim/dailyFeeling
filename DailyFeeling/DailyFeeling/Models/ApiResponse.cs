namespace DailyFeeling.Models;

public class ApiResponse<T>
{
    public int StatusCode { get; set; } // Código HTTP da resposta
    public bool Success { get; set; } // Indica se a requisição foi bem-sucedida
    public string? Message { get; set; } // Mensagem de resposta
    public T? Data { get; set; } // Dados retornados (pode ser null)

    private ApiResponse(int statusCode, bool success, string? message, T? data = default)
    {
        StatusCode = statusCode;
        Success = success;
        Message = message;
        Data = data;
    }

    public static ApiResponse<T?> SuccessResponse(T? data, string message = "Operação bem-sucedida.")
    {
        return new ApiResponse<T?>(200, true, message, data);
    }

    public static ApiResponse<T?> ErrorResponse(int statusCode, string? message = null)
    {
        return new ApiResponse<T?>(statusCode, false, message);
    }
    
    public static ApiResponse<T?> NotFound(string? message = null)
    {
        return new ApiResponse<T?>(404, false, message);
    }
    
    public static ApiResponse<T?> Unauthorized(string? message = null)
    {
        return new ApiResponse<T?>(403, false, message);
    }
}
