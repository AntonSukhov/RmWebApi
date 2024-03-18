using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RM.Common.Helpers;
using RM.Common.Services;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Repositories;
using RM.Tests.Common.TestData;

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

    /// <summary>
    /// Тестируемый репозиторий, работающий с базой данных SQLite в памяти.
    /// </summary>
    public IWorkUnitRepository WorkUnitRepositorySqliteInMemory { get; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    public WorkUnitRepositoryFixture()
    {
        var optionsBuilderMsSql = new DbContextOptionsBuilder<MsSql.DbContexts.ContractGpdDbContext>();
        var optionsBuilderPostgreSql = new DbContextOptionsBuilder<PostgreSql.DbContexts.ContractGpdDbContext>();
        
        optionsBuilderMsSql.UseSqlServer(ConfigurationHelper.GetConnectionString(Constants.MsSqlDbContractConnectionString));
        optionsBuilderPostgreSql.UseNpgsql(ConfigurationHelper.GetConnectionString(Constants.PostgreDbContractConnectionString));
        
        var optionsMsSql = optionsBuilderMsSql.Options;
        var optionsPostgreSql = optionsBuilderPostgreSql.Options;
     
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var optionsSqliteInMemory = new DbContextOptionsBuilder<ContractGpdDbContextSqliteInMemory>()
            .UseSqlite(connection)
            .Options;

        var context = new ContractGpdDbContextSqliteInMemory(optionsSqliteInMemory);
        using var command = context.Database.GetDbConnection().CreateCommand();
        command.CommandText = @"CREATE TABLE ""WorkUnits"" (
                                    ""Id"" INTEGER NOT NULL,
                                    ""Name"" TEXT NOT NULL,
                                    PRIMARY KEY(""Id"")
                                );";

        command.ExecuteNonQuery();
        
        context.AddRange(DataSourceTestData.WorkUnits);
        
        context.SaveChanges();

        WorkUnitRepositoryMsSql = new WorkUnitRepository(new MsSql.DbContexts.ContractGpdDbContext(optionsMsSql));
        WorkUnitRepositoryPostgreSql = new WorkUnitRepository(new PostgreSql.DbContexts.ContractGpdDbContext(optionsPostgreSql));
        WorkUnitRepositorySqliteInMemory = new WorkUnitRepository(new ContractGpdDbContextSqliteInMemory(optionsSqliteInMemory));
    }

    #endregion
}
