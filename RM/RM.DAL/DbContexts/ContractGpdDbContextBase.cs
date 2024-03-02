using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;
using RM.DAL.DbContexts;

namespace RM.DAL;

/// <summary>
/// Базовый контекст работы с базой данных договоров ГПД.
/// </summary>
/// <param name="options">Опции контекста работы с базой данных договоров ГПД.</param>
public abstract class ContractGpdDbContextBase(DbContextOptions options) : DbContextBase(options)
{
    #region Свойства

    /// <summary>
    /// Единицы работ.
    /// </summary>
    public DbSet<WorkUnitModel> WorkUnits { get; set; }

    /// <summary>
    /// Виды работ.
    /// </summary>
    public DbSet<WorkTypeModel> WorkTypes { get; set; }

    #endregion
}
