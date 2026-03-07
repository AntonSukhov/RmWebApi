using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RM.DAL.DbContexts;
using System.Diagnostics;

namespace RM.DAL.PostgreSql.DbContexts;

/// <summary>
/// Контекст работы с базой данных договоров ГПД.
/// </summary>
public class ContractGpdDbContext : ContractGpdDbContextBase
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="ContractGpdDbContext"/>.
    /// </summary>
    /// <param name="options">Опции контекста работы с базой данных договоров ГПД.</param>
    public ContractGpdDbContext(DbContextOptions<ContractGpdDbContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        //TODO: для отладки, потом заменить на нормальное протоколирование
        optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information)
                        .EnableSensitiveDataLogging();
    }
}

