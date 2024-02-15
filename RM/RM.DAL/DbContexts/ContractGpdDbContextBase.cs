using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;

namespace RM.DAL;

/// <summary>
/// Базовый контекст работы с базой данных договоров ГПД.
/// </summary>
public abstract class ContractGpdDbContextBase: DbContext
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

    #region  Конструкторы

    
    /// <summary>
    /// Конструктор. 
    /// </summary>
    /// <param name="options">Опции контекста работы с базой данных договоров ГПД.</param>
    protected ContractGpdDbContextBase(DbContextOptions options) : base(options) { }

    #endregion
}
