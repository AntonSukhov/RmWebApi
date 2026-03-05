using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using Infrastructure.Testing.XUnit;
using Moq;
using RM.BLL.Abstractions.Services;
using RM.BLL.Exceptions;
using RM.BLL.Tests.Services.AuthenticationService.GetTokenAsync.Models;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.AuthenticationService.GetTokenAsync;

/// <summary>
/// Тесты для метода <see cref="IAuthenticationService.GetTokenAsync"/>.
/// </summary>
public class GetTokenAsyncTests : BaseTest<AuthenticationServiceFixture>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="GetTokenAsyncTests"/>.
    /// </summary>
    /// <param name="fixture">
    /// Настройка контекста для тестирования сервиса аутентификации пользователей.
    /// </param>
    public GetTokenAsyncTests(AuthenticationServiceFixture fixture) : base(fixture) { }

    /// <summary>
    /// Проверяет, что метод <see cref="IAuthenticationService.GetTokenAsync""/> 
    /// успешно выполняет аутентификацию пользователя и возвращает токен.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetTokenAsyncTestCases.SuccessTestCases),
                MemberType = typeof(GetTokenAsyncTestCases))]
    public async Task SucceedsForValidInput(TestCaseWithStubs<UserCredentials, string> testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[new StubOutputKey(
            ServiceMethodNames.AuthenticationService.GetTokenAsync,
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<IdentityWebApp.Api.Models.TokenModel>()
            ?? new IdentityWebApp.Api.Models.TokenModel { Value = string.Empty, Expires = default};

        _fixture.AuthenticationServiceMock.Setup(p => p.LoginAsync(
            It.IsAny<string>(),
            It.IsAny<int?>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<bool>(),
            default
        )).ReturnsAsync(stubOutputData);

        // Act:         
        var result = await _fixture.AuthenticationService.GetTokenAsync(
            testCase.InputData.Login, testCase.InputData.Password);

        // Assert:
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(result, stubOutputData.Value);       
    }

    /// <summary>
    /// Проверяет, что метод <see cref="IAuthenticationService.GetTokenAsync""/> 
    /// не выполняет аутентификацию, а возвращает ошибку валидации.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetTokenAsyncTestCases.UnSuccessTestCases),
                MemberType = typeof(GetTokenAsyncTestCases))]
    public async Task FailsForInvalidInput(TestCaseInput<UserCredentials> testCase)
    {
        // Arrange & Act & Assert:
        var exception = await Assert.ThrowsAnyAsync<Exception>(
            async () => await _fixture.AuthenticationService.GetTokenAsync(
                testCase.InputData.Login, 
                testCase.InputData.Password)
        );

        // Проверяем, что исключение относится к разрешённым типам
        Assert.True(
            exception is ValidationException || 
            exception is ValidationAggregationException
        );
    }
}
