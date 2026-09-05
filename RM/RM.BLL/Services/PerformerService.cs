using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Disposable;
using RM.BLL.Abstractions.Models;
using RM.BLL.Abstractions.Services;
using RM.BLL.Abstractions.Validators;
using RM.BLL.Exceptions;
using RM.BLL.Mapping.MapperSets;
using RM.DAL.Abstractions.Repositories;

namespace RM.BLL.Services
{
    /// <summary>
    /// Реализация сервиса исполнителей договоров.
    /// </summary>
    public class PerformerService : DisposableBase, IPerformerService
    {
        private readonly IPerformerRepository _performerRepository;
        private readonly IPageOptionsValidator _pageOptionsValidator;
        private readonly IPageOptionsBllMappers _pageOptionsBllMappers;
        private readonly IPerformerBllMappers _performerBllMappers;

        /// <summary>
        /// Инициализирует экземпляр <see cref="PerformerService"/>.
        /// </summary>
        /// <param name="performerRepository">Репозиторий исполнителей договоров.</param>
        /// <param name="pageOptionsValidator">Валидатор настроек страницы.</param>
        /// <param name="pageOptionsBllMappers">Контейнер мапперов для работы с настройками страницы.</param>
        /// <param name="performerBllMappers">Контейнер мапперов для работы с исполнителями договоров.</param>
        public PerformerService(IPerformerRepository performerRepository, 
            IPageOptionsValidator pageOptionsValidator,
            IPageOptionsBllMappers pageOptionsBllMappers,
            IPerformerBllMappers performerBllMappers)
        {
            ArgumentNullException.ThrowIfNull(performerRepository, nameof(performerRepository));
            ArgumentNullException.ThrowIfNull(pageOptionsValidator, nameof(pageOptionsValidator));
            ArgumentNullException.ThrowIfNull(pageOptionsBllMappers, nameof(pageOptionsBllMappers));
            ArgumentNullException.ThrowIfNull(performerBllMappers, nameof(performerBllMappers));

            _performerRepository = performerRepository;
            _pageOptionsValidator = pageOptionsValidator;
            _pageOptionsBllMappers = pageOptionsBllMappers;
            _performerBllMappers = performerBllMappers;
        }


        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<PerformerModel>> GetAllAsync(
            PageOptionsModel? pageOptions = null)
        {
            Infrastructure.Shared.Models.PageOptionsModel? pageOptionsLocal = null;

            if (pageOptions != null)
            {
                await _pageOptionsValidator.ValidateAndThrowAsync(pageOptions);

                pageOptionsLocal = _pageOptionsBllMappers.ToPageOptionsModel.Map(pageOptions);
            }
            
            var performers = await _performerRepository.GetAllAsync(pageOptionsLocal);

            var results = performers.Select(_performerBllMappers.ToPerformerModel.Map)
                                    .ToList();

            return results;
        }

        /// <inheritdoc/>
        public async Task<PerformerModel?> GetByIdAsync(Guid performerId)
        {
            var performerEntity = await _performerRepository.GetByIdAsync(performerId);

            var result = performerEntity is not null? 
                _performerBllMappers.ToPerformerModel.Map(performerEntity) : null;

            return result;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Guid performerId)
        {
            var deletedCount = await _performerRepository.DeleteAsync(performerId);

            if (deletedCount == 0)
            {
                throw new DataNotFoundException($"Исполнитель договоров по ИД '{performerId}' не существует.");
            }
        }

        /// <inheritdoc/>
        protected override void DisposeManagedResources()
        {
            _performerRepository?.Dispose();
        }

        /// <inheritdoc/>
        protected override async ValueTask DisposeManagedResourcesAsync()
        {
            if(_performerRepository != null)
            {
                await _performerRepository.DisposeAsync().ConfigureAwait(false);
            }
        }
    }
}