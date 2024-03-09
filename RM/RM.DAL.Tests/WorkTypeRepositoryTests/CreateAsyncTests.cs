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
public class CreateAsyncTests: IClassFixture<WorkTypeRepositoryFixture>
{
    #region Поля

    /// <summary>
    /// Репозиторий вида работ, работающий с MS SQL.
    /// </summary>
    private readonly IWorkTypeRepository _repositoryMsSql;

    /// <summary>
    /// Репозиторий вида работ, работающий с PostgreSQL.
    /// </summary>
    private readonly IWorkTypeRepository _repositoryPostgreSql;

    #endregion


    public CreateAsyncTests(WorkTypeRepositoryFixture fixture)
    {
        _repositoryMsSql = fixture.WorkTypeRepositoryMsSql;
        _repositoryPostgreSql = fixture.WorkTypeRepositoryPostgreSql;
    }

    #region Методы

    /// <summary>
    /// Тест создания вида работ для корректных входных данных. MS SQL.
    /// </summary>
    [Theory(Skip = "На Linux нельзя установить MS SQL Server, поэтому отключил тест.")]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForCorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForCorrectDataMsSql(WorkTypeModel workTypeModel)
    {      
        await ForCorrectData(workTypeModel, _repositoryMsSql);
    }

    /// <summary>
    /// Тест создания вида работ для корректных входных данных. PostgreSQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForCorrectDataTestData2),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForCorrectDataPostgreSql(WorkTypeModel workTypeModel)
    {      
        await ForCorrectData(workTypeModel, _repositoryPostgreSql);
    }

    /// <summary>
    /// Тест создания вида работ для некорректных входных данных. MS SQL.
    /// </summary>
    [Theory(Skip = "На Linux нельзя установить MS SQL Server, поэтому отключил тест.")]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForIncorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForIncorrectDataMsSql(WorkTypeModel? workTypeModel)
    {      
         await ForIncorrectData(workTypeModel, _repositoryMsSql);
    }

    /// <summary>
    /// Тест создания вида работ для некорректных входных данных. PostgreSQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForIncorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForIncorrectDataPostgreSql(WorkTypeModel? workTypeModel)
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
    private async Task ForCorrectData(WorkTypeModel workTypeModel, IWorkTypeRepository repository)
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
    private async Task ForIncorrectData(WorkTypeModel? workTypeModel, IWorkTypeRepository repository)
    {      
        var action = async () => await repository.CreateAsync(workTypeModel);

        await action.Should().ThrowAsync<Exception>();
    }


    #endregion 
    
    #endregion
}
