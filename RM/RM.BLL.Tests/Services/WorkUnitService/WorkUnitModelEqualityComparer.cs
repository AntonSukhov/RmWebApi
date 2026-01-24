using RM.BLL.Abstractions.Models;

namespace RM.BLL.Tests.Services.WorkUnitService;

 /// <summary>
/// Пользовательский компаратор для сравнения объектов <see cref="WorkUnitModel"/>.
/// </summary>
public class WorkUnitModelEqualityComparer : IEqualityComparer<WorkUnitModel?>
{   
    /// <summary>
    /// Определяет равенство двух объектов <see cref="WorkUnitModel"/>.
    /// </summary>
    /// <param name="workUnit1">Первый сравниваемый объект.</param>
    /// <param name="workUnit2">Второй сравниваемый объект.</param>
    /// <returns><c>true</c>, если объекты равны; иначе <c>false</c>.</returns>
    public bool Equals(WorkUnitModel? workUnit1, WorkUnitModel? workUnit2)
    {
        if(ReferenceEquals(workUnit1, workUnit2))
           return true;
        
        if(workUnit1 is null || workUnit2 is null)
          return false;

        return workUnit1.Id == workUnit2.Id &&
               workUnit1.Name == workUnit2.Name;
    }

    /// <summary>
    /// Вычисляет хэш-код для объекта <see cref="WorkUnitModel"/>.
    /// </summary>
    /// <param name="workUnit">Объект для вычисления хэш-кода.</param>
    /// <returns>Хэш-код объекта.</returns>
    /// <exception cref="ArgumentNullException"/>
    public int GetHashCode(WorkUnitModel workUnit)
    {
        ArgumentNullException.ThrowIfNull(workUnit);

        return HashCode.Combine(workUnit.Id, workUnit.Name);
    }
}
