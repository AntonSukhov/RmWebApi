using System;
using RM.Api.DTOs.Responses;
using RM.Api.Enums;

namespace RM.Api.Mapping.Extensions
{
    internal static class PerformerMapper
    {
        /// <summary>
        /// Преобразует <see cref="GeneratedApiClients.PerformerResponse"/> в <see cref="PerformerResponse"/>.
        /// </summary>
        /// <param name="performerResponse">Ответ исполнителя договоров из внешнего API.</param>
        /// <returns>Преобразованный объект <see cref="PerformerResponse"/>.</returns>
        public static PerformerResponse ToPerformerResponse (
            this GeneratedApiClients.PerformerResponse performerResponse)
        {
            ArgumentNullException.ThrowIfNull(performerResponse, nameof(performerResponse));

            return new PerformerResponse
            {
                Id = performerResponse.Id,
                EntityId = performerResponse.EntityId,
                CreateDate = performerResponse.CreateDate.UtcDateTime,
                EditDate = performerResponse.EditDate?.UtcDateTime,
                Creator = performerResponse.Creator ?? string.Empty,
                Editor = performerResponse.Editor,
                Snils = performerResponse.Snils ?? string.Empty,
                Inn = performerResponse.Inn,
                Surname = performerResponse.Surname ?? string.Empty,
                Name = performerResponse.Name ?? string.Empty,
                Patronymic = performerResponse.Patronymic ?? string.Empty,
                Gender = (GenderEnum)performerResponse.Gender,
                PassportNumber = performerResponse.PassportNumber ?? string.Empty,
                PassportSeries = performerResponse.PassportSeries ?? string.Empty,
                PassportIssuePlace = performerResponse.PassportIssuePlace ?? string.Empty,
                PassportIssueDate = performerResponse.PassportIssueDate.UtcDateTime,
                PassportDepartmentCode = performerResponse.PassportDepartmentCode ?? string.Empty,
                PassportBirthPlace = performerResponse.PassportBirthPlace ?? string.Empty,
                PassportBirthDate = performerResponse.PassportBirthDate.UtcDateTime,
                PassportRegistrationPlace = performerResponse.PassportRegistrationPlace ?? string.Empty,
            };
        }
    }
}