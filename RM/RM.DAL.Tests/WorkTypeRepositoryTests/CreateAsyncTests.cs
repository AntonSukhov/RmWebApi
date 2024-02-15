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

    /// <summary>
    /// Репозиторий вида работ.
    /// </summary>
    private readonly IWorkTypeRepository _repository;

    #endregion

    #region Конструкторы

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="fixture"></param>
    public CreateAsyncTests(WorkTypeRepositoryFixture fixture)
    {
        _repository = fixture?.WorkTypeRepository ?? throw new ArgumentNullException(nameof(fixture));
    }

    #endregion

    #region Методы

    /// <summary>
    /// Тест создания вида работ для корректных входных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForCorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task CreateAsyncForCorrectDataTest(WorkTypeModel workTypeModel)
    {      
        await _repository.CreateAsync(workTypeModel);

        var expected = await _repository.GetByIdAsync(workTypeModel.Id);
        
        await _repository.DeleteAsync(workTypeModel.Id);

        expected.Should().NotBeNull()
                         .And
                         .Match<WorkTypeModel>(p => p.Id == workTypeModel.Id && p.Name == workTypeModel.Name && 
                                                    p.WorkUnitId == workTypeModel.WorkUnitId);
    }

    /// <summary>
    /// Тест создания вида работ для некорректных входных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForIncorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task CreateAsyncForIncorrectDataTest(WorkTypeModel? workTypeModel)
    {      

        var action = async () => await _repository.CreateAsync(workTypeModel);

        await action.Should().ThrowAsync<Exception>();
    }

    #endregion
}
