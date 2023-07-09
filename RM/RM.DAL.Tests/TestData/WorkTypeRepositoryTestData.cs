using RM.DAL.Tests.WorkTypeRepositoryTests;

namespace RM.DAL.Tests.TestData
{
    /// <summary>
    /// Тестовые данные для репозитория работы с видами работ
    /// </summary>
    public class WorkTypeRepositoryTestData : BaseTestData
    {
        #region Свойства

        /// <summary>
        /// Корректные данные для теста <see cref="CreateTests.CreateWithCorrectDataTest"/>
        /// </summary>
        public static IEnumerable<object?[]> CreateWithCorrectDataTestData
        {
            get
            {
                return new List<object?[]>
                {
                   new object?[]
                   {
                       $"Вид работ {DateTime.Now:yyyy.MM.dd HH:mm:ss ff}",
                       null

                   },
                   new object?[]
                   {
                       $"Вид работ {DateTime.Now:yyyy.MM.dd HH:mm:ss ff}",
                       (byte)1
                   },
                   new object?[]
                   {
                       $"Вид работ {DateTime.Now:yyyy.MM.dd HH:mm:ss ff}",
                       (byte)2
                   }
                };
            }
        }

        #endregion
    }
}
