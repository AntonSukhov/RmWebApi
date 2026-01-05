using RM.BLL.Abstractions.Models;

namespace RM.BLL.Extensions;

/// <summary>
/// Расширения модели единицы работ.
/// </summary>
public static class WorkUnitModelExtensions
{
    /// <summary>
    /// Преобразует единицу работ DAL в единицу работ BLL.
    /// </summary>
    /// <param name="workUnitModel">Единица работ DAL.</param>
    /// <returns>Единица работ BLL.</returns>
    public static WorkUnitModel ToBll(this DAL.Abstractions.Models.WorkUnitModel workUnitModel)
    {
        return workUnitModel != null ? new WorkUnitModel
        {
            Id = workUnitModel.Id,
            Name = workUnitModel.Name
        }: null;
    }
}
