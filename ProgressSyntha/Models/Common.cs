using System.Text.Json.Serialization;

namespace ProgressSyntha.Models;

/// <summary>
/// HTTP Validation Error response
/// </summary>
public class HttpValidationError
{
    [JsonPropertyName("detail")]
    public ValidationErrorDetail[]? Detail { get; set; }
}

/// <summary>
/// Validation error detail
/// </summary>
public class ValidationErrorDetail
{
    [JsonPropertyName("loc")]
    public object[]? Loc { get; set; }

    [JsonPropertyName("msg")]
    public string? Msg { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

/// <summary>
/// NucliaDB Client Type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NucliaDbClientType
{
    [JsonPropertyName("api")]
    Api,
    
    [JsonPropertyName("web")]
    Web,
    
    [JsonPropertyName("widget")]
    Widget,
    
    [JsonPropertyName("dashboard")]
    Dashboard
}

/// <summary>
/// Base API response wrapper
/// </summary>
public class ApiResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? Error { get; set; }
    public HttpValidationError? ValidationError { get; set; }

    public static ApiResponse<T> CreateSuccess(T data)
    {
        return new ApiResponse<T>
        {
            Data = data,
            Success = true
        };
    }

    public static ApiResponse<T> CreateError(string error)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Error = error
        };
    }

    public static ApiResponse<T> CreateValidationError(HttpValidationError validationError)
    {
        return new ApiResponse<T>
        {
            Success = false,
            ValidationError = validationError
        };
    }
}
