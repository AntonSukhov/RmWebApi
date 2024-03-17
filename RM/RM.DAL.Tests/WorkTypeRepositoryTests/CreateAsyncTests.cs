using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;
using RM.DAL.Tests.TestData;

namespace RM.DAL.Tests.WorkTypeRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeRepository.CreateAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования репозитория видов работ.</param>
public class CreateAsyncTests(WorkTypeRepositoryFixture fixture) : IClassFixture<WorkTypeRepositoryFixture>
{
    #region Поля

    /// <summary>
    /// Репозиторий вида работ, работающий с MS SQL.
    /// </summary>
    private readonly IWorkTypeRepository _repositoryMsSql = fixture.WorkTypeRepositoryMsSql;

    /// <summary>
    /// Репозиторий вида работ, работающий с PostgreSQL.
    /// </summary>
    private readonly IWorkTypeRepository _repositoryPostgreSql = fixture.WorkTypeRepositoryPostgreSql;
    
    /// <summary>
    /// Репозиторий вида работ, работающий с SQLite в памяти.
    /// </summary>
    private readonly IWorkTypeRepository _repositorySqliteInMemory = fixture.WorkTypeRepositorySqliteInMemory;

    #endregion

    #region Методы

    /// <summary>
    /// Тест создания вида работ для корректных входных данных в 
    /// источнике данных MS SQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForCorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForCorrectDataInMsSql(WorkTypeModel workTypeModel)
    {      
        await ForCorrectData(workTypeModel, _repositoryMsSql);
    }

    /// <summary>
    /// Тест создания вида работ для корректных входных данных в 
    /// источнике данных PostgreSQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForCorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForCorrectDataInPostgreSql(WorkTypeModel workTypeModel)
    {      
        await ForCorrectData(workTypeModel, _repositoryPostgreSql);
    }

    /// <summary>
    /// Тест создания вида работ для корректных входных данных в 
    /// источнике данных SQLite в памяти.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForCorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForCorrectDataInSqliteInMemory(WorkTypeModel workTypeModel)
    {      
        await _repositorySqliteInMemory.CreateAsync(workTypeModel);

        var expected = await _repositorySqliteInMemory.GetByIdAsync(workTypeModel.Id);
        
        expected.Should().NotBeNull()
                         .And
                         .Match<WorkTypeModel>(p => p.Id == workTypeModel.Id && 
                                                    p.Name == workTypeModel.Name && 
                                                    p.WorkUnitId == workTypeModel.WorkUnitId);
    }

    /// <summary>
    /// Тест создания вида работ для некорректных входных данных в 
    /// источнике данных MS SQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForIncorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForIncorrectDataInMsSql(WorkTypeModel? workTypeModel)
    {      
        await ForIncorrectData(workTypeModel, _repositoryMsSql);
    }

    /// <summary>
    /// Тест создания вида работ для некорректных входных данных в 
    /// источнике данных PostgreSQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForIncorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForIncorrectDataInPostgreSql(WorkTypeModel? workTypeModel)
    {      
        await ForIncorrectData(workTypeModel, _repositoryPostgreSql);
    }

    /// <summary>
    /// Тест создания вида работ для некорректных входных данных в 
    /// источнике данных SQLite в памяти.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForIncorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForIncorrectDataInSqliteInMemory(WorkTypeModel? workTypeModel)
    {      
        await ForIncorrectData(workTypeModel, _repositoryPostgreSql);
    }

    #region Закрытые методы

    /// <summary>
    /// Тест создания вида работ для корректных входных данных.
    /// </summary>
    /// <param name="workTypeModel">Модель вида работ.</param>
    /// <param name="repository">Репозиторий вида работ.</param>
    /// <returns/>
    private static async Task ForCorrectData(WorkTypeModel workTypeModel, IWorkTypeRepository repository)
    {      
        await repository.CreateAsync(workTypeModel);

        var expected = await repository.GetByIdAsync(workTypeModel.Id);
        
        await repository.DeleteAsync(workTypeModel.Id);

        expected.Should().NotBeNull()
                         .And
                         .Match<WorkTypeModel>(p => p.Id == workTypeModel.Id && p.Name == workTypeModel.Name && 
                                                    p.WorkUnitId == workTypeModel.WorkUnitId);
    }

    /// <summary>
    /// Тест создания вида работ для некорректных входных данных.
    /// </summary>
    /// <param name="workTypeModel">Модель вида работ.</param>
    /// <param name="repository">Репозиторий вида работ.</param>
    /// <returns/>
    private static async Task ForIncorrectData(WorkTypeModel? workTypeModel, IWorkTypeRepository repository)
    {      
        var action = async () => await repository.CreateAsync(workTypeModel);

        await action.Should().ThrowAsync<Exception>();
    }

    #endregion 
    
    #endregion
}
