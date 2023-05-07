using RM.DAL.Abstractions.Models;
using RM.DAL.Entities;
using System;

namespace RM.DAL.Converters
{
    /// <summary>
    /// Конвертер сущности вида работ
    /// </summary>
    internal static class WorkTypeEntityConverter
    {
        #region Методы

        /// <summary>
        /// Конвертирование сущности вида работ в модель вида работ
        /// </summary>
        /// <param name="entity">Сущность вида работ</param>
        /// <param name="workUnitName">Название единицы работ</param>
        /// <returns>Модель вида работ</returns>
        public static WorkTypeModel ConvertEntityToModel(this WorkTypeEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var workTypeModel = new WorkTypeModel
            {
                Id = entity.Id,
                Name = entity.Name
            };

            if (entity.WorkUnitId != null && entity.WorkUnit?.Name != null)
            {
                workTypeModel.WorkUnit = new WorkUnitModel
                {
                    Id = entity.WorkUnitId.Value,
                    Name = entity.WorkUnit.Name
                };
            }

            return workTypeModel;
        }

        #endregion
    }
}
