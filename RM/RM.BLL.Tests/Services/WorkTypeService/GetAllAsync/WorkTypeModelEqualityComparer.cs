using RM.BLL.Abstractions.Models;

namespace RM.BLL.Tests.Services.WorkTypeService.GetAllAsync;

 /// <summary>
/// Пользовательский компаратор для сравнения объектов <see cref="WorkTypeModel"/>.
/// </summary>
public class WorkTypeModelEqualityComparer : IEqualityComparer<WorkTypeModel>
{
    private readonly IEqualityComparer<WorkUnitModel> _workUnitEqualityComparer =
        new WorkUnitModelEqualityComparer();

    /// <summary>
    /// Определяет равенство двух объектов <see cref="WorkTypeModel"/>.
    /// </summary>
    /// <param name="workType1">Первый сравниваемый объект.</param>
    /// <param name="workType2">Второй сравниваемый объект.</param>
    /// <returns><c>true</c>, если объекты равны; иначе <c>false</c>.</returns>
    public bool Equals(WorkTypeModel? workType1, WorkTypeModel? workType2)
    {
        if(ReferenceEquals(workType1, workType2))
            return true;

        if(workType1 is null || workType2 is null)
            return false;

        return  workType1.Id == workType2.Id && 
                workType1.Name == workType2.Name &&
                _workUnitEqualityComparer.Equals(workType1.WorkUnit, workType2.WorkUnit);
    }

    /// <summary>
    /// Вычисляет хэш-код для объекта <see cref="WorkTypeModel"/>.
    /// </summary>
    /// <param name="workType">Объект для вычисления хэш-кода.</param>
    /// <returns>Хэш-код объекта.</returns>
    /// <exception cref="ArgumentNullException"/>
    public int GetHashCode(WorkTypeModel workType)
    {
        ArgumentNullException.ThrowIfNull(workType);

        var hashCode = new HashCode();
        hashCode.Add(workType.Id);
        hashCode.Add(workType.Name);

        if(workType.WorkUnit is not null)
        {
            hashCode.Add(_workUnitEqualityComparer.GetHashCode(workType.WorkUnit));
        }

        return hashCode.ToHashCode();
    }
}
