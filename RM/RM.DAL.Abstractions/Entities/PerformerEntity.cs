using System;

namespace RM.DAL.Abstractions.Entities;

/// <summary>
/// Сущность исполнитель договора.
/// </summary>
public class PerformerEntity
{
    /// <summary>
    /// Получает или задаёт идентификатор исполнителя.
    /// </summary>
    /// <value>
    /// Идентификатор исполнителя.
    /// </value>
    public Guid Id { get; set; }

    /// <summary>
    /// Получает или задаёт идентификатор истории исполнителя.
    /// </summary>
    /// <value>
    /// Идентификатор истории исполнителя.
    /// </value>
    public Guid EntityId { get; set; }

    /// <summary>
    /// Получает или задаёт дату и время создания исполнителя.
    /// </summary>
    /// <value>
    /// Дата и время создания исполнителя.
    /// </value>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// Получает или задаёт дату и время редактирования исполнителя.
    /// </summary>
    /// <value>
    /// Дата и время редактирования исполнителя.
    /// </value>
    public DateTime? EditDate { get; set; }

    /// <summary>
    /// Получает или задаёт создателя исполнителя.
    /// </summary>
    /// <value>
    /// Создатель исполнителя.
    /// </value>
    public required string Creator { get; set; }

    /// <summary>
    /// Получает или задаёт редактора исполнителя.
    /// </summary>
    /// <value>
    /// Редактор исполнителя.
    /// </value>
    public string? Editor { get; set; }

    /// <summary>
    /// Получает или задаёт СНИЛС исполнителя.
    /// </summary>
    /// <value>
    /// СНИЛС исполнителя.
    /// </value>
    public required string Snils { get; set; }

    /// <summary>
    /// Получает или задаёт ИНН исполнителя.
    /// </summary>
    /// <value>
    /// ИНН исполнителя.
    /// </value>
    public string? Inn { get; set; }

    /// <summary>
    /// Получает или задаёт фамилию исполнителя.
    /// </summary>
    /// <value>
    /// Фамилия исполнителя.
    /// </value>
    public required string Surname { get; set; }

    /// <summary>
    /// Получает или задаёт имя исполнителя.
    /// </summary>
    /// <value>
    /// Имя исполнителя.
    /// </value>
    public required string Name { get; set; }

    /// <summary>
    /// Получает или задаёт отчество исполнителя.
    /// </summary>
    /// <value>
    /// Отчество исполнителя.
    /// </value>
    public required string Patronymic { get; set; }

    /// <summary>
    /// Получает или задаёт пол исполнителя.
    /// </summary>
    /// <value>
    /// Пол исполнителя.
    /// </value>
    public int Gender { get; set; }

    /// <summary>
    /// Получает или задаёт номер паспорта исполнителя.
    /// </summary>
    /// <value>
    /// Номер паспорта исполнителя.
    /// </value>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Получает или задаёт серия паспорта исполнителя.
    /// </summary>
    /// <value>
    /// Серия паспорта исполнителя.
    /// </value>
    public required string PassportSeries { get; set; }

    /// <summary>
    /// Получает или задаёт место выдачи паспорта исполнителя.
    /// </summary>
    /// <value>
    /// Место выдачи паспорта исполнителя.
    /// </value>
    public required string PassportIssuePlace { get; set; }

    /// <summary>
    /// Получает или задаёт дата выдачи паспорта исполнителя.
    /// </summary>
    /// <value>
    /// Дата выдачи паспорта исполнителя.
    /// </value>
    public DateTime PassportIssueDate { get; set; }

    /// <summary>
    /// Получает или задаёт код подразделения паспорта исполнителя.
    /// </summary>
    /// <value>
    /// Код подразделения паспорта исполнителя.
    /// </value>
    public required string PassportDepartmentCode { get; set; }

    /// <summary>
    /// Получает или задаёт место рождения в паспорте исполнителя.
    /// </summary>
    /// <value>
    /// Место рождения в паспорте исполнителя.
    /// </value>
    public required string PassportBirthPlace { get; set; }

    /// <summary>
    /// Получает или задаёт дата рождения в паспорте исполнителя.
    /// </summary>
    /// <value>
    /// Дата рождения в паспорте исполнителя.
    /// </value>
    public DateTime PassportBirthDate { get; set; }

    /// <summary>
    /// Получает или задаёт место регистрации в паспорте исполнителя.
    /// </summary>
    /// <value>
    /// Место регистрации в паспорте исполнителя.
    /// </value>
    public required string PassportRegistrationPlace { get; set; }
}