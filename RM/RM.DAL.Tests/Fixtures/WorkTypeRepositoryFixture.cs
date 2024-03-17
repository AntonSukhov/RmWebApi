using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RM.Common.Helpers;
using RM.Common.Services;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Repositories;
using RM.DAL.Tests.TestData;

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

    /// <summary>
    /// Тестируемый репозиторий, работающий с базой данных SQLite в памяти.
    /// </summary>
    public IWorkTypeRepository WorkTypeRepositorySqliteInMemory { get; }

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

        command.CommandText = @"CREATE TABLE ""WorkTypes"" (
            ""Id"" TEXT PRIMARY KEY NOT NULL,
            ""Name"" TEXT NOT NULL,
            ""WorkUnitId"" INTEGER,
            FOREIGN KEY(""WorkUnitId"") REFERENCES WorkUnits(""Id"")
        );";

        command.ExecuteNonQuery();

        command.CommandText = @"CREATE INDEX IF NOT EXISTS ""IX_WorkUnitId"" ON ""WorkTypes""(""WorkUnitId"");";
        
        command.ExecuteNonQuery();

        context.AddRange(DataBaseTestData.WorkUnits);
        context.AddRange(DataBaseTestData.WorkTypes);
        
        context.SaveChanges();

        WorkTypeRepositoryMsSql = new WorkTypeRepository(new MsSql.DbContexts.ContractGpdDbContext(optionsMsSql));
        WorkTypeRepositoryPostgreSql = new WorkTypeRepository(new PostgreSql.DbContexts.ContractGpdDbContext(optionsPostgreSql));
        WorkTypeRepositorySqliteInMemory = new WorkTypeRepository(new ContractGpdDbContextSqliteInMemory(optionsSqliteInMemory));
    }

    #endregion
}
