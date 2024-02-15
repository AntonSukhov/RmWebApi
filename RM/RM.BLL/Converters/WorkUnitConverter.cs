using RM.BLL.Abstractions.Models;

namespace RM.BLL.Converters;

/// <summary>
/// Преобразователь единицы работ из DAL в BLL и обратно.
/// </summary>
public static class WorkUnitConverter
{
 
    #region Методы

    /// <summary>
    /// Преобразует единицу работ DAL в единицу работ BLL.
    /// </summary>
    /// <param name="workUnitModel">Единица работ DAL.</param>
    /// <returns>Единица работ BLL.</returns>
    public static WorkUnitModel ConvertDalToBllModel(DAL.Abstractions.Models.WorkUnitModel workUnitModel)
    {
        if (workUnitModel == null)
        {
            return null;
        }

        return new WorkUnitModel
        {
            Id = workUnitModel.Id,
            Name = workUnitModel.Name
        };
    }

    #endregion
}
