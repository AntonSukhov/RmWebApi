using Infrastructure.Testing.TestCases;
using Infrastructure.Testing.XUnit;
using Infrastructure.Testing.XUnit.Helpers;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.TestSupport.Constants;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Tests.Services.WorkUnitService.GetAllAsync;

/// <summary>
/// Тесты для метода <see cref="IWorkUnitService.GetAllAsync"/>.
/// </summary>
/// <param name="fixture">Настройка контекста для тестирования сервиса единиц работ.</param>
public class GetAllAsyncTests : BaseTest<WorkUnitServiceFixture>
{
    public GetAllAsyncTests(WorkUnitServiceFixture fixture): base(fixture){ }

    /// <summary>
    /// Проверяет, что метод <see cref="IWorkUnitService.GetAllAsync"/> возвращает список единиц работ,
    /// когда репозиторий вернёт правильные данные.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetAllAsyncTestCases.SuccessTestCases), 
                MemberType = typeof(GetAllAsyncTestCases))]
    public async Task ReturnsDataWhenRepositoryHasEntries(
        TestCaseResultWithStubs<IEnumerable<WorkUnitModel>> testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[(RepositoryMethodNames.WorkUnitRepository.GetAllAsync, 
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<List<DAL.Abstractions.Models.WorkUnitModel>>();

        _fixture.WorkUnitRepositoryMock.Setup(p => p.GetAllAsync())
                                       .Returns(Task.FromResult<
                                            IReadOnlyCollection<DAL.Abstractions.Models.WorkUnitModel>?>(stubOutputData));

        // Act:
        var result = await _fixture.WorkUnitService.GetAllAsync();

        // Assert: 
        AssertHelper.CollectionEqual(result, testCase.OutputData, _fixture.WorkUnitModelEqualityComparer);
    }

    /// <summary>
    /// Проверяет, что метод <see cref="IWorkUnitService.GetAllAsync"/> корректно пробрасывает исключение,
    /// выброшенное репозиторием при вызове <see cref="IWorkUnitRepository.GetAllAsync"/>.
    /// </summary>
    /// <param name="expectedException">
    /// Исключение, которое ожидается получить от репозитория и пробросить наверх без изменений.
    /// Предоставляется через тестовые данные (<see cref="GetAllAsyncTestCases.ErrorTestCases"/>).
    /// </param>
    [Theory]
    [MemberData(nameof(GetAllAsyncTestCases.ErrorTestCases), 
                MemberType = typeof(GetAllAsyncTestCases))]
    public async Task RethrowsRepositoryException(
        Exception expectedException 
    )
    {
        // Arrange:
        _fixture.WorkUnitRepositoryMock.Setup(p => p.GetAllAsync())
                                        .Throws(expectedException);
        
        // Act & Assert
        var thrownException = await Assert.ThrowsAnyAsync<Exception>(
            _fixture.WorkUnitService.GetAllAsync);

        // Проверяем, что проброшено именно исходное исключение
        Assert.Same(expectedException, thrownException);
    
        // Проверяем, сообщения исключений
        Assert.Equal(expectedException.Message, thrownException.Message);
    }
}


