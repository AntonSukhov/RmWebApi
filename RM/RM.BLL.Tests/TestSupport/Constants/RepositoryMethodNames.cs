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
        public const string GetAllAsync = $"{nameof(IWorkUnitRepository)}.{nameof(IWorkUnitRepository.GetAllAsync)}";

        /// <summary>
        /// Имя метода <see cref="IWorkUnitRepository.GetByIdAsync"/>.
        /// </summary>
        public const string GetByIdAsync = $"{nameof(IWorkUnitRepository)}.{nameof(IWorkUnitRepository.GetByIdAsync)}";
    }

    /// <summary>
    /// Константы для методов репозитория <see cref="IWorkTypeRepository"/>.
    /// </summary>
    public static class WorkTypeRepository
    {
        /// <summary>
        /// Имя метода <see cref="IWorkTypeRepository.GetByNameAsync"/>.
        /// </summary>
        public const string GetByNameAsync = $"{nameof(IWorkTypeRepository)}.{nameof(IWorkTypeRepository.GetByNameAsync)}";

        /// <summary>
        /// Имя метода <see cref="IWorkTypeRepository.GetByIdAsync"/>.
        /// </summary>
        public const string GetByIdAsync = $"{nameof(IWorkTypeRepository)}.{nameof(IWorkTypeRepository.GetByIdAsync)}";

        /// <summary>
        /// Имя метода <see cref="IWorkTypeRepository.GetAllAsync"/>.
        /// </summary>
        public const string GetAllAsync = $"{nameof(IWorkTypeRepository)}.{nameof(IWorkTypeRepository.GetAllAsync)}";

    }

    /// <summary>
    /// Константы для методов репозитория <see cref="IPerformerRepository"/>.
    /// </summary>
    public static class PerformerRepository
    {
        /// <summary>
        /// Имя метода <see cref="IPerformerRepository.GetAllAsync"/>.
        /// </summary>
        public const string GetAllAsync = $"{nameof(IPerformerRepository)}.{nameof(IPerformerRepository.GetAllAsync)}";

        /// <summary>
        /// Имя метода <see cref="IPerformerRepository.GetByIdAsync"/>.
        /// </summary>
        public const string GetByIdAsync = $"{nameof(IPerformerRepository)}.{nameof(IPerformerRepository.GetByIdAsync)}";
    }
}
