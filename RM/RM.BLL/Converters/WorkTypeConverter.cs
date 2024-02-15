using RM.BLL.Abstractions.Models;

namespace RM.BLL.Converters;

/// <summary>
/// Преобразователь вида работ из DAL в BLL и обратно.
/// </summary>
public static class WorkTypeConverter
{
   #region Методы

    /// <summary>
    /// Преобразует вид работ DAL в вид работ BLL.
    /// </summary>
    /// <param name="workTypeModel">Вид работ DAL.</param>
    /// <returns>Вид работ BLL.</returns>
    public static WorkTypeModel ConvertDalToBllModel(DAL.Abstractions.Models.WorkTypeModel workTypeModel)
    {
        if (workTypeModel == null)
        {
            return null;
        }

        return new WorkTypeModel
        {
            Id = workTypeModel.Id,
            Name = workTypeModel.Name,
            WorkUnit = WorkUnitConverter.ConvertDalToBllModel(workTypeModel.WorkUnit)
        };
    }

    #endregion
}
