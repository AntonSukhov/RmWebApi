using System;
using RM.BLL.Abstractions.Models;

namespace RM.BLL.Extensions;

/// <summary>
/// Расширения модели создания вида работ.
/// </summary>
public static class WorkTypeCreationModelExtensions
{
    #region Методы

    /// <summary>
    /// Преобразует модель создания вида работ BLL в модель сокращенного вида работ DAL.
    /// </summary>
    /// <param name="workTypeCreationModel">Модель создания вида работ BLL.</param>
    /// <param name="workTypeId">Идентификатор вида работ.</param>
    /// <returns>Модель сокращенного вида работ DAL.</returns>
    public static DAL.Abstractions.Models.WorkTypeShortModel ToDal(this WorkTypeCreationModel workTypeCreationModel, 
                                                                        Guid workTypeId)
    {
        return workTypeCreationModel != null ? new DAL.Abstractions.Models.WorkTypeShortModel
        {
            Id = workTypeId,
            Name = workTypeCreationModel.Name,
            WorkUnitId = workTypeCreationModel.WorkUnitId
        } : null;
    }

    #endregion
}
