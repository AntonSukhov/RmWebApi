using Microsoft.EntityFrameworkCore;
using RM.Common.Helpers;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.MsSql.DbContexts;
using RM.DAL.Repositories;

namespace RM.DAL.Tests.Fixtures;

/// <summary>
/// Настройка контекста для тестирования репозитория единиц работ.
/// </summary>
public class WorkUnitRepositoryFixture
{
    #region Свойства

    /// <summary>
    /// Тестируемый репозиторий.
    /// </summary>
    public IWorkUnitRepository WorkUnitRepository { get; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    public WorkUnitRepositoryFixture()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContractGpdDbContext>();
        optionsBuilder.UseSqlServer(ConfigurationHelper.GetConnectionString());
        var options = optionsBuilder.Options;

        WorkUnitRepository = new WorkUnitRepository(new ContractGpdDbContext(options));
    }

    #endregion
}
