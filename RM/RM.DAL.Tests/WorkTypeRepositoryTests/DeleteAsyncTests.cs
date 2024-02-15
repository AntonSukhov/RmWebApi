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
public class DeleteAsyncTests : IClassFixture<WorkTypeRepositoryFixture>
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
    public DeleteAsyncTests(WorkTypeRepositoryFixture fixture)
    {
        _repository = fixture?.WorkTypeRepository ?? throw new ArgumentNullException(nameof(fixture));
    }

    #endregion

    #region Методы

    /// <summary>
    /// Тест удаления вида работ для корректных данных из источника данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(WorkTypeRepositoryTestData.DeleteAsyncForCorrectDataTestData),
                MemberType = typeof(WorkTypeRepositoryTestData))]
    public async Task DeleteAsyncForCorrectDataTest(WorkTypeModel workTypeModel)
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

    public async Task DeleteAsyncNotExistedWorkTypeTest(Guid workTypeId)
    {
        var action = async () => await _repository.DeleteAsync(workTypeId);

        await action.Should().ThrowAsync<DbUpdateConcurrencyException>();    
    }


    #endregion
}
