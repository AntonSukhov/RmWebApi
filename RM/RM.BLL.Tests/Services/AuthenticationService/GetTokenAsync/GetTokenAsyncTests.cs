using IdentityWebApp.Api.Models;
using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using Infrastructure.Testing.XUnit;
using Moq;
using RM.BLL.Abstractions.Services;
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
    [MemberData(nameof(GetTokenAsyncTestCases.Success),
                MemberType = typeof(GetTokenAsyncTestCases))]
    public async Task SucceedsForValidInput(TestCaseWithStubs<UserCredentials, string> testCase)
    {
        // Arrange:
        var stubOutput = testCase.StubOutputs[new StubOutputKey(
            ServiceMethodNames.AuthenticationService.GetTokenAsync,
            StubSequenceConstants.First)];
        var stubOutputData = stubOutput.GetOutputData<TokenModel>()
            ?? new TokenModel { Value = string.Empty, Expires = default};

        _fixture.AuthenticationServiceMock.Setup(p => p.LoginAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
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
    /// Проверяет, что метод <see cref="IAuthenticationService.GetTokenAsync"/> 
    /// не выполняет аутентификацию, а возвращает ошибку <see cref="HttpRequestException"/>
    /// для некорректных входных данных.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetTokenAsyncTestCases.InvalidInputData),
                MemberType = typeof(GetTokenAsyncTestCases))]
    public async Task FailsForInvalidInput(TestCaseInput<UserCredentials> testCase)
    {
        // Arrange:
        _fixture.AuthenticationServiceMock.Setup(p => p.LoginAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            default
        )).Throws<HttpRequestException>();

        // Act & Assert:
        await Assert.ThrowsAnyAsync<HttpRequestException>(
            async () => await _fixture.AuthenticationService.GetTokenAsync(
                testCase.InputData.Login, 
                testCase.InputData.Password)
        );
    }

     /// <summary>
    /// Проверяет, что метод <see cref="IAuthenticationService.GetTokenAsync"/> 
    /// не выполняет аутентификацию, а возвращает ошибку <see cref="InvalidOperationException"/>
    /// если пользователь не существует или для существующего пользователя указан неверный пароль.
    /// </summary>
    [Theory]
    [MemberData(nameof(GetTokenAsyncTestCases.InvalidCredentials),
                MemberType = typeof(GetTokenAsyncTestCases))]
    public async Task FailsForInvalidCredentials(TestCaseInput<UserCredentials> testCase)
    {
        // Arrange:
        _fixture.AuthenticationServiceMock.Setup(p => p.LoginAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            default
        )).Throws<InvalidOperationException>();

        // Act & Assert:
        await Assert.ThrowsAnyAsync<InvalidOperationException>(
            async () => await _fixture.AuthenticationService.GetTokenAsync(
                testCase.InputData.Login, 
                testCase.InputData.Password)
        );
    }
}
