using RM.BLL.Abstractions.Models;

namespace RM.BLL.Extensions;

/// <summary>
/// Расширения модели обновления вида работ.
/// </summary>
public static class WorkTypeUpdationModelExtensions
{
    /// <summary>
    /// Преобразует модель обновления вида работ BLL в модель сокращенного вида работ DAL.
    /// </summary>
    /// <param name="workTypeUpdationModel">Модель обновления вида работ BLL.</param>
    /// <returns>Модель сокращенного вида работ DAL.</returns>
    public static DAL.Abstractions.Models.WorkTypeShortModel ToDal(this WorkTypeUpdationModel workTypeUpdationModel)
    {
        if (workTypeUpdationModel != null)
        {
            var workTypeName = string.IsNullOrEmpty(workTypeUpdationModel.Name) ? null : workTypeUpdationModel.Name;
            
            return new DAL.Abstractions.Models.WorkTypeShortModel
            {
                Id = workTypeUpdationModel.Id,
                Name = workTypeName,
                WorkUnitId = workTypeUpdationModel.WorkUnitId
            };
        }
        
        return null;
    }
}
