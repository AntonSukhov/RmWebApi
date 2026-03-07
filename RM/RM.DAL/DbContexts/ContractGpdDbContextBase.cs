using Infrastructure.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;

namespace RM.DAL.DbContexts;

/// <summary>
/// Базовый контекст работы с базой данных договоров ГПД.
/// </summary>
public abstract class ContractGpdDbContextBase : DbContextBase
{
    /// <summary>
    /// Единицы работ.
    /// </summary>
    public DbSet<WorkUnitModel> WorkUnits { get; set; }

    /// <summary>
    /// Виды работ.
    /// </summary>
    public DbSet<WorkTypeModel> WorkTypes { get; set; }

    /// <summary>
    /// Инициализирует экземпляр <see cref="ContractGpdDbContextBase"/>.
    /// </summary>
    /// <param name="options">Опции контекста работы с базой данных договоров ГПД.</param>
    public ContractGpdDbContextBase(DbContextOptions options): base(options) {}
}
