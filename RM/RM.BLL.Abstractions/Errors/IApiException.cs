namespace RM.BLL.Abstractions.Errors;

/// <summary>
/// Исключение API.
/// </summary>
public interface IApiException
{
    /// <summary>
    /// Проводит <see cref="IApiException"/> к <see cref="ApiError"/>.
    /// </summary>
    /// <returns>Экземпляр <see cref="ApiError"/>.</returns>
    public ApiError ToApiError();
}
