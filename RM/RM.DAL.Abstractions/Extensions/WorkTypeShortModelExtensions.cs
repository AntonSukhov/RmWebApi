using RM.DAL.Abstractions.Models;

namespace RM.DAL.Abstractions.Extensions;

/// <summary>
/// Расширения модели сокращенного вида работ.
/// </summary>
public static class WorkTypeShortModelExtensions
{
    /// <summary>
    /// Преобразует сокращенный вид работ DAL в вид работ DAL.
    /// </summary>
    /// <param name="workTypeShortModel">Сокращенный вид работ DAL.</param>
    /// <returns>Вид работ DAL.</returns>
    public static WorkTypeModel ToDal(this WorkTypeShortModel workTypeShortModel)
    {
        return workTypeShortModel != null ? new WorkTypeModel
        {
            Id = workTypeShortModel.Id,
            Name = workTypeShortModel.Name,
            WorkUnitId = workTypeShortModel.WorkUnitId
        } : null;
    }
}
