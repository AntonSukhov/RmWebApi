using RM.BLL.Abstractions.Models;

namespace RM.BLL.Tests.Services.PerformerService;

 /// <summary>
/// Пользовательский компаратор для сравнения объектов <see cref="PerformerModel"/>.
/// </summary>
public class PerformerModelEqualityComparer : IEqualityComparer<PerformerModel?>
{
    /// <summary>
    /// Определяет равенство двух объектов <see cref="PerformerModel"/>.
    /// </summary>
    /// <param name="performer1">Первый сравниваемый объект.</param>
    /// <param name="performer2">Второй сравниваемый объект.</param>
    /// <returns><c>true</c>, если объекты равны; иначе <c>false</c>.</returns>
    public bool Equals(PerformerModel? performer1, PerformerModel? performer2)
    {
        if(ReferenceEquals(performer1, performer2))
           return true;

        if(performer1 is null || performer2 is null)
          return false;

        return performer1.Id == performer2.Id &&
               performer1.EntityId == performer2.EntityId &&
               performer1.CreateDate == performer2.CreateDate &&
               performer1.EditDate == performer2.EditDate &&
               performer1.Creator == performer2.Creator &&
               performer1.Editor == performer2.Editor &&
               performer1.Snils == performer2.Snils &&
               performer1.Inn == performer2.Inn &&
               performer1.Surname == performer2.Surname &&
               performer1.Name == performer2.Name &&
               performer1.Patronymic == performer2.Patronymic &&
               performer1.Gender == performer2.Gender &&
               performer1.PassportNumber == performer2.PassportNumber &&
               performer1.PassportSeries == performer2.PassportSeries &&
               performer1.PassportIssuePlace == performer2.PassportIssuePlace &&
               performer1.PassportIssueDate == performer2.PassportIssueDate &&
               performer1.PassportDepartmentCode == performer2.PassportDepartmentCode &&
               performer1.PassportBirthPlace == performer2.PassportBirthPlace &&
               performer1.PassportBirthDate == performer2.PassportBirthDate &&
               performer1.PassportRegistrationPlace == performer2.PassportRegistrationPlace;
    }

    /// <summary>
    /// Вычисляет хэш-код для объекта <see cref="PerformerModel"/>.
    /// </summary>
    /// <param name="performer">Объект для вычисления хэш-кода.</param>
    /// <returns>Хэш-код объекта.</returns>
    /// <exception cref="ArgumentNullException"/>
    public int GetHashCode(PerformerModel performer)
    {
        ArgumentNullException.ThrowIfNull(performer);

        var hashCode = new HashCode();

        hashCode.Add(performer.Id);
        hashCode.Add(performer.EntityId);
        hashCode.Add(performer.CreateDate);
        hashCode.Add(performer.EditDate);
        hashCode.Add(performer.Creator);
        hashCode.Add(performer.Editor);
        hashCode.Add(performer.Snils);
        hashCode.Add(performer.Inn);
        hashCode.Add(performer.Surname);
        hashCode.Add(performer.Name);
        hashCode.Add(performer.Patronymic);
        hashCode.Add(performer.Gender);
        hashCode.Add(performer.PassportNumber);
        hashCode.Add(performer.PassportSeries);
        hashCode.Add(performer.PassportIssuePlace);
        hashCode.Add(performer.PassportIssueDate);
        hashCode.Add(performer.PassportDepartmentCode);
        hashCode.Add(performer.PassportBirthPlace);
        hashCode.Add(performer.PassportBirthDate);
        hashCode.Add(performer.PassportRegistrationPlace);

        return hashCode.ToHashCode();
    }
}
