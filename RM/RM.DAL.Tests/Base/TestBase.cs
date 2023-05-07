using Xunit.Abstractions;

namespace RM.DAL.Tests.Base
{
    /// <summary>
    /// Базовый класс для тестирования
    /// </summary>
    public class TestBase
    {
        #region Поля

        /// <summary>
        /// Инструмент вывода данных в консоль
        /// </summary>
        protected readonly ITestOutputHelper _output;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="output">Инструмент вывода данных в консоль</param>
        public TestBase(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        #endregion
    }
}
