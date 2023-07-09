using ContractGpdApiTests.Helpers;
using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.MsSql.DbContexts;
using RM.DAL.Repositories;

namespace RM.DAL.Tests.Fixtures
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkTypeRepositoryFixture
    {
        #region Свойства

        /// <summary>
        /// Тестируемый репозиторий
        /// </summary>
        public IWorkTypeRepository WorkTypeRepository { get; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор
        /// </summary>
        public WorkTypeRepositoryFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContractGpdDbContext>();
            optionsBuilder.UseSqlServer(ConfigurationHelper.GetConnectionString());
            var options = optionsBuilder.Options;

            WorkTypeRepository = new WorkTypeRepository(new ContractGpdDbContext(options));
        }

        #endregion
    }
}
