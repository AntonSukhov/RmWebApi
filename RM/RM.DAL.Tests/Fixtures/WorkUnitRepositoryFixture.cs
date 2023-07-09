using ContractGpdApiTests.Helpers;
using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.MsSql.DbContexts;
using RM.DAL.Repositories;

namespace RM.DAL.Tests.Fixtures
{
    public class WorkUnitRepositoryFixture
    {
        #region Свойства

        /// <summary>
        /// Тестируемый репозиторий
        /// </summary>
        public IWorkUnitRepository WorkUnitRepository { get; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор
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
}
