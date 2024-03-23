using Microsoft.EntityFrameworkCore;
using RM.Common.Helpers;
using RM.Common.Services;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Repositories;

namespace RM.DAL.Tests.Fixtures;

/// <summary>
/// Настройка контекста для тестирования репозитория единиц работ.
/// </summary>
public class WorkUnitRepositoryFixture
{
    #region Свойства

    /// <summary>
    /// Тестируемый репозиторий, работающий с базой данных MS SQL.
    /// </summary>
    public IWorkUnitRepository WorkUnitRepositoryMsSql { get; }

    /// <summary>
    /// Тестируемый репозиторий, работающий с базой данных PostgreSql.
    /// </summary>
    public IWorkUnitRepository WorkUnitRepositoryPostgreSql { get; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public WorkUnitRepositoryFixture()
    {
        var optionsBuilderMsSql = new DbContextOptionsBuilder<MsSql.DbContexts.ContractGpdDbContext>();
        var optionsBuilderPostgreSql = new DbContextOptionsBuilder<PostgreSql.DbContexts.ContractGpdDbContext>();
        
        optionsBuilderMsSql.UseSqlServer(ConfigurationHelper.GetConnectionString(Constants.MsSqlDbContractConnectionString));
        optionsBuilderPostgreSql.UseNpgsql(ConfigurationHelper.GetConnectionString(Constants.PostgreDbContractConnectionString));
        
        var optionsMsSql = optionsBuilderMsSql.Options;
        var optionsPostgreSql = optionsBuilderPostgreSql.Options;
     
        WorkUnitRepositoryMsSql = new WorkUnitRepository(new MsSql.DbContexts.ContractGpdDbContext(optionsMsSql));
        WorkUnitRepositoryPostgreSql = new WorkUnitRepository(new PostgreSql.DbContexts.ContractGpdDbContext(optionsPostgreSql));
    }

    #endregion
}
