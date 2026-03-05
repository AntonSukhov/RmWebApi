using Infrastructure.Testing.Common;
using Infrastructure.Testing.TestCases;
using RM.BLL.Abstractions.Services;
using RM.BLL.Tests.Services.AuthenticationService.GetTokenAsync.Models;
using RM.BLL.Tests.TestSupport.Constants;

namespace RM.BLL.Tests.Services.AuthenticationService.GetTokenAsync;

/// <summary>
/// Набор тестовых сценариев для проверки метода <see cref="IAuthenticationService.GetTokenAsync"/>.
/// </summary>
public static class GetTokenAsyncTestCases
{
    private static readonly string _login = "User189";
    private static readonly string _password = "Qwerty";
    private static readonly string _token = "eyJhbGciOiJIUzUxMiIsIn";

    /// <summary>
    /// Получает сценарии успешного выполнения метода <see cref="IAuthenticationService.GetTokenAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseWithStubs<UserCredentials, string>> SuccessTestCases
    {
        get
        {
            var theoryData = new TheoryData<TestCaseWithStubs<UserCredentials, string>>
            {
                new() {
                    ScenarioNumber = 1,
                    Description = "Проверка успешной аутентификации пользователя и получения токена.",
                    InputData = new UserCredentials(_login, _password),
                    OutputData = _token,
                    StubOutputs = new Dictionary<StubOutputKey, StubOutput>
                    {
                        [new StubOutputKey(ServiceMethodNames.AuthenticationService.GetTokenAsync,
                        StubSequenceConstants.First)] = new StubOutput
                        {
                            OutputData = new IdentityWebApp.Api.Models.TokenModel
                            {
                                Value = _token,
                                Expires = DateTimeOffset.Now.AddMinutes(5)
                            },
                            ExpectedType = typeof(IdentityWebApp.Api.Models.TokenModel)
                        }
                    }
                }
            };

            return  theoryData;
        }
    }

    /// <summary>
    /// Получает сценарии неуспешного выполнения метода <see cref="IAuthenticationService.GetTokenAsync"/>.
    /// </summary>
    public static TheoryData<TestCaseInput<UserCredentials>> UnSuccessTestCases
    {
        get
        {
            var spaceChar = " ";

            var theoryData = new TheoryData<TestCaseInput<UserCredentials>>
            {
                new() {
                    ScenarioNumber = 1,
                    Description = "Проверка неуспешной аутентификации пользователя и получения токена. Логин пустой.",
                    InputData = new UserCredentials(string.Empty, _password)
                },
                new() {
                    ScenarioNumber = 2,
                    Description = "Проверка неуспешной аутентификации пользователя и получения токена. Логин равен пробелу.",
                    InputData = new UserCredentials(spaceChar, _password)
                },
                new() {
                    ScenarioNumber = 3,
                    Description = "Проверка неуспешной аутентификации пользователя и получения токена. Длина логина равна 257.",
                    InputData = new UserCredentials(new string('u', 257), _password)
                },
                new() {
                    ScenarioNumber = 4,
                    Description = "Проверка неуспешной аутентификации пользователя и получения токена. Пароль пустой.",
                    InputData = new UserCredentials(_login, string.Empty)
                },
                new() {
                    ScenarioNumber = 5,
                    Description = "Проверка неуспешной аутентификации пользователя и получения токена. Пароль равен пробелу.",
                    InputData = new UserCredentials(_login, spaceChar)
                }
            };

            return  theoryData;
        }
    }
    
}
