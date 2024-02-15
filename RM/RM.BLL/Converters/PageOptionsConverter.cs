using RM.BLL.Abstractions.Models;

namespace RM.BLL.Converters;

/// <summary>
/// Преобразователь настроек страницы из DAL в BLL и обратно.
/// </summary>
public static class PageOptionsConverter
{
    #region Методы

    /// <summary>
    /// Преобразует настройки страницы DAL в настройки страницы BLL.
    /// </summary>
    /// <param name="pageOptionsModel">Настройки страницы DAL.</param>
    /// <returns>Настройки страницы BLL.</returns>
    public static PageOptionsModel ConvertDalToBllModel(DAL.Abstractions.Models.PageOptionsModel pageOptionsModel)
    {
        if (pageOptionsModel == null)
        {
            return null;
        }

        return new PageOptionsModel
        {
            PageNumber = pageOptionsModel.PageNumber,
            PageSize = pageOptionsModel.PageSize
        };
    }

    /// <summary>
    /// Преобразует настройки страницы BLL в настройки страницы DAL.
    /// </summary>
    /// <param name="pageOptionsModel">Настройки страницы BLL.</param>
    /// <returns>Настройки страницы DAL.</returns>
    public static DAL.Abstractions.Models.PageOptionsModel ConvertBllToDalModel(PageOptionsModel pageOptionsModel)
    {
        if (pageOptionsModel == null)
        {
            return null;
        }

        return new DAL.Abstractions.Models.PageOptionsModel
        {
            PageNumber = pageOptionsModel.PageNumber,
            PageSize = pageOptionsModel.PageSize
        };
    }

    #endregion
}
