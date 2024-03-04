using Microsoft.EntityFrameworkCore;
using RM.Common.Helpers;
using RM.Common.Services;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Repositories;

namespace RM.DAL.Tests.Fixtures;

/// <summary>
/// Настройка контекста для тестирования репозитория видов работ.
/// </summary>
public class WorkTypeRepositoryFixture
{
    #region Свойства

    /// <summary>
    /// Тестируемый репозиторий, работающий с базой данных MS SQL.
    /// </summary>
    public IWorkTypeRepository WorkTypeRepositoryMsSql { get; }

    /// <summary>
    /// Тестируемый репозиторий, работающий с базой данных PostgreSql.
    /// </summary>
    public IWorkTypeRepository WorkTypeRepositoryPostgreSql { get; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    public WorkTypeRepositoryFixture()
    {
        var optionsBuilderMsSql = new DbContextOptionsBuilder<MsSql.DbContexts.ContractGpdDbContext>();
        var optionsBuilderPostgreSql = new DbContextOptionsBuilder<PostgreSql.DbContexts.ContractGpdDbContext>();
        
        optionsBuilderMsSql.UseSqlServer(ConfigurationHelper.GetConnectionString(Constants.MsSqlDbContractConnectionString));
        optionsBuilderPostgreSql.UseNpgsql(ConfigurationHelper.GetConnectionString(Constants.PostgreDbContractConnectionString));
        
        var optionsMsSql = optionsBuilderMsSql.Options;
        var optionsPostgreSql = optionsBuilderPostgreSql.Options;

        WorkTypeRepositoryMsSql = new WorkTypeRepository(new MsSql.DbContexts.ContractGpdDbContext(optionsMsSql));
        WorkTypeRepositoryPostgreSql = new WorkTypeRepository(new PostgreSql.DbContexts.ContractGpdDbContext(optionsPostgreSql));
    }

    #endregion
}
