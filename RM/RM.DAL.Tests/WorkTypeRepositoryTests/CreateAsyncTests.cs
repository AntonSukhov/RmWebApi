using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;
using RM.DAL.Tests.TestData;

namespace RM.DAL.Tests.WorkTypeRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeRepository.CreateAsync"/>.
/// </summary>
public class CreateAsyncTests : IClassFixture<WorkTypeRepositoryFixture>
{
    #region Поля

    private readonly WorkTypeRepositoryFixture _fixture;

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    /// <param name="fixture">Настройка контекста для тестирования репозитория видов работ.</param>
    public CreateAsyncTests(WorkTypeRepositoryFixture fixture)
    {
        _fixture = fixture;
    }

    #endregion

    #region Методы

    /// <summary>
    /// Тест создания вида работ для корректных входных данных в 
    /// источнике данных MS SQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForCorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForCorrectDataInMsSql(WorkTypeShortModel workTypeModel)
    {      
        await ForCorrectData(workTypeModel, _fixture.WorkTypeRepositoryMsSql);
    }

    /// <summary>
    /// Тест создания вида работ для корректных входных данных в 
    /// источнике данных PostgreSQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForCorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForCorrectDataInPostgreSql(WorkTypeShortModel workTypeModel)
    {      
        await ForCorrectData(workTypeModel, _fixture.WorkTypeRepositoryPostgreSql);
    }

    /// <summary>
    /// Тест создания вида работ для некорректных входных данных в 
    /// источнике данных MS SQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForIncorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForIncorrectDataInMsSql(WorkTypeShortModel? workTypeModel)
    {      
        await ForIncorrectData(workTypeModel, _fixture.WorkTypeRepositoryMsSql);
    }

    /// <summary>
    /// Тест создания вида работ для некорректных входных данных в 
    /// источнике данных PostgreSQL.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForIncorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForIncorrectDataInPostgreSql(WorkTypeShortModel? workTypeModel)
    {      
        await ForIncorrectData(workTypeModel, _fixture.WorkTypeRepositoryPostgreSql);
    }

    #region Закрытые методы

    /// <summary>
    /// Тест создания вида работ для корректных входных данных.
    /// </summary>
    /// <param name="workTypeModel">Модель вида работ.</param>
    /// <param name="repository">Репозиторий вида работ.</param>
    /// <returns/>
    private static async Task ForCorrectData(WorkTypeShortModel workTypeModel, 
                                             IWorkTypeRepository repository)
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
    private static async Task ForIncorrectData(WorkTypeShortModel? workTypeModel, 
                                               IWorkTypeRepository repository)
    {      
        var expected = async () => await repository.CreateAsync(workTypeModel);

        await expected.Should().ThrowAsync<Exception>();
    }

    #endregion 
    
    #endregion
}
