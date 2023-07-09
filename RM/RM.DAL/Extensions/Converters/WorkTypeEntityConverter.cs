using RM.DAL.Abstractions.Models;
using RM.DAL.Entities;

namespace RM.DAL.Extensions.Converters
{
    // <summary>
    /// Конвертер сущности вида работ
    /// </summary>
    internal static class WorkTypeEntityConverter
    {
        #region Методы

        /// <summary>
        /// Конвертирование сущности вида работ в модель вида работ
        /// </summary>
        /// <param name="entity">Сущность вида работ</param>
        /// <returns>Модель вида работ</returns>
        public static WorkTypeModel ConvertEntityToModel(this WorkTypeEntity entity)
        {
            return new WorkTypeModel
            {
                Id = entity.Id,
                Name = entity.Name,
                WorkUnit = entity.WorkUnitId != null ? new WorkUnitModel
                {
                    Id = entity.WorkUnitId.Value,
                    Name = entity.WorkUnit.Name
                } : null
            };
        }

        /// <summary>
        /// Конвертирование модели вида работ в сущность вида работ
        /// </summary>
        /// <param name="model">Модель вида работ</param>
        /// <param name="notDefineWorkUnitEntity">Не определять вложенную сущность Единица работ</param>
        /// <returns>Сущность вида работ</returns>
        public static WorkTypeEntity ConvertModelToEntity(this WorkTypeModel model, bool notDefineWorkUnitEntity = false)
        {
            return new WorkTypeEntity
            {
                Id = model.Id,
                Name = model.Name,
                WorkUnitId = model.WorkUnit?.Id,
                WorkUnit = model.WorkUnit == null || notDefineWorkUnitEntity ? null : new WorkUnitEntity
                {
                    Id = model.WorkUnit.Id,
                    Name = model.WorkUnit.Name
                }
            };
        }

        #endregion
    }
}
