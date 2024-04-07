using RM.DAL.Abstractions.Models;

namespace RM.BLL.Extensions;

/// <summary>
/// Расширения модели вида работ.
/// </summary>
public static class WorkTypeModelExtensions
{
    #region Методы

    /// <summary>
    /// Преобразует вид работ DAL в вид работ BLL.
    /// </summary>
    /// <param name="workTypeModel">Вид работ DAL.</param>
    /// <returns>Вид работ BLL.</returns>
    public static Abstractions.Models.WorkTypeModel ToBll(this WorkTypeModel workTypeModel)
    {
        return workTypeModel != null ? new Abstractions.Models.WorkTypeModel
        {
            Id = workTypeModel.Id,
            Name = workTypeModel.Name,
            WorkUnit = workTypeModel.WorkUnit.ToBll()
        }: null;
    }

    #endregion
}
