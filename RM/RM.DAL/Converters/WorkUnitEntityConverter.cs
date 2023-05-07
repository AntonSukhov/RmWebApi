using RM.DAL.Abstractions.Models;
using RM.DAL.Entities;
using System;

namespace RM.DAL.Converters
{
    /// <summary>
    /// Конвертер сущности единицы работ
    /// </summary>
    internal static class WorkUnitEntityConverter
    {
        #region Методы

        /// <summary>
        /// Конвертирование сущности единицы работ в модель единицы работ
        /// </summary>
        /// <param name="entity">Сущность единицы работ</param>
        /// <returns>Модель единицы работ</returns>
        public static WorkUnitModel ConvertEntityToModel(this WorkUnitEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return new WorkUnitModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        #endregion
    }
}
