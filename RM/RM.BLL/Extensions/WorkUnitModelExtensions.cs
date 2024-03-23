using RM.DAL.Abstractions.Models;

namespace RM.BLL.Extensions;

/// <summary>
/// Расширения класса <see cref="WorkUnitModel"/>.
/// </summary>
public static class WorkUnitModelExtensions
{
    #region Методы

    /// <summary>
    /// Преобразует единицу работ DAL в единицу работ BLL.
    /// </summary>
    /// <param name="workUnitModel">Единица работ DAL.</param>
    /// <returns>Единица работ BLL.</returns>
    public static Abstractions.Models.WorkUnitModel ToBll(this WorkUnitModel workUnitModel)
    {
        return workUnitModel != null ? new Abstractions.Models.WorkUnitModel
        {
            Id = workUnitModel.Id,
            Name = workUnitModel.Name
        }: null;
    }

    #endregion
}
