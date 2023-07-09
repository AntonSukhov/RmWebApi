using RM.DAL.Abstractions.Models;
using RM.DAL.Entities;
using System.Linq;

namespace RM.DAL.Extensions
{
    internal static class WorkUnitEntityQueryableExtensions
    {
        public static IQueryable<WorkUnitModel> MapWorkUnitEntityToModel(this IQueryable<WorkUnitEntity> workUnits)
        {
            return workUnits.Select(p => new WorkUnitModel
            {
                Id = p.Id,
                Name = p.Name
            });
        }
    }
}
