using FluentAssertions;
using RM.DAL.Abstractions.Models;
using RM.DAL.Abstractions.Repositories;
using RM.DAL.Tests.Fixtures;

namespace RM.DAL.Tests.WorkTypeRepositoryTests;

/// <summary>
/// Тесты для метода <see cref="IWorkTypeRepository.GetAllAsync"/>.
/// </summary>
public class GetAllAsyncTests : IClassFixture<WorkTypeRepositoryFixture>
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
    public GetAllAsyncTests(WorkTypeRepositoryFixture fixture)
    {
        _repository = fixture?.WorkTypeRepository ?? throw new ArgumentNullException(nameof(fixture));
    }

    #endregion

    #region Методы

    /// <summary>
    /// Тест получения видов работ из базы данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(PaginationTestData.GetCorrectPageOptions), MemberType = typeof(PaginationTestData))]
    public async Task GetAllAsyncForCorrectPageOptionsTest(PageOptionsModel? pageOptions)
    {     
        var expected = await _repository.GetAllAsync(pageOptions);

        expected.Should().NotBeNull()
                         .And
                         .HaveCountGreaterThan(0);
    }

     /// <summary>
    /// Тест получения видов работ из базы данных для некорректных настроек страницы.
    /// </summary>
    [Fact]
    public async Task GetAllAsyncForIncorrectPageOptionsTest()
    {     
       var errors = new List<Exception>();

       foreach (var pageOptions in PaginationTestData.GetIncorrectPageOptions())
       {
            try
            {
                var action = async () => await _repository.GetAllAsync(pageOptions);
                await action.Invoke();         
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }
       }

       errors.Should().HaveCount(4);
    }

    #endregion
}
