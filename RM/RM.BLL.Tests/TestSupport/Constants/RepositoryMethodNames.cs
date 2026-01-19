using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Tests.TestSupport.Constants;

/// <summary>
/// Набор константных строк с именами методов репозиториев для использования в тестах.
/// </summary>
public static class RepositoryMethodNames
{
    /// <summary>
    /// Константы для методов репозитория <see cref="IWorkUnitRepository"/>.
    /// </summary>
    public static class WorkUnitRepository
    {
        /// <summary>
        /// Имя метода <see cref="IWorkUnitRepository.GetAllAsync"/>.
        /// </summary>
        public const string GetAllAsync = nameof(IWorkUnitRepository.GetAllAsync);

        /// <summary>
        /// Имя метода <see cref="IWorkUnitRepository.GetByIdAsync"/>.
        /// </summary>
        public const string GetByIdAsync = nameof(IWorkUnitRepository.GetByIdAsync);
    }

    /// <summary>
    /// Константы для методов репозитория <see cref="IWorkTypeRepository"/>.
    /// </summary>
    public static class WorkTypeRepository
    {
        /// <summary>
        /// Имя метода <see cref="IWorkTypeRepository.GetByNameAsync"/>.
        /// </summary>
        public const string GetByNameAsync = nameof(IWorkTypeRepository.GetByNameAsync);

        /// <summary>
        /// Имя метода <see cref="IWorkTypeRepository.GetAllAsync"/>.
        /// </summary>
        public const string GetAllAsync = nameof(IWorkTypeRepository.GetAllAsync);

    }
}
