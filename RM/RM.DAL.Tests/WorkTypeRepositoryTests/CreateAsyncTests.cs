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
    /// Репозиторий вида работ.
    /// </summary>
    private readonly IWorkTypeRepository _repository = fixture.WorkTypeRepository;

    #endregion

    #region Методы

    /// <summary>
    /// Тест создания вида работ для корректных входных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.CreateAsyncForCorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForCorrectData(WorkTypeModel workTypeModel)
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
    public async Task ForIncorrectData(WorkTypeModel? workTypeModel)
    {      
        var action = async () => await _repository.CreateAsync(workTypeModel);

        await action.Should().ThrowAsync<Exception>();
    }

    #endregion
}
