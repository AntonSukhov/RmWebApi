using Infrastructure.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Entities;

namespace RM.DAL.DbContexts;

/// <summary>
/// Базовый контекст работы с базой данных договоров ГПД.
/// </summary>
public abstract class ContractGpdDbContextBase : DbContextBase
{
    /// <summary>
    /// Получает или задает единицы работ.
    /// </summary>
    /// <value>Единицы работ.</value>
    public DbSet<WorkUnitEntity> WorkUnits { get; set; }

    /// <summary>
    /// Получает или задает виды работ.
    /// </summary>
    /// <value>Виды работ.</value>
    public DbSet<WorkTypeEntity> WorkTypes { get; set; }

    /// <summary>
    /// Получает или задает исполнителей договоров.
    /// </summary>
    /// <value>Исполнители договоров.</value>
    public DbSet<PerformerEntity> Performers { get; set; }

    /// <summary>
    /// Инициализирует экземпляр <see cref="ContractGpdDbContextBase"/>.
    /// </summary>
    /// <param name="options">Опции контекста работы с базой данных договоров ГПД.</param>
    public ContractGpdDbContextBase(DbContextOptions options): base(options) {}
}
