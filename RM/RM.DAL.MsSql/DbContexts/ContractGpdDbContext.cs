using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace RM.DAL.MsSql.DbContexts;

/// <summary>
/// Контекст работы с базой данных договоров ГПД.
/// </summary>
public class ContractGpdDbContext : ContractGpdDbContextBase
{

    #region Конструкторы

    /// <summary>
    /// Конструктор. 
    /// </summary>
    /// <param name="options">Опции контекста работы с базой данных договоров ГПД.</param>
    public ContractGpdDbContext(DbContextOptions<ContractGpdDbContext> options) : base(options) { }

    #endregion

    #region Методы

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        //TODO: для отладки, потом заменить на нормальное протоколирование
        optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information)
                        .EnableSensitiveDataLogging();
    }

    #endregion
}
