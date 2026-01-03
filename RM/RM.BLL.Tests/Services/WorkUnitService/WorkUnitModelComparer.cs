using RM.BLL.Abstractions.Models;

namespace RM.BLL.Tests.Services.WorkUnitService;

/// <summary>
/// Компаратор для сравнения объектов <see cref="WorkUnitModel"/>.
/// </summary>
public class WorkUnitModelEqualityComparer : IEqualityComparer<WorkUnitModel?>
{
    #region Методы

    /// <inheritdoc/>
    public bool Equals(WorkUnitModel? workUnit1, WorkUnitModel? workUnit2)
    {
         if (workUnit1 is null && workUnit2 is null)
            return true;
        
        if (workUnit1 is null || workUnit2 is null)
            return false;
        
        return workUnit1.Id == workUnit2.Id &&
               workUnit1.Name == workUnit2.Name;
    }

    /// <inheritdoc/>
    public int GetHashCode(WorkUnitModel workUnit)
    {
        if(workUnit is null)
           return 0;

        return HashCode.Combine(workUnit.Id, workUnit.Name);
    }

    #endregion
}
