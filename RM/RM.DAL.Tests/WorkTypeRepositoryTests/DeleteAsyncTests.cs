using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;
using RM.DAL.Tests.TestData;

namespace RM.DAL.Tests.WorkTypeRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeRepository.Delete"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования репозитория видов работ.</param>
public class DeleteAsyncTests(WorkTypeRepositoryFixture fixture) : IClassFixture<WorkTypeRepositoryFixture>
{
    #region Поля

    /// <summary>
    /// Репозиторий вида работ.
    /// </summary>
    private readonly IWorkTypeRepository _repository = fixture.WorkTypeRepository;

    #endregion

    #region Методы

    /// <summary>
    /// Тест удаления вида работ для корректных данных из источника данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.DeleteAsyncForCorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task ForCorrectData(WorkTypeModel workTypeModel)
    {      
        await _repository.CreateAsync(workTypeModel);

        await _repository.DeleteAsync(workTypeModel.Id);

        var expected = await _repository.GetByIdAsync(workTypeModel.Id);
        
        expected.Should().BeNull();
    }

    /// <summary>
    /// Тест удаления несуществующего вида работ.
    /// </summary>
    /// <param name="workTypeId">Идентификатор вида работ.</param>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.DeleteAsyncNotExistedWorkTypeTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]

    public async Task NotExistedWorkType(Guid workTypeId)
    {
        var action = async () => await _repository.DeleteAsync(workTypeId);

        await action.Should().ThrowAsync<DbUpdateConcurrencyException>();    
    }


    #endregion
}
