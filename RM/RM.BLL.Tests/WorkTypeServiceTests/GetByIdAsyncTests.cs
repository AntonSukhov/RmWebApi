// using FluentAssertions;
// using Moq;
// using RM.BLL.Extensions;
// using RM.BLL.Tests.TestData;
// using RM.BLL.Abstractions.Services;
// using RM.Tests.Common.TestData;
// using RM.BLL.Abstractions.Models;
// using System.Diagnostics.CodeAnalysis;

// namespace RM.BLL.Tests.WorkTypeServiceTests;

// /// <summary>
// /// Тесты для метода <see cref="IWorkTypeService.GetByIdAsync"/>
// /// </summary>
// /// <param name="fixture">Настройка контекста для тестирования сервиса видов работ.</param>
// public class GetByIdAsyncTests(WorkTypeServiceFixture fixture) : IClassFixture<WorkTypeServiceFixture>
// {
//     #region Поля

//     private readonly WorkTypeServiceFixture _fixture = fixture;

//     #endregion

//     #region Методы

//     /// <summary>
//     /// Тест получения вида работ по его идентификатору для корректных данных.
//     /// </summary>
//     [Theory]
//     [MemberData(nameof(WorkTypeServiceTestData.GetByIdAsyncForCorrectDataTestData), 
//                 MemberType = typeof(WorkTypeServiceTestData))]
//     public async Task ForCorrectDataTest(WorkTypeGettingByIdModel workTypeGettingByIdModel)
//     {       
//         var actual = new DAL.Abstractions.Models.WorkTypeModel
//                     {
//                         Id = workTypeGettingByIdModel.Id,
//                         Name = "Вид работ"
//                     };
//         _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
//                                        .ReturnsAsync(actual);

//         var workTypeService = new WorkTypeService(_fixture.WorkTypeRepositoryMock.Object, 
//                                                   _fixture.WorkUnitRepositoryMock.Object,
//                                                   _fixture.WorkTypeNameValidator,
//                                                   _fixture.WorkTypeUpdationModelValidator, 
//                                                   _fixture.PageOptionsValidator);

//         var expected = await workTypeService.GetByIdAsync(workTypeGettingByIdModel);

//         expected.Should().BeEquivalentTo(actual.ToBll());
//     }

//      /// <summary>
//     /// Тест получения вида работ по его идентификатору для корректных данных.
//     /// </summary>
//     [Theory]
//     [MemberData(nameof(WorkTypeServiceTestData.GetByIdAsyncForIncorrectDataTestData),
//                 MemberType = typeof(WorkTypeServiceTestData))]
//     public async Task ForIncorrectDataTest(WorkTypeGettingByIdModel? workTypeGettingByIdModel)
//     {
//         _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
//                                        .Throws(() => new Exception("Repository Exception"));

//         var workTypeService = new WorkTypeService(_fixture.WorkTypeRepositoryMock.Object, 
//                                                   _fixture.WorkUnitRepositoryMock.Object,
//                                                   _fixture.WorkTypeNameValidator,
//                                                   _fixture.WorkTypeUpdationModelValidator, 
//                                                   _fixture.PageOptionsValidator);

//         var expected = async () => await workTypeService.GetByIdAsync(workTypeGettingByIdModel);

//         await expected.Should().ThrowAsync<Exception>();
//     }


//     #endregion
// }
