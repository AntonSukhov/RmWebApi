using RM.DAL.Abstractions.Models;

namespace RM.BLL.Extensions;

/// <summary>
/// Расширения модели настройки страницы BLL и DAL.
/// </summary>
public static class PageOptionsModelExtensions
{
    #region Методы

    /// <summary>
    /// Преобразует настройки страницы DAL в настройки страницы BLL.
    /// </summary>
    /// <param name="pageOptionsModel">Настройки страницы DAL.</param>
    /// <returns>Настройки страницы BLL.</returns>
    public static Abstractions.Models.PageOptionsModel ToBll(this PageOptionsModel pageOptionsModel)
    {
        return pageOptionsModel != null ? new Abstractions.Models.PageOptionsModel
        {
            PageNumber = pageOptionsModel.PageNumber,
            PageSize = pageOptionsModel.PageSize
        }: null;
    }

    /// <summary>
    /// Преобразует настройки страницы BLL в настройки страницы DAL.
    /// </summary>
    /// <param name="pageOptionsModel">Настройки страницы BLL.</param>
    /// <returns>Настройки страницы DAL.</returns>
    public static PageOptionsModel ToDal(this Abstractions.Models.PageOptionsModel pageOptionsModel)
    {
        return pageOptionsModel != null ? new PageOptionsModel
        {
            PageNumber = pageOptionsModel.PageNumber,
            PageSize = pageOptionsModel.PageSize
        } : null;
    }

    #endregion
}
