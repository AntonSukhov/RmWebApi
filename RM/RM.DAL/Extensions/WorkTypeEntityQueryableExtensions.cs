using RM.DAL.Abstractions.Models;
using RM.DAL.Entities;
using System.Linq;

namespace RM.DAL.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal static class WorkTypeEntityQueryableExtensions
    {
        #region Методы

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workTypes"></param>
        /// <returns></returns>
        public static IQueryable<WorkTypeModel> MapWorkTypeEntityToModel(this IQueryable<WorkTypeEntity> workTypes)
        {
            return workTypes.Select(p => new WorkTypeModel
            {
                Id = p.Id,
                Name = p.Name,
                WorkUnit = p.WorkUnit != null ? new WorkUnitModel { Id = p.WorkUnit.Id, Name = p.WorkUnit.Name } : null
            });
        }

        #endregion
    }
}
